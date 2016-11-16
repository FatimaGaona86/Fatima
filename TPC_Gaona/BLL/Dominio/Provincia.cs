using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Provincia
    {
        private int idProvincia; 
        public int IdProvincia
        {
            get { return idProvincia; }
            set { idProvincia = value; }
        }

        private string provincia;
        public string _Provincia
        {
            get { return provincia; }
            set { provincia = value; }
        }

        public override string ToString()
        {
            return _Provincia;
        }
    }
}
