using System;
using System.Windows.Forms;
using BLL.Enums;

namespace PL
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            frmListadoGral frmListadoGral = new frmListadoGral(ePantalla.Paciente);
            frmListadoGral.ShowDialog();
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            frmListadoGral frmListadoGral = new frmListadoGral(ePantalla.Medico);
            frmListadoGral.ShowDialog();
        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {
            frmTurnos frmTurnos = new frmTurnos();
            frmTurnos.ShowDialog();
        }
    }
}
