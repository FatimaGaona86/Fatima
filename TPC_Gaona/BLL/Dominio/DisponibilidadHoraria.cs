using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class DisponibilidadHoraria
    {
        private int idDisponibilidadHoraria;
        public int IdDisponibilidadHoraria
        {
            get { return idDisponibilidadHoraria; }
            set { idDisponibilidadHoraria = value; }
        }

        private Horario entrada; 
        public Horario Entrada
        {
            get { return entrada; }
            set { entrada = value; }
        }

        private Horario salida;
        public Horario Salida
        {
            get { return salida; }
            set { salida = value; }
        }

        private Dia dia;
        public Dia Dia
        {
            get { return dia; }
            set { dia = value; }
        }
    }
}
