using BLL.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Servicio
{
    public class MedicoService
    {
        public IList<Medico> traerTodos() 
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            IList<Medico> listaDeMedicos = new List<Medico>();

            try
            {
                conexion.ConnectionString = "initial catalog = GAONA_DB; data source = (local); integrated security = sspi ";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT M.ESTADO, M.ID_MEDICO, M.NOMBRE, M.APELLIDO, M.DNI, M.DIRECCION, L.ID_LOCALIDAD, L.CODIGOPOSTAL, L.LOCALIDAD, M.MATRICULA FROM MEDICO AS M LEFT JOIN LOCALIDAD AS L ON M.ID_LOCALIDAD = L.ID_LOCALIDAD WHERE M.ESTADO = 1";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Medico medicoAux = new Medico();

                    medicoAux.Estado = lector.GetBoolean(0);
                    medicoAux.IdMedico = lector.GetInt32(1);
                    medicoAux.Nombre = lector.GetString(2);
                    medicoAux.Apellido = lector.GetString(3);
                    medicoAux.Dni = lector.GetInt32(4);
                    medicoAux.Direccion = lector.GetString(5);

                    medicoAux._Localidad = new Localidad();
                    medicoAux._Localidad.IdLocalidad = lector.GetInt32(6);
                    medicoAux._Localidad.CodigoPostal = lector.GetInt32(7);
                    medicoAux._Localidad._Localidad = lector.GetString(8);
                    medicoAux.Matricula = lector.GetInt32(9);

                    listaDeMedicos.Add(medicoAux);
                }

                return listaDeMedicos;
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

        public IList<Medico> traerMedicosPorEspecialidad(int idEspecialidad )
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            IList<Medico> listaDeMedicos = new List<Medico>();

            try
            {
                conexion.ConnectionString = "initial catalog = GAONA_DB; data source = (local); integrated security = sspi ";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT M.ESTADO, M.ID_MEDICO, M.NOMBRE, M.APELLIDO, M.DNI FROM MEDICO AS M INNER JOIN ESPECIALIDAD_MEDICO AS EM ON M.ID_MEDICO = EM.ID_MEDICO AND EM.ID_ESPECIALIDAD = " + idEspecialidad;

                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Medico medicoAux = new Medico();

                    medicoAux.Estado = lector.GetBoolean(0);
                    medicoAux.IdMedico = lector.GetInt32(1);
                    medicoAux.Nombre = lector.GetString(2);
                    medicoAux.Apellido = lector.GetString(3);
                    medicoAux.Dni = lector.GetInt32(4);

                    listaDeMedicos.Add(medicoAux);
                }

                return listaDeMedicos;
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

        public int traerIdMedico()
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
                comando.CommandText = "SELECT MAX(ID_MEDICO)FROM MEDICO";

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

        public void agregarMedico(Medico medico) 
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " INSERT INTO MEDICO VALUES ('" + medico.Nombre + "' , '" + medico.Apellido + "' ," + medico.Dni + " , '" + medico.Direccion + "', " + medico._Localidad.IdLocalidad + ", " + medico.Matricula + " , " + 1 + ")";

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

        public bool existeEspecialidad(int idEspecialidad, int idMedico) 
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            //SqlDataReader lector;

            //bool existe = false;

            try
            {
                conexion.ConnectionString = "initial catalog = GAONA_DB; data source = (local); integrated security = sspi ";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = " SELECT COUNT(0) FROM ESPECIALIDAD_MEDICO " +
                                      " WHERE ID_ESPECIALIDAD = "+ idEspecialidad +
                                      " AND ID_MEDICO = " + idMedico ;
                comando.Connection = conexion;

                conexion.Open();
                //lector = comando.ExecuteReader();

                //if (lector.Read())
                //{
                //    existe = true;
                //}
                //else
                //{
                //    existe = false;
                //}

                return Convert.ToBoolean(comando.ExecuteScalar());
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

        public void agregarEspecialidadMedico(int IdMedico, int IdEspecialidad) 
        {

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " INSERT INTO ESPECIALIDAD_MEDICO VALUES (" + IdMedico + ", " + IdEspecialidad + ")";

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

        public void eliminarEspecialidadMedico(int id_medico, int id_especialidad) 
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = "DELETE FROM ESPECIALIDAD_MEDICO WHERE ID_MEDICO = " + id_medico + " AND ID_ESPECIALIDAD = " + id_especialidad;

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

        public void modificarMedico(Medico medico) 
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " UPDATE MEDICO SET NOMBRE = '" + medico.Nombre + "', APELLIDO =  '" + medico.Apellido + "', DNI =" + medico.Dni + ", DIRECCION ='" + medico.Direccion + "', ID_LOCALIDAD = " + medico._Localidad.IdLocalidad + ", MATRICULA =  " + medico.Matricula + ", ESTADO = " + 1 + "WHERE ID_MEDICO = " + medico.IdMedico;

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

        public void eliminarMedico(Medico medico) 
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "initial catalog= GAONA_DB; data source=(local); integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;

                comando.CommandText = " UPDATE MEDICO SET ESTADO = " + 0 + "WHERE ID_MEDICO = " + medico.IdMedico;

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
