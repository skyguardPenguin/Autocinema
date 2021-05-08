using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Autocinema.bdConexion
{
    class ConexionHorarios
    {
        Conexion conexion;
        SqlCommand command;
        SqlDataReader reader;
        DataTable table;

        public ConexionHorarios()
        {
            conexion = new Conexion();
            table = new DataTable();
            command = new SqlCommand();
            
        }
        public void Insert(int idPelicula,string fecha,string hora)
        {
            command.Connection = conexion.Open();
            command.CommandText = "InsertHorario";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idPelicula",idPelicula);
            command.Parameters.AddWithValue("@fecha",fecha);
            command.Parameters.AddWithValue("@hora",hora);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();

        }
        public void Update(int id,int idPelicula, string fecha, string hora)
        {
            command.Connection = conexion.Open();
            command.CommandText = "UpdateHorario";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idPelicula", idPelicula);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.Parameters.AddWithValue("@hora", hora);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();
        }
        public DataTable ConsultAll()
        {
            command.Connection = conexion.Open();
            command.CommandText = "ConsultHorarios";
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            table.Load(reader);
            conexion.Close();
            return table;
        }

        public int[] ConsultIds()
        {
            command.Connection = conexion.Open();
            command.CommandText = "ConsultIdHorarios";
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();

            List<int> listIds = new List<int>();
            while (reader.Read())
                listIds.Add((int)reader[0]);
            return listIds.ToArray();
        }
        public void Delete(int id)
        {
           
            command.Connection = conexion.Open();
            command.CommandText = "DeleteHorario";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id",id);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();
        
           
        }

      
    }
}
