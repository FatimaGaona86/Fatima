using System;
using System.Windows.Forms;
using DAL.Servicio;
using BLL.Enums;
using BLL.Dominio;

namespace PL
{
    public partial class frmListadoGral : Form
    {
        ePantalla pantalla;

        public frmListadoGral() // constructor
        {
            InitializeComponent();
        } 

        public frmListadoGral(ePantalla pantalla) // otro constructor
        {
            InitializeComponent();
            this.pantalla = pantalla;
        }

        private void frmListadoGral_Load(object sender, EventArgs e)
        {
            switch (pantalla)
            {
                case ePantalla.Paciente:
                    lblTitulo.Text = "Pacientes: ";
                    btnVerEspecialidades.Visible = false;
                    cargarGrillaPacientes();
                    dgvGrilla.Select();

                    break;

                case ePantalla.Medico:
                    lblTitulo.Text = "Medicos: ";
                    btnVerEspecialidades.Visible = true;
                    cargarGrillaMedicos();
                    dgvGrilla.Select();
                    break;

                case ePantalla.Localidad:

                    lblTitulo.Text = "Localidades: ";
                    btnVerEspecialidades.Visible = false;
                    cargarGrillaLocalidades();
                    dgvGrilla.Select();
                    break;

                case ePantalla.Especialidad:
                    lblTitulo.Text = "Especialidades: ";
                    btnVerEspecialidades.Visible = false;
                    cargarGrillaEspecialidades();
                    dgvGrilla.Select();
                    break;   
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            switch (pantalla)
            {
                case ePantalla.Paciente:
                    frmAbmPaciente frmAbmPaciente = new frmAbmPaciente(eAccion.Alta);
                    frmAbmPaciente.ShowDialog();

                    cargarGrillaPacientes(); //Actualiza grilla
                    break;

                case ePantalla.Medico:
                    frmAbmMedico frmAbmMedico = new frmAbmMedico(eAccion.Alta);
                    frmAbmMedico.ShowDialog();

                    cargarGrillaMedicos();
                    break;

                case ePantalla.Localidad:
                    frmAbmLocalidad frmAbmLocalidad = new frmAbmLocalidad(eAccion.Alta);
                    frmAbmLocalidad.ShowDialog();

                    cargarGrillaLocalidades();
                    break;

                case ePantalla.Especialidad:
                    frmAbmEspecialidad frmEspecialidad = new frmAbmEspecialidad(eAccion.Alta);
                    frmEspecialidad.ShowDialog();

                    cargarGrillaEspecialidades();
                    break;

            }  
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            switch (pantalla)
            {
                case ePantalla.Paciente:
                    try
                    {
                        frmAbmPaciente frmAbmPaciente = new frmAbmPaciente(eAccion.Modificacion, (Paciente)dgvGrilla.CurrentRow.DataBoundItem);
                        frmAbmPaciente.ShowDialog();

                        cargarGrillaPacientes(); //Actualiza grilla con el paciente recién agregado
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case ePantalla.Medico:
                    try
                    {
                        frmAbmMedico frmAbmMedico = new frmAbmMedico(eAccion.Modificacion, (Medico)dgvGrilla.CurrentRow.DataBoundItem);
                        frmAbmMedico.ShowDialog();

                        cargarGrillaMedicos(); //Actualiza grilla con el medico recién agregado
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case ePantalla.Localidad:
                    try
                    {
                        frmAbmLocalidad frmAbmLocalidad = new frmAbmLocalidad(eAccion.Modificacion, (Localidad)dgvGrilla.CurrentRow.DataBoundItem);
                        frmAbmLocalidad.ShowDialog();

                        cargarGrillaLocalidades();
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case ePantalla.Especialidad:
                    frmAbmEspecialidad frmEspecialidad = new frmAbmEspecialidad(eAccion.Modificacion, (Especialidad)dgvGrilla.CurrentRow.DataBoundItem);
                    frmEspecialidad.ShowDialog();

                    cargarGrillaEspecialidades();
                    break;
            }    
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            switch (pantalla)
            {
                case ePantalla.Paciente:
                    frmAbmPaciente frmAbmPaciente = new frmAbmPaciente(eAccion.Baja, (Paciente)dgvGrilla.CurrentRow.DataBoundItem);
                    frmAbmPaciente.ShowDialog();

                    cargarGrillaPacientes(); //Actualiza grilla
                    break;

                case ePantalla.Medico:
                    frmAbmMedico frmAbmMedico = new frmAbmMedico(eAccion.Baja, (Medico)dgvGrilla.CurrentRow.DataBoundItem);
                    frmAbmMedico.ShowDialog();

                    cargarGrillaMedicos();
                    break;

                case ePantalla.Localidad:
                    frmAbmLocalidad frmAbmLocalidad = new frmAbmLocalidad(eAccion.Baja, (Localidad)dgvGrilla.CurrentRow.DataBoundItem);
                    frmAbmLocalidad.ShowDialog();

                    cargarGrillaLocalidades();
                    break;

                case ePantalla.Especialidad:
                    frmAbmEspecialidad frmAbmEspecialidad = new frmAbmEspecialidad(eAccion.Baja, (Especialidad)dgvGrilla.CurrentRow.DataBoundItem);
                    frmAbmEspecialidad.ShowDialog();

                    cargarGrillaEspecialidades();
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void cargarGrillaPacientes()
        {
            PacienteService pacienteService = new PacienteService();
            try
            {
                dgvGrilla.DataSource = pacienteService.traerPacientes();

                dgvGrilla.Columns[0].Visible = false; // IdPaciente
                dgvGrilla.Columns[1].Visible = false; // HistoriaClinica
                dgvGrilla.Columns[2].Visible = false; // IdPersona
                dgvGrilla.Columns[8].Visible = false; // Estado
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        private void cargarGrillaMedicos()
        {
            MedicoService medicoService = new MedicoService();
            try
            {
                dgvGrilla.DataSource = medicoService.traerTodos();

                dgvGrilla.Columns[0].Visible = false; // IdMedico
                dgvGrilla.Columns[2].Visible = false; // Disponibilidad
                dgvGrilla.Columns[3].Visible = false; // IdPersona
                dgvGrilla.Columns[9].Visible = false; // Estado
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarGrillaLocalidades()
        {
            LocalidadService localidadService = new LocalidadService();
            try
            {
                dgvGrilla.DataSource = localidadService.traerLocalidades();

                dgvGrilla.Columns[0].Visible = false;
                dgvGrilla.Columns[4].Visible = false;
                ////dgvGrilla.Columns[3].Visible = false;
                //dgvGrilla.Columns[4].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarGrillaEspecialidades()
        {
            EspecialidadService especialidadService = new EspecialidadService();
            try
            {
                dgvGrilla.DataSource = especialidadService.traerEspecialidades();

                dgvGrilla.Columns[0].Visible = false; 
                dgvGrilla.Columns[2].Visible = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void linkAdmEspecialidades_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnVerEspecialidades_Click(object sender, EventArgs e)
        {
            frmAbmEspecialidad_Medico frmAbmEspecialidadMedico = new frmAbmEspecialidad_Medico((Medico)dgvGrilla.CurrentRow.DataBoundItem);
            frmAbmEspecialidadMedico.ShowDialog();

        }

        private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMedicos_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmListadoGral frmListadoGral = new frmListadoGral(ePantalla.Medico);
            frmListadoGral.ShowDialog();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmListadoGral frmListadoGral = new frmListadoGral(ePantalla.Paciente);
            frmListadoGral.ShowDialog();
        }

        private void btnEspecialidades_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmListadoGral frmListadoGral = new frmListadoGral(ePantalla.Especialidad);
            frmListadoGral.ShowDialog();
        }

        private void btnLocalidades_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmListadoGral frmListadoGral = new frmListadoGral(ePantalla.Localidad);
            frmListadoGral.ShowDialog();
        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmTurnos frmTurnos = new frmTurnos();
            frmTurnos.ShowDialog();
        }
    }
}
