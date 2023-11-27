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

}