using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace datareporter.Entity
{
    public class UserInformation_Entity
    {
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 机构号
        /// </summary>
        public string AgencyNo { get; set; }

    }
    /// <summary>
    /// 机构信息：开户银行
    /// </summary>
    public class AgencyInformation_Entity
    {
        public int ID { get; set; }
        /// <summary>
        /// 内部机构号
        /// </summary>
        public string AgencyNo { get; set; }
        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string account_bank { get; set; }
        /// <summary>
        /// 开户银行行号
        /// </summary>
        public string account_bank_code { get; set; }
        /// <summary>
        /// 开户银行类别编码
        /// </summary>
        public string banktype_code { get; set; }
        /// <summary>
        /// 开户银行类别名称
        /// </summary>
        public string banktype_name { get; set; }
    }
    public class FTPInformation_Entity
    {
        public int ID { get; set; }
        /// <summary>
        /// FTP地址
        /// </summary>
        public string FTPAddress { get; set; }
        /// <summary>
        /// FTP登陆用户名
        /// </summary>
        public string FTPUsername { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string FTPPassword { get; set; }
        /// <summary>
        /// des3加密密钥
        /// </summary>
        public string data_Key { get; set; }
        /// <summary>
        /// 机构号
        /// </summary>
        public string AgencyNo { get; set; }
    }
    /// <summary>
    /// 数据存放
    /// </summary>
    public class data_Entity
    {
        public int ID { get; set; }
        /// <summary>
        /// 数据文件名称
        /// </summary>
        public string dataName { get; set; }
        /// <summary>
        /// 文件位置
        /// </summary>
        public string data_Position { get; set; }
        /// <summary>
        /// 机构号
        /// </summary>
        public string AgencyNo { get; set; }
        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime inputTime { get; set; }
        /// <summary>
        /// 数据类型：1为明文，0为密文
        /// </summary>
        public string datatype { get; set; }
        /// <summary>
        /// 是否上传：1为已上传，0为未上传
        /// </summary>
        public string upload { get; set; }
    }
    /// <summary>
    /// 数据日志
    /// </summary>
    public class logs_Entity
    {
        public int ID { get; set; }
        /// <summary>
        /// 日志编号：日期+时间
        /// </summary>
        public string log_No { get; set; }
        /// <summary>
        /// 日志名称：文件名
        /// </summary>
        public string logName { get; set; }
        /// <summary>
        /// 导入时间
        /// </summary>
        public DateTime inputTime { get; set; }
        /// <summary>
        /// 1为成功||0为失败
        /// </summary>
        public string succeed { get; set; }
        /// <summary>
        /// 机构号
        /// </summary>
        public string AgencyNo { get; set; }
    }
    public class admin_Entity
    {
        public int ID { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

    }
    public class account_Entity
    {
        public int ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string account_no { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string account_name { get; set; }
        /// <summary>
        /// 区划码
        /// </summary>
        public string rg_code { get; set; }
        /// <summary>
        /// 账户类别编码
        /// </summary>
        public string type_code { get; set; }
        /// <summary>
        /// 账户类别名称
        /// </summary>
        public string type_name { get; set; }
        /// <summary>
        /// 账户所属机构号
        /// </summary>
        public string AgencyNo { get; set; }
    }
    public class liushui_Entity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string account_no { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string account_name { get; set; }
        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string account_bank { get; set; }
        /// <summary>
        /// 开户银行名称编码
        /// </summary>
        public string account_bank_code { get; set; }
        /// <summary>
        /// 开户银行类别编码
        /// </summary>
        public string banktype_code { get; set; }
        /// <summary>
        /// 开户银行类别名称
        /// </summary>
        public string banktype_name { get; set; }
        /// <summary>
        /// 交易发生时间：[YYYYMMDD HHMMSS]时间取不了的为[000000]
        /// </summary>
        public string bill_time { get; set; }
        /// <summary>
        /// 银行交易流水号
        /// </summary>
        public string bank_order_no { get; set; }
        /// <summary>
        /// 交易类型：[1转账，2现金]
        /// </summary>
        public string bill_type { get; set; }
        /// <summary>
        /// 交易凭证号：无法提供则为[000000]
        /// </summary>
        public string bill_no { get; set; }
        /// <summary>
        /// 币种编码：156人民币
        /// </summary>
        public string fm_code { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string fm_name { get; set; }
        /// <summary>
        /// 本笔支出/收入：income_money在流水类型区分收支
        /// </summary>
        public string pay_money { get; set; }
        /// <summary>
        /// 区划编码
        /// </summary>
        public string rg_code { get; set; }
        /// <summary>
        /// 业务年度
        /// </summary>
        public string year { get; set; }
        /// <summary>
        /// 收款账户账号：取不到则为[000000]
        /// </summary>
        public string other_account_no { get; set; }
        /// <summary>
        /// 收款账户名称：取不到则为[无收款账户名称]
        /// </summary>
        public string other_account_name { get; set; }
        /// <summary>
        /// 收款账户开户银行名称：取不到则为[无收款账户开户银行名称]
        /// </summary>
        public string other_bank_name { get; set; }
        /// <summary>
        /// 用途：不存在时为[无]
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 流水类型：1为支出，0为收入
        /// </summary>
        public string Money_type { get; set; }

    }
    public class yue_Entity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string account_no { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string account_name { get; set; }
        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string account_bank { get; set; }
        /// <summary>
        /// 开户银行名称编码
        /// </summary>
        public string account_bank_code { get; set; }
        /// <summary>
        /// 开户银行类别编码
        /// </summary>
        public string banktype_code { get; set; }
        /// <summary>
        /// 开户银行类别名称
        /// </summary>
        public string banktype_name { get; set; }
        /// <summary>
        /// 报告日期：[YYYYMMDD]
        /// </summary>
        public string report_date { get; set; }
        /// <summary>
        /// 上期余额
        /// </summary>
        public string yesterday_balance { get; set; }
        /// <summary>
        /// 本日收入
        /// </summary>
        public string income_money { get; set; }
        /// <summary>
        /// 本日支出
        /// </summary>
        public string pay_money { get; set; }
        /// <summary>
        /// 本期余额
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 币种编码
        /// </summary>
        public string fm_code { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string fm_name { get; set; }
        /// <summary>
        /// 区划编码
        /// </summary>
        public string rg_code { get; set; }
        /// <summary>
        /// 业务年度
        /// </summary>
        public string year { get; set; }
    }
    public class TableInfo //返回给前端的json格式
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }
    }
    public class MD5
    {
        public static string EnctryByMD51(string toCryString)
        {
            toCryString = FormsAuthentication.HashPasswordForStoringInConfigFile(toCryString, "md5");
            return FormsAuthentication.HashPasswordForStoringInConfigFile(toCryString, "md5");
        }
    }
}
