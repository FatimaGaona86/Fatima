using BLL.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Servicio
{
    public class EspecialidadService
    {

        public IList<Especialidad> traerEspecialidades()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            IList<Especialidad> listaDeEspecialidades = new List<Especialidad>();

            try
            {
                conexion.ConnectionString = "initial catalog = GAONA_DB; data source = (local); integrated security = sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT ID_ESPECIALIDAD, NOMBRE_ESPECIALIDAD, ESTADO FROM ESPECIALIDAD WHERE ESTADO = " + 1;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Especialidad especialidadAux = new Especialidad();

                    especialidadAux.IdEspecialidad = lector.GetInt32(0);
                    especialidadAux._Especialidad = lector.GetString(1);
                    especialidadAux.Estado = lector.GetBoolean(2);

                    listaDeEspecialidades.Add(especialidadAux);
                }
                return listaDeEspecialidades;
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

        public IList<Especialidad> traerEspecialidadesPorMedico(int idMedico, bool traerActivos = true)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            IList<Especialidad> listaDeEspecialidades = new List<Especialidad>();

            try
            {
                conexion.ConnectionString = "initial catalog = GAONA_DB; data source = (local); integrated security = sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = " SELECT E.NOMBRE_ESPECIALIDAD, E.ID_ESPECIALIDAD" +
                                      " FROM ESPECIALIDAD_MEDICO EM INNER JOIN" +
                                      " ESPECIALIDAD E ON E.ID_ESPECIALIDAD = EM.ID_ESPECIALIDAD" +
                                      " WHERE ID_MEDICO = " + idMedico;

                if (traerActivos)
                    comando.CommandText += " AND E.ESTADO = 1";

                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Especialidad especialidadAux = new Especialidad();

                    especialidadAux._Especialidad = lector.GetString(0);
                    especialidadAux.IdEspecialidad = lector.GetInt32(1);

                    listaDeEspecialidades.Add(especialidadAux);
                }
                return listaDeEspecialidades;
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

        public void agregarEspecialidad(Especialidad especialidad)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " INSERT INTO ESPECIALIDAD VALUES ('" + especialidad._Especialidad + "', " + 1 + ")";

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

        public void modificarEspecialidad(Especialidad especialidad)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " UPDATE ESPECIALIDAD SET NOMBRE_ESPECIALIDAD = '" + especialidad._Especialidad + "' WHERE ID_ESPECIALIDAD =  " + especialidad.IdEspecialidad;

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

        public void eliminarEspecialidad(Especialidad especialidad)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " UPDATE ESPECIALIDAD SET ESTADO = " + 0 + "WHERE ID_ESPECIALIDAD = " + especialidad.IdEspecialidad;

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
