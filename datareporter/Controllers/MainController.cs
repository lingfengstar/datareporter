using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using datareporter.Entity;
using datareporter.BLL;
using System.Text;
using System.IO;
using FTPUtil;
using System.Collections;

namespace datareporter.Controllers
{
    public class MainController : FilterController
    {
        #region Agency
        public ActionResult AgencyChange()
        {
            string AgencyNo =Session["AgencyNo"].ToString();
            if (string.IsNullOrEmpty(AgencyNo))
            {
                return Content("空数据！");
            }
            else
            {
                AgencyInformation_Entity agency = new AgencyInformation_Entity();
                agency = BLL.AgencyInformation_BLL.GetAgencyInfoByAgencyNo1(AgencyNo);
                ViewBag.Param = new
                {
                    AgencyNo = agency.AgencyNo,
                    account_bank = agency.account_bank,
                    account_bank_code = agency.account_bank_code,
                    ID = agency.ID,
                    banktype_code = agency.banktype_code,
                    banktype_name = agency.banktype_name

                };

                return View();
            }
        }
        public ActionResult addAgency()
        {
            return View();
        }
        public ActionResult Agency()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addagency()
        {
            AgencyInformation_Entity agencyinfo = new AgencyInformation_Entity();
            agencyinfo.account_bank = Request["account_bank"];
            agencyinfo.AgencyNo = Request["AgencyNo"];
            agencyinfo.account_bank_code = Request["account_bank_code"];
            agencyinfo.banktype_code = Request["banktype_code"];
            agencyinfo.banktype_name=Request["banktype_name"];
            List<AgencyInformation_Entity> agency = AgencyInformation_BLL.GetAgencyInfoByAgencyNo(agencyinfo.AgencyNo);
            if (agency!=null)
            {
                var json = new
                {
                    code = 1,
                    msg = "机构号已使用！"
                };
                return Json(json);

            }
            else
            {
                int i = AgencyInformation_BLL.CreateAgencyInfo(agencyinfo);
                if(i>0)
                {
                    var json = new
                    {
                        code = 0,
                        msg = "添加机构成功！"
                    };
                    return Json(json);
                }
                else
                {
                    var json = new
                    {
                        code = 1,
                        msg = "添加机构失败！"
                    };
                    return Json(json);
                }
            }

        }
        [HttpPost]
        public ActionResult editAgency()
        {
            Entity.AgencyInformation_Entity agency = new AgencyInformation_Entity();
            agency.AgencyNo = Request["AgencyNo"];
            agency.account_bank = Request["account_bank"];
            agency.account_bank_code = Request["account_bank_code"];
            agency.banktype_code = Request["banktype_code"];
            agency.banktype_name = Request["banktype_name"];

            int i= BLL.AgencyInformation_BLL.UpdateAgencyInfo(agency);
            if(i>0)
            {

                var json = new
                {
                    code = 0,
                    msg = "机构信息修改成功！"
                };
                return Json(json);
            }
            else
            {
                var json = new
                {
                    code = 1,
                    msg = "机构信息修改失败！"
                };
                return Json(json);
            }
        }
        [HttpGet]
        public ActionResult AgencyPageList(string AgencyNo = "")
        {
            //if (Session["Admin"].ToString() == "1")//管理员权限
            string role = null;
            if ( role== "admin")//管理员权限
            {
                var agencyList = new List<AgencyInformation_Entity>();
                agencyList = AgencyInformation_BLL.GetAllAgencyInfo();
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = agencyList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = agencyList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);
            }
            else//当前用户权限
            {
                AgencyNo = Session["AgencyNo"].ToString();
                var agencyList = new List<AgencyInformation_Entity>();
                agencyList = BLL.AgencyInformation_BLL.GetAgencyInfoByAgencyNo(AgencyNo);
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = agencyList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = agencyList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion
        #region account
        public ActionResult account()
        {
            return View();
        }
        public ActionResult addaccount()
        {
            return View();
        }
        public ActionResult editaccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Editaccount()
        {
            Entity.account_Entity account = new account_Entity();

            account.ID = Convert.ToInt32(Request["ID"]);
            account.AgencyNo = Request["AgencyNo"];
            account.account_no = Request["account_no"];
            account.account_name = Request["account_name"];
            account.rg_code = Request["rg_code"];
            account.type_code = Request["type_code"];
            account.type_name = Request["type_name"];
            int i = account_BLL.Updateaccount(account);
            if (i > 0)
            {
                var json = new
                {
                    code = 0,
                    msg = "修改成功！"
                };
                return Json(json);
            }
            else
            {
                var json = new
                {
                    code = 1,
                    msg = "修改失败！"
                };
                return Json(json);
            }

        }
        [HttpPost]
        public ActionResult delaccount()
        {
            Entity.account_Entity account = new account_Entity();

            int id=Convert.ToInt32( Request["ID"]);
            account.AgencyNo = Request["AgencyNo"];
            account.account_no = Request["account_no"];
            account.account_name = Request["account_name"];
            account.rg_code = Request["rg_code"];
            account.type_code = Request["type_code"];
            account.type_name = Request["type_name"];
            int i = account_BLL.deleteaccount(id);
            if (i > 0)
            {
                var json = new
                {
                    code = 0,
                    msg = "删除成功！"
                };
                return Json(json);
            }
            else
            {
                var json = new
                {
                    code = 1,
                    msg = "删除失败！"
                };
                return Json(json);
            }

        }
        [HttpPost]
        public ActionResult Addaccount()
        {
            Entity.account_Entity account = new account_Entity();


            account.AgencyNo = Request["AgencyNo"];
            account.account_no = Request["account_no"];
            account.account_name = Request["account_name"];
            account.rg_code = Request["rg_code"];
            account.type_code = Request["type_code"];
            account.type_name = Request["type_name"];
            int i = account_BLL.Createaccount(account);
            if(i>0)
            {
                var json = new
                {
                    code = 0,
                    msg = "添加成功！"
                };
                return Json(json);
            }
            else
            {
                var json = new
                {
                    code = 1,
                    msg = "添加失败！"
                };
                return Json(json);
            }

        }
        [HttpGet]
        public ActionResult accountPageList(string AgencyNo = "")
        {
            string role = null;
            if (role == "admin")//管理员权限
            {
                var accountList = new List<account_Entity>();
                accountList = BLL.account_BLL.GetAllaccount();
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = accountList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = accountList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);
            }
            else//当前用户权限
            {
                AgencyNo = Session["AgencyNo"].ToString();
                var accountList = new List<account_Entity>();
                accountList = BLL.account_BLL.GetaccountByAgencyNo(AgencyNo);
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = accountList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = accountList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion
        #region data
        public ActionResult dataimport()
        {
            return View();
        }
        public ActionResult datapage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult deldata()
        {
            Entity.data_Entity account = new data_Entity();

            int id = Convert.ToInt32(Request["ID"]);
            data_Entity data = new data_Entity();
            data = data_BLL.GetdataByID(id.ToString());
            string fileName = data.dataName + ".txt";
            string filePath = Server.MapPath("/") + data.data_Position + "\\" + fileName;
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    int i = data_BLL.deldata(id);

                    var json = new
                    {
                        code = 0,
                        msg = "删除成功！"
                    };
                    return Json(json);

                }
                catch (Exception ex)
                {
                    var json = new
                    {
                        code = 1,
                        msg = "删除失败！" + ex
                    };
                    return Json(json);

                }
            }
            else
            {
                int i = data_BLL.deldata(id);
                var json = new
                {
                    code = 0,
                    msg = "删除成功！"
                };
                return Json(json);

            }

        }

        [HttpPost]
        public ActionResult dataupload1()
        {
            string  upload, datetime, yesterday, today, accountid, agencyno;

            upload = Request["upload"];
            datetime = Request["datetime"];
            yesterday = Request["yesterday"];
            today = Request["today"];
            accountid = Request["accountid"];
            agencyno = Request["AgencyNo"];
            string log = null;
            if (string.IsNullOrEmpty(datetime))
            {
                log = "日期不能为空";
                var Person = new
                {
                    code = 1,//0表示成功
                    msg = log,//这个是失败返回的错误
                };
                return Json(Person);//格式化为json  
            }
            else
            {                
                log = data.process.cjProcess.cjdataProcess(agencyno, accountid, upload, datetime, yesterday, today);
                var Person = new
            {
                code = 0,//0表示成功
                msg = log,//这个是失败返回的错误
            };
            return Json(Person);//格式化为json 
            }            
        }
        [HttpPost]
        public ActionResult dataupload()
        {
            string file = null;
            string log = null;
            if (Request.Files["file"] != null&& Request.Files["file"].ContentLength != 0)
            {
                file = Request.Files["file"].FileName;
                string liushuidata, yuedata, upload, datetime, yesterday, today, accountid, agencyno;
                liushuidata = Request["liushuidata"];
                yuedata = Request["yuedata"];
                upload = Request["upload"];
                datetime = Request["datetime"];
                yesterday = Request["yesterday"];
                today = Request["today"];
                accountid = Request["accountid"];
                agencyno = Request["AgencyNo"];
                if (string.IsNullOrEmpty( datetime))
                {
                    log = "日期不能为空！";
                    var Person = new
                    {
                        code = 1,//0表示成功
                        msg = log,//这个是失败返回的错误
                    };
                    return Json(Person);//格式化为json  
                }
                else
                {
                    log = data.process.cjProcess.cjdataProcess(file, liushuidata, yuedata, agencyno, accountid, upload, datetime, yesterday, today);
                    var Person = new
                    {
                        code = 0,//0表示成功
                        msg = log,//这个是失败返回的错误
                    };
                    return Json(Person);//格式化为json
                }
            }
            else
            {
                log = "请选择上传文件！";
                var Person = new
                {
                    code = 1,//0表示成功
                    msg = log,//这个是失败返回的错误
                };
                return Json(Person);//格式化为json
            }
        }
        /// <summary>
        /// 上传缓存文件
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult temp()
        {
            
            HttpPostedFileBase ff = Request.Files["file"];
            if (ff != null && ff.ContentLength != 0)
            {
                if (!Directory.Exists(Server.MapPath("/")+"temp"))
                {
                    Directory.CreateDirectory("/"+"temp");
                }
            }
            string filepath = Server.MapPath("/")+"temp"+"\\" + ff.FileName;
            ff.SaveAs(filepath);//在服务器上保存上传文件

            //string[] readFile = System.IO.File.ReadAllLines(filepath);//读取txt文档存放在字符数组中

            var Person = new
            {
                code = 0,//0表示成功
                msg = "",//这个是失败返回的错误
                data = ""
            };
            return Json(Person);//格式化为json
        }
        [HttpGet]
        public ActionResult Download()
        {
            try
            {
                string ID = Request["ID"];
                Encoding encoding;
                data_Entity data = new data_Entity();
                data = data_BLL.GetdataByID(ID);
                string fileName = data.dataName + ".txt";
                string filePath = Server.MapPath("/") + data.data_Position + "\\" + fileName;
                string outputFileName = null;
                fileName = fileName.Replace("'", "");

                string browser = Request.UserAgent.ToUpper();
                if (browser.Contains("MS") == true && browser.Contains("IE") == true)
                {
                    outputFileName = HttpUtility.UrlEncode(fileName);
                    encoding = Encoding.Default;
                }
                else if (browser.Contains("FIREFOX") == true)
                {
                    outputFileName = fileName;
                    encoding = Encoding.GetEncoding("GB2312");
                }
                else
                {
                    outputFileName = HttpUtility.UrlEncode(fileName);
                    encoding = Encoding.Default;
                }
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.Charset = "UTF-8";
                Response.ContentType = "application/octet-stream";
                Response.ContentEncoding = encoding;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + outputFileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                var Person = new
                {
                    code = 0,//0表示成功
                    msg = "",//这个是失败返回的错误
                };
                return Json(Person);//格式化为json  
            }
            catch (Exception ex)
            {
                var Person = new
                {
                    code = 1,//0表示成功
                    msg = ex,//这个是失败返回的错误
                };
                return Json(Person);//格式化为json  
            }
        }
        [HttpGet]
        public ActionResult dataPageList(string AgencyNo = "")
        {
            string role = null;
            if (role == "admin")//管理员权限
            {
                var dataList = new List<data_Entity>();
                dataList = BLL.data_BLL.GetAlldata();
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = dataList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = dataList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);
            }
            else//当前用户权限
            {
                AgencyNo = Session["AgencyNo"].ToString();
                var dataList = new List<data_Entity>();
                dataList = BLL.data_BLL.GetdataByAgencyNo(AgencyNo);
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = dataList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = dataList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public ActionResult Upload()
        {
            string ID = Request["ID"];
            data_Entity data = new data_Entity();
            data = data_BLL.GetdataByID(ID);
            string fileName = data.dataName + ".txt";
            string filePath = Server.MapPath("/") + data.data_Position + "\\" + fileName;
            FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
            ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(data.AgencyNo);
            FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress,ftpinfo.FTPUsername,ftpinfo.FTPPassword);
            FileInfo file = new FileInfo(filePath);
            bool i= ftp.Upload(file, fileName);
            if(i)
            {
                data.upload = "1";
                data_BLL.Updatedata(data);
                logs_Entity logs = new logs_Entity();
                logs.AgencyNo =data.AgencyNo;
                logs.inputTime = DateTime.Now;
                logs.logName = "上传密文：" + data.dataName;
                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                logs.succeed = "1";
                logs_BLL.Createlog(logs);

                var Person = new
                {
                    code = 0,//0表示成功
                    msg = "上传成功！",//这个是失败返回的错误
                };
                return Json(Person,JsonRequestBehavior.AllowGet);//格式化为json
            }
            else
            {
                logs_Entity logs = new logs_Entity();
                logs.AgencyNo = data.AgencyNo;
                logs.inputTime = DateTime.Now;
                logs.logName = "上传密文：" + data.dataName;
                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                logs.succeed = "0";
                logs_BLL.Createlog(logs);
                var Person = new
                {
                    code = 1,//0表示成功
                    msg = "上传失败",//这个是失败返回的错误
                };
                return Json(Person, JsonRequestBehavior.AllowGet);//格式化为json

            }
        }

        #endregion
        #region FTP
        public ActionResult AddFTP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addFTP()
        {
            FTPInformation_Entity ftp = new FTPInformation_Entity();
            ftp.AgencyNo = Request["AgencyNo"];
            ftp.FTPAddress = Request["FTPAddress"];
            ftp.FTPUsername = Request["FTPUsername"];
            ftp.FTPPassword = Request["FTPPassword"];
            ftp.data_Key = Request["data_Key"];
            List<FTPInformation_Entity> list = FTPInformation_BLL.GetFTPInfoByAgencyNo(ftp.AgencyNo);
            if (list.Count > 0)
            {
                var json = new
                {
                    code = 1,
                    msg = "本机构FTP信息已存在！"
                };
                return Json(json);

            }
            else
            {
                int i = FTPInformation_BLL.CreateFTPInfo(ftp);
                if (i > 0)
                {
                    var json = new
                    {
                        code = 0,
                        msg = "添加成功！"
                    };
                    return Json(json);

                }
                else
                {
                    var json = new
                    {
                        code = 1,
                        msg = "添加失败！"
                    };
                    return Json(json);
                }
            }
        }
        public ActionResult editFTP()
        {
            return View();
        }
        public ActionResult FTPInfo()
        {
                    return View();
        }
        [HttpPost]
        public ActionResult delFTP()
        {

            int id = Convert.ToInt32(Request["ID"]);
            int i = FTPInformation_BLL.deleteFTP(id);
            if (i > 0)
            {
                var json = new
                {
                    code = 0,
                    msg = "删除成功！"
                };
                return Json(json);
            }
            else
            {
                var json = new
                {
                    code = 1,
                    msg = "删除失败！"
                };
                return Json(json);
            }

        }
        [HttpPost]
        public ActionResult editftp()
        {
            Entity.FTPInformation_Entity ftp = new FTPInformation_Entity();

            ftp.ID = Convert.ToInt32(Request["ID"]);
            ftp.AgencyNo = Request["AgencyNo"];
            ftp.FTPAddress = Request["FTPAddress"];
            ftp.FTPUsername = Request["FTPUsername"];
            ftp.FTPPassword = Request["FTPPassword"];
            ftp.data_Key = Request["data_Key"];

            int i =FTPInformation_BLL.UpdateFTPInfo(ftp);
            if (i > 0)
            {
                var json = new
                {
                    code = 0,
                    msg = "修改成功！"
                };
                return Json(json);
            }
            else
            {
                var json = new
                {
                    code = 1,
                    msg = "修改失败！"
                };
                return Json(json);
            }

        }

        [HttpGet]
        public ActionResult ftpPageList(string AgencyNo = "")
        {

            string role = null;
            if (role == "admin")//管理员权限
            {
                var ftpList = new List<FTPInformation_Entity>();
                ftpList = BLL.FTPInformation_BLL.GetAllFTPInfo();
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = ftpList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = ftpList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);
            }
            else//当前用户权限
            {
                AgencyNo = Session["AgencyNo"].ToString();
                var ftpList = new List<FTPInformation_Entity>();
                ftpList = BLL.FTPInformation_BLL.GetFTPInfoByAgencyNo(AgencyNo);
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = ftpList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = ftpList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion
        #region logs
        public ActionResult logs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult logdel()
        {
            logs_BLL.dellog();

                var json = new
                {
                    code = 0,
                    msg = "清空成功！"
                };
                return Json(json);

        }
        [HttpGet]
        public ActionResult logPageList(string AgencyNo = "")
        {
            string role = null;
            if (role == "admin")//管理员权限
            {
                var logsList = new List<logs_Entity>();
                logsList = BLL.logs_BLL.GetAlllogs();
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = logsList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = logsList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);
            }
            else//当前用户权限
            {
                AgencyNo = Session["AgencyNo"].ToString();
                var logsList = new List<logs_Entity>();
                logsList = BLL.logs_BLL.GetlogsByAgencyNo(AgencyNo);
                int page = Convert.ToInt32(Request["page"]);
                int limit = Convert.ToInt32(Request["limit"]);

                //真分页 根据page limit来进行分页
                var page_list = logsList.OrderBy(md => md.ID).Skip((page - 1) * limit).Take(limit).ToList();
                Hashtable table = new Hashtable
                {
                    ["code"] = 0,
                    ["msg"] = "",
                    ["count"] = logsList.Count(),//总条数
                    ["data"] = page_list//分页数据
                };
                return Json(table, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion
        #region user
        // GET: Main
        public ActionResult Main()
        {
            return View();
        }
        public ActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public ActionResult passwordchange()
        {
            string old = Request["oldPassword"];
            string newpassword=Request["password"];
            string username = Session["UserName"].ToString();
            if(BLL.UserInformation_BLL.UserLogin(username, old))
            {
                UserInformation_Entity user = UserInformation_BLL.GetUserInfoByName(username);
                user.UserPassword = newpassword;
                user.AgencyNo = Session["AgencyNo"].ToString();
                if(UserInformation_BLL.UpdateUserInfo(user)>0)
                {
                    var json = new
                    {
                        code = 0,
                        msg = "密码修改成功！"
                    };
                    return Json(json);
                }
                else
                {
                    var json = new
                    {
                        code = 1,
                        msg = "密码修改失败！"
                    };
                    return Json(json);
                }
            }
            else
            {
                var json = new
                {
                    code = 2,
                    msg = "原密码错误！"
                };
                return Json(json);
            }
        }
        [HttpPost]
        public ActionResult adduser()
        {
            string username = Request["username"];
            string password = Request["password"];
            string AgencyNo = Session["AgencyNo"].ToString();
            if (BLL.UserInformation_BLL.GetUserInfoByName(username)!=null)
            {
                var json = new
                {
                    code = 1,
                    msg = "用户已存在！"
                };
                return Json(json);
            }
            else
            {
                UserInformation_Entity user =new UserInformation_Entity();
                user.UserName = username;
                user.UserPassword = password;
                user.AgencyNo = AgencyNo;
                if (UserInformation_BLL.CreateUserInfo(user) > 0)
                {
                    var json = new
                    {
                        code = 0,
                        msg = "注册用户成功！"
                    };
                    return Json(json);
                }
                else
                {
                    var json = new
                    {
                        code = 2,
                        msg = "注册失败！"
                    };
                    return Json(json);
                }
            }

        }
        #endregion


    }
}
