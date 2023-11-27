using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository.Interface
{
    public interface IUnitOfWork
    {
        DataSet DatasetQuery(string query, params object[] parameters);
    }
}
