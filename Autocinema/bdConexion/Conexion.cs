using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Autocinema.bdConexion
{
    class Conexion
    {
        SqlConnection conexion = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=autocinema; Integrated Security=SSPI");

        public SqlConnection Open()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }
        public SqlConnection Close()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
            return conexion;
        }
    }
}
