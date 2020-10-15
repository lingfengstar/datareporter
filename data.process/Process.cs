using System;
using System.Text;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using datareporter.Entity;
using datareporter.BLL;
using System.Text.RegularExpressions;
using FTPUtil;

namespace data.process
{
    public class cjProcess
    {

        Encoding encoding;
        /// <summary>
        /// 缓存文件地址
        /// </summary>
        static string path = System.Web.HttpContext.Current.Server.MapPath("/") + "temp";
        /// <summary>
        /// 数据入库合成
        /// </summary>
        /// <param name="filename">缓存文件名</param>
        /// <param name="liushuidata">是否生成水文件</param>
        /// <param name="yuedata">是否生成余额文件</param>
        /// <param name="AgencyNo">机构号</param>
        /// <param name="accountid">账户ID</param>
        /// <param name="upload">是否上传</param>
        /// <param name="datetime">数据日期</param>
        /// <param name="yesterday">上日余额</param>
        /// <param name="today">本日余额</param>
        public static string cjdataProcess(string filename, string liushuidata, string yuedata, string AgencyNo, string accountid, string upload, string datetime, string yesterday, string today)
        {
            string log = null;//返回日志
            string filepath = path + "\\" + filename;
            string datapath = System.Web.HttpContext.Current.Server.MapPath("/") + "data";//明文目录
            string sendpath = System.Web.HttpContext.Current.Server.MapPath("/") + "send";//密文目录
            //DataTable dt = datadt(filepath);//导入流水表
            DataTable dt = cjdt(filepath);
            System.IO.File.Delete(filepath);
            string liushuiname = null;//流水文件名
            string yuename = null;//余额文件名


            account_Entity account = account_BLL.GetaccountByID(accountid);//当前账户信息
            AgencyInformation_Entity agency = AgencyInformation_BLL.GetAgencyInfoByAgencyNo1(AgencyNo);//当前机构信息
            FTPInformation_Entity fTP = FTPInformation_BLL.GetFTPInfoByAgencyNo1(AgencyNo);//机构FTP信息

            //判断文件处理
            try
            {
                //处理流水文件
                if (liushuidata == "1")//是否生成流水
                {

                    string ls = cjlsdata(dt, agency, account);//流水数据

                    //当天流水次日上传
                    string name = agency.banktype_code.ToString() + "1" + account.type_code.ToString() + account.rg_code.ToString() + Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
                    string dataname = null;
                    //判断文件是否存在
                    for (int i = 1; i <= 9999; i++)
                    {
                        dataname = name + string.Format("{0:D4}", i);
                        if (File.Exists(datapath + "\\" + dataname + ".txt"))
                        {
                            //i++;
                        }
                        else
                        {
                            liushuiname = datapath + "\\" + name + string.Format("{0:D4}", i) + ".txt";
                            break;
                        }
                    }
                    //写入流水文件
                    FileStream fs = new FileStream(liushuiname, FileMode.CreateNew, FileAccess.ReadWrite);
                    fs.Close();
                    //判断流水文件是否生成
                    if (File.Exists(liushuiname))
                    {
                        File.WriteAllText(liushuiname, ls, Encoding.UTF8);
                        string lslog = "生成流水明文成功。";
                        log = lslog;
                        //添加日志表
                        logs_Entity logs = new logs_Entity();
                        logs.AgencyNo = AgencyNo;
                        logs.inputTime = DateTime.Now;
                        logs.logName = "生成流水明文：" + dataname;
                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                        logs.succeed = "1";
                        logs_BLL.Createlog(logs);
                        logs = null;
                        //添加数据表
                        data_Entity mingwen = new data_Entity();
                        mingwen.AgencyNo = AgencyNo;
                        mingwen.dataName = dataname;
                        mingwen.datatype = "1";
                        mingwen.data_Position = "data";
                        mingwen.inputTime = Convert.ToDateTime(DateTime.Now);
                        mingwen.upload = "0";
                        data_BLL.Createdata(mingwen);
                        //判断是否上传
                        if (upload == "1")
                        {
                            data_Entity miwen = new data_Entity();
                            miwen.AgencyNo = AgencyNo;
                            miwen.dataName = dataname;
                            miwen.datatype = "0";
                            miwen.data_Position = "send";
                            miwen.inputTime = Convert.ToDateTime(DateTime.Now);
                            string mingpath = liushuiname;
                            string mipath = sendpath + "\\" + dataname + ".txt";
                            string a = cj3des.cj3des.main(mingpath, mipath, fTP.data_Key);
                            //判断是否加密成功
                            if (a == "1")
                            {
                                lslog = "生成流水密文成功。";
                                log = log + lslog;
                                logs = new logs_Entity();
                                logs.AgencyNo = AgencyNo;
                                logs.inputTime = DateTime.Now;
                                logs.logName = "生成流水密文：" + dataname;
                                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                logs.succeed = "1";
                                logs_BLL.Createlog(logs);
                                logs = null;

                                FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
                                ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(agency.AgencyNo);
                                FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress, ftpinfo.FTPUsername, ftpinfo.FTPPassword);
                                FileInfo file = new FileInfo(mipath);
                                //上传加密文件
                                bool i = ftp.Upload(file, dataname + ".txt");
                                if (i)
                                {
                                    lslog = "上传流水密文成功。";
                                    log = log + lslog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "上传流水密文：" + dataname;
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "1";
                                    logs_BLL.Createlog(logs);
                                    logs = null;
                                    miwen.upload = "1";
                                    data_BLL.Createdata(miwen);
                                }
                                else
                                {
                                    miwen.upload = "0";
                                    data_BLL.Createdata(miwen);
                                    lslog = "上传流水密文不成功。";
                                    log = log + lslog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "上传流水密文：" + dataname + "不成功";
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "0";
                                    logs_BLL.Createlog(logs);
                                    logs = null;

                                }

                            }
                            else
                            {
                                lslog = "生成流水密文不成功。";
                                log = log + lslog;
                                logs = new logs_Entity();
                                logs.AgencyNo = AgencyNo;
                                logs.inputTime = DateTime.Now;
                                logs.logName = "生成流水密文：" + dataname + "不成功";
                                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                logs.succeed = "0";
                                logs_BLL.Createlog(logs);
                                logs = null;

                            }
                        }
                    }
                    else
                    {
                        string lslog = "生成流水明文不成功。";
                        log = lslog;
                        //添加日志表
                        logs_Entity logs = new logs_Entity();
                        logs.AgencyNo = AgencyNo;
                        logs.inputTime = DateTime.Now;
                        logs.logName = "生成流水明文：" + dataname + "不成功";
                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                        logs.succeed = "0";
                        logs_BLL.Createlog(logs);
                        logs = null;

                    }

                }
                //处理余额文件
                if (yuedata == "1")//是否生成余额
                {
                    //当天余额次日上传
                    string name = agency.banktype_code.ToString() + "0" + account.type_code.ToString() + account.rg_code.ToString() + Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
                    string dataname = null;
                    //判断文件是否存在
                    for (int i = 1; i <= 9999; i++)
                    {
                        dataname = name + string.Format("{0:D4}", i);
                        if (File.Exists(datapath + "\\" + dataname + ".txt"))
                        {
                            i++;
                        }
                        else
                        {
                            yuename = datapath + "\\" + name + string.Format("{0:D4}", i) + ".txt";
                            break;
                        }
                    }
                    //流水文件已上传时
                    if (liushuidata == "1")
                    {
                        string yue = cjyuedata(dt, agency, account, datetime);//余额数据：有流水

                        FileStream fs = new FileStream(yuename, FileMode.CreateNew, FileAccess.ReadWrite);
                        fs.Close();
                        //判断余额文件是否存在
                        if (File.Exists(yuename))
                        {
                            File.WriteAllText(yuename, yue, Encoding.UTF8);
                            string yuelog = "生成余额明文成功。";
                            log = log + yuelog;
                            logs_Entity logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额明文：" + dataname;
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "1";
                            logs_BLL.Createlog(logs);
                            logs = null;

                            data_Entity mingwen = new data_Entity();
                            mingwen.AgencyNo = AgencyNo;
                            mingwen.dataName = dataname;
                            mingwen.datatype = "1";
                            mingwen.data_Position = "data";
                            mingwen.inputTime = Convert.ToDateTime(DateTime.Now);
                            mingwen.upload = "0";
                            data_BLL.Createdata(mingwen);
                            //判断是否上传
                            if (upload == "1")
                            {
                                data_Entity miwen = new data_Entity();
                                miwen.AgencyNo = AgencyNo;
                                miwen.dataName = dataname;
                                miwen.datatype = "0";
                                miwen.data_Position = "send";
                                miwen.inputTime = Convert.ToDateTime(DateTime.Now);
                                string mingpath = yuename;
                                string mipath = sendpath + "\\" + dataname + ".txt";
                                string a = cj3des.cj3des.main(mingpath, mipath, fTP.data_Key);
                                if (a == "1")
                                {
                                    yuelog = "生成余额密文成功。";
                                    log = log + yuelog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "生成余额密文：" + dataname;
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "1";
                                    logs_BLL.Createlog(logs);

                                    logs = null;
                                    FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
                                    ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(agency.AgencyNo);
                                    FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress, ftpinfo.FTPUsername, ftpinfo.FTPPassword);
                                    FileInfo file = new FileInfo(mipath);
                                    //上传加密文件
                                    bool i = ftp.Upload(file, dataname + ".txt");
                                    if (i)
                                    {
                                        yuelog = "上传余额密文成功。";
                                        log = log + yuelog;
                                        logs = new logs_Entity();
                                        logs.AgencyNo = AgencyNo;
                                        logs.inputTime = DateTime.Now;
                                        logs.logName = "上传余额密文：" + dataname;
                                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                        logs.succeed = "1";
                                        logs_BLL.Createlog(logs);

                                        logs = null;
                                        miwen.upload = "1";
                                        data_BLL.Createdata(miwen);
                                    }
                                    else
                                    {
                                        yuelog = "上传余额密文不成功。";
                                        log = log + yuelog;
                                        logs = new logs_Entity();
                                        logs.AgencyNo = AgencyNo;
                                        logs.inputTime = DateTime.Now;
                                        logs.logName = "上传余额密文：" + dataname + "不成功";
                                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                        logs.succeed = "1";
                                        logs_BLL.Createlog(logs);

                                        logs = null;
                                        miwen.upload = "0";
                                        data_BLL.Createdata(miwen);
                                    }

                                }
                                else
                                {
                                    yuelog = "生成余额密文不成功。";
                                    log = log + yuelog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "生成余额密文：" + dataname + "不成功";
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "0";
                                    logs_BLL.Createlog(logs);

                                    logs = null;
                                }
                            }


                        }
                        else
                        {
                            string yuelog = "生成余额明文不成功。";
                            log = log + yuelog;
                            logs_Entity logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额明文：" + dataname + "不成功";
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "0";
                            logs_BLL.Createlog(logs);
                            logs = null;

                        }
                    }
                    //未上传流水文件
                    else
                    {
                        string yuenols = cjyuedata(agency, account, datetime, yesterday, today);//余额数据：无流水

                        FileStream fs = new FileStream(yuename, FileMode.CreateNew, FileAccess.ReadWrite);
                        fs.Close();
                        if (File.Exists(yuename))
                        {
                            File.WriteAllText(yuename, yuenols, Encoding.UTF8);
                            string yuelog = "生成余额明文成功。";
                            log = log + yuelog;
                            logs_Entity logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额明文：" + dataname;
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "1";
                            logs_BLL.Createlog(logs);

                            logs = null;
                            data_Entity mingwen = new data_Entity();
                            mingwen.AgencyNo = AgencyNo;
                            mingwen.dataName = dataname;
                            mingwen.datatype = "1";
                            mingwen.data_Position = "data";
                            mingwen.inputTime = Convert.ToDateTime(DateTime.Now);
                            mingwen.upload = "0";
                            data_BLL.Createdata(mingwen);
                            //判断是否上传
                            if (upload == "1")
                            {
                                data_Entity miwen = new data_Entity();
                                miwen.AgencyNo = AgencyNo;
                                miwen.dataName = dataname;
                                miwen.datatype = "0";
                                miwen.data_Position = "send";
                                miwen.inputTime = Convert.ToDateTime(DateTime.Now);
                                string mingpath = yuename;
                                string mipath = sendpath + "\\" + dataname + ".txt";
                                string a = cj3des.cj3des.main(mingpath, mipath, fTP.data_Key);
                                if (a == "1")
                                {
                                    yuelog = "生成余额密文成功。";
                                    log = log + yuelog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "生成余额密文：" + dataname;
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "1";
                                    logs_BLL.Createlog(logs);

                                    logs = null;
                                    FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
                                    ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(agency.AgencyNo);
                                    FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress, ftpinfo.FTPUsername, ftpinfo.FTPPassword);
                                    FileInfo file = new FileInfo(mipath);
                                    //上传加密文件
                                    bool i = ftp.Upload(file, dataname + ".txt");
                                    if (i)
                                    {
                                        yuelog = "上传余额密文成功。";
                                        log = log + yuelog;
                                        logs = new logs_Entity();
                                        logs.AgencyNo = AgencyNo;
                                        logs.inputTime = DateTime.Now;
                                        logs.logName = "上传余额密文：" + dataname;
                                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                        logs.succeed = "1";
                                        logs_BLL.Createlog(logs);

                                        logs = null;
                                        miwen.upload = "1";
                                        data_BLL.Createdata(miwen);
                                    }
                                    else
                                    {
                                        yuelog = "上传余额密文不成功。";
                                        log = log + yuelog;
                                        logs = new logs_Entity();
                                        logs.AgencyNo = AgencyNo;
                                        logs.inputTime = DateTime.Now;
                                        logs.logName = "上传余额密文：" + dataname + "不成功";
                                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                        logs.succeed = "1";
                                        logs_BLL.Createlog(logs);

                                        logs = null;
                                        miwen.upload = "0";
                                        data_BLL.Createdata(miwen);
                                    }

                                }

                            }


                        }
                        else
                        {
                            string yuelog = "生成余额明文不成功。";
                            log = log + yuelog;
                            logs_Entity logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额明文：" + dataname + "不成功";
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "0";
                            logs_BLL.Createlog(logs);
                            logs = null;

                        }
                    }
                }
                return log;
            }
            catch (Exception ex)
            {
                string a = log + ex.ToString();
                return a;
            }
        }
        /// <summary>
        /// 数据入库合成
        /// </summary>
        /// <param name="AgencyNo">机构号</param>
        /// <param name="accountid">账户ID</param>
        /// <param name="upload">是否上传</param>
        /// <param name="datetime">数据日期</param>
        /// <param name="yesterday">上日余额</param>
        /// <param name="today">本日余额</param>
        public static string cjdataProcess(string AgencyNo, string accountid, string upload, string datetime, string yesterday, string today)
        {
            string log = null;//返回日志
            string datapath = System.Web.HttpContext.Current.Server.MapPath("/") + "data";//明文目录
            string sendpath = System.Web.HttpContext.Current.Server.MapPath("/") + "send";//密文目录
            string yuename = null;//余额文件名


            account_Entity account = account_BLL.GetaccountByID(accountid);//当前账户信息
            AgencyInformation_Entity agency = AgencyInformation_BLL.GetAgencyInfoByAgencyNo1(AgencyNo);//当前机构信息
            FTPInformation_Entity fTP = FTPInformation_BLL.GetFTPInfoByAgencyNo1(AgencyNo);//机构FTP信息

            //判断文件处理
            try
            {

                //当天余额次日上传
                string name = agency.banktype_code.ToString() + "0" + account.type_code.ToString() + account.rg_code.ToString() + Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
                string dataname = null;
                //判断文件是否存在
                for (int i = 1; i <= 9999; i++)
                {
                    dataname = name + string.Format("{0:D4}", i);
                    if (File.Exists(datapath + "\\" + dataname + ".txt"))
                    {
                        //i++;
                    }
                    else
                    {
                        yuename = datapath + "\\" + name + string.Format("{0:D4}", i) + ".txt";
                        break;
                    }
                }


                string yuenols = cjyuedata(agency, account, datetime, yesterday, today);//余额数据：无流水

                FileStream fs = new FileStream(yuename, FileMode.CreateNew, FileAccess.ReadWrite);
                fs.Close();
                if (File.Exists(yuename))
                {
                    File.WriteAllText(yuename, yuenols, Encoding.UTF8);
                    string yuelog = "生成余额明文成功。";
                    log = log + yuelog;
                    logs_Entity logs = new logs_Entity();
                    logs.AgencyNo = AgencyNo;
                    logs.inputTime = DateTime.Now;
                    logs.logName = "生成余额明文：" + dataname;
                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                    logs.succeed = "1";
                    logs_BLL.Createlog(logs);

                    logs = null;
                    data_Entity mingwen = new data_Entity();
                    mingwen.AgencyNo = AgencyNo;
                    mingwen.dataName = dataname;
                    mingwen.datatype = "1";
                    mingwen.data_Position = "data";
                    mingwen.inputTime = Convert.ToDateTime(DateTime.Now);
                    mingwen.upload = "0";
                    data_BLL.Createdata(mingwen);
                    //判断是否上传
                    if (upload == "1")
                    {
                        data_Entity miwen = new data_Entity();
                        miwen.AgencyNo = AgencyNo;
                        miwen.dataName = dataname;
                        miwen.datatype = "0";
                        miwen.data_Position = "send";
                        miwen.inputTime = Convert.ToDateTime(DateTime.Now);
                        string mingpath = yuename;
                        string mipath = sendpath + "\\" + dataname + ".txt";
                        string a = cj3des.cj3des.main(mingpath, mipath, fTP.data_Key);
                        if (a == "1")
                        {
                            yuelog = "生成余额密文成功。";
                            log = log + yuelog;
                            logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额密文：" + dataname;
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "1";
                            logs_BLL.Createlog(logs);

                            logs = null;
                            FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
                            ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(agency.AgencyNo);
                            FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress, ftpinfo.FTPUsername, ftpinfo.FTPPassword);
                            FileInfo file = new FileInfo(mipath);
                            //上传加密文件
                            bool i = ftp.Upload(file, dataname + ".txt");
                            if (i)
                            {
                                yuelog = "上传余额密文成功。";
                                log = log + yuelog;
                                logs = new logs_Entity();
                                logs.AgencyNo = AgencyNo;
                                logs.inputTime = DateTime.Now;
                                logs.logName = "上传余额密文：" + dataname;
                                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                logs.succeed = "1";
                                logs_BLL.Createlog(logs);

                                logs = null;
                                miwen.upload = "1";
                                data_BLL.Createdata(miwen);
                            }
                            else
                            {
                                yuelog = "上传余额密文不成功。";
                                log = log + yuelog;
                                logs = new logs_Entity();
                                logs.AgencyNo = AgencyNo;
                                logs.inputTime = DateTime.Now;
                                logs.logName = "上传余额密文：" + dataname + "不成功";
                                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                logs.succeed = "1";
                                logs_BLL.Createlog(logs);

                                logs = null;
                                miwen.upload = "0";
                                data_BLL.Createdata(miwen);
                            }

                        }

                    }


                }
                else
                {
                    string yuelog = "生成余额明文不成功。";
                    log = log + yuelog;
                    logs_Entity logs = new logs_Entity();
                    logs.AgencyNo = AgencyNo;
                    logs.inputTime = DateTime.Now;
                    logs.logName = "生成余额明文：" + dataname + "不成功";
                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                    logs.succeed = "0";
                    logs_BLL.Createlog(logs);
                    logs = null;

                }


                return log;
            }
            catch (Exception ex)
            {
                string a = log + ex.ToString();
                return a;
            }
        }

        /// <summary>
        /// 读入流水excel表
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static DataTable cjdt(string filepath)
        {
            try
            {
                IWorkbook workbook = null;
                FileStream file = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                //判断版本
                if (filepath.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook = new XSSFWorkbook(file);  //xlsx数据读入workbook
                }
                else if (filepath.IndexOf(".xls") > 0) // 2003版本
                {
                    workbook = new HSSFWorkbook(file);  //xls数据读入workbook
                }
                ISheet sheet = workbook.GetSheetAt(0);
                //设置隐藏列 为 不隐藏
                for (int iHide = 0; iHide <= 10; iHide++)
                {
                    sheet.SetColumnHidden(iHide, false);
                }
                //最后一列的标号(即总的行数)
                int rowCount = sheet.LastRowNum;

                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                DataTable dt = new DataTable();

                IRow headerRow = sheet.GetRow(1);//列标题
                int cellCount = headerRow.LastCellNum;//行单元格数
                int rfirst = sheet.FirstRowNum;//首行号hns为第4行
                int rlast = sheet.LastRowNum;//末行号
                IRow row = sheet.GetRow(rfirst + 1);//首行数据
                int cfirst = row.FirstCellNum;//首单元格号
                int clast = row.LastCellNum;//末单元格号
                //单行表头
                for (int i = cfirst; i < clast; i++)
                {
                    if (row.GetCell(i) != null)
                        dt.Columns.Add(row.GetCell(i).StringCellValue, System.Type.GetType("System.String"));
                }
                // 循环行数据，第5行开始到倒数第4行
                row = null;
                for (int i = rfirst + 2; i <= rlast - 1; i++)
                {
                    DataRow r = dt.NewRow();
                    IRow ir = sheet.GetRow(i);
                    for (int j = cfirst; j < clast; j++)
                    {
                        if (ir.GetCell(j) != null)
                        {
                            r[j] = ir.GetCell(j).ToString();
                        }
                    }
                    dt.Rows.Add(r);
                    ir = null;
                    r = null;
                }
                sheet = null;
                workbook = null;
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }



        }
        /// <summary>
        /// 合成流水数据文本
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="Agency">机构信息</param>
        /// <param name="account">账户信息</param>
        /// <returns></returns>
        private static string cjlsdata(DataTable dt, AgencyInformation_Entity Agency, account_Entity account)
        {
            int len = dt.Rows.Count;
            string datarow;
            string data = null;
            for (int i = 0; i < len; i++)
            {
                DataRow dr = dt.Rows[i];
                datarow = cjlsRow(dr, Agency, account);
                if (i == 0)
                {
                    data = datarow;
                }
                else
                {
                    data = data + "||" + datarow;

                }
            }
            return data;

        }
        /// <summary>
        /// 合成流水每行文本
        /// </summary>
        /// <param name="dr">流水数据行</param>
        /// <param name="agency">机构信息</param>
        /// <param name="account">账户信息</param>
        /// <returns></returns>
        private static string cjlsRow(DataRow dr, AgencyInformation_Entity agency, account_Entity account)
        {
            string datarow = null;
            liushui_Entity liushui = new liushui_Entity();

            liushui.account_no = dr[5].ToString();
            liushui.account_name = dr[7].ToString();
            liushui.account_bank = agency.account_bank;
            liushui.account_bank_code = agency.account_bank_code;
            liushui.banktype_code = agency.banktype_code;
            liushui.banktype_name = agency.banktype_name;
            liushui.bill_time = Regex.Replace(dr[8].ToString(), "-", "") + " " + Regex.Replace(dr[9].ToString(), ":", "");
            liushui.bank_order_no = dr[26].ToString();
            //判断交易类型
            if (dr[17].ToString() == "2-转账")
            {
                liushui.bill_type = "1";
            }
            else
            {
                if (dr[17].ToString() == "1-现金")
                {
                    liushui.bill_type = "2";
                }
            }
            //判断交易凭证号
            if (string.IsNullOrEmpty(dr[15].ToString()))
            {
                liushui.bill_no = "000000";
            }
            else
            {
                liushui.bill_no = dr[15].ToString();
            }
            liushui.fm_code = "156";//默认值
            liushui.fm_name = "人民币";//默认值
            liushui.pay_money = dr[11].ToString();
            liushui.rg_code = account.rg_code;
            liushui.year = Convert.ToDateTime(dr[8].ToString()).Year.ToString();
            //判断收款账户账号
            if (string.IsNullOrEmpty(dr[27].ToString()))
            {
                liushui.other_account_no = "000000";
            }
            else
            {
                liushui.other_account_no = dr[27].ToString();
            }
            //判断收款账户名称
            if (string.IsNullOrEmpty(dr[28].ToString()))
            {
                liushui.other_account_name = "无收款账户名称";
            }
            else
            {
                liushui.other_account_name = dr[28].ToString();
            }
            liushui.other_bank_name = "无收款账户开户银行名称";
            //判断用途
            if (string.IsNullOrEmpty(dr[25].ToString()))
            {
                liushui.summary = "无";
            }
            else
            {
                liushui.summary = dr[25].ToString();
            }
            //判断借贷方向
            if (dr[12].ToString() == "C-贷方")
            {
                liushui.Money_type = "0";
            }
            else
            if (dr[12].ToString() == "D-借方")
            {
                liushui.Money_type = "1";
            }
            //合成一行数据
            datarow = liushui.account_no + "," + liushui.account_name + "," + liushui.account_bank + "," + liushui.account_bank_code + "," +
                liushui.banktype_code + "," + liushui.banktype_name + "," + liushui.bill_time + "," + liushui.bank_order_no + "," + liushui.bill_type
                + "," + liushui.bill_no + "," + liushui.fm_code + "," + liushui.fm_name + "," + liushui.pay_money + "," + liushui.rg_code
                + "," + liushui.year + "," + liushui.other_account_no + "," + liushui.other_account_name + "," + liushui.other_bank_name
                + "," + liushui.summary + "," + liushui.Money_type;

            return datarow;
        }

        /// <summary>
        /// 合成余额文本:有流水
        /// </summary>
        /// <param name="dt">流水数据表</param>
        /// <param name="agency">机构信息</param>
        /// <param name="account">账户信息</param>
        /// <param name="datetime">传入数据日期</param>
        /// <returns></returns>
        private static string cjyuedata(DataTable dt, AgencyInformation_Entity agency, account_Entity account, string datetime)
        {
            int rows = dt.Rows.Count;
            string datarow = null;

            yue_Entity yue = new yue_Entity();
            yue.account_no = account.account_no;
            yue.account_name = account.account_name;
            yue.account_bank = agency.account_bank;
            yue.account_bank_code = agency.account_bank_code;
            yue.banktype_code = agency.banktype_code;
            yue.banktype_name = agency.banktype_name;

            //判断上期余额
            string date = Convert.ToDateTime(datetime).ToString("yyyyMMdd");//传入数据日期
            string date1 = Convert.ToDateTime(dt.Rows[rows - 1][8].ToString()).ToString("yyyyMMdd");
            if (date == date1)//判断传入日期与数据最后日期是否相同
            {
                yue.report_date = Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
            }
            else
            {
                yue.report_date = Convert.ToString(DateTime.ParseExact(date1, "yyyyMMdd", null).AddDays(1).ToString("yyyyMMdd"));
            }
            for (int i = 0; i <rows; i++)
            {
                string datebalance = Convert.ToDateTime(dt.Rows[i][8].ToString()).ToString("yyyyMMdd");
                if (datebalance == date1)
                {
                    if (dt.Rows[i][12].ToString() == "C-贷方")
                    {
                        yue.yesterday_balance = String.Format("{0:F}", Convert.ToDecimal(dt.Rows[i][13].ToString()) - Convert.ToDecimal(dt.Rows[i][11].ToString()));
                        break;
                    }
                    else if (dt.Rows[i][12].ToString() == "D-借方")
                    {
                        yue.yesterday_balance = String.Format("{0:F}", Convert.ToDecimal(dt.Rows[i][13].ToString()) + Convert.ToDecimal(dt.Rows[i][11].ToString()));
                        break;
                    }
                }


            }
            //判断本日收入/支出
            System.Decimal income = 0, pay = 0;
            for (int i = 0; i < rows; i++)
            {
                string datebalance = Convert.ToDateTime(dt.Rows[i][8].ToString()).ToString("yyyyMMdd");

                if (datebalance == date1)
                {
                    if (dt.Rows[i][12].ToString() == "C-贷方")
                    {
                        income = income + Convert.ToDecimal(dt.Rows[i][11].ToString());

                    }
                    else if (dt.Rows[i][12].ToString() == "D-借方")
                    {
                        pay = pay + Convert.ToDecimal(dt.Rows[i][11].ToString());
                    }
                }
            }
            yue.income_money = income.ToString();
            yue.pay_money = pay.ToString();
            //本期余额
            yue.balance = dt.Rows[rows - 1][13].ToString();

            yue.fm_code = "156";//默认值
            yue.fm_name = "人民币";//默认值
            yue.rg_code = account.rg_code;
            yue.year = DateTime.ParseExact(yue.report_date, "yyyyMMdd", null).AddDays(-1).Year.ToString();

            datarow = yue.account_no + "," + yue.account_name + "," + yue.account_bank + "," + yue.account_bank_code + "," +
                yue.banktype_code + "," + yue.banktype_name + "," + yue.report_date + "," + yue.yesterday_balance + "," +
                yue.income_money + "," + yue.pay_money + "," + yue.balance + "," + yue.fm_code + "," + yue.fm_name + "," + yue.rg_code + "," + yue.year;

            return datarow;
        }
        /// <summary>
        /// 合成余额文本:无流水
        /// </summary>
        /// <param name="agency">机构信息</param>
        /// <param name="account">账户信息</param>
        /// <param name="datetime">传入数据日期</param>
        /// <param name="yesterday">上日余额</param>
        /// <param name="today">本日余额</param>
        /// <returns></returns>
        private static string cjyuedata(AgencyInformation_Entity agency, account_Entity account, string datetime, string yesterday, string today)
        {

            string datarow = null;

            yue_Entity yue = new yue_Entity();
            yue.account_no = account.account_no;
            yue.account_name = account.account_name;
            yue.account_bank = agency.account_bank;
            yue.account_bank_code = agency.account_bank_code;
            yue.banktype_code = agency.banktype_code;
            yue.banktype_name = agency.banktype_name;
            yue.report_date = Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
            yue.yesterday_balance = yesterday;
            yue.income_money = "0";
            yue.pay_money = "0";
            yue.balance = today;
            yue.fm_code = "156";//默认值
            yue.fm_name = "人民币";//默认值
            yue.rg_code = account.rg_code;
            yue.year = Convert.ToDateTime(datetime).Year.ToString();

            datarow = yue.account_no + "," + yue.account_name + "," + yue.account_bank + "," + yue.account_bank_code + "," +
                yue.banktype_code + "," + yue.banktype_name + "," + yue.report_date + "," + yue.yesterday_balance + "," +
                yue.income_money + "," + yue.pay_money + "," + yue.balance + "," + yue.fm_code + "," + yue.fm_name + "," + yue.rg_code + "," + yue.year;

            return datarow;
        }

    }
    public class hnsProcess
    {

        Encoding encoding;
        /// <summary>
        /// 缓存文件地址
        /// </summary>
        static string path = System.Web.HttpContext.Current.Server.MapPath("/") + "temp";
        /// <summary>
        /// 数据入库合成
        /// </summary>
        /// <param name="filename">缓存文件名</param>
        /// <param name="liushuidata">是否生成水文件</param>
        /// <param name="yuedata">是否生成余额文件</param>
        /// <param name="AgencyNo">机构号</param>
        /// <param name="accountid">账户ID</param>
        /// <param name="upload">是否上传</param>
        /// <param name="datetime">数据日期</param>
        /// <param name="yesterday">上日余额</param>
        /// <param name="today">本日余额</param>
        public static string hnsdataProcess(string filename, string liushuidata, string yuedata, string AgencyNo, string accountid, string upload, string datetime, string yesterday, string today)
        {
            string log = null;//返回日志
            string filepath = path + "\\" + filename;
            string datapath = System.Web.HttpContext.Current.Server.MapPath("/") + "data";//明文目录
            string sendpath = System.Web.HttpContext.Current.Server.MapPath("/") + "send";//密文目录
            DataTable dt = hnsdt(filepath);
            System.IO.File.Delete(filepath);
            string liushuiname = null;//流水文件名
            string yuename = null;//余额文件名
            account_Entity account = account_BLL.GetaccountByID(accountid);//当前账户信息
            AgencyInformation_Entity agency = AgencyInformation_BLL.GetAgencyInfoByAgencyNo1(AgencyNo);//当前机构信息
            FTPInformation_Entity fTP = FTPInformation_BLL.GetFTPInfoByAgencyNo1(AgencyNo);//机构FTP信息
            //判断文件处理
            try
            {
                //处理流水文件
                if (liushuidata == "1")//是否生成流水
                {
                    string ls = hnslsdata(dt, agency, account);//流水数据
                    //当天流水次日上传
                    string name = agency.banktype_code.ToString() + "1" + account.type_code.ToString() + account.rg_code.ToString() + Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
                    string dataname = null;
                    //判断文件是否存在
                    for (int i = 1; i <= 9999; i++)
                    {
                        dataname = name + string.Format("{0:D4}", i);
                        if (File.Exists(datapath + "\\" + dataname + ".txt"))
                        {
                            //i++;
                        }
                        else
                        {
                            liushuiname = datapath + "\\" + name + string.Format("{0:D4}", i) + ".txt";
                            break;
                        }
                    }
                    //写入流水文件
                    FileStream fs = new FileStream(liushuiname, FileMode.CreateNew, FileAccess.ReadWrite);
                    fs.Close();
                    //判断流水文件是否生成
                    if (File.Exists(liushuiname))
                    {
                        File.WriteAllText(liushuiname, ls, Encoding.UTF8);
                        string lslog = "生成流水明文成功。";
                        log = lslog;
                        //添加日志表
                        logs_Entity logs = new logs_Entity();
                        logs.AgencyNo = AgencyNo;
                        logs.inputTime = DateTime.Now;
                        logs.logName = "生成流水明文：" + dataname;
                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                        logs.succeed = "1";
                        logs_BLL.Createlog(logs);
                        logs = null;
                        //添加数据表
                        data_Entity mingwen = new data_Entity();
                        mingwen.AgencyNo = AgencyNo;
                        mingwen.dataName = dataname;
                        mingwen.datatype = "1";
                        mingwen.data_Position = "data";
                        mingwen.inputTime = Convert.ToDateTime(DateTime.Now);
                        mingwen.upload = "0";
                        data_BLL.Createdata(mingwen);
                        //判断是否上传
                        if (upload == "1")
                        {
                            data_Entity miwen = new data_Entity();
                            miwen.AgencyNo = AgencyNo;
                            miwen.dataName = dataname;
                            miwen.datatype = "0";
                            miwen.data_Position = "send";
                            miwen.inputTime = Convert.ToDateTime(DateTime.Now);
                            string mingpath = liushuiname;
                            string mipath = sendpath + "\\" + dataname + ".txt";
                            string a = cj3des.cj3des.main(mingpath, mipath, fTP.data_Key);
                            //判断是否加密成功
                            if (a == "1")
                            {
                                lslog = "生成流水密文成功。";
                                log = log + lslog;
                                logs = new logs_Entity();
                                logs.AgencyNo = AgencyNo;
                                logs.inputTime = DateTime.Now;
                                logs.logName = "生成流水密文：" + dataname;
                                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                logs.succeed = "1";
                                logs_BLL.Createlog(logs);
                                logs = null;
                                FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
                                ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(agency.AgencyNo);
                                FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress, ftpinfo.FTPUsername, ftpinfo.FTPPassword);
                                FileInfo file = new FileInfo(mipath);
                                //上传加密文件
                                bool i = ftp.Upload(file, dataname + ".txt");
                                if (i)
                                {
                                    lslog = "上传流水密文成功。";
                                    log = log + lslog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "上传流水密文：" + dataname;
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "1";
                                    logs_BLL.Createlog(logs);
                                    logs = null;
                                    miwen.upload = "1";
                                    data_BLL.Createdata(miwen);
                                }
                                else
                                {
                                    miwen.upload = "0";
                                    data_BLL.Createdata(miwen);
                                    lslog = "上传流水密文不成功。";
                                    log = log + lslog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "上传流水密文：" + dataname + "不成功";
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "0";
                                    logs_BLL.Createlog(logs);
                                    logs = null;
                                }
                            }
                            else
                            {
                                lslog = "生成流水密文不成功。";
                                log = log + lslog;
                                logs = new logs_Entity();
                                logs.AgencyNo = AgencyNo;
                                logs.inputTime = DateTime.Now;
                                logs.logName = "生成流水密文：" + dataname + "不成功";
                                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                logs.succeed = "0";
                                logs_BLL.Createlog(logs);
                                logs = null;
                            }
                        }
                    }
                    else
                    {
                        string lslog = "生成流水明文不成功。";
                        log = lslog;
                        //添加日志表
                        logs_Entity logs = new logs_Entity();
                        logs.AgencyNo = AgencyNo;
                        logs.inputTime = DateTime.Now;
                        logs.logName = "生成流水明文：" + dataname + "不成功";
                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                        logs.succeed = "0";
                        logs_BLL.Createlog(logs);
                        logs = null;
                    }
                }
                //处理余额文件
                if (yuedata == "1")//是否生成余额
                {
                    //当天余额次日上传
                    string name = agency.banktype_code.ToString() + "0" + account.type_code.ToString() + account.rg_code.ToString() + Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
                    string dataname = null;
                    //判断文件是否存在
                    for (int i = 1; i <= 9999; i++)
                    {
                        dataname = name + string.Format("{0:D4}", i);
                        if (File.Exists(datapath + "\\" + dataname + ".txt"))
                        {
                            //i++;
                        }
                        else
                        {
                            yuename = datapath + "\\" + name + string.Format("{0:D4}", i) + ".txt";
                            break;
                        }
                    }
                    //流水文件已上传时
                    if (liushuidata == "1")
                    {
                        string yue = hnsyuedata(dt, agency, account, datetime);//余额数据：有流水
                        FileStream fs = new FileStream(yuename, FileMode.CreateNew, FileAccess.ReadWrite);
                        fs.Close();
                        //判断余额文件是否存在
                        if (File.Exists(yuename))
                        {
                            File.WriteAllText(yuename, yue, Encoding.UTF8);
                            string yuelog = "生成余额明文成功。";
                            log = log + yuelog;
                            logs_Entity logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额明文：" + dataname;
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "1";
                            logs_BLL.Createlog(logs);
                            logs = null;
                            data_Entity mingwen = new data_Entity();
                            mingwen.AgencyNo = AgencyNo;
                            mingwen.dataName = dataname;
                            mingwen.datatype = "1";
                            mingwen.data_Position = "data";
                            mingwen.inputTime = Convert.ToDateTime(DateTime.Now);
                            mingwen.upload = "0";
                            data_BLL.Createdata(mingwen);
                            //判断是否上传
                            if (upload == "1")
                            {
                                data_Entity miwen = new data_Entity();
                                miwen.AgencyNo = AgencyNo;
                                miwen.dataName = dataname;
                                miwen.datatype = "0";
                                miwen.data_Position = "send";
                                miwen.inputTime = Convert.ToDateTime(DateTime.Now);
                                string mingpath = yuename;
                                string mipath = sendpath + "\\" + dataname + ".txt";
                                string a = cj3des.cj3des.main(mingpath, mipath, fTP.data_Key);
                                if (a == "1")
                                {
                                    yuelog = "生成余额密文成功。";
                                    log = log + yuelog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "生成余额密文：" + dataname;
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "1";
                                    logs_BLL.Createlog(logs);
                                    logs = null;
                                    FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
                                    ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(agency.AgencyNo);
                                    FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress, ftpinfo.FTPUsername, ftpinfo.FTPPassword);
                                    FileInfo file = new FileInfo(mipath);
                                    //上传加密文件
                                    bool i = ftp.Upload(file, dataname + ".txt");
                                    if (i)
                                    {
                                        yuelog = "上传余额密文成功。";
                                        log = log + yuelog;
                                        logs = new logs_Entity();
                                        logs.AgencyNo = AgencyNo;
                                        logs.inputTime = DateTime.Now;
                                        logs.logName = "上传余额密文：" + dataname;
                                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                        logs.succeed = "1";
                                        logs_BLL.Createlog(logs);
                                        logs = null;
                                        miwen.upload = "1";
                                        data_BLL.Createdata(miwen);
                                    }
                                    else
                                    {
                                        yuelog = "上传余额密文不成功。";
                                        log = log + yuelog;
                                        logs = new logs_Entity();
                                        logs.AgencyNo = AgencyNo;
                                        logs.inputTime = DateTime.Now;
                                        logs.logName = "上传余额密文：" + dataname + "不成功";
                                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                        logs.succeed = "1";
                                        logs_BLL.Createlog(logs);
                                        logs = null;
                                        miwen.upload = "0";
                                        data_BLL.Createdata(miwen);
                                    }
                                }
                                else
                                {
                                    yuelog = "生成余额密文不成功。";
                                    log = log + yuelog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "生成余额密文：" + dataname + "不成功";
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "0";
                                    logs_BLL.Createlog(logs);
                                    logs = null;
                                }
                            }
                        }
                        else
                        {
                            string yuelog = "生成余额明文不成功。";
                            log = log + yuelog;
                            logs_Entity logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额明文：" + dataname + "不成功";
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "0";
                            logs_BLL.Createlog(logs);
                            logs = null;
                        }
                    }
                    //未上传流水文件
                    else
                    {
                        string yuenols = hnsyuedata(agency, account, datetime, yesterday, today);//余额数据：无流水
                        FileStream fs = new FileStream(yuename, FileMode.CreateNew, FileAccess.ReadWrite);
                        fs.Close();
                        if (File.Exists(yuename))
                        {
                            File.WriteAllText(yuename, yuenols, Encoding.UTF8);
                            string yuelog = "生成余额明文成功。";
                            log = log + yuelog;
                            logs_Entity logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额明文：" + dataname;
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "1";
                            logs_BLL.Createlog(logs);
                            logs = null;
                            data_Entity mingwen = new data_Entity();
                            mingwen.AgencyNo = AgencyNo;
                            mingwen.dataName = dataname;
                            mingwen.datatype = "1";
                            mingwen.data_Position = "data";
                            mingwen.inputTime = Convert.ToDateTime(DateTime.Now);
                            mingwen.upload = "0";
                            data_BLL.Createdata(mingwen);
                            //判断是否上传
                            if (upload == "1")
                            {
                                data_Entity miwen = new data_Entity();
                                miwen.AgencyNo = AgencyNo;
                                miwen.dataName = dataname;
                                miwen.datatype = "0";
                                miwen.data_Position = "send";
                                miwen.inputTime = Convert.ToDateTime(DateTime.Now);
                                string mingpath = yuename;
                                string mipath = sendpath + "\\" + dataname + ".txt";
                                string a = cj3des.cj3des.main(mingpath, mipath, fTP.data_Key);
                                if (a == "1")
                                {
                                    yuelog = "生成余额密文成功。";
                                    log = log + yuelog;
                                    logs = new logs_Entity();
                                    logs.AgencyNo = AgencyNo;
                                    logs.inputTime = DateTime.Now;
                                    logs.logName = "生成余额密文：" + dataname;
                                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    logs.succeed = "1";
                                    logs_BLL.Createlog(logs);
                                    logs = null;
                                    FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
                                    ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(agency.AgencyNo);
                                    FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress, ftpinfo.FTPUsername, ftpinfo.FTPPassword);
                                    FileInfo file = new FileInfo(mipath);
                                    //上传加密文件
                                    bool i = ftp.Upload(file, dataname + ".txt");
                                    if (i)
                                    {
                                        yuelog = "上传余额密文成功。";
                                        log = log + yuelog;
                                        logs = new logs_Entity();
                                        logs.AgencyNo = AgencyNo;
                                        logs.inputTime = DateTime.Now;
                                        logs.logName = "上传余额密文：" + dataname;
                                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                        logs.succeed = "1";
                                        logs_BLL.Createlog(logs);
                                        logs = null;
                                        miwen.upload = "1";
                                        data_BLL.Createdata(miwen);
                                    }
                                    else
                                    {
                                        yuelog = "上传余额密文不成功。";
                                        log = log + yuelog;
                                        logs = new logs_Entity();
                                        logs.AgencyNo = AgencyNo;
                                        logs.inputTime = DateTime.Now;
                                        logs.logName = "上传余额密文：" + dataname + "不成功";
                                        logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                        logs.succeed = "1";
                                        logs_BLL.Createlog(logs);
                                        logs = null;
                                        miwen.upload = "0";
                                        data_BLL.Createdata(miwen);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string yuelog = "生成余额明文不成功。";
                            log = log + yuelog;
                            logs_Entity logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额明文：" + dataname + "不成功";
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "0";
                            logs_BLL.Createlog(logs);
                            logs = null;
                        }
                    }
                }
                return log;
            }
            catch (Exception ex)
            {
                string a = log + ex.ToString();
                return a;
            }
        }
        /// <summary>
        /// 数据入库合成
        /// </summary>
        /// <param name="filename">缓存文件名</param>
        /// <param name="liushuidata">是否生成水文件</param>
        /// <param name="yuedata">是否生成余额文件</param>
        /// <param name="AgencyNo">机构号</param>
        /// <param name="accountid">账户ID</param>
        /// <param name="upload">是否上传</param>
        /// <param name="datetime">数据日期</param>
        /// <param name="yesterday">上日余额</param>
        /// <param name="today">本日余额</param>
        public static string hnsdataProcess(string AgencyNo, string accountid, string upload, string datetime, string yesterday, string today)
        {
            string log = null;//返回日志
            string datapath = System.Web.HttpContext.Current.Server.MapPath("/") + "data";//明文目录
            string sendpath = System.Web.HttpContext.Current.Server.MapPath("/") + "send";//密文目录
            string yuename = null;//余额文件名
            account_Entity account = account_BLL.GetaccountByID(accountid);//当前账户信息
            AgencyInformation_Entity agency = AgencyInformation_BLL.GetAgencyInfoByAgencyNo1(AgencyNo);//当前机构信息
            FTPInformation_Entity fTP = FTPInformation_BLL.GetFTPInfoByAgencyNo1(AgencyNo);//机构FTP信息

            //判断文件处理
            try
            {
                //当天余额次日上传
                string name = agency.banktype_code.ToString() + "0" + account.type_code.ToString() + account.rg_code.ToString() + Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
                string dataname = null;
                //判断文件是否存在
                for (int i = 1; i <= 9999; i++)
                {
                    dataname = name + string.Format("{0:D4}", i);
                    if (File.Exists(datapath + "\\" + dataname + ".txt"))
                    {
                        //i++;
                    }
                    else
                    {
                        yuename = datapath + "\\" + name + string.Format("{0:D4}", i) + ".txt";
                        break;
                    }
                }
                string yuenols = hnsyuedata(agency, account, datetime, yesterday, today);//余额数据：无流水
                FileStream fs = new FileStream(yuename, FileMode.CreateNew, FileAccess.ReadWrite);
                fs.Close();
                if (File.Exists(yuename))
                {
                    File.WriteAllText(yuename, yuenols, Encoding.UTF8);
                    string yuelog = "生成余额明文成功。";
                    log = log + yuelog;
                    logs_Entity logs = new logs_Entity();
                    logs.AgencyNo = AgencyNo;
                    logs.inputTime = DateTime.Now;
                    logs.logName = "生成余额明文：" + dataname;
                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                    logs.succeed = "1";
                    logs_BLL.Createlog(logs);
                    logs = null;
                    data_Entity mingwen = new data_Entity();
                    mingwen.AgencyNo = AgencyNo;
                    mingwen.dataName = dataname;
                    mingwen.datatype = "1";
                    mingwen.data_Position = "data";
                    mingwen.inputTime = Convert.ToDateTime(DateTime.Now);
                    mingwen.upload = "0";
                    data_BLL.Createdata(mingwen);
                    //判断是否上传
                    if (upload == "1")
                    {
                        data_Entity miwen = new data_Entity();
                        miwen.AgencyNo = AgencyNo;
                        miwen.dataName = dataname;
                        miwen.datatype = "0";
                        miwen.data_Position = "send";
                        miwen.inputTime = Convert.ToDateTime(DateTime.Now);
                        string mingpath = yuename;
                        string mipath = sendpath + "\\" + dataname + ".txt";
                        string a = cj3des.cj3des.main(mingpath, mipath, fTP.data_Key);
                        if (a == "1")
                        {
                            yuelog = "生成余额密文成功。";
                            log = log + yuelog;
                            logs = new logs_Entity();
                            logs.AgencyNo = AgencyNo;
                            logs.inputTime = DateTime.Now;
                            logs.logName = "生成余额密文：" + dataname;
                            logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                            logs.succeed = "1";
                            logs_BLL.Createlog(logs);
                            logs = null;
                            FTPInformation_Entity ftpinfo = new FTPInformation_Entity();
                            ftpinfo = FTPInformation_BLL.GetFTPInfoByAgencyNo1(agency.AgencyNo);
                            FtpHelper ftp = new FtpHelper(ftpinfo.FTPAddress, ftpinfo.FTPUsername, ftpinfo.FTPPassword);
                            FileInfo file = new FileInfo(mipath);
                            //上传加密文件
                            bool i = ftp.Upload(file, dataname + ".txt");
                            if (i)
                            {
                                yuelog = "上传余额密文成功。";
                                log = log + yuelog;
                                logs = new logs_Entity();
                                logs.AgencyNo = AgencyNo;
                                logs.inputTime = DateTime.Now;
                                logs.logName = "上传余额密文：" + dataname;
                                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                logs.succeed = "1";
                                logs_BLL.Createlog(logs);
                                logs = null;
                                miwen.upload = "1";
                                data_BLL.Createdata(miwen);
                            }
                            else
                            {
                                yuelog = "上传余额密文不成功。";
                                log = log + yuelog;
                                logs = new logs_Entity();
                                logs.AgencyNo = AgencyNo;
                                logs.inputTime = DateTime.Now;
                                logs.logName = "上传余额密文：" + dataname + "不成功";
                                logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                                logs.succeed = "1";
                                logs_BLL.Createlog(logs);
                                logs = null;
                                miwen.upload = "0";
                                data_BLL.Createdata(miwen);
                            }
                        }
                    }
                }
                else
                {
                    string yuelog = "生成余额明文不成功。";
                    log = log + yuelog;
                    logs_Entity logs = new logs_Entity();
                    logs.AgencyNo = AgencyNo;
                    logs.inputTime = DateTime.Now;
                    logs.logName = "生成余额明文：" + dataname + "不成功";
                    logs.log_No = DateTime.Now.ToString("yyyyMMddhhmmss");
                    logs.succeed = "0";
                    logs_BLL.Createlog(logs);
                    logs = null;
                }
                return log;
            }
            catch (Exception ex)
            {
                string a = log + ex.ToString();
                return a;
            }
        }

        /// <summary>
        /// 读入流水excel表
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static DataTable hnsdt(string filepath)
        {
            try
            {
                IWorkbook workbook = null;
                FileStream file = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                //判断版本
                if (filepath.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook = new XSSFWorkbook(file);  //xlsx数据读入workbook
                }
                else if (filepath.IndexOf(".xls") > 0) // 2003版本
                {
                    workbook = new HSSFWorkbook(file);  //xls数据读入workbook
                }
                ISheet sheet = workbook.GetSheetAt(0);
                //设置隐藏列 为 不隐藏
                for (int iHide = 0; iHide <= 10; iHide++)
                {
                    sheet.SetColumnHidden(iHide, false);
                }
                //最后一列的标号(即总的行数)
                int rowCount = sheet.LastRowNum;

                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                DataTable dt = new DataTable();

                IRow headerRow = sheet.GetRow(3);//列标题
                int cellCount = headerRow.LastCellNum;//行单元格数
                int rfirst = sheet.FirstRowNum;//首行号hns为第4行
                int rlast = sheet.LastRowNum;//末行号
                IRow row = sheet.GetRow(rfirst+3);//首行数据
                int cfirst = row.FirstCellNum;//首单元格号
                int clast = row.LastCellNum;//末单元格号
                //单行表头
                for (int i = cfirst; i < clast; i++)
                {
                    if (row.GetCell(i) != null)
                        dt.Columns.Add(row.GetCell(i).StringCellValue, System.Type.GetType("System.String"));
                }
                // 循环行数据，第5行开始到倒数第4行
                row = null;
                for (int i = rfirst + 4; i <= rlast-3; i++)
                {
                    DataRow r = dt.NewRow();
                    IRow ir = sheet.GetRow(i);
                    for (int j = cfirst; j < clast; j++)
                    {
                        if (ir.GetCell(j) != null)
                        {
                            r[j] = ir.GetCell(j).ToString();
                        }
                    }
                    dt.Rows.Add(r);
                    ir = null;
                    r = null;
                }
                sheet = null;
                workbook = null;
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 合成流水数据文本
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="Agency">机构信息</param>
        /// <param name="account">账户信息</param>
        /// <returns></returns>
        private static string hnslsdata(DataTable dt, AgencyInformation_Entity Agency, account_Entity account)
        {
            int len = dt.Rows.Count;
            string datarow;
            string data = null;
            for (int i = 0; i < len; i++)
            {
                DataRow dr = dt.Rows[i];
                datarow = hnslsRow(dr, Agency, account);
                if (i == 0)
                {
                    data = datarow;
                }
                else
                {
                    data = data + "||" + datarow;

                }
            }
            return data;

        }
        /// <summary>
        /// 合成流水每行文本
        /// </summary>
        /// <param name="dr">流水数据行</param>
        /// <param name="agency">机构信息</param>
        /// <param name="account">账户信息</param>
        /// <returns></returns>
        private static string hnslsRow(DataRow dr, AgencyInformation_Entity agency, account_Entity account)
        {
            string datarow = null;
            liushui_Entity liushui = new liushui_Entity();

            liushui.account_no = dr[2].ToString();
            liushui.account_name = dr[1].ToString();
            liushui.account_bank = agency.account_bank;
            liushui.account_bank_code = agency.account_bank_code;
            liushui.banktype_code = agency.banktype_code;
            liushui.banktype_name = agency.banktype_name;
            liushui.bill_time =Regex.Replace( Regex.Replace(dr[6].ToString(), "-", ""),":","");
            liushui.bank_order_no = dr[14].ToString();
            //判断交易类型
            if (dr[10].ToString() == "2")
            {
                liushui.bill_type = "1";
            }
            else
            {
                if (dr[10].ToString() == "1")
                {
                    liushui.bill_type = "2";
                }
            }
            //判断交易凭证号
                liushui.bill_no = "000000";
            liushui.fm_code = "156";//默认值
            liushui.fm_name = "人民币";//默认值
            liushui.pay_money = Regex.Replace(dr[12].ToString(),",","");
            liushui.rg_code = account.rg_code;
            liushui.year = Convert.ToDateTime(dr[5].ToString()).Year.ToString();
            //判断收款账户账号
            if (string.IsNullOrEmpty(dr[16].ToString()))
            {
                if(string.IsNullOrEmpty(dr[17].ToString()))
                {
                liushui.other_account_no = "000000";
                }
                else
                {
                    liushui.other_account_no = dr[17].ToString();

                }

            }
            else
            {
                liushui.other_account_no = dr[16].ToString();
            }
            //判断收款账户名称
            if (string.IsNullOrEmpty(dr[15].ToString()))
            {
                liushui.other_account_name = "无收款账户名称";
            }
            else
            {
                liushui.other_account_name = dr[15].ToString();
            }
            //判断收款账户开户银行名称
            if(string.IsNullOrEmpty(dr[20].ToString()))
            {
            liushui.other_bank_name = "无收款账户开户银行名称";
            }
            else
            {
                liushui.other_bank_name = dr[20].ToString();

            }
            //判断用途
            if (string.IsNullOrEmpty(dr[28].ToString()))
            {
                liushui.summary = "无";
            }
            else
            {
                liushui.summary = dr[28].ToString();
            }
            //判断借贷方向
            if (dr[9].ToString() == "C")
            {
                liushui.Money_type = "0";
            }
            else
            if (dr[9].ToString() == "D")
            {
                liushui.Money_type = "1";
            }
            //合成一行数据
            datarow = liushui.account_no + "," + liushui.account_name + "," + liushui.account_bank + "," + liushui.account_bank_code + "," +
                liushui.banktype_code + "," + liushui.banktype_name + "," + liushui.bill_time + "," + liushui.bank_order_no + "," + liushui.bill_type
                + "," + liushui.bill_no + "," + liushui.fm_code + "," + liushui.fm_name + "," + liushui.pay_money + "," + liushui.rg_code
                + "," + liushui.year + "," + liushui.other_account_no + "," + liushui.other_account_name + "," + liushui.other_bank_name
                + "," + liushui.summary + "," + liushui.Money_type;

            return datarow;
        }

        /// <summary>
        /// 合成余额文本:有流水
        /// </summary>
        /// <param name="dt">流水数据表</param>
        /// <param name="agency">机构信息</param>
        /// <param name="account">账户信息</param>
        /// <param name="datetime">传入数据日期</param>
        /// <returns></returns>
        private static string hnsyuedata(DataTable dt, AgencyInformation_Entity agency, account_Entity account, string datetime)
        {
            int rows = dt.Rows.Count;
            string datarow = null;

            yue_Entity yue = new yue_Entity();
            yue.account_no = account.account_no;
            yue.account_name = account.account_name;
            yue.account_bank = agency.account_bank;
            yue.account_bank_code = agency.account_bank_code;
            yue.banktype_code = agency.banktype_code;
            yue.banktype_name = agency.banktype_name;

            //判断上期余额
            string date = Convert.ToDateTime(datetime).ToString("yyyyMMdd");//传入数据日期
            string date1 = Convert.ToDateTime(dt.Rows[rows - 1][5].ToString()).ToString("yyyyMMdd");
            if (date == date1)//判断传入日期与数据最后日期是否相同
            {
                yue.report_date = Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
            }
            else
            {
                yue.report_date = Convert.ToString(DateTime.ParseExact(date1, "yyyyMMdd",null).AddDays(1).ToString("yyyyMMdd"));
            }
            for (int i = 0; i < rows; i++)
            {
                string datebalance = Convert.ToDateTime(dt.Rows[i][5].ToString()).ToString("yyyyMMdd");
                if (datebalance == date1)
                {
                    if (dt.Rows[i][9].ToString() == "C")
                    {
                        yue.yesterday_balance = String.Format("{0:F}", Convert.ToDecimal(dt.Rows[i][13].ToString()) - Convert.ToDecimal(dt.Rows[i][12].ToString()));
                        break;
                    }
                    else if (dt.Rows[i][9].ToString() == "D")
                    {
                        yue.yesterday_balance = String.Format("{0:F}", Convert.ToDecimal(dt.Rows[i][13].ToString()) + Convert.ToDecimal(dt.Rows[i][12].ToString()));
                        break;
                    }
                }


            }
            //判断本日收入/支出
            System.Decimal income = 0, pay = 0;
            for (int i = 0; i < rows; i++)
            {
                string datebalance = Convert.ToDateTime(dt.Rows[i][5].ToString()).ToString("yyyyMMdd");

                if (datebalance == date1)
                {
                    if (dt.Rows[i][9].ToString() == "C")
                    {
                        income = income + Convert.ToDecimal(dt.Rows[i][12].ToString());

                    }
                    else if (dt.Rows[i][9].ToString() == "D")
                    {
                        pay = pay + Convert.ToDecimal(dt.Rows[i][12].ToString());
                    }
                }
            }
            yue.income_money = Regex.Replace(income.ToString(),",","");
            yue.pay_money = Regex.Replace(pay.ToString(),",","");
            //本期余额
            yue.balance = Regex.Replace(dt.Rows[rows - 1][13].ToString(),",","");

            yue.fm_code = "156";//默认值
            yue.fm_name = "人民币";//默认值
            yue.rg_code = account.rg_code;
            yue.year = DateTime.ParseExact(yue.report_date, "yyyyMMdd",null).AddDays(-1).Year.ToString();

            datarow = yue.account_no + "," + yue.account_name + "," + yue.account_bank + "," + yue.account_bank_code + "," +
                yue.banktype_code + "," + yue.banktype_name + "," + yue.report_date + "," + yue.yesterday_balance + "," +
                yue.income_money + "," + yue.pay_money + "," + yue.balance + "," + yue.fm_code + "," + yue.fm_name + "," + yue.rg_code + "," + yue.year;

            return datarow;
        }
        /// <summary>
        /// 合成余额文本:无流水
        /// </summary>
        /// <param name="agency">机构信息</param>
        /// <param name="account">账户信息</param>
        /// <param name="datetime">传入数据日期</param>
        /// <param name="yesterday">上日余额</param>
        /// <param name="today">本日余额</param>
        /// <returns></returns>
        private static string hnsyuedata(AgencyInformation_Entity agency, account_Entity account, string datetime, string yesterday, string today)
        {

            string datarow = null;

            yue_Entity yue = new yue_Entity();
            yue.account_no = account.account_no;
            yue.account_name = account.account_name;
            yue.account_bank = agency.account_bank;
            yue.account_bank_code = agency.account_bank_code;
            yue.banktype_code = agency.banktype_code;
            yue.banktype_name = agency.banktype_name;
            yue.report_date = Convert.ToString(Convert.ToDateTime(datetime).AddDays(1).ToString("yyyyMMdd"));
            yue.yesterday_balance = yesterday;
            yue.income_money = "0";
            yue.pay_money = "0";
            yue.balance = today;
            yue.fm_code = "156";//默认值
            yue.fm_name = "人民币";//默认值
            yue.rg_code = account.rg_code;
            yue.year = Convert.ToDateTime(datetime).Year.ToString();

            datarow = yue.account_no + "," + yue.account_name + "," + yue.account_bank + "," + yue.account_bank_code + "," +
                yue.banktype_code + "," + yue.banktype_name + "," + yue.report_date + "," + yue.yesterday_balance + "," +
                yue.income_money + "," + yue.pay_money + "," + yue.balance + "," + yue.fm_code + "," + yue.fm_name + "," + yue.rg_code + "," + yue.year;

            return datarow;
        }
    }
}


