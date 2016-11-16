using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dominio;
using System.Data.SqlClient;

namespace DAL.Servicio
{
    public class PacienteService
    {
        public IList<Paciente> traerPacientes()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            IList<Paciente> listaDePacientes = new List<Paciente>();

            try
            {
                conexion.ConnectionString = "initial catalog = GAONA_DB; data source = (local); integrated security = sspi ";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT P.ESTADO, P.ID_PACIENTE, P.NOMBRE, P.APELLIDO, P.DNI, P.EMAIL, P.DIRECCION, L.ID_LOCALIDAD, L.CODIGOPOSTAL, L.LOCALIDAD FROM PACIENTE AS P LEFT JOIN LOCALIDAD AS L ON P.ID_LOCALIDAD = L.ID_LOCALIDAD WHERE P.ESTADO = 1";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Paciente pacienteAux = new Paciente();

                    pacienteAux.Estado = lector.GetBoolean(0);
                    pacienteAux.IdPaciente = lector.GetInt32(1);
                    pacienteAux.Nombre = lector.GetString(2);
                    pacienteAux.Apellido = lector.GetString(3);
                    pacienteAux.Dni = lector.GetInt32(4);
                    pacienteAux.Email = lector.GetString(5);
                    pacienteAux.Direccion = lector.GetString(6);

                    pacienteAux._Localidad = new Localidad();
                    pacienteAux._Localidad.IdLocalidad = lector.GetInt32(7);
                    pacienteAux._Localidad.CodigoPostal= lector.GetInt32(8);
                    pacienteAux._Localidad._Localidad = lector.GetString(9);

                    listaDePacientes.Add(pacienteAux);
                }

                return listaDePacientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
        }

        public void agregarPaciente(Paciente paciente)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " INSERT INTO PACIENTE VALUES ('" + paciente.Nombre + "' , '" + 
                                                                          paciente.Apellido + "' ," + 
                                                                          paciente.Dni + " , '" +
                                                                          paciente.Email + "',  '"+ 
                                                                          paciente.Direccion + "', " + 
                                                                          paciente._Localidad.IdLocalidad + ", " + 
                                                                          " 'Historia clinica: '" + " , "+ 1 + ")";
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
        }

        public int traerIdPaciente()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            int variable = 0;

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;
                comando.CommandText = "SELECT MAX(ID_PACIENTE)FROM PACIENTE";

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    variable = lector.GetInt32(0);
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }

            return variable;
        }

        public void agregarTelefonoPaciente(int IdPaciente, int telefono)
        {

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = "INSERT INTO TELEFONO_PERSONA VALUES (" + IdPaciente + ", " + telefono + ")";

                conexion.Open();
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
        }

        public IList<int> traerTelefonosPaciente(int id)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            IList<int> listaDeTelefonos = new List<int>();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;
                comando.CommandText = "SELECT TELEFONO FROM TELEFONO_PERSONA WHERE ID_PERSONA = " + id;

                conexion.Open();
                lector = comando.ExecuteReader();

                int i = 0;
                while (lector.Read())
                {
                    listaDeTelefonos.Add(lector.GetInt32(i));
                    i++;
                }
                return listaDeTelefonos;
            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
        }

        public void modificarPaciente(Paciente paciente)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " UPDATE PACIENTE SET NOMBRE = '" + paciente.Nombre + "', APELLIDO =  '" + paciente.Apellido + "', DNI =" + paciente.Dni + ", DIRECCION ='" + paciente.Direccion + "', ID_LOCALIDAD = " + paciente._Localidad.IdLocalidad + ", HISTORIA_CLINICA = ' Historia clinica: '" + " , ESTADO = " + 1 + "WHERE ID_PACIENTE = " + paciente.IdPaciente;

                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
        }

        public void eliminarPaciente(Paciente paciente)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " UPDATE PACIENTE SET ESTADO = " + 0 + "WHERE ID_PACIENTE = " + paciente.IdPaciente;

                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
        }
    }
}
