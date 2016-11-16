using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Horario
    {
        private int idHorario;
        public int IdHorario
        {
            get { return idHorario; }
            set { idHorario = value; }
        }

        private int hora;
        public int Hora
        {
            get { return hora; }
            set { hora = value; }
        }



    }
}
