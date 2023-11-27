using System;
using Microsoft.Extensions.Configuration;
public class DapperDBContext
{
	 IConfiguration Configuration { get; }
	private readonly string connectionstring
	public DapperDBContext(IConfiguration configuration)
	{
		this.Configuration = configuration;
		this.connectionstring= this.Configuration.GetConnectionString("DEVConnection")
	}
}
