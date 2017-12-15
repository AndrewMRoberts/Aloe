using System;

namespace api.DataAccess
{
    public class Transaction 
    {
        public DateTime TransactionDate {get;set;}
        public int AccountId {get;set;}
        public string Description {get;set;}
        public int CategoryId {get;set;}
        public decimal Amount {get; set;}
    }
}