using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocinema.bdConexion
{
    class ConexionPeliculas
    {
        Conexion conexion;
        SqlCommand command;
        SqlDataReader reader;
        DataTable table;

        public ConexionPeliculas()
        {
            conexion = new Conexion();
            table = new DataTable();
            command = new SqlCommand();

        }
        public void Insert(string nombre, int duracion,string clasificacion)
        {
            command.Connection=conexion.Open();
            command.CommandText = "InsertPelicula";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombre",nombre);
            command.Parameters.AddWithValue("@duracion",duracion);
            command.Parameters.AddWithValue("@clasificacion", clasificacion.ToCharArray());

            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();
        }

        public void Update(int id,string nombre, int duracion, string clasificacion)
        {
            command.Connection = conexion.Open();
            command.CommandText = "UpdatePelicula";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id",id);
            command.Parameters.AddWithValue("@nombre", nombre);
            command.Parameters.AddWithValue("@duracion", duracion);
            command.Parameters.AddWithValue("@clasificacion", clasificacion.ToCharArray());

            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();

        }
        public DataTable ConsultAll()
        {
            command.Connection = conexion.Open();
            command.CommandText = "ConsultPeliculas";
            command.CommandType = CommandType.StoredProcedure;

            reader = command.ExecuteReader();
            table.Load(reader);
            conexion.Close();
            return table;
        }
        public Dictionary<int,string> ConsultIdName()
        {
            command.Connection = conexion.Open();
            command.CommandText = "ConsultIdNamePeliculas";
            command.CommandType = CommandType.StoredProcedure;

            Dictionary<int, string> dict= new Dictionary<int, string>();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                dict[(int)reader[0]] = reader[1].ToString();
            }
            return dict;
        }
        public void Delete(int id)
        {
            command.Connection = conexion.Open();
            command.CommandText = "DeletePelicula";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();
        }
    }
}
