using BLL.Enums;
using System;
using System.Windows.Forms;
using DAL.Servicio;
using BLL.Dominio;

namespace PL
{
    public partial class frmAbmLocalidad : Form
    {
        eAccion accion;
        Localidad localidad;
        ProvinciaService provinciaService = new ProvinciaService();
        LocalidadService localidadService = new LocalidadService() ;

        public frmAbmLocalidad()
        {
            InitializeComponent();
        }

        public frmAbmLocalidad(eAccion accion)
        {
            InitializeComponent();
            this.accion = accion;
        }

        public frmAbmLocalidad(eAccion accion, Localidad localidad)
        {
            InitializeComponent();
            this.accion = accion;
            this.localidad = localidad;
        }

        private void frmAbmLocalidad_Load(object sender, EventArgs e)
        {
            cboProvincia.DataSource =  provinciaService.traerProvincias();
            cboProvincia.ValueMember = "IdProvincia";
            cboProvincia.DisplayMember = "_Provincia";
            cboProvincia.SelectedValue = -1;


            switch (accion)
            {
                case eAccion.Alta:
                    lblTitulo.Text = "Alta de Localidad";
                    localidad = new Localidad();
                    break;

                case eAccion.Modificacion:
                    lblTitulo.Text = "Modificacion: ";
                    txtLocalidad.Text = localidad._Localidad.ToString();
                    txtCodigoPostal.Text = localidad.CodigoPostal.ToString();
                    cboProvincia.SelectedValue = localidad.Provincia.IdProvincia;

                    break;

                case eAccion.Baja:
                    lblTitulo.Text = "Eliminación: ";
                    txtLocalidad.Text = localidad._Localidad.ToString();
                    txtCodigoPostal.Text = localidad.CodigoPostal.ToString();
                    cboProvincia.SelectedValue = localidad.Provincia.IdProvincia;

                    txtLocalidad.Enabled = false;
                    txtCodigoPostal.Enabled = false;
                    cboProvincia.Enabled = false;
                    break;

            }               
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (accion)
            {
                case eAccion.Alta:
                    localidad._Localidad = txtLocalidad.Text.Trim();
                    localidad.CodigoPostal = int.Parse(txtCodigoPostal.Text); ;
                    localidad.Provincia = (Provincia)cboProvincia.SelectedItem;

                    localidadService.agregarLocalidad(localidad);
                    MessageBox.Show("Localidad agregada correctamente!!...");
                    this.Dispose();

                    break;

                case eAccion.Modificacion:
                    localidad._Localidad = txtLocalidad.Text.Trim();
                    localidad.CodigoPostal = int.Parse(txtCodigoPostal.Text); ;
                    localidad.Provincia = (Provincia)cboProvincia.SelectedItem;

                    localidadService.modificarLocalidad(localidad);
                    MessageBox.Show("Localidad modificada correctamente!!...");
                    this.Dispose();

                    break;

                case eAccion.Baja:
                    if (MessageBox.Show("Esta seguro que desea eliminar esta localidad", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        localidadService.eliminarLocalidad(localidad);
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
            this.Dispose();
        }
    }
}
