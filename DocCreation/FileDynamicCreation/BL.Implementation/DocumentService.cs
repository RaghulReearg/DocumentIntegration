using BL.Interaface;
using Repository.Interface;
using System;
using System.Threading.Tasks;

namespace BL.Implementation
{
    public class DocumentService: IDocumentService
    {
        IDocumentRepository _IdocumentRepository;
        public DocumentService(IDocumentRepository IdocumentRepository)
        {
            this._IdocumentRepository = IdocumentRepository;
        }
        public Task<object> GetAllValues()
        {
            return this._IdocumentRepository.GetAllValues();
        }
    }
}
