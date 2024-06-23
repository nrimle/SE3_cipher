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
                connection.Open();

                // Überprüfen, ob der Eintrag bereits existiert
                string checkQuery = "SELECT COUNT(*) FROM EncryptedTexts WHERE EncryptText = @EncryptText AND DecryptText = @DecryptText AND KeyValue = @Key";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@EncryptText", encryption.EncryptText);
                checkCommand.Parameters.AddWithValue("@DecryptText", encryption.DecryptText);
                checkCommand.Parameters.AddWithValue("@Key", encryption.Key);

                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    // Falls der Eintrag existiert, aktualisiere den TimeStamp
                    string updateQuery = "UPDATE EncryptedTexts SET TimeStamp = @TimeStamp WHERE EncryptText = @EncryptText AND DecryptText = @DecryptText AND KeyValue = @Key";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@EncryptText", encryption.EncryptText);
                    updateCommand.Parameters.AddWithValue("@DecryptText", encryption.DecryptText);
                    updateCommand.Parameters.AddWithValue("@Key", encryption.Key);
                    updateCommand.Parameters.AddWithValue("@TimeStamp", encryption.TimeStamp);

                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    // Falls der Eintrag nicht existiert, füge einen neuen Eintrag hinzu
                    string insertQuery = "INSERT INTO EncryptedTexts (EncryptText, DecryptText, KeyValue, TimeStamp) VALUES (@EncryptText, @DecryptText, @Key, @TimeStamp)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@EncryptText", encryption.EncryptText);
                    insertCommand.Parameters.AddWithValue("@DecryptText", encryption.DecryptText);
                    insertCommand.Parameters.AddWithValue("@Key", encryption.Key);
                    insertCommand.Parameters.AddWithValue("@TimeStamp", encryption.TimeStamp);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }


        public static List<EncryptionModel> GetLastFiveRecords()
        {
            var records = new List<EncryptionModel>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT TOP 5 EncryptText, DecryptText, KeyValue FROM EncryptedTexts ORDER BY TimeStamp DESC";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new EncryptionModel
                            {
                                EncryptText = reader.GetString(0),
                                DecryptText = reader.GetString(1),
                                Key = reader.GetString(2),
                            });
                        }
                    }
                }
            }

            return records;
        }
    }
}
