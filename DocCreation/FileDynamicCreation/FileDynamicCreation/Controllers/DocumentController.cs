﻿using BL.Interaface;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;

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

        [HttpPost]

        public Task<ActionResult> CreatePDFDoc(DocumentContent content)
        {
            try
            {
                
                var data =this.CreateDocument(content);
                return data;
               // return _documentService.GetAllValues();
            }
            catch(Exception w)
            {
                this._logger.LogError(w.Message);
            }
            return null;
        }

        //Create document method
        private async Task<ActionResult> CreateDocument(DocumentContent content)
        {
            try
            {
                var data = new PdfDocument();
                string htmlContent = "<div style = 'margin: 20px auto; max-width: 600px; padding: 20px; border: 1px solid #ccc; background-color: #FFFFFF; font-family: Arial, sans-serif;' >";
                htmlContent += "<div style = 'margin-bottom: 20px; text-align: center;'>";
                htmlContent += "<img src = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROnYPD5QO8ZJvPQt8ClnJNPXduCeX89dSOxA&usqp=CAU' alt = 'School Logo' style = 'max-width: 100px; margin-bottom: 10px;' >";
                htmlContent += "</div>";
                //header
                htmlContent += "<div style = 'text-align: center; margin-bottom: 20px;'>";
                htmlContent += String.Format("<h2>  {0} </h2>",content.HeaderLabel);
                htmlContent += String.Format("<p>  {0} </p>", content.HeaderValue);
                htmlContent += "</div>";

                // Body
                htmlContent += "<div style = 'text-align: left; margin-bottom: 20px;'>";
                htmlContent += String.Format("<h2>  {0} </h2>", content.ContentLabel);
                htmlContent += String.Format("<p>  {0} </p>", content.ContentValue);
                htmlContent += "</div>";
                // footer 
                htmlContent += "<div style = 'text-align: center; margin-bottom: 20px;'>";
                htmlContent += String.Format("<h2>  {0} </h2>", content.FooterLabel);
                htmlContent += String.Format("<p>  {0} </p>", content.FooterValue);
                htmlContent += "</div>";
                htmlContent += "</div>";
                PdfGenerator.AddPdfPages(data, htmlContent, PageSize.A4);
                byte[]? response = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    data.Save(ms);
                    response = ms.ToArray();
                }
                string fileName = "sample" +  ".pdf";
                return File(response, "application/pdf", fileName);
            }
    
    
            catch(Exception e)
            {
                return null;
            }
         }

      }
}
