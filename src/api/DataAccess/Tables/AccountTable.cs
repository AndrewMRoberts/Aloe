using System;
using System.Collections.Generic;

using Microsoft.Data.Sqlite;

namespace api.DataAccess.Tables
{
    public class AccountTable : Table<Account>
    {
        public readonly string TableName = "Account";

        private SqliteConnection _connection;

        public void Initialize() 
        {
            var database = new Database();
            _connection = database.GetConnection();

            if (!database.IsTableInitialized(TableName)) {
                using (var transaction = _connection.BeginTransaction()) {
                    var createCommand = _connection.CreateCommand();
                    createCommand.Transaction = transaction;
                    createCommand.CommandText = 
                    @"CREATE TABLE Account (
                        Id int primary key AUTOINCREMENT not null,
                        Name varchar(50) not null
                    )";
                    createCommand.ExecuteNonQuery();

                    database.SetTableInitialized(TableName);
                }
            }
        }

        public Dictionary<int, Account> Select(Account options = null) 
        {
            var retAccounts = new Dictionary<int, Account>();

            using (var transaction = _connection.BeginTransaction()) 
            {
                var selectCommand = _connection.CreateCommand();
                selectCommand.Transaction = transaction;
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

                var reader = selectCommand.ExecuteReader();
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

        public void Insert(Account obj) 
        {
             using (var transaction = _connection.BeginTransaction()) 
            {
                var insertCommand = _connection.CreateCommand();
                insertCommand.Transaction = transaction;
                insertCommand.CommandText = 
                    @"INSERT (Id, Name) INTO Account VALUES (@Id, @Name)";

                insertCommand.Parameters.Add(new SqliteParameter("@Id", obj.Id));
                insertCommand.Parameters.Add(new SqliteParameter("@Name", obj.Name));

                insertCommand.ExecuteNonQuery();
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