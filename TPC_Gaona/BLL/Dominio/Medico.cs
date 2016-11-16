using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Medico: Persona
    {
        private int idMedico;
        public int IdMedico
        {
            get { return idMedico; }
            set { idMedico = value; }
        }

        private int matricula;
        public int Matricula
        {
            get { return matricula; }
            set { matricula = value; }

        }

        private List<Especialidad> listaDeEspecialidades;
        public List<Especialidad> ListaDeEspecialidades
        {
            get { return listaDeEspecialidades; }
            set { listaDeEspecialidades = value; }
        }

        private DisponibilidadHoraria disponibilidad;
        public DisponibilidadHoraria Disponibilidad
        {
            get { return disponibilidad; }
            set { disponibilidad = value; }
        }
    }
}
