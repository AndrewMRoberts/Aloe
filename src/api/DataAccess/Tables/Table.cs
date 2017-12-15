using System;
using System.Collections;
using System.Collections.Generic;

namespace api.DataAccess.Tables
{
    public interface Table<T>
    {
        void Initialize();
        Dictionary<int, T> Select(T options);
    }    
}