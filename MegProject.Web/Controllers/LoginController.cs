using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
//using MegProject.Business.Manager.UserAppService;
using MegProject.Dto.CustomDto.ViewModels;
using MegProject.Web.Auth;
using MegProject.Web.Base;
using Ninject;

namespace MegProject.Web.Controllers
{
    //[AllowAnonymous]
    //public class LoginController : BaseController
    //{

    //    //private readonly IUserApp _userApp;

    //    //public LoginController(IUserApp userApp)
    //    //{
    //    //    _userApp = userApp;
    //    //}


    //    //// GET: Login
    //    //public ActionResult Index()
    //    //{
    //    //    LoginViewModel model = new LoginViewModel();
    //    //    return View(model);
    //    //}

    //    //[HttpPost]
    //    //[ValidateForgeryTokenCore]
    //    //[ActionName("Index")]
    //    //public ActionResult Index(LoginViewModel login)
    //    //{
    //    //    #region Validate User 

    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        if (String.IsNullOrEmpty(login.Email) || String.IsNullOrEmpty(login.Password))
    //    //        {
    //    //            ModelState.AddModelError("Error", "Email ve Şifre Alanı Boş Olamaz!");
    //    //        }

    //    //        var user = _userApp.GetUser(login.Email, login.Password);
    //    //        if (user == null)
    //    //        {
    //    //            ModelState.AddModelError("Error", "Email veya şifre hatalı!");
    //    //            return View(login);
    //    //        }
    //    //        else
    //    //        {

    //    //            var SerializeModel = new SerializeLoginModel()
    //    //            {
    //    //                Id = user.Id,
    //    //                UserName = user.UserName,
    //    //                Email = user.Email
    //    //            };


    //    //            var serializer = new JavaScriptSerializer();
    //    //            var userData = serializer.Serialize(SerializeModel);
    //    //            var authTicket = new FormsAuthenticationTicket(
    //    //                1,
    //    //                user.Email,
    //    //                DateTime.Now,
    //    //                DateTime.Now.AddMinutes(15),
    //    //                login.RememberMe,
    //    //                userData);
    //    //            int timeout = login.RememberMe ? 525600 : 30; // Timeout in minutes, 525600 = 365 days.
    //    //            var encTicket = FormsAuthentication.Encrypt(authTicket);
    //    //            var faCookie = new HttpCookie(
    //    //                FormsAuthentication.FormsCookieName, encTicket);

    //    //            faCookie.Expires = System.DateTime.Now.AddMinutes(timeout);// Not my line
    //    //            faCookie.HttpOnly = true; // cookie not available in javascript.
    //    //            Response.Cookies.Add(faCookie);

    //    //            return RedirectToAction("Index", "Home");
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        ModelState.AddModelError("Error", "Login Model Doğru Tanımlanmamış!");
    //    //        return View(login);
    //    //    }

    //    //    #endregion

    //    //}


    //    //public ActionResult Logout()
    //    //{
    //    //    FormsAuthentication.SignOut();
    //    //    return RedirectToAction("Index", "Login");
    //    //}


    //}
}