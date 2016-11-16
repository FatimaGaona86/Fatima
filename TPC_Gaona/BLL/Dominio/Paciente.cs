using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Paciente:Persona
    {
        private int idPaciente;
        public int IdPaciente
        {
            get { return idPaciente; }
            set { idPaciente = value; }
        }

        private string historiaClinica;
        public string HistoriaClinica
        {
            get { return historiaClinica; }
            set { historiaClinica = value; }
        }
    }
}
