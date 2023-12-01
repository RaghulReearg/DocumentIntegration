using System;
using System.Collections.Generic;
using System.Text;


namespace DTO
{
	public class DocTypeModel
	{
		public int Id { get; set; }
		public string FileType { get; set; }
		public string Extension { get; set; }
		public DateTime? CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }


	}
	public class DocumentContent
    {
		public string HeaderLabel { get; set; }
        public string HeaderValue { get; set; }
        public string ContentLabel { get; set; }
        public string ContentValue { get; set; }
        public string FooterLabel { get; set; }

        public string FooterValue { get; set; }



    }

}