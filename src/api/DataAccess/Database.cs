using System;
using Microsoft.Data.Sqlite;

namespace aloe.DataAccess
{
    public class Database 
    {
        SqliteConnection _connection;

        public Database() 
        {
            using (_connection = new SqliteConnection(
                new SqliteConnectionStringBuilder("aloe.db").ToString()
            ))
            {
                _connection.Open();

                if (!IsDatabaseInitialized()) {
                    InitializeDatabase();
                }
            }
        }

        public bool IsDatabaseInitialized() {
            using (var transaction = _connection.BeginTransaction()) 
            {
                var selectCommand = _connection.CreateCommand();
                selectCommand.Transaction = transaction;
                selectCommand.CommandText = "SELECT IsInit FROM DbInit";
                using (var reader = selectCommand.ExecuteReader()) {
                    var initialized = false;
                    if (reader.HasRows) {
                        initialized = true;
                    }
                    transaction.Commit();
                    return initialized;
                }
            };
        }

        private void InitializeDatabase() {
            using (var transaction = _connection.BeginTransaction())
            {
                var createCommand = _connection.CreateCommand();
                createCommand.Transaction = transaction;
                createCommand.CommandText = 
                @"CREATE TABLE DbInit ( 
                    IsInit bit primary key not null
                )";
                createCommand.ExecuteNonQuery();

                var insertCommand = _connection.CreateCommand();
                insertCommand.Transaction = transaction;
                insertCommand.CommandText = 
                @"INSERT INTO DbInit ( IsInit ) VALUES ( $IsInit )
                ";
                insertCommand.Parameters.AddWithValue("$IsInit", true);
                insertCommand.ExecuteNonQuery();

                transaction.Commit();
            }
        }
    }
}