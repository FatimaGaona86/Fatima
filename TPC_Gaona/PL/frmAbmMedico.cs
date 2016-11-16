using BLL.Dominio;
using BLL.Enums;
using DAL.Servicio;
using System;
using System.Windows.Forms;

namespace PL
{
    public partial class frmAbmMedico : Form
    {
        eAccion accion;
        Medico medico;
        Especialidad especialidad;

        LocalidadService localidadService = new LocalidadService();
        EspecialidadService especialidadService = new EspecialidadService();
        MedicoService medicoService = new MedicoService();

        public frmAbmMedico()
        {
            InitializeComponent();
        }

        public frmAbmMedico(eAccion accion)
        {
            InitializeComponent();
            this.accion = accion;
        }

        public frmAbmMedico(eAccion accion, Medico medico) // Constructor para modificar y eliminiar
        {
            InitializeComponent();
            this.accion = accion;
            this.medico = medico;
        }

        private void frmAbmMedico_Load(object sender, EventArgs e)
        {
            cboLocalidades.DataSource = localidadService.traerLocalidades();
            cboLocalidades.ValueMember = "IdLocalidad";
            cboLocalidades.DisplayMember = "_Localidad";
            cboLocalidades.SelectedValue = -1;

            cboEspecialidades.DataSource = especialidadService.traerEspecialidades();
            cboEspecialidades.ValueMember = "IdEspecialidad";
            cboEspecialidades.DisplayMember = "_Especialidad";
            cboEspecialidades.SelectedValue = -1;

            switch (accion)
            {
                case eAccion.Alta:
                    lblTitulo.Text = "Alta de Medico: ";
                    medico = new Medico();
                    lblEspecialidad.Visible = true;
                    cboEspecialidades.Visible = true;
                    break;

                case eAccion.Modificacion:
                    lblTitulo.Text = "Modificación";
                    txtNombre.Text = medico.Nombre.ToString();
                    txtApellido.Text = medico.Apellido.ToString();
                    txtDni.Text = medico.Dni.ToString();
                    txtDireccion.Text = medico.Direccion.ToString();
                    txtMatricula.Text = medico.Matricula.ToString();
                    cboLocalidades.SelectedValue = medico._Localidad.IdLocalidad;
                    lblEspecialidad.Visible = false;
                    cboEspecialidades.Visible = false;
                    break;

                case eAccion.Baja:
                    lblTitulo.Text = "Eliminación";
                    txtNombre.Text = medico.Nombre.ToString();
                    txtApellido.Text = medico.Apellido.ToString();
                    txtDni.Text = medico.Dni.ToString();
                    txtDireccion.Text = medico.Direccion.ToString();
                    txtMatricula.Text = medico.Matricula.ToString();
                    cboLocalidades.SelectedValue = medico._Localidad.IdLocalidad;

                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtDni.Enabled = false;
                    txtDireccion.Enabled = false;
                    txtMatricula.Enabled = false;
                    cboLocalidades.Enabled = false;
                    lblEspecialidad.Visible = false;
                    cboEspecialidades.Visible = false;

                    break;     
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case eAccion.Alta:
                    medico.Nombre = txtNombre.Text.Trim();
                    medico.Apellido = txtApellido.Text.Trim();
                    medico.Dni = int.Parse(txtDni.Text);
                    medico.Direccion = txtDireccion.Text.Trim();
                    medico.Matricula = int.Parse(txtMatricula.Text);
                    medico._Localidad = (Localidad)cboLocalidades.SelectedItem;

                    medicoService.agregarMedico(medico); // Agrega medico

                    //especialidad = (Especialidad)cboEspecialidades.SelectedItem; // IdEspecialidad 
                    //medico.IdMedico = medicoService.traerIdMedico();
                    //medicoService.agregarEspecialidadMedico(medico.IdMedico, especialidad.IdEspecialidad); //Asocia una localidad al médico

                    MessageBox.Show("Medico agregado correctamente!!...");
                    this.Dispose();
                    break;

                case eAccion.Modificacion:
                    medico.Nombre = txtNombre.Text.Trim();
                    medico.Apellido = txtApellido.Text.Trim();
                    medico.Dni = int.Parse(txtDni.Text);
                    medico.Direccion = txtDireccion.Text.Trim();
                    medico._Localidad = (Localidad)cboLocalidades.SelectedItem;

                    medicoService.modificarMedico(medico);

                    MessageBox.Show("Medico modificado correctamente!!...");
                    this.Dispose();
                    break;

                case eAccion.Baja:
                    if (MessageBox.Show("Esta seguro que desea eliminar este médico", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        medicoService.eliminarMedico(medico);
                        this.Dispose();
                    }
                    else
                    {
                        this.Dispose();
                    }
                    break;
            }
        }

        private void linkAgregarNuevaLocalidad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmListadoGral frmListadoGral = new frmListadoGral(ePantalla.Localidad);
            frmListadoGral.ShowDialog();

            cboLocalidades.DataSource = localidadService.traerLocalidades();
            cboLocalidades.ValueMember = "IdLocalidad";
            cboLocalidades.DisplayMember = "_Localidad";
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
                {
                    errorNombre.SetError(this.txtNombre, "");
                }
            }
        }

        private void txtApellido_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < txtApellido.Text.Length; i++)
            {
                if (!char.IsLetter(txtApellido.Text[i]))
                {
                    if (txtApellido.Text[i] != 32) // Que acepte espacio (para los casos de dos apellidos)
                    {
                        errorApellido.SetError(this.txtApellido, "El apellido no puede contener números ni caracteres especiales");
                        e.Cancel = true;
                        i = txtApellido.Text.Length;
                    }
                }
                else
                {
                    errorApellido.SetError(this.txtApellido, "");
                }
            }
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
                {
                    errorDni.SetError(this.txtDni, "");
                }
            }
        }

        private bool validacionGral()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    (string.IsNullOrWhiteSpace(txtApellido.Text) ||
                        (string.IsNullOrWhiteSpace(txtDni.Text) ||
                            (string.IsNullOrWhiteSpace(txtMatricula.Text) ||
                                (string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                                    (string.IsNullOrWhiteSpace(cboLocalidades.Text)))))))
            {
                MessageBox.Show("Todos los campos son requeridos!!");
                return false;
            }

            return true;
        }

        private void btnAdministracionEspecialidades_Click(object sender, EventArgs e)
        {
            frmAbmEspecialidad_Medico frmEspecialidadMedico = new frmAbmEspecialidad_Medico();
            frmEspecialidadMedico.ShowDialog();
        }

        private void btnDisponibilidadHoraria_Click(object sender, EventArgs e)
        {

        }
    }
}
