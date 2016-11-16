using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class AdministradorGral : Persona
    {
        private int idAdministradorGral;
        public int IdAdministradorGral
        {
            get { return idAdministradorGral; }
            set { idAdministradorGral = value; }
        }
    }
}
