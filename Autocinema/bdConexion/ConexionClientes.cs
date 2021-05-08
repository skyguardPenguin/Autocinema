using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autocinema.bdConexion
{
    class ConexionClientes
    {
        Conexion conexion;
        SqlCommand command;
        SqlDataReader reader;
        DataTable table;

        public ConexionClientes()
        {
            conexion = new Conexion();
            table = new DataTable();
            command = new SqlCommand();
        }
        public void Insert(string matriculaAuto,string marcaAuto)
        {
            command.Connection = conexion.Open();
            command.CommandText = "InsertCliente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@matriculaAuto",matriculaAuto);
            command.Parameters.AddWithValue("@marcaAuto", marcaAuto);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();

        }
        public void Update(int id,string matriculaAuto, string marcaAuto)
        {
            command.Connection = conexion.Open();
            command.CommandText = "UpdateCliente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@matriculaAuto", matriculaAuto);
            command.Parameters.AddWithValue("@marcaAuto", marcaAuto);
            command.Parameters.AddWithValue("@id",id);
            
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();
        }
        public DataTable ConsultAll()
        {
            command.Connection = conexion.Open();
            command.CommandText = "ConsultClientes";
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
             table.Load(reader);
            conexion.Close();
            return table;
        }
        public int[] ConsultIds()
        {
            command.Connection = conexion.Open();
            command.CommandText = "ConsultIdClientes";
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            List<int> Lids = new List<int>();

            while (reader.Read())
                Lids.Add((int)reader[0]);
                
            
            conexion.Close();
            return Lids.ToArray();
        }
        public void Delete(int id)
        {

            command.Connection = conexion.Open();
            command.CommandText = "DeleteCliente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();


        }
    }
}
