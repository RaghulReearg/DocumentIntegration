using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Implementation
{
    public interface IApplicationConnectionString
    {
        string DEVConnection { get; set; }
    }
    public class ApplicationConnectionString: IApplicationConnectionString
    {
        public string DEVConnection { get; set; }
    }
    public class UnityOfWork: IUnitOfWork
    {
        
        private IOptions<ApplicationConnectionString> sqlConnection;
        public IConfiguration Configuration { get; }

        public UnityOfWork(IOptions<ApplicationConnectionString> options, IConfiguration configuration)
        {
            this.sqlConnection = options;
            this.Configuration = configuration;
        }
        public DataSet DatasetQuery(string query, params object[] parameters)
        {
            // System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(sqlConnection.Value.DEVConnection);
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Configuration.GetConnectionString("DEVConnection"));

            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(query);
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }

           // System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(sqlConnection.Value.DEVConnection);
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            sda.SelectCommand = cmd;
            cmd.CommandTimeout = 100;
            sda.Fill(ds);
            con.Close();

            return ds;
        }
    }
}
