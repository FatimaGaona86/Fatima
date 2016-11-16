using System;
using System.Windows.Forms;
using BLL.Enums;
using DAL.Servicio;
using BLL.Dominio;
using System.Net.Mail;
using System.Collections.Generic;

namespace PL
{
    public partial class frmAbmPaciente : Form
    {
        eAccion accion;
        private Paciente paciente;
 
        LocalidadService localidadService = new LocalidadService();
        PacienteService pacienteService = new PacienteService();
         
        public frmAbmPaciente(eAccion accion) // Constructor para dar de alta 
        {
            InitializeComponent();
            this.accion = accion;
        }

        public frmAbmPaciente(eAccion accion, Paciente paciente) // Constructor para modificar y eliminiar
        {
            InitializeComponent();
            this.accion = accion;
            this.paciente = paciente;
        }

        private void frmAbmPaciente_Load(object sender, EventArgs e)
        {
            cboLocalidades.DataSource = localidadService.traerLocalidades();
            cboLocalidades.ValueMember = "IdLocalidad";
            cboLocalidades.DisplayMember = "_Localidad";
            cboLocalidades.SelectedValue = -1 ;

            switch (accion)
            {
                case eAccion.Alta:
                    lblTitulo.Text = "Alta de paciente";
                    paciente = new Paciente();
                    break;

                case eAccion.Modificacion:
                    lblTitulo.Text = "Modificación";
                    txtNombre.Text = paciente.Nombre.ToString();
                    txtApellido.Text = paciente.Apellido.ToString();
                    txtDni.Text = paciente.Dni.ToString();
                    txtEmail.Text = paciente.Email.ToString();

                    IList<int> telefonos = new List<int>();
                    int i = paciente.IdPaciente;
                    telefonos = pacienteService.traerTelefonosPaciente(i);
                    txtTelefono1.Text = telefonos[0].ToString();
                    txtTelefono1.Text = telefonos[1].ToString();
                    txtDireccion.Text = paciente.Direccion.ToString();
                    cboLocalidades.SelectedValue = paciente._Localidad.IdLocalidad;
                    break;

                case eAccion.Baja:
                    lblTitulo.Text = "Eliminación";
                    txtNombre.Text = paciente.Nombre.ToString();
                    txtApellido.Text = paciente.Apellido.ToString();
                    txtDni.Text = paciente.Dni.ToString();
                    txtDireccion.Text = paciente.Direccion.ToString();
                    cboLocalidades.SelectedValue = paciente._Localidad.IdLocalidad;

                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtDni.Enabled = false;
                    txtDireccion.Enabled = false;
                    cboLocalidades.Enabled = false;
                    break;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case eAccion.Alta:
                    if (validacionGral())
                    {
                        paciente.Nombre = txtNombre.Text.Trim();
                        paciente.Apellido = txtApellido.Text.Trim();
                        paciente.Dni = int.Parse(txtDni.Text);
                        paciente.Direccion = txtDireccion.Text.Trim();
                        paciente._Localidad = (Localidad)cboLocalidades.SelectedItem;

                        pacienteService.agregarPaciente(paciente);

                        paciente.ListaDeTelefonos = new List<int>();

                        if (!string.IsNullOrWhiteSpace(txtTelefono1.Text))
                        {
                            paciente.ListaDeTelefonos.Add(Convert.ToInt32(txtTelefono1.Text));
                            pacienteService.agregarTelefonoPaciente(pacienteService.traerIdPaciente(), Convert.ToInt32(txtTelefono1.Text));
                        }

                        if (!string.IsNullOrWhiteSpace(txtTelefono2.Text))
                        {
                            paciente.ListaDeTelefonos.Add(Convert.ToInt32(txtTelefono2.Text));
                            pacienteService.agregarTelefonoPaciente(pacienteService.traerIdPaciente(), Convert.ToInt32(txtTelefono2.Text));
                        }

                        MessageBox.Show("Paciente agregado correctamente!!...");
                        this.Dispose();
                    }
                    break;

                case eAccion.Modificacion:
                    paciente.Nombre = txtNombre.Text.Trim();
                    paciente.Apellido = txtApellido.Text.Trim();
                    paciente.Dni = int.Parse(txtDni.Text);
                    paciente.Direccion = txtDireccion.Text.Trim();
                    paciente._Localidad = (Localidad)cboLocalidades.SelectedItem;

                    pacienteService.modificarPaciente(paciente);
                    MessageBox.Show("Paciente modificado correctamente!!...");
                    this.Dispose();
                    break;
                case eAccion.Baja:
                    if (MessageBox.Show("Esta seguro que desea eliminar este paciente", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        pacienteService.eliminarPaciente(paciente);
                        this.Dispose();
                    }
                    else
                    {
                        this.Dispose();
                    }
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool esEmailValido(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void txtNombre_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < txtNombre.Text.Length; i++)
            {
                if (!char.IsLetter(txtNombre.Text[i]))
                {
                    if (txtNombre.Text[i] != 32) // que acepte espacio (para los casos que tenga dos nombres Ej: "Juan Pablo")
                    {
                        errorNombre.SetError(this.txtNombre, "El nombre no puede contener números ni caracteres especiales");
                        e.Cancel = true;
                        i = txtNombre.Text.Length;
                    }
                }
                else
                    errorNombre.SetError(this.txtNombre, "");
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                errorNombre.SetError(this.txtNombre, "");
        }

        private void txtApellido_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < txtApellido.Text.Length; i++)
            {
                if ((!char.IsLetter(txtApellido.Text[i])) && (txtApellido.Text[i] != 32)) // Que acepte espacio (para los casos de dos apellidos)
                {
                    errorApellido.SetError(this.txtApellido, "El apellido no puede contener números");
                    e.Cancel = true;
                    i = txtApellido.Text.Length;
                }
                else
                    errorApellido.SetError(this.txtApellido, "");
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
                errorApellido.SetError(this.txtApellido, "");
        }

        private void txtDni_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < txtDni.Text.Length; i++)
            {
                if (!char.IsNumber(txtDni.Text[i]))
                {
                    errorDni.SetError(this.txtDni, "Sólo números!");
                    e.Cancel = true;
                    i = txtDni.Text.Length;
                }
                else
                    errorDni.SetError(this.txtDni, "");
            }

            if (string.IsNullOrWhiteSpace(txtDni.Text))
                errorDni.SetError(this.txtDni, "");
        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (esEmailValido(txtEmail.ToString()))
                errorEmail.SetError(this.txtEmail, "");
            else
                errorEmail.SetError(this.txtEmail, "Mail inválido");

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                errorEmail.SetError(this.txtEmail, "");
        }

        private void txtTelefono1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < txtTelefono1.Text.Length; i++)
            {
                if (!char.IsNumber(txtTelefono1.Text[i]))
                {
                    errorTelefono1.SetError(this.txtTelefono1, "Sólo números!");
                    e.Cancel = true;
                    i = txtTelefono1.Text.Length;
                }
                else
                    errorTelefono1.SetError(this.txtTelefono1, "");

            }

            if (string.IsNullOrWhiteSpace(txtTelefono1.Text))
                errorTelefono1.SetError(this.txtTelefono1, "");
        }

        private void txtTelefono2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < txtTelefono2.Text.Length; i++)
            {
                if (!char.IsNumber(txtTelefono2.Text[i]))
                {
                    errorTelefono2.SetError(this.txtTelefono2, "Sólo números!");
                    e.Cancel = true;
                    i = txtTelefono2.Text.Length;
                }
                else
                    errorTelefono2.SetError(this.txtTelefono2, "");
            }

            if (string.IsNullOrWhiteSpace(txtTelefono2.Text))
                errorTelefono2.SetError(this.txtTelefono2, "");
        }

        private bool validacionGral()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    (string.IsNullOrWhiteSpace(txtApellido.Text) ||
                        (string.IsNullOrWhiteSpace(txtDni.Text) ||
                            (string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                                (string.IsNullOrWhiteSpace(cboLocalidades.Text))))))
            {
                MessageBox.Show("Todos los campos son requeridos!!");
                return false;
            }

            return true;
        }
    }
}
