using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class AgendaDeTurnos
    {
        private int idAgendaDeTurnos; 
        public int IdAgendaDeTurnos
        {
            get { return idAgendaDeTurnos; }
            set { idAgendaDeTurnos = value; }
        }

        private List<Turno> listaDeTurnos;
        public List<Turno> ListaDeTurnos
        {
            get { return listaDeTurnos; }
            set { listaDeTurnos = value; }
        }
    }
}
