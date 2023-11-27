using BL.Interaface;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDynamicCreation.Controllers
{
    
    
     [Route("api/[controller]/[action]")]
     [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        //ILogger<WeatherForecastController> logger
        //public DocumentController()
        //{
        //   // _logger = logger;
        //}

        public IConfiguration Configuration { get; }
        public IDocumentService _documentService;
        public DocumentController(IConfiguration _configuration, IDocumentService documentService, ILogger<DocumentController> logger)
        {
            this.Configuration = _configuration;
            this._logger = logger;
            this._documentService = documentService;
        }

       
        public Task<object> GetAllValues()
        {
            try
            {
                return _documentService.GetAllValues();
            }
            catch(Exception w)
            {
                this._logger.LogError(w.Message);
            }
            return null;
        }

    }
}
