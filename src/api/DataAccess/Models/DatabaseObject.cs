using System;

namespace api.DataAccess
{
    public interface DatabaseObject 
    {
        bool HasWhereClause();
    }
}