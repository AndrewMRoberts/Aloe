using System;

namespace api.DataAccess
{
    public class Transaction  : DatabaseObject
    {
        public int Id {get;set;}
        public string Description {get;set;}
        public decimal Amount {get; set;}
        public DateTime TransactionDate {get;set;}
        public int AccountId {get;set;}
        public int CategoryId {get;set;}

        public bool HasWhereClause() 
        {
            return Id != 0 || !string.IsNullOrWhiteSpace(Description) || Amount != 0 || !TransactionDate.Equals(DateTime.MinValue)
                || AccountId != 0 || CategoryId != 0;
        }
    }
}