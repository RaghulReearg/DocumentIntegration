using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Implementation
{
	public class DapperDBContext
	{
		private IConfiguration configuration { get; }
		private readonly string connectionstring;
		public DapperDBContext(IConfiguration _configuration)
		{
			this.configuration = _configuration;
			this.connectionstring = configuration.GetConnectionString("DEVConnection");
		}
		public IDbConnection CreateConnection() => new SqlConnection(connectionstring);

	}
}
