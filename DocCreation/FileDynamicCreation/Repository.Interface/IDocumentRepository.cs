
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDocumentRepository
    {
        public Task<object> GetAllValues();
    }
}
