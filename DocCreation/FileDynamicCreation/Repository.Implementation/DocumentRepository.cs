using DTO;
using Repository.Interface;
using System;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class DocumentRepository: IDocumentRepository
    {
        private IUnitOfWork _unitOfWork;
        private DapperDBContext _dBContext;
        public DocumentRepository(IUnitOfWork unitOfWork, DapperDBContext dBContext)
        {
            this._unitOfWork=unitOfWork;
            this._dBContext = dBContext;
        }
        public async Task<object> CreatePDFDoc()
        {
            string query = "select * from TB_FILE_TYPE_MST";
            using(var connection = _dBContext.CreateConnection())
            {
                var list = await connection.QueryAsync<DocTypeModel>(query);
                return list.ToList();
            }
           
            // return null;
        }

    }
}
