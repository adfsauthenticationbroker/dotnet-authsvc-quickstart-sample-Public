﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Web.Mvc;
using System.IO;

using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace ClaimAppDemo01
{
    public partial class login : System.Web.UI.Page
    {
        public string _URL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _URL = formQueryString();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string formQueryString()
        {
            string state = RandomString(20); // your application state
            string nonce = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US")) + "." + state; // date time in GMT+8 (timezone)
            string returnUri = "http://localhost:8080/postback.aspx"; // your return url
            string tenantId = "f98188a6-88cb-4663-a2b4-46e4335969dc"; // tenant id "31cd39c5-af74-42c0-ae10-bef29ed6bffa"  
            string url = "https://localhost:8111/api/users/login"; // auth svc url //string url = "https://mha_aws_auth_adfs_nprd.htd.gov.sg:8111/api/users/login"; // auth svc url

            string urlString = url + "?tenantid=" + Base64Encode(tenantId) + "&redirecturi=" + Base64Encode(returnUri) + "&nonce=" + Base64Encode(nonce) + "&state=" + Base64Encode(state);
            return urlString;
        }
    }
}
