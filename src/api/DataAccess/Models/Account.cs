using System;

namespace api.DataAccess
{
    public class Account : DatabaseObject
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public bool IsCredit {get; set;}

        public bool HasWhereClause() 
        {
            return Id != 0 || !string.IsNullOrWhiteSpace(Name);
        }
    }
}