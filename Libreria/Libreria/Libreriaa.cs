using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WindowsFormsApp4
{
    static class Libreriaa
    {
        public static void Ejecutar(string sql)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.ConnectionStrings["base"].ConnectionString;
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = sql;
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public static DataTable Recuperar(string sql)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.ConnectionStrings["base"].ConnectionString;
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = sql;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }

    }
}