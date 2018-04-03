using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace api.DataAccess.Tables
{
    public class CategoryTable : Table<Category>
    {
        public readonly string TableName = "Category";

        private SqliteConnection _connection;

        public CategoryTable() {
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
                        @"CREATE TABLE Category (
                            Id INTEGER PRIMARY KEY autoincrement,
                            Description text not null
                        )";
                        createCommand.ExecuteNonQuery();
                        transaction.Commit();

                        database.SetTableInitialized(TableName);
                    }
                }
            }
        }

        public void Close() {
            throw new NotImplementedException();
        }

        public bool IsInitialized() 
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, Category> Select(Category options) 
        {
            throw new NotImplementedException();
        }

        public void Insert(Category category) {
            throw new NotImplementedException();
        }
    }
}