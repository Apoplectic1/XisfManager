using Microsoft.Data.Sqlite;
using System;

namespace XisfFileManager.TargetScheduler
{
    internal class SqlLiteWriter
    {
        private SqlLiteManager _manager;

        public SqlLiteWriter(SqlLiteManager manager)
        {
            _manager = manager;
        }

        public static bool WriteDatabaseFile()
        {
            string connectionString = "Data Source=mydatabase.db"; // Replace with your database file path.

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Example: Insert data from mProfilePreferenceList into a table
                        using (SqliteCommand command = new SqliteCommand("INSERT INTO your_table (column1, column2) VALUES (@value1, @value2)", connection))
                        {
                            // Replace "your_table" with the name of your table and specify the columns accordingly
                            // command.Parameters.AddWithValue("@value1", mProfilePreferenceList[0].Column1); // Assuming Column1 is an int
                            // command.Parameters.AddWithValue("@value2", mProfilePreferenceList[0].Column2); // Assuming Column2 is a string

                            command.ExecuteNonQuery();
                        }

                        // Repeat the above process for other lists and tables

                        transaction.Commit(); // Commit the transaction to save changes
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback the transaction in case of an error
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return true;
        }

    }
}
