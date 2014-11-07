using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.Net;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Dynamic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace BitsharesAPI.Service
{
    public class BitsharesClientService
    {
        public string GetCommandResponse(string command, string paramString)
        {
            string url = "http://localhost:9989/rpc";
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            	
            String username = "user";
            String password = "pass";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            webReq.Headers.Add("Authorization", "Basic " + encoded); 

            webReq.Accept = "application/json-rpc";
            webReq.Method = "POST";

            JArray paramAry = new JArray();

            if (!String.IsNullOrEmpty(paramString))
            {
                string[] paramStringArray = paramString.Split(' ');
                foreach (var param in paramStringArray)
                {
                    paramAry.Add(param);
                }
            }

            JObject postReq = new JObject();
            postReq["jsonrpc"] = 2.0;
            postReq["id"] = 0;
            postReq["method"] = command;
            postReq["params"] = paramAry;

            string s = JsonConvert.SerializeObject(postReq);
            byte[] contentByteArray = Encoding.UTF8.GetBytes(s);
            webReq.ContentLength = contentByteArray.Length;

            try
            {
                using (Stream dataStream = webReq.GetRequestStream())
                {
                    dataStream.Write(contentByteArray, 0, contentByteArray.Length);
                }
            }
            catch (WebException dsException)
            {
                return dsException.Message;
            }

            string responseText = String.Empty;
            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                responseText = sr.ReadToEnd();
            }
            catch (WebException wrException)
            {
                responseText = wrException.Message;
            }

            return responseText;
        }
    }
}