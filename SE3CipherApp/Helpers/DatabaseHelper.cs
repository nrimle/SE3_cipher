using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SE3CipherApp.Models;
using System.Configuration;

namespace SE3CipherApp
{
    public static class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static void SaveEncryption(EncryptionModel encryption)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO EncryptedTexts (EncryptText, DecryptText, KeyValue, TimeStamp) VALUES (@EncryptText, @DecryptText, @Key, @TimeStamp)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EncryptText", encryption.EncryptText);
                command.Parameters.AddWithValue("@DecryptText", encryption.DecryptText);
                command.Parameters.AddWithValue("@Key", encryption.Key);
                command.Parameters.AddWithValue("@TimeStamp", encryption.TimeStamp);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
