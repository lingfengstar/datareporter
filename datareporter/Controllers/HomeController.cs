using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using datareporter.BLL;


namespace datareporter.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 全局变量：用户在Session对象中的标识
        /// </summary>
        public static string UseInfo = "UserName@CCC";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            string username = Request["username"];
                string password=Request["password"];

                if (BLL.UserInformation_BLL.UserLogin(username, password))
                {
                    Entity.UserInformation_Entity UI = BLL.UserInformation_BLL.GetUserInfoByName(username);
                    Entity.AgencyInformation_Entity AI = BLL.AgencyInformation_BLL.GetAgencyInfoByAgencyNo1(UI.AgencyNo);
                    ViewBag.ID = UI.ID;
                    ViewBag.Name = UI.UserName;
                    ViewBag.AgencyName = AI.account_bank;
                    ViewBag.AgencyNo=AI.AgencyNo;
                    Session["UserName"] = UI.UserName;
                    Session["AgencyName"] = AI.account_bank;
                    Session["AgencyNo"] = AI.AgencyNo;
                var token  =new {access_token="c262e61cd13ad99fc650e6908c7e5e65b63d2f32185ecfed6b801ee3fbdd5c0a" };
                var json = new
                {
                    code = 0,
                    msg = "登陆成功！",
                    data = token
                };
                return Json(json, JsonRequestBehavior.AllowGet);

            }
            else
                {
                    var json = new
                    {
                        code = 1001,
                        msg = "用户名或密码错误！",
                        data=""
                    };
                    return Json(json,JsonRequestBehavior.AllowGet);
                }
            
        }
    }
}