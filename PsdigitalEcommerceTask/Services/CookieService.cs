using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsdigitalEcommerceTask.Services
{
    public static class CookieService
    {
        public static void Save(string key , string value ,HttpContextBase context)
        {
            HttpCookie _Cookie = new HttpCookie(key, value);
            _Cookie.Expires = DateTime.Now.AddMonths(1);
            context.Response.Cookies.Add(_Cookie);
            
        }
    }
}