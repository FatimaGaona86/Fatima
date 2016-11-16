using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Turno
    {
        private int idTurno; 
        public int IdTurno
        {
            get { return idTurno; }
            set { idTurno = value; }
        }

        private DateTime fecha;
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private Horario horario;
        public Horario Horario
        {
            get { return horario; }
            set { horario = value; }
        }

        private Medico medico;
        public Medico Medico
        {
            get { return medico; }
            set { medico = value; }
        }

        private Paciente paciente;
        public Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        private string motivoDeConsulta;
        public string MotivoDeConsulta
        {
            get { return motivoDeConsulta; }
            set { motivoDeConsulta = value; }
        }

        private bool estado;
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
