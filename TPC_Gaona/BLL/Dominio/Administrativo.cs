using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Administrativo
    {
        private int idAdministrativo;
        public int IdAdministrativo
        {
            get { return idAdministrativo; }
            set { idAdministrativo = value; }
        }

        private int legajo;
        public int Legajo
        {
            get { return legajo; }
            set { legajo = value; }
        }
    }
}
