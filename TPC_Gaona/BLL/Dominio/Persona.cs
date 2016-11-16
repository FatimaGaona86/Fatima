using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Persona
    {
        private int idPersona; 
        public int IdPersona
        {
            get { return idPersona; }
            set { idPersona = value; }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apellido;
        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        private int dni;
        public int Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email= value; }
        }

        private string direccion;
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private Localidad localidad;
        public Localidad _Localidad
        {
            get { return localidad; }
            set { localidad = value; }
        }

        private bool estado;
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private string nombreApellido;
        public string NombreApellido
        {
            get { return Nombre + Apellido; }
        }

        private List<Telefono> listaDeTelefonos;
        public List<Telefono> ListaDeTelefonos
        {
            get { return listaDeTelefonos; }
            set { listaDeTelefonos = value; }
        }
    }
}
