using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Especialidad
    {
        private int idEspecialidad;
        public int IdEspecialidad
        {
            get { return idEspecialidad; }
            set { idEspecialidad = value; }
        }

        private string especialidad;
        public string _Especialidad
        {
            get { return especialidad; }
            set { especialidad = value; }
        }

        private bool estado; 
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
      
    }
}
