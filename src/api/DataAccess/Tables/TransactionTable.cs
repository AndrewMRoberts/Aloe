using System;
using System.Collections.Generic;

using Microsoft.Data.Sqlite;

namespace api.DataAccess.Tables
{
    public class TransactionTable : Table<Transaction>
    {
        public static readonly string TableName = "AloeTransaction";

        private SqliteConnection _connection;

        public TransactionTable() {
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
                        @"CREATE TABLE " + TableName + @" (
                            Id INTEGER PRIMARY KEY autoincrement,
                            Description text not null,
                            Amount Decimal(18,2) not null,
                            TransactionDate datetime not null,
                            CategoryId int references Category(Id),
                            AccountId int references Account(Id) not null                      
                        )";
                        createCommand.ExecuteNonQuery();
                        transaction.Commit();

                        database.SetTableInitialized(TableName);
                    }
                }
            }
        }

        public Dictionary<int, Transaction> Select(Transaction options = null) 
        {
            options = options ?? new Transaction();
            var retAccounts = new Dictionary<int, Transaction>();

            var database = new Database();
            using (_connection = database.GetConnection()) {
                _connection.Open();
                using (var selectCommand = _connection.CreateCommand()) {
                    selectCommand.CommandText = 
                    @"SELECT 
                        Id,
                        Description,
                        Amount,
                        CategoryId,
                        AccountId,
                        TransactionDate
                     FROM " + TableName;

                    if (options.HasWhereClause()) {
                        var needAnd = false;
                        selectCommand.CommandText += @" WHERE ";

                        if (options.Id != 0) {
                            AddWhereClauseParameter(selectCommand, 
                                new KeyValuePair<string, string>("Id", options.Id.ToString()), needAnd);
                            needAnd = true;
                        }
                        if (string.IsNullOrWhiteSpace(options.Description)) {
                            AddWhereClauseParameter(selectCommand,
                                new KeyValuePair<string, string>("Description", options.Description), needAnd);
                            needAnd = true;
                        }
                        if (options.Amount != 0) {
                            AddWhereClauseParameter(selectCommand,
                                new KeyValuePair<string, string>("Amount", options.Amount.ToString()), needAnd);
                            needAnd = true;
                        }
                        if (options.CategoryId != 0) {
                            AddWhereClauseParameter(selectCommand,
                                new KeyValuePair<string, string>("CategoryId", options.CategoryId.ToString()), needAnd);
                            needAnd = true;
                        }
                        if (options.AccountId != 0) {
                            AddWhereClauseParameter(selectCommand,
                                new KeyValuePair<string, string>("AccountId", options.AccountId.ToString()), needAnd);
                            needAnd = true;
                        }
                        if (!options.TransactionDate.Equals(DateTime.MinValue)) {
                            AddWhereClauseParameter(selectCommand,
                                new KeyValuePair<string, string>("TransactionDate", options.TransactionDate.ToString()), needAnd);
                            needAnd = true;
                        }
                    }

                    using (var reader = selectCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            var obj = new Transaction {
                                Id = reader.GetInt32(0),
                                Description = reader.GetString(1),
                                Amount = reader.GetDecimal(2),
                                CategoryId = reader.GetInt32(3),
                                AccountId = reader.GetInt32(4),
                                TransactionDate = reader.GetDateTime(5)
                            };
                            retAccounts.Add(obj.Id, obj);
                        }

                        return retAccounts;
                    }
                }
            }
        }

        public void Insert(Transaction transaction) {
            var database = new Database();
            using (_connection = database.GetConnection()) {
                _connection.Open();
                using (var insertCommand = _connection.CreateCommand()) {
                    insertCommand.CommandText = 
                    @"INSERT INTO " + TableName + @" (
                        Description,
                        Amount,
                        TransactionDate,
                        CategoryId,
                        AccountId
                    ) VALUES (
                        @Description,
                        @Amount,
                        @TransactionDate,
                        @CategoryId,
                        @AccountId
                    )";
                    insertCommand.Parameters.Add(new SqliteParameter("@Description", transaction.Description));
                    insertCommand.Parameters.Add(new SqliteParameter("@Amount", transaction.Amount));
                    insertCommand.Parameters.Add(new SqliteParameter("@TransactionDate", transaction.TransactionDate));
                    insertCommand.Parameters.Add(new SqliteParameter("@CategoryId", transaction.CategoryId));
                    insertCommand.Parameters.Add(new SqliteParameter("@AccountId", transaction.AccountId));

                    insertCommand.ExecuteNonQuery();
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