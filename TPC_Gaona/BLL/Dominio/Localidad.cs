using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Localidad
    {
        private int idlocalidad; 
        public int IdLocalidad
        {
            get { return idlocalidad; }
            set { idlocalidad = value; }
        }

        private string localidad;
        public string _Localidad
        {
            get { return localidad; }
            set { localidad = value; }
        }

        private int codigoPostal;
        public int CodigoPostal
        {
            get { return codigoPostal; }
            set { codigoPostal = value; }
        }

        private Provincia provincia;
        public Provincia Provincia
        {
            get { return provincia; }
            set { provincia = value; }
        }

        private bool estado;
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public override string ToString()
        {
            return _Localidad;
        }
    }
}
