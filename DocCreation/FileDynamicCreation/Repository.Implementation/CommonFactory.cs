using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class CommonFactory
    {
        public readonly object _locker = new object();
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {

                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    lock (_locker)
                    {
                        sw.WriteLine("-----------------------------------------");
                        sw.WriteLine(DateTime.Now.ToString() + "--->" + Message);
                        sw.WriteLine("-----------------------------------------");

                    }

                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    lock (_locker)
                    {
                        sw.WriteLine("-----------------------------------------");
                        sw.WriteLine(DateTime.Now.ToString() + "--->" + Message);
                        sw.WriteLine("-----------------------------------------");
                    }

                    sw.Flush();
                    sw.Close();
                }
            }
        }

        public readonly object _lockerPR = new object();
        public void WriteToFile(string Message, int processId)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "\\" + processId + "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "\\" + processId + "\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {

                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    lock (_lockerPR)
                    {
                        sw.WriteLine("-----------------------------------------");
                        sw.WriteLine(DateTime.Now.ToString() + "--->" + Message);
                        sw.WriteLine("-----------------------------------------");

                    }

                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    lock (_lockerPR)
                    {
                        sw.WriteLine("-----------------------------------------");
                        sw.WriteLine(DateTime.Now.ToString() + "--->" + Message);
                        sw.WriteLine("-----------------------------------------");
                    }

                    sw.Flush();
                    sw.Close();
                }
            }
        }

        public readonly object _lockerFR = new object();
        public void WriteFailedRecords(string Message, int processId)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "\\" + processId + "\\FailedRecords" + "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "\\" + processId + "\\FailedRecords\\FailedRecords_" + DateTime.Now.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {

                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    lock (_lockerFR)
                    {
                        sw.WriteLine("-----------------------------------------");
                        sw.WriteLine(DateTime.Now.ToString() + "--->" + Message);
                        sw.WriteLine("-----------------------------------------");

                    }

                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    lock (_lockerFR)
                    {
                        sw.WriteLine("-----------------------------------------");
                        sw.WriteLine(DateTime.Now.ToString() + "--->" + Message);
                        sw.WriteLine("-----------------------------------------");
                    }

                    sw.Flush();
                    sw.Close();
                }
            }
        }

        //public string GetLocalDate(string date)
        //{
        //    string result = string.Empty;
        //    if (!string.IsNullOrEmpty(date) && ConfigurationManager.AppSettings["DateFormat"] != null)
        //    {
        //        string dateFormat = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
        //        if (dateFormat == "MM/dd/yyyy")
        //            result = date;
        //        else if (dateFormat == "dd/MM/yyyy")
        //            result = string.Format("{0}/{1}/{2}", date.Split('/')[1], date.Split('/')[0], date.Split('/')[2]);

        //    }
        //    return result;
        //}

    }
}




