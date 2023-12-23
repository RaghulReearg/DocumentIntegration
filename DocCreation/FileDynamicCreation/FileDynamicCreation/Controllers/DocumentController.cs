using BL.Interaface;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using Repository.Implementation;
using Spire.Doc;
using Spire.Pdf;
using Spire.Pdf.General.Find;
using Spire.Pdf.Graphics;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronPdf;

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
        public CommonFactory CommonFactory = new CommonFactory();

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
                return null;
               // var data = new PdfDocument();
               // string htmlContent = "<div style = 'margin: 20px auto; max-width: 600px; padding: 20px; border: 1px solid #ccc; background-color: #FFFFFF; font-family: Arial, sans-serif;' >";
               // htmlContent += "<div style = 'margin-bottom: 20px; text-align: center;'>";
               // htmlContent += "<img src = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROnYPD5QO8ZJvPQt8ClnJNPXduCeX89dSOxA&usqp=CAU' alt = 'School Logo' style = 'max-width: 100px; margin-bottom: 10px;' >";
               // htmlContent += "</div>";
               // //header
               // htmlContent += "<div style = 'text-align: center; margin-bottom: 20px;'>";
               // htmlContent += String.Format("<h2>  {0} </h2>",content.HeaderLabel);
               // htmlContent += String.Format("<p>  {0} </p>", content.HeaderValue);
               // htmlContent += "</div>";

               // // Body
               // htmlContent += "<div style = 'text-align: left; margin-bottom: 20px;'>";
               //// htmlContent += String.Format("<h2>  {0} </h2>", content.ContentLabel);
               //// htmlContent += String.Format("<p>  {0} </p>", content.ContentValue);
               // htmlContent += "</div>";
               // // footer 
               // htmlContent += "<div style = 'text-align: center; margin-bottom: 20px;'>";
               // htmlContent += String.Format("<h2>  {0} </h2>", content.FooterLabel);
               // htmlContent += String.Format("<p>  {0} </p>", content.FooterValue);
               // htmlContent += "</div>";
               // htmlContent += "</div>";
               // PdfGenerator.AddPdfPages(data, htmlContent, PdfSharpCore.PageSize.A4);
               // byte[]? response = null;
               // using (MemoryStream ms = new MemoryStream())
               // {
               //     data.Save(ms);
               //     response = ms.ToArray();
               // }
               // string fileName = "sample" +  ".pdf";
               // return File(response, "application/pdf", fileName);
            }
    
    
            catch(Exception e)
            {
                CommonFactory.WriteToFile("Error Occured : " + e.Message);
                return null;
            }
         }
        [HttpPost]
        public async Task<ActionResult> generateDocument(DocumentContent contents)
        {
            try
            {
                if (contents.DocumentCode != "")
                {
                    //read file from Configuration if Document Code present
                    string documentPath = this.Configuration.GetValue<string>(contents.DocumentCode);
                    //  documentPath =  + documentPath 
                    var filename = Path.GetFileName(documentPath);

                    CommonFactory.WriteToFile("Reading File "+filename +"  -> Code : "+contents.DocumentCode);


                    using (FileStream inputFileStream = new FileStream(Path.GetFullPath(documentPath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        if (inputFileStream != null)
                        {
                            if (contents.DocumentCode == "WORDDOC")
                            {
                                return await this.GenerateMSWordDoc( contents, inputFileStream,filename, documentPath);
                            }
                            else if (contents.DocumentCode == "PDFDOC")
                            {
                                return await this.GeneratePDFDoc(contents, inputFileStream,filename, documentPath);
                            }
                        }
                    }
                    
                }
                else
                {
                    CommonFactory.WriteToFile("Document code cannot be empty");
                    return null;
                   // return "Please Provide the Docuemnt Code";
                }
                return null;
            }
            catch(Exception e)
            {
                CommonFactory.WriteToFile("Error Occured : " + e.Message);
                return null;
               // return e.Message;
            }
        }

        private async Task<ActionResult> GenerateMSWordDoc(DocumentContent contents, FileStream inputFileStream,string filename,string documentPath)
        {
            try {
                //Load the file stream into a Word document.
                using (WordDocument document = new WordDocument(inputFileStream, FormatType.Automatic))
                {



                    //Create the document object;
                   Document worddoc = new Document();

                    //Create a paragraph and add some text
                    worddoc.LoadFromFile(documentPath);
                    //var paragraph = doc.AddSection().AddParagraph();

                    //paragraph.AppendText("Hello World!");
                    foreach (var contentData in contents.ContentFields)
                    {
                        worddoc.Replace(contentData.ContentPlaceholder, contentData.ContentValue, true, true);
                    }

                    //Create and save the document to a memory stream
                    var ms = new MemoryStream();
                    worddoc.SaveToStream(ms, Spire.Doc.FileFormat.Docx);
                    ms.Seek(0, SeekOrigin.Begin);

                    //You may want to make this MIME type string into a constant
                    var file = new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    file.FileDownloadName = filename;

                    return file;

                    //------------------------ OLD Logic------------------------

                    //Get the Word document text.

                    //filename = filename.Split(".")[0];
                    //string text = document.GetText();
                    //text = text.Remove(0, 182);
                    //text = text.Substring(0, text.Length - 182);

                    //foreach (var contentData in contents.ContentFields)
                    //{
                    //    text = text.Replace(contentData.ContentPlaceholder, contentData.ContentValue);
                    //}

                    ////Display Word document's text content.
                    ////create word document
                    //WordDocument documents = new WordDocument();
                    ////Add a section & a paragraph in the empty document
                    //documents.EnsureMinimal();
                    ////Append text to the last paragraph of the document
                    //documents.LastParagraph.AppendText(text);
                    ////Save and close the Word document
                    ////  MemoryStream stream = new MemoryStream();

                    ////document.Open(inputFileStream, FormatType.Docx);
                    ////inputFileStream.Dispose();
                    //////inputFileStream all occurrences of a misspelled word and replaces with properly spelled word.
                    ////document.Replace("Cyles", "Cycles", true, true);

                    //byte[]? response = null;
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    documents.Save(ms, FormatType.Docx);
                    //    response = ms.ToArray();
                    //}
                    //string fileName = filename+ ".docx";
                    //return File(response, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
                }

            }
            catch(Exception e)
            {
                CommonFactory.WriteToFile("Error Occured : " + e.Message);
                return null;
            }
        }

        private async Task<ActionResult> GeneratePDFDoc(DocumentContent content, FileStream inputFileStream, string filename,string documentPath)
        {

            try
            {

                Spire.Pdf.PdfDocument pdfdoc = new Spire.Pdf.PdfDocument();
                pdfdoc.LoadFromFile(documentPath);
                pdfdoc.SaveToFile("Document\\PDFTOWORD\\todoc.docx", Spire.Pdf.FileFormat.DOCX);
                Spire.Doc.Document worddoc = new Spire.Doc.Document();

                //Create a paragraph and add some text
                worddoc.LoadFromFile(this.Configuration.GetValue<string>("WORDTOPDF"));
                //var paragraph = doc.AddSection().AddParagraph();

                //paragraph.AppendText("Hello World!");
                foreach (var contentData in content.ContentFields)
                {
                    worddoc.Replace(contentData.ContentPlaceholder, contentData.ContentValue, true, true);
                }

                // PdfPageBase page = pdfdoc.Pages[0];



                //foreach (var contentData in content.ContentFields)
                //{
                //    PdfTextFindCollection collection = page.FindText(contentData.ContentPlaceholder, TextFindParameter.IgnoreCase);
                //    PdfBrush brush = new PdfSolidBrush(Color.Black);
                //    PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("Arial", 12f, FontStyle.Regular));
                //    RectangleF rec;
                //    foreach (PdfTextFind find in collection.Finds)
                //    {

                //        rec = find.Bounds;
                //        page.Canvas.DrawRectangle(PdfBrushes.White, rec);

                //        page.Canvas.DrawString(contentData.ContentValue, font, brush, rec);

                //    }


                //}
                var ms = new MemoryStream();
                worddoc.SaveToStream(ms, Spire.Doc.FileFormat.PDF);
                ms.Seek(0, SeekOrigin.Begin);

                //You may want to make this MIME type string into a constant
                var file = new FileStreamResult(ms, "application/pdf");
                file.FileDownloadName = filename;

                return file;
                
            }


            catch (Exception e)
            {
                return null;
            }
            return null;
        }
    }
}
