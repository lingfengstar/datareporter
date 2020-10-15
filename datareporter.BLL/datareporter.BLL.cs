using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datareporter.BLL
{
    public class UserInformation_BLL
    {
        /// <summary>
                /// 用户登录验证
                /// </summary>
                /// <param name="userName">用户名</param>
                /// <param name="userPassword">密码</param>
                /// <returns>布尔值True成功</returns>
        public static bool UserLogin(string userName, string userPassword)
        {
            userPassword = Entity.MD5.EnctryByMD51(userPassword);
            return datareporter.DAL.UserInformation_DAL.UserLogin(userName, userPassword);
        }

        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="ui">用户信息实体</param>
        /// <returns>用户编号</returns>
        public static int CreateUserInfo(datareporter.Entity.UserInformation_Entity ui)
        {
            ui.UserPassword = Entity.MD5.EnctryByMD51(ui.UserPassword);
            return datareporter.DAL.UserInformation_DAL.CreateUserInfo(ui);
        }
        /// <summary>
        /// 获取某用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>用户信息实体</returns>
        public static Entity.UserInformation_Entity GetUserInfoById(string id)
        {
            return DAL.UserInformation_DAL.GetUserInfoById(id);
        }
        /// <summary>
                /// 获取用户所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.UserInformation_Entity> GetAllUserInfo()
        {

            return DAL.UserInformation_DAL.GetAllUserInfo();
        }

        /// <summary>
        /// 获取用户所有信息同上，不同的是不是调用的存储过程，而是直接拼写的SQL
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAllUserInfoBySql()
        {

            return datareporter.DAL.UserInformation_DAL.GetAllUserInfoBySql();

        }

        /// <summary>
        /// 获取某用户信息
        /// </summary>
        /// <param name="empName">用户Name</param>
        /// <returns>用户信息实体</returns>
        public static Entity.UserInformation_Entity GetUserInfoByName(string empName)
        {
            return DAL.UserInformation_DAL.GetUserInfoByName(empName);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="ui">用户实体</param>
        /// <returns>影响行数</returns>
        public static int UpdateUserInfo(Entity.UserInformation_Entity ui)
        {
            ui.UserPassword = Entity.MD5.EnctryByMD51(ui.UserPassword);
            return DAL.UserInformation_DAL.UpdateUserInfo(ui);
        }
    }
    public class AgencyInformation_BLL
    {
        public static  int CreateAgencyInfo(Entity.AgencyInformation_Entity ai)
        {
            return DAL.AgencyInformation_DAL.CreateAgencyInfo(ai);
        }
        public static List <Entity.AgencyInformation_Entity>GetAllAgencyInfo()
        {
            return DAL.AgencyInformation_DAL.GetAllAgencyInfo();
        }
        public static List< Entity.AgencyInformation_Entity> GetAgencyInfoByAgencyNo(string no)
        {
            return DAL.AgencyInformation_DAL.GetAgencyInfoByAgencyNo(no);
        }
        public static Entity.AgencyInformation_Entity GetAgencyInfoByAgencyNo1(string no)
        {
            return DAL.AgencyInformation_DAL.GetAgencyInfoByAgencyNo1(no);
        }
        public static int UpdateAgencyInfo(Entity.AgencyInformation_Entity ai)
        {
            return DAL.AgencyInformation_DAL.UpdateAgencyInfo(ai);
        }
        public static Entity.AgencyInformation_Entity GetAgencyInfoById(int id)
        {
            return DAL.AgencyInformation_DAL.GetAgencyInfoById(id);
        }

    }

    public class FTPInformation_BLL
    {
        public static int CreateFTPInfo(Entity.FTPInformation_Entity ai)
        {
            return DAL.FTPInformation_DAL.CreateFTPInfo(ai);
        }
        public static int deleteFTP(int id)
        {
            return DAL.FTPInformation_DAL.deleteFTP(id);
        }
        public static List<Entity.FTPInformation_Entity>GetAllFTPInfo()
        {
            return DAL.FTPInformation_DAL.GetAllFTPInfo();
        }
        public static List<Entity.FTPInformation_Entity >GetFTPInfoByAgencyNo(string no)
        {
            return DAL.FTPInformation_DAL.GetFTPInfoByAgencyNo(no);
        }
        public static Entity.FTPInformation_Entity GetFTPInfoByAgencyNo1(string no)
        {
            return DAL.FTPInformation_DAL.GetFTPInfoByAgencyNo1(no);
        }
        public static int UpdateFTPInfo(Entity.FTPInformation_Entity ai)
        {
            return DAL.FTPInformation_DAL.UpdateFTPInfo(ai);
        }

    }
    public class data_BLL
    {
        public static int Createdata(Entity.data_Entity ai)
        {
            return DAL.data_DAL.Createdata(ai);
        }
        public static int deldata(int id)
        {
            return DAL.data_DAL.deldata(id);
        }

        public static List<Entity.data_Entity >GetAlldata()
        {
            return DAL.data_DAL.GetAlldata();
        }
        public static List<Entity.data_Entity >GetdataByAgencyNo(string no)
        {
            return DAL.data_DAL.GetdataByAgencyNo(no);
        }
        public static Entity.data_Entity GetdataByAgencyNo1(string no)
        {
            return DAL.data_DAL.GetdataByAgencyNo1(no);
        }
        public static Entity.data_Entity GetdataByID(string no)
        {
            return DAL.data_DAL.GetdataByID(no);
        }
        public static int Updatedata(Entity.data_Entity ai)
        {
            return DAL.data_DAL.Updatedata(ai);
        }
    }
    public class logs_BLL
    {
        public static int Createlog(Entity.logs_Entity ai)
        {
            return DAL.logs_DAL.Createlog(ai);
        }
        public static int dellog()
        {
            return DAL.logs_DAL.dellog();
        }
        public static List<Entity.logs_Entity >GetAlllogs()
        {
            return DAL.logs_DAL.GetAlllogs();
        }
        public static List<Entity.logs_Entity >GetlogsByAgencyNo(string no)
        {
            return DAL.logs_DAL.GetlogsByAgencyNo(no);
        }
        public static int Updatelogs(Entity.logs_Entity ai)
        {
            return DAL.logs_DAL.Updatelogs(ai);
        }
    }
    public class account_BLL
    {
        public static int deleteaccount(int id)
        {
            return DAL.account_DAL.deleteaccount(id);
        }
        public static int Createaccount(Entity.account_Entity ai)
        {
            return DAL.account_DAL.Createaccount(ai);
        }
        public static List<Entity.account_Entity> GetAllaccount()
        {
            return DAL.account_DAL.GetAllaccount();
        }

        public static List<Entity.account_Entity> GetaccountByAgencyNo(string no)
        {
            return DAL.account_DAL.GetaccountByAgencyNo(no);
        }
        public static Entity.account_Entity GetaccountByID(string no)
        {
            return DAL.account_DAL.GetaccountByID(no);
        }
        public static int Updateaccount(Entity.account_Entity ai)
        {
            return DAL.account_DAL.Updateaccount(ai);
        }
    }
}
