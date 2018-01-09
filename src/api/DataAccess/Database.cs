using System;
using Microsoft.Data.Sqlite;

namespace api.DataAccess
{
    public class Database 
    {
        SqliteConnection _connection;

        public Database() 
        {
            using (_connection = new SqliteConnection(
                new SqliteConnectionStringBuilder() {
                    // DataSource = @"/home/andrew/Dev/Aloe/src/api/DataAccess/aloe"
                    DataSource = @"C:\Users\Andrew\Development\Aloe\src\api\DataAccess\aloe"
                }.ToString()
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
                try {
                    using (var reader = selectCommand.ExecuteReader()) {
                        var initialized = false;
                        if (reader.HasRows) {
                            initialized = true;
                        }
                        transaction.Commit();
                        return initialized;
                    }
                } catch (SqliteException e) {
                    var initialized = false;
                    return initialized;
                }
            };
        }

        public bool IsTableInitialized(string tableName) {
            using (var transaction = _connection.BeginTransaction())
            {
                var createCommand = _connection.CreateCommand();
                createCommand.Transaction = transaction;
                createCommand.CommandText = 
                @"SELECT 
                    * 
                FROM 
                    InitializedTable 
                WHERE 
                    Name = @TableName";
                createCommand.Parameters.Add(new SqliteParameter("@TableName", tableName));
                var reader = createCommand.ExecuteReader();
                return reader.HasRows;
            }
        }

        public void SetTableInitialized(string tableName) {
            using (var transaction = _connection.BeginTransaction())
            {
                var createCommand = _connection.CreateCommand();
                createCommand.Transaction = transaction;
                createCommand.CommandText = 
                @"INSERT INTO InitializedTable (Name)
                VALUES (@TableName)";
                createCommand.Parameters.Add(new SqliteParameter("@TableName", tableName));
                createCommand.ExecuteNonQuery();

                transaction.Commit();
            }
        }

        public SqliteConnection GetConnection() {
            return _connection;
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

                var createInitTableCommand = _connection.CreateCommand();
                createInitTableCommand.Transaction = transaction;
                createInitTableCommand.CommandText = 
                @"CREATE TABLE InitializedTable (
                    Id INTEGER PRIMARY KEY,
                    Name varchar(50) not null
                )";
                createInitTableCommand.ExecuteNonQuery();

                transaction.Commit();
            }
        }
    }
}