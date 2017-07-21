using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Domain.Manager;
using CEA_EDU.Web.Models;
using CEA_EDU.Web.Utils;
using Newtonsoft.Json;

namespace CEA_EDU.Web.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public string CheckHealth()
        {
            return "1";
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Session[SessionHelper.CurrentUserKey] = null;
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUserViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Code == "sa" && model.Password == "password")
            {
                LoginUserViewModel sa = new LoginUserViewModel { Name = "超级管理员", Code = "sa", Type = "超级管理员", Company = "上海敏慧" };
                Session[SessionHelper.CurrentUserKey] = sa;
                return RedirectToLocal(returnUrl);
            }
            else
            {
                string code = model.Code;
                UserInfoManager um = new UserInfoManager();
                UserInfoEntity ui = um.GetUserByCode(code);
                Session[SessionHelper.CurrentUserKey] = new LoginUserViewModel { Name = ui.Name, Code = ui.Code, Type = ui.Type, Company = ui.Company };
                if (ui == null)
                {
                    ModelState.AddModelError("", "用户名不存在!");
                    return View(model);
                }
                else
                {
                    if (ui.Password == model.Password)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "密码错误!");
                        return View(model);
                    }
                }
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
