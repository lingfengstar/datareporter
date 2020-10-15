using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace datareporter.Controllers
{
    public class FilterController : Controller
    {
        //// <summary>
        /// 该方法将在其它方法之前首先执行
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //若用户未登陆（已登录的用户信息保存在Session全局对象中）
            if (Session["UserName"] == null)
            {
                //跳转到带有原页面地址作为地址参数的登陆页面
                string path = filterContext.HttpContext.Request.Path;//原页面的地址
                filterContext.Result = Redirect("/Home/Index?ReturnUrl=" + path);
            }

        }
    }
}
