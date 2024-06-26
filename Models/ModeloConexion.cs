﻿using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloConexion
    {
        public static MySqlConnection Conect()
        {
            String Servidor = "localhost";
            String BaseDatos = "alquiler_vehiculos";
            String Usuario = "root";
            String Contrasena = "root";
            String CadenaConexion = "database = " + BaseDatos + "; Data Source = " + Servidor + "; User Id= " + Usuario + "; Password = " + Contrasena;
            try
            {
                MySqlConnection ConexionBD = new(CadenaConexion);
                return ConexionBD;
            }
            catch (MySqlException)
            {
                return null;
            }
        }
        public static Boolean ExecuteNonQuerySentence(String ConsultaSQL)
        {
            Boolean Bandera = false;
            MySqlConnection Conexion = Conect();
            Conexion.Open();
            try
            {
                MySqlCommand Comando = new(ConsultaSQL, Conexion);
                Comando.ExecuteNonQuery();
                Bandera = true;
            }
            catch (MySqlException)
            {
                Bandera = false;
            }
            finally
            {
                Conexion.Close();
            }
            return Bandera;
        }
    }
}
