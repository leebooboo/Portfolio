using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using AspNet.BoardGameMall.Models;
using Microsoft.AspNet.Identity.Owin;

namespace AspNet.BoardGameMall.Utils
{
    public class Util
    {
        public static string GetIpAddress(HttpRequestBase request)
        {
            string ipAddress = string.Empty;
            try
            {
                ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    return ipAddress.Split(',')[0];
                }
                //if (ipaddress == "" || ipaddress == null)
                // ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                return request.ServerVariables["REMOTE_ADDR"];
            }
            catch
            { }
            return ipAddress;
        }

        public static string GetProtectedUserName(IPrincipal user)
        {
            if (user.IsInRole("Admin"))
            {
                return user.Identity.GetUserName();
            }

            string userName = user.Identity.GetUserName();

            if (!userName.Contains("@"))
            {
                return null;
            }

            string id = userName.Split('@')[0];
            string domain = userName.Split('@')[1];
            string protectedId = string.Empty;

            if (id.Length >= 5)
            {
                protectedId = id.Substring(0, 4) + "****";
            }
            else if (id.Length == 1)
            {
                protectedId = id + "*****";
            }
            else
            {
                protectedId = id.Substring(0, 2) + "****";
            }

            return $"{protectedId}@{domain}";
        }

        public static Dictionary<string, int> GetPageAndPageSize_FromPreviewPage(HttpRequestBase request)
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();

            if (request.UrlReferrer != null && request.UrlReferrer.Query.Length >= 1)
            {
                string queryString = request.UrlReferrer.Query;
                string[] queryStrings = queryString.Substring(1).Split('&');
                foreach (string query in queryStrings)
                {
                    if (query.ToLower().Contains("page="))
                    {
                        int page;
                        if (!Int32.TryParse(query.Split('=')[1], out page))
                            page = 1;
                        keyValuePairs.Add("Page", page);
                        continue;
                    }

                    if (query.ToLower().Contains("pagesize="))
                    {
                        int pageSize;
                        if (!Int32.TryParse(query.Split('=')[1], out pageSize))
                            pageSize = 10;
                        keyValuePairs.Add("PageSize", pageSize);
                        continue;
                    }
                }
            }

            if(!keyValuePairs.ContainsKey("Page"))
                keyValuePairs.Add("Page", 1);

            if (!keyValuePairs.ContainsKey("PageSize"))
                keyValuePairs.Add("PageSize", 10);

            return keyValuePairs;
        }
    }
}