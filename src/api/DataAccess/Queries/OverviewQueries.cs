using System;
using System.Collections.Generic;

namespace api.DataAccess
{
    public class OverviewQueries
    {

        public ICollection<Overview> SelectOverview() 
        {
            if (CheckDependencies()) 
            {
                var database = new Database();
                using (var connection = database.GetConnection()) {
                    connection.Open();
                    using (var command = connection.CreateCommand()) {
                        command.CommandText = string.Format(@"
                            SELECT
                                SUM(t.Amount) Amount,
                                MAX(c.Description) Category,
                                MAX(a.Name) Account
                            FROM
                                {0} t
                                JOIN {1} a
                                    on t.AccountId = a.Id
                                LEFT JOIN {2} c
                                    on t.CategoryId = c.Id
                            GROUP BY
                                a.Id,
                                c.Id
                        ",Tables.TransactionTable.TableName
                        ,Tables.AccountTable.TableName
                        ,Tables.CategoryTable.TableName);

                        var overviews = new List<Overview>();
                        var id = 0;

                        using (var reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                var obj = new Overview {
                                    Id = id++,
                                    Amount = reader.GetDecimal(0),
                                    Category = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                    Account = reader.GetString(2)
                                };
                                overviews.Add(obj);
                            }

                            return overviews;
                        }   
                    }
                }
            }

            return null;
        }

        #region Private Methods

        private bool CheckDependencies()
        {
            var database = new Database();
            return 
                database.IsDatabaseInitialized()
                && database.IsTableInitialized(Tables.AccountTable.TableName)
                && database.IsTableInitialized(Tables.CategoryTable.TableName)
                && database.IsTableInitialized(Tables.TransactionTable.TableName)
            ;
        }

        #endregion
        
    }
}