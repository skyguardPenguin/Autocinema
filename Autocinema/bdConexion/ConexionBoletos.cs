using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocinema.bdConexion
{
    class ConexionBoletos
    {
        Conexion conexion;
        SqlCommand command;
        SqlDataReader reader;
        DataTable table;

        public ConexionBoletos()
        {
            conexion = new Conexion();
            table = new DataTable();
            command = new SqlCommand();
        }
        public void Insert(int folio,int idCliente, int idHorario)
        {
            command.Connection = conexion.Open();
            command.CommandText = "InsertBoleto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@folio", folio);
            command.Parameters.AddWithValue("@idCliente", idCliente);
            command.Parameters.AddWithValue("@idHorario", idHorario);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();

        }
        public void Update(int folio, int idCliente, int idHorario)
        {
            command.Connection = conexion.Open();
            command.CommandText = "UpdateBoleto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@folio", folio);
            command.Parameters.AddWithValue("@idCliente", idCliente);
            command.Parameters.AddWithValue("@idHorario", idHorario);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();
        }
        public DataTable ConsultAll()
        {
            command.Connection = conexion.Open();
            command.CommandText = "ConsultBoletos";
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            table.Load(reader);
            conexion.Close();
            return table;
        }
        public void Delete(int folio)
        {

            command.Connection = conexion.Open();
            command.CommandText = "DeleteBoleto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@folio", folio);
            command.ExecuteNonQuery();
            conexion.Close();
            command.Parameters.Clear();


        }
    }

}
