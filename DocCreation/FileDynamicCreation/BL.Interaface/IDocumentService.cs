using System;
using System.Threading.Tasks;

namespace BL.Interaface
{
    public interface IDocumentService
    {
        public Task<object> GetAllValues();

    }
}
