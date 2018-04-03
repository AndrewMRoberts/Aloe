using System;
using System.Collections.Generic;

using Microsoft.Data.Sqlite;

namespace api.DataAccess.Tables
{
    public class AccountTable : Table<Account>
    {
        public readonly string TableName = "Account";

        private SqliteConnection _connection;

        public AccountTable() {
            Initialize();
        }

        public void Initialize() 
        {
            var database = new Database();
            using (_connection = database.GetConnection()) {
                _connection.Open();
                if (!database.IsTableInitialized(TableName)) {
                    using (var transaction = _connection.BeginTransaction()) {
                        var createCommand = _connection.CreateCommand();
                        createCommand.Transaction = transaction;
                        createCommand.CommandText = 
                        @"CREATE TABLE Account (
                            Id INTEGER PRIMARY KEY autoincrement,
                            Name text not null
                        )";
                        createCommand.ExecuteNonQuery();
                        transaction.Commit();

                        database.SetTableInitialized(TableName);
                    }
                }
            }
        }

        public Dictionary<int, Account> Select(Account options = null) 
        {
            options = options ?? new Account();
            var retAccounts = new Dictionary<int, Account>();

            var database = new Database();
            using (_connection = database.GetConnection()) {
                _connection.Open();
                using (var selectCommand = _connection.CreateCommand()) {
                    selectCommand.CommandText = @"SELECT * FROM Account";

                    if (options.HasWhereClause()) {
                        var needAnd = false;
                        selectCommand.CommandText += @" WHERE ";

                        if (options.Id != 0) {
                            AddWhereClauseParameter(selectCommand, 
                                new KeyValuePair<string, string>("Id", options.Id.ToString()), needAnd);
                            needAnd = true;
                        }
                        if (string.IsNullOrWhiteSpace(options.Name)) {
                            AddWhereClauseParameter(selectCommand,
                                new KeyValuePair<string, string>("Name", options.Name), needAnd);
                            needAnd = true;
                        }
                    }

                    using (var reader = selectCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            var obj = new Account {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            retAccounts.Add(obj.Id, obj);
                        }

                        return retAccounts;
                    }
                }
            }
        }

        public void Insert(Account obj) 
        {
            var database = new Database();
            using (_connection = database.GetConnection()) {
                _connection.Open();
                using (var insertCommand = _connection.CreateCommand()) 
                {
                    insertCommand.CommandText = 
                        @"INSERT INTO Account (Name) VALUES (@Name)";

                    insertCommand.Parameters.Add(new SqliteParameter("@Name", obj.Name));

                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public void Remove(int id) 
        {
            var database = new Database();
            using (_connection = database.GetConnection()) {
                _connection.Open();
                using (var deleteCommand = _connection.CreateCommand()) 
                {
                    deleteCommand.CommandText = 
                        @"DELETE FROM Account WHERE Id = @Id";

                    deleteCommand.Parameters.Add(new SqliteParameter("@Id", id));

                    deleteCommand.ExecuteNonQuery();
                }
            }
        }

        private void AddWhereClauseParameter(SqliteCommand cmd, KeyValuePair<string, string> keyValuePair, bool needAnd) 
        {
            if (needAnd) {
                cmd.CommandText += " AND ";
            }
            
            cmd.CommandText += keyValuePair.Key + " = @" + keyValuePair.Key;
            cmd.Parameters.Add(new SqliteParameter("@" + keyValuePair.Key, keyValuePair.Value)); 
        }
    }
}