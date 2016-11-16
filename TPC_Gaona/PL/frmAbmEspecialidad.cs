using BLL.Dominio;
using BLL.Enums;
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
    
    public partial class frmAbmEspecialidad : Form
    {
        private Especialidad especialidad;
        eAccion accion;

        EspecialidadService especialidadService = new EspecialidadService();

        public frmAbmEspecialidad()
        {
            InitializeComponent();
        }

        public frmAbmEspecialidad(eAccion accion)
        {
            InitializeComponent();
            this.accion = accion;
        }

        public frmAbmEspecialidad(eAccion accion, Especialidad especialidad)
        {
            InitializeComponent();
            this.accion = accion;
            this.especialidad = especialidad;
        }

        private void frmEspecialidad_Load(object sender, EventArgs e)
        {
            switch (accion)
            {
                case eAccion.Alta:
                    lblTitulo.Text = "Agregando nueva especialidad: ";
                    especialidad = new Especialidad();
                    break;

                case eAccion.Modificacion:
                    lblTitulo.Text = "Modificando Especialidad: ";
                    txtEspecialidad.Text = especialidad._Especialidad;
                    break;

                case eAccion.Baja:
                    lblTitulo.Text = "Eliminando especialidad: ";
                    txtEspecialidad.Text = especialidad._Especialidad;
                    txtEspecialidad.Enabled = false;
                    break;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case eAccion.Alta:
                    especialidad._Especialidad = txtEspecialidad.Text.Trim();
                    especialidadService.agregarEspecialidad(especialidad); // Falta validar que no exista esa especialidad
                    MessageBox.Show("Especialidad agregado correctamente!!...");
                    this.Dispose();
                    break;

                case eAccion.Modificacion:
                    especialidad._Especialidad = txtEspecialidad.Text ;
                    especialidadService.modificarEspecialidad(especialidad);
                    MessageBox.Show("Especialidad modificada correctamente!!...");
                    this.Dispose();
                    break;

                case eAccion.Baja:

                    if (MessageBox.Show("Esta seguro que desea eliminar esta especialidad", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        especialidadService.eliminarEspecialidad(especialidad);
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
    }
}
