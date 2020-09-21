using System.Web.Mvc;
using BL;
using DB.ViewModels;

namespace Mailroom.Controllers
{
    public class LoginController : Controller
    {
        const string USERNAME = "username";
        const string PASSWORD = "password";
        // GET: Login

        [HttpPost]
        public JsonResult Do(string user, string password)
        {
            BLSeguridad blSeguridad = new BLSeguridad();

            GenericResponse<LoginResponse> login = blSeguridad.CheckUserPass(user, password);

            Session.Add(USERNAME, login.Result.username);
            Session.Add(PASSWORD, login.Result.password);

            return Json(login);
        }

        [HttpPost]
        public JsonResult GetUsername()
        {
            return Json(Helper.SecurityHelper.GetLogin(Session));
        }
    }
}