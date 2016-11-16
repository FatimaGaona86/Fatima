using BLL.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Servicio
{
    public class ProvinciaService
    {
        public IList<Provincia> traerProvincias()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            IList<Provincia> listaDeProvincias = new List<Provincia>();

            try
            {
                conexion.ConnectionString = "initial catalog = GAONA_DB; data source = (local); integrated security = sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT ID_PROVINCIA, PROVINCIA FROM PROVINCIA";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Provincia provinciaAux = new Provincia();

                    provinciaAux.IdProvincia = lector.GetInt32(0);
                    provinciaAux._Provincia = lector.GetString(1);

                    listaDeProvincias.Add(provinciaAux);
                }
                return listaDeProvincias;
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
