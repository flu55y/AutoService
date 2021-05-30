using AutoService.Models;
using AutoService.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutoService.Models
{
    public static class Tool
    {
        public async static Task<Owner> GetOwner(HttpRequestBase request, AutoServiceContext db) //Tool - управление куки в прроекте при заходе и рег аккаунтов, куда стандартных примеров в гугле
        {
            var loginCookie = request.Cookies["login"];
            var signCookie = request.Cookies["sign"];

            if (loginCookie == null || signCookie == null)
                return null;

            var login = loginCookie.Value;
            var sign = signCookie.Value;

            Owner owner;
            try { owner = await db.Owners.FirstAsync(u => u.Login == login); }
            catch { return null; }

            if (SignGenerator.GenerateSign(owner.Password) == sign)
                return owner;

            return null;
        }

        internal static void AddLoginCookie(HttpResponseBase response, string login, string password)
        {
            var loginCookie = new HttpCookie("login", login) { Expires = DateTime.Now.AddDays(60) };
            var signCookie = new HttpCookie("sign", SignGenerator.GenerateSign(password)) { Expires = DateTime.Now.AddDays(60) };
            response.Cookies.Add(loginCookie);
            response.Cookies.Add(signCookie);
        }

        internal async static Task<bool> UserExists(string login, AutoServiceContext db)
        {
            try
            {
                var user = await db.Owners.FirstAsync(u => u.Login == login);
                return true;
            }
            catch { }

            return false;
        }

        public static void ClearLoginCookie(HttpResponseBase response)
        {
            var loginCookie = new HttpCookie("login", "") { Expires = DateTime.Now.AddDays(-60) };
            var signCookie = new HttpCookie("sign", "") { Expires = DateTime.Now.AddDays(-60) };
            response.Cookies.Add(loginCookie);
            response.Cookies.Add(signCookie);
        }
    }
}