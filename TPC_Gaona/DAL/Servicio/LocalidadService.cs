using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dominio;
using System.Data.SqlClient;

namespace DAL.Servicio
{
    public class LocalidadService
    {
        public IList<Localidad> traerLocalidades()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            IList<Localidad> listaDeLocalidades = new List<Localidad>();

            try
            {
                conexion.ConnectionString = "initial catalog = GAONA_DB; data source = (local); integrated security = sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT L.ID_LOCALIDAD, L.CODIGOPOSTAL,L.LOCALIDAD, L.ESTADO, P.ID_PROVINCIA, P.PROVINCIA FROM LOCALIDAD AS L INNER JOIN PROVINCIA AS P ON L.ID_PROVINCIA = P.ID_PROVINCIA WHERE ESTADO = 1";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Localidad localidadAux = new Localidad();

                    localidadAux.IdLocalidad = lector.GetInt32(0);
                    localidadAux.CodigoPostal = lector.GetInt32(1);
                    localidadAux._Localidad = lector.GetString(2);
                    localidadAux.Estado = lector.GetBoolean(3);
                    localidadAux.Provincia = new Provincia();
                    localidadAux.Provincia.IdProvincia = lector.GetInt32(4);
                    localidadAux.Provincia._Provincia = lector.GetString(5);

                    listaDeLocalidades.Add(localidadAux);
                }
                return listaDeLocalidades;
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

        public void agregarLocalidad(Localidad localidad)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " INSERT INTO LOCALIDAD VALUES (" + localidad.CodigoPostal + " , '" + localidad._Localidad + "', " + localidad.Provincia.IdProvincia + " , " + 1 + ")";

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

        public void modificarLocalidad(Localidad localidad)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " UPDATE LOCALIDAD SET CODIGOPOSTAL = " + localidad.CodigoPostal + ", LOCALIDAD = '" + localidad._Localidad + "', ID_PROVINCIA = " + localidad.Provincia.IdProvincia + ", ESTADO = " + 1 + "WHERE ID_LOCALIDAD = " + localidad.IdLocalidad;

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

        public void eliminarLocalidad(Localidad localidad)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " UPDATE LOCALIDAD SET ESTADO = " + 0 + "WHERE ID_LOCALIDAD = " + localidad.IdLocalidad;

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
