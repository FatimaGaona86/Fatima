using BLL.Dominio;
using DAL.Servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PL
{
    public partial class frmTurnos : Form
    {
        public frmTurnos()
        {
            InitializeComponent();
        }

        private void frmTurnos_Load(object sender, EventArgs e)
        {
            PacienteService pacienteService = new PacienteService();
            EspecialidadService especialidadService = new EspecialidadService();

            cmbPacientes.DataSource = pacienteService.traerPacientes();
            cmbPacientes.ValueMember = "IdPaciente";
            cmbPacientes.DisplayMember = "NombreApellido"; 

            cmbEspecialidades.DataSource = especialidadService.traerEspecialidades();
            cmbEspecialidades.ValueMember = "IdEspecialidad";
            cmbEspecialidades.DisplayMember = "_Especialidad";
        }


        private void cmbEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            MedicoService medicoService = new MedicoService();

            Especialidad especialidad = (Especialidad)cmbEspecialidades.SelectedItem;
            cmbMedicos.DataSource = medicoService.traerMedicosPorEspecialidad(especialidad.IdEspecialidad);
            cmbMedicos.ValueMember = "IdMedico";
            cmbMedicos.DisplayMember = "NombreApellido"; 
        }
    }
}
