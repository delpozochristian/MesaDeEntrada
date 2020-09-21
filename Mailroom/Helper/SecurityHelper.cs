using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using DB.ViewModels;

namespace Mailroom.Helper
{
    public class SecurityHelper
    {
        public static bool CheckLogin(HttpSessionStateBase session)
        {
            var username = "";
            var password = "";

            if (session["username"] != null)
                username = session["username"].ToString();

            if (session["password"] != null)
                password = session["password"].ToString();

            if (username == "" && password == "")
                return false;

            GenericResponse<LoginResponse> response = new BLSeguridad().CheckUserPass(username, password);

            if (response.Code == 200)
            {
                return response.Result.Estado;
            }

            return false;
        }

        public static LoginResponse GetLogin(HttpSessionStateBase session)
        {
            GenericResponse<LoginResponse> response = new GenericResponse<LoginResponse>();

            var username = "";
            var password = "";

            if (session["username"] != null)
                username = session["username"].ToString();

            if (session["password"] != null)
                password = session["password"].ToString();

            response = new BLSeguridad().CheckUserPass(username, password);
            
            return response.Result;
        }
    }
}