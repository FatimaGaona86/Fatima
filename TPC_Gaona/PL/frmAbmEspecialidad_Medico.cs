using BLL.Dominio;
using BLL.Enums;
using DAL.Servicio;
using System;
using System.Windows.Forms;

namespace PL
{
    public partial class frmAbmEspecialidad_Medico : Form
    { 
        Medico medico;

        public frmAbmEspecialidad_Medico()
        {
            InitializeComponent();
        }

        public frmAbmEspecialidad_Medico(Medico medico)
        {
            InitializeComponent();
            this.medico = medico;
        }

        private void frmAbmEspecialidad_Load(object sender, EventArgs e)
        {
            try
            {
                txtMedico.Text = medico.Nombre + " " + medico.Apellido;
                txtMedico.Enabled = false;

                cargarGrillaEspecialidad_Medico();
                cargarGrillaListadoGeneral();

                dgvEspecialidades_x_Medico.Select();
                dgvListadoGeneral.Select();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MedicoService medicoService = new MedicoService();
            Especialidad especialidad = (Especialidad)dgvListadoGeneral.CurrentRow.DataBoundItem;
            try
            {
                if (!(medicoService.existeEspecialidad(especialidad.IdEspecialidad, medico.IdMedico)))//si no existe esa especialidad...
                {
                    medicoService.agregarEspecialidadMedico(medico.IdMedico, especialidad.IdEspecialidad);//Asocia la especialidad al médico
                    cargarGrillaEspecialidad_Medico();
                }
                else
                {
                    MessageBox.Show("La especialidad: " + especialidad._Especialidad + " ya existe!..");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MedicoService medicoService = new MedicoService();
            Especialidad especialidad = (Especialidad)dgvEspecialidades_x_Medico.CurrentRow.DataBoundItem;
            try
            {
                if (MessageBox.Show("Esta seguro que desea eliminar especialidad: " + especialidad._Especialidad + " ?" , "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    medicoService.eliminarEspecialidadMedico(medico.IdMedico, especialidad.IdEspecialidad);
                    cargarGrillaEspecialidad_Medico();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void linkAdministracionEspecialidades_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmListadoGral frmListadoGral = new frmListadoGral(ePantalla.Especialidad);
            frmListadoGral.ShowDialog();
        }

        private void cargarGrillaEspecialidad_Medico()
        {
            try
            {
                EspecialidadService especialidadService = new EspecialidadService();
                dgvEspecialidades_x_Medico.DataSource = especialidadService.traerEspecialidadesPorMedico(medico.IdMedico);

                dgvEspecialidades_x_Medico.Columns[0].Visible = false; // IdMedico
                dgvEspecialidades_x_Medico.Columns[2].Visible = false; // Disponibilidad
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarGrillaListadoGeneral()
        {
            try
            {
                EspecialidadService especialidadService = new EspecialidadService();
                dgvListadoGeneral.DataSource = especialidadService.traerEspecialidades();

                dgvListadoGeneral.Columns[0].Visible = false; // IdMedico
                dgvListadoGeneral.Columns[2].Visible = false; // Disponibilidad
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
