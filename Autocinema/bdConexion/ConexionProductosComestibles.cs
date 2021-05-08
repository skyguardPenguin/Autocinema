using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocinema.bdConexion
{
    class ConexionProductosComestibles
    {
        Conexion conexion;
        SqlCommand command;
        SqlDataReader reader;
        DataTable table;
        public ConexionProductosComestibles()
        {
            conexion = new Conexion();
            table = new DataTable();
            command = new SqlCommand();

        }
        public void Insert(string nombre, float precio)
        {
            command.Connection = conexion.Open();
            command.CommandText = "InsertProductoComestible";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombre", nombre);
            command.Parameters.AddWithValue("@precio", precio);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();

        }
        public void Update(int id, string nombre, float precio)
        {
            command.Connection = conexion.Open();
            command.CommandText = "UpdateProductoComestible";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nombre", nombre);
            command.Parameters.AddWithValue("@precio", precio);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();
        }
        public DataTable ConsultAll()
        {
            command.Connection = conexion.Open();
            command.CommandText = "ConsultProductosComestibles";
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            table.Load(reader);
            conexion.Close();
            return table;
        }
        public void Delete(int id)
        {

            command.Connection = conexion.Open();
            command.CommandText = "DeleteProductoComestible";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();


        }
    }
}
