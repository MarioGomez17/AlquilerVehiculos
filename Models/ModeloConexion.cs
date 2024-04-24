using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloConexion
    {

        public static MySqlConnection Conect()
        {
            String Server = "localhost";
            String DataBase = "alquiler_vehiculos";
            String User = "root";
            String Password = "M@rio1002960089";
            String ConnectionString = "database = " + DataBase + "; Data Source = " + Server + "; User Id = " + User + "; Password = " + Password;

            try
            {
                MySqlConnection ConecctionBD = new(ConnectionString);
                return ConecctionBD;
            }
            catch (MySqlException)
            {
                return null;
            }
        }

        public static Boolean ExecuteNonQuerySentence(String SQLQuery)
        {
            Boolean Flag = false;
            MySqlConnection ConecctionBD = Conect();
            ConecctionBD.Open();
            try
            {
                MySqlCommand Command = new(SQLQuery, ConecctionBD);
                Command.ExecuteNonQuery();
                Flag = true;
            }
            catch (MySqlException error)
            {
                Flag = false;
                Console.WriteLine(error);
            }
            finally
            {
                ConecctionBD.Close();
            }
            return Flag;
        }

    }
}
