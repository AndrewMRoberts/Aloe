using System;

namespace api.DataAccess
{
    public class Overview  : DatabaseObject
    {
        public int Id {get;set;}
        public string Account {get;set;}
        public string Category {get;set;}
        public decimal Amount {get; set;}

        public bool HasWhereClause() 
        {
            return Amount != 0 ||  string.IsNullOrEmpty(Account)|| string.IsNullOrEmpty(Category);
        }
    }
}