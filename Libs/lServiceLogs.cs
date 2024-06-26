﻿
using sky.recovery.Controllers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sky.recovery.Libs
{
    public class lServiceLogs
    {
        private BaseController bc = new BaseController();
        private lDbConn dbconn = new lDbConn();
        public void ServiceRecordLogs(string method, string actionpath, string requestform, string result)
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skylog");

            var _path = Path.GetFullPath("Logs");
            string urlPath = "";
            string apimodule = "";

            apimodule = GetApiModule(actionpath);
            urlPath = GetUrlApi(apimodule, actionpath);

            var today = DateTime.Now.ToString("yyyy-MM-dd");
            string filename = _path + "/" + apimodule + "_" + today + ".txt";

            var message = " Method : " + method + "; " + "url : " + urlPath + "; request form : " + requestform + " ; result : " + result;

            string spname = "insert_service_log";
            string p1 = "p_module;" + apimodule + ";s";
            string p2 = "p_date;" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ";s";
            string p3 = "p_method;" + method + ";s";
            string p4 = "p_url;" + urlPath + ";s";
            string p5 = "p_reqform;" + (requestform).Replace("\r\n", "") + ";s";
            string p6 = "p_result;" + (result).Replace("\r\n","") + ";s";


            var retObject = new List<dynamic>();
            bc.execSqlWithSplitSemicolon(provider, cstrname, spname, p1,p2,p3, p4, p5, p6);
        }

        public string GetApiModule(string path)
        {
            string strResult = "";
            var _apiPath = path.Split("/");
            if (_apiPath.Length > 2)
            {
                strResult = _apiPath[1].ToString();
            }
            return strResult;
        }

        public string GetUrlApi(string apimodule, string actionpath)
        {
            string strDomain = "";
            string urlPath = "";

            strDomain = GetDomainApi(apimodule);
            var _domain = strDomain.Split("/");
            if (_domain.Length > 3)
            {
                urlPath = _domain[0] + "//" + _domain[2] + actionpath;
            }
            else
            {
                urlPath = actionpath;
            }

            return urlPath;
        }

        public string GetDomainApi(string apimodule)
        {
            string strResult = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            strResult = config.GetSection("APISettings:urlAPI_" + apimodule + "").Value.ToString();

            return strResult;
        }        
    }
}
