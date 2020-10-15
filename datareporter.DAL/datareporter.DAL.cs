using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using datareporter.DAL;

namespace datareporter.DAL
{
    public class UserInformation_DAL
    {
        /// <summary>
                /// 用户登录验证
                /// </summary>
                /// <param name="userName">用户名</param>
                /// <param name="userPassword">密码</param>
        /// <param name="userEmpID">工号</param>
                /// <returns>布尔值True成功</returns>
        public static bool UserLogin(string userName, string userPassword)
        {
            string sequel = "DR_UserLogin";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@UserPassword",userPassword),
            };
            int result = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, sequel, paras);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="ui">用户信息实体</param>
        /// <returns>用户编号</returns>
        public static int CreateUserInfo(datareporter.Entity.UserInformation_Entity ui)
        {
            string sequel = "DR_InsertUserinformation";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ui);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }


        /// <summary>
        /// 获取用户所有信息
        /// </summary>
        /// <returns>泛型实体</returns>
        public static List<Entity.UserInformation_Entity> GetAllUserInfo()
        {
            string sequel = "DR_SelectAllFromUserinformation";
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, null).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }

        /// <summary>
        /// 获取用户所有信息同上，不同的是不是调用的存储过程，而是直接拼写的SQL
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAllUserInfoBySql()
        {
            string sequel = "Select * from DR_UserInformation";
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.Text, sequel, null).Tables[0];
            return dt;
        }



        /// <summary>
        /// 获取某用户信息
        /// </summary>
        /// <param name="empId">用户id</param>
        /// <returns>用户信息实体</returns>
        public static Entity.UserInformation_Entity GetUserInfoById(string id)
        {
            string sequel = "DR_SelectAllFromUserinformationById";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UID", id) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataRow row = dt.Rows[0];
                return GetEntity(row);
            }
        }
        /// <summary>
                /// 获取某用户信息
                /// </summary>
                /// <param name="empName">用户Name</param>
                /// <returns>用户信息实体</returns>
        public static Entity.UserInformation_Entity GetUserInfoByName(string userName)
        {
            string sequel = "DR_SelectAllFromUserinformationByName";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UName", userName) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataRow row = dt.Rows[0];
                return GetEntity(row);
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="ui">用户实体</param>
        /// <returns>影响行数</returns>
        public static int UpdateUserInfo(Entity.UserInformation_Entity ui)
        {
            string sequel = "DR_UpdateUserinformation";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ui);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }

        /// <summary>
        /// 将DataView转换为泛型实体对象
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <returns>泛型实体对象</returns>
        private static List<Entity.UserInformation_Entity> LoadListFromDataView(DataView dv)
        {
            List<Entity.UserInformation_Entity> list = new List<Entity.UserInformation_Entity>();
            for (int index = 0; index <= dv.Count - 1; index++)
            {
                list.Add(GetEntity(dv[index].Row));
            }
            return list;
        }

        /// <summary>
        /// 从DataReader中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.UserInformation_Entity GetEntity(IDataReader dataReader)
        {
            Entity.UserInformation_Entity _obj = new Entity.UserInformation_Entity();
            if (!dataReader["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(dataReader["ID"]);
            if (!dataReader["UserName"].Equals(DBNull.Value))
                _obj.UserName = Convert.ToString(dataReader["UserName"]);
            if (!dataReader["UserPassword"].Equals(DBNull.Value))
                _obj.UserPassword = Convert.ToString(dataReader["UserPassword"]);
            if (!dataReader["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(dataReader["AgencyNo"]);

            return _obj;
        }
        /// <summary>
                        /// 从行中读取数据映射到实体类的属性中
                        /// </summary>
                        /// <remarks></remarks>
        private static Entity.UserInformation_Entity GetEntity(DataRow row)
        {
            Entity.UserInformation_Entity _obj = new Entity.UserInformation_Entity();
            if (!row["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(row["ID"]);
            if (!row["UserName"].Equals(DBNull.Value))
                _obj.UserName = Convert.ToString(row["UserName"]);
            if (!row["UserPassword"].Equals(DBNull.Value))
                _obj.UserPassword = Convert.ToString(row["UserPassword"]);
            if (!row["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(row["AgencyNo"]);

            return _obj;
        }

        /// <summary>
                        /// 该数据访问对象的属性值装载到数据库更新参数数组Insert用
                        /// </summary>
                        /// <remarks></remarks>
        private static IDbDataParameter[] ValueParas(Entity.UserInformation_Entity _obj)
        {
            SqlParameter[] paras = new SqlParameter[4];
            paras[0] = new SqlParameter("@ID", _obj.ID);
            paras[1] = new SqlParameter("@UserName", _obj.UserName);
            paras[2] = new SqlParameter("@UserPassword", _obj.UserPassword);
            paras[3] = new SqlParameter("@AgencyNo", _obj.AgencyNo);


            paras[0].DbType = DbType.Int32;
            paras[1].DbType = DbType.String;
            paras[2].DbType = DbType.String;
            paras[3].DbType = DbType.String;
            return paras;
        }
    }
    public class AgencyInformation_DAL
    {
        /// <summary>
                /// 添加新机构
                /// </summary>
                /// <param name="ai">机构信息实体</param>
                /// <returns>机构编号</returns>
        public static int CreateAgencyInfo(Entity.AgencyInformation_Entity ai)
        {
            string sequel = "DR_InsertAgencyinformation";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        /// <summary>
                /// 获取所有机构信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.AgencyInformation_Entity> GetAllAgencyInfo()
        {
            string sequel = "DR_SelectAllFromAgencyinformation";
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, null).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }
        /// <summary>
                /// 按机构号获取机构所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List< Entity.AgencyInformation_Entity> GetAgencyInfoByAgencyNo(string no)
        {
            string sequel = "DR_SelectAllFromAgencyInformationByAgencyNo";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AgencyNo", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                //DataRow row = dt.Rows[0];
                //return GetEntity(row);
               return  LoadListFromDataView(dt.DefaultView);
            }
        }
        /// <summary>
                /// 按机构号获取机构所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static Entity.AgencyInformation_Entity GetAgencyInfoByAgencyNo1(string no)
        {
            string sequel = "DR_SelectAllFromAgencyInformationByAgencyNo";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AgencyNo", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataRow row = dt.Rows[0];
                return GetEntity(row);

            }
        }
        /// <summary>
                /// 更新机构信息
                /// </summary>
                /// <param name="ai">用户实体</param>
                /// <returns>影响行数</returns>
        public static int UpdateAgencyInfo(Entity.AgencyInformation_Entity ai)
        {
            string sequel = "DR_UpdateAgencyinformation";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        /// <summary>
        /// 按机构号查询机构信息
        /// </summary>
        /// <param name="DotNo"></param>
        /// <returns></returns>
        public static Entity.AgencyInformation_Entity GetAgencyInfoById(int Id)
        {
            string sequel = "DR_SelectAllFromAgencyInformationById";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@DotNo", Id) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataRow row = dt.Rows[0];
                return GetEntity(row);
            }
        }

        /// <summary>
        /// 将DataView转换为泛型实体对象
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <returns>泛型实体对象</returns>
        private static List<Entity.AgencyInformation_Entity> LoadListFromDataView(DataView dv)
        {
            List<Entity.AgencyInformation_Entity> list = new List<Entity.AgencyInformation_Entity>();
            for (int index = 0; index <= dv.Count - 1; index++)
            {
                list.Add(GetEntity(dv[index].Row));
            }
            return list;
        }
        /// <summary>
        /// 从DataReader中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.AgencyInformation_Entity GetEntity(IDataReader dataReader)
        {
            Entity.AgencyInformation_Entity _obj = new Entity.AgencyInformation_Entity();
            if (!dataReader["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(dataReader["ID"]);
            if (!dataReader["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(dataReader["AgencyNo"]);
                   if (!dataReader["account_bank"].Equals(DBNull.Value))
                _obj.account_bank = Convert.ToString(dataReader["account_bank"]);
            if (!dataReader["account_bank_code"].Equals(DBNull.Value))
                _obj.account_bank_code = Convert.ToString(dataReader["account_bank_code"]);
            if (!dataReader["banktype_code"].Equals(DBNull.Value))
                _obj.banktype_code = Convert.ToString(dataReader["banktype_code"]);
            if (!dataReader["banktype_name"].Equals(DBNull.Value))
                _obj.banktype_name = Convert.ToString(dataReader["banktype_name"]);

            return _obj;
        }
        /// <summary>
        /// 从行中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.AgencyInformation_Entity GetEntity(DataRow row)
        {
            Entity.AgencyInformation_Entity _obj = new Entity.AgencyInformation_Entity();
            if (!row["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(row["ID"]);
            if (!row["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(row["AgencyNo"]);
            if (!row["account_bank"].Equals(DBNull.Value))
                _obj.account_bank = Convert.ToString(row["account_bank"]);
            if (!row["account_bank_code"].Equals(DBNull.Value))
                _obj.account_bank_code = Convert.ToString(row["account_bank_code"]);
            if (!row["banktype_code"].Equals(DBNull.Value))
                _obj.banktype_code = Convert.ToString(row["banktype_code"]);
            if (!row["banktype_name"].Equals(DBNull.Value))
                _obj.banktype_name = Convert.ToString(row["banktype_name"]);

            return _obj;
        }
        /// <summary>
        /// 该数据访问对象的属性值装载到数据库更新参数数组Insert用
        /// </summary>
        /// <remarks></remarks>
        private static IDbDataParameter[] ValueParas(Entity.AgencyInformation_Entity _obj)
        {
            SqlParameter[] paras = new SqlParameter[6];
            paras[0] = new SqlParameter("@ID", _obj.ID);
            paras[1] = new SqlParameter("@AgencyNo", _obj.AgencyNo);
            paras[2] = new SqlParameter("@account_bank", _obj.account_bank);
            paras[3] = new SqlParameter("@account_bank_code", _obj.account_bank_code);
            paras[4] = new SqlParameter("@banktype_code", _obj.banktype_code);
            paras[5] = new SqlParameter("@banktype_name", _obj.banktype_name);


            paras[0].DbType = DbType.Int32;
            paras[1].DbType = DbType.String;
            paras[2].DbType = DbType.String;
            paras[3].DbType = DbType.String;
            paras[4].DbType = DbType.String;
            paras[5].DbType = DbType.String;
            return paras;
        }
    }
    public class FTPInformation_DAL
    {
        /// <summary>
                /// 添加新机构
                /// </summary>
                /// <param name="ai">机构信息实体</param>
                /// <returns>机构编号</returns>
        public static int CreateFTPInfo(Entity.FTPInformation_Entity ai)
        {
            string sequel = "DR_InsertFTPinformation";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        public static int deleteFTP(int id)
        {
            string sequel = "DR_deleteFTP";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ID", id) };
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        /// <summary>
                /// 获取所有机构信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.FTPInformation_Entity> GetAllFTPInfo()
        {
            string sequel = "DR_SelectAllFromFTPinformation";
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, null).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }
        /// <summary>
                /// 按机构号获取机构所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.FTPInformation_Entity> GetFTPInfoByAgencyNo(string no)
        {
            string sequel = "DR_SelectAllFromFTPInformationByAgencyNo";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AgencyNo", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }
               /// <summary>
                /// 按机构号获取机构所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static Entity.FTPInformation_Entity GetFTPInfoByAgencyNo1(string no)
        {
            string sequel = "DR_SelectAllFromFTPInformationByAgencyNo";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AgencyNo", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataRow row = dt.Rows[0];
                return GetEntity(row);
            }
        } /// <summary>
                /// 更新机构信息
                /// </summary>
                /// <param name="ai">用户实体</param>
                /// <returns>影响行数</returns>
        public static int UpdateFTPInfo(Entity.FTPInformation_Entity ai)
        {
            string sequel = "DR_UpdateFTPinformation";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        /// <summary>
        /// 将DataView转换为泛型实体对象
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <returns>泛型实体对象</returns>
        private static List<Entity.FTPInformation_Entity> LoadListFromDataView(DataView dv)
        {
            List<Entity.FTPInformation_Entity> list = new List<Entity.FTPInformation_Entity>();
            for (int index = 0; index <= dv.Count - 1; index++)
            {
                list.Add(GetEntity(dv[index].Row));
            }
            return list;
        }
        /// <summary>
        /// 从DataReader中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.FTPInformation_Entity GetEntity(IDataReader dataReader)
        {
            Entity.FTPInformation_Entity _obj = new Entity.FTPInformation_Entity();
            if (!dataReader["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(dataReader["ID"]);
            if (!dataReader["FTPAddress"].Equals(DBNull.Value))
                _obj.FTPAddress = Convert.ToString(dataReader["FTPAddress"]);
            if (!dataReader["FTPUsername"].Equals(DBNull.Value))
                _obj.FTPUsername = Convert.ToString(dataReader["FTPUsername"]);
            if (!dataReader["FTPPassword"].Equals(DBNull.Value))
                _obj.FTPPassword = Convert.ToString(dataReader["FTPPassword"]);
            if (!dataReader["data_Key"].Equals(DBNull.Value))
                _obj.data_Key = Convert.ToString(dataReader["data_Key"]);
            if (!dataReader["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(dataReader["AgencyNo"]);
            return _obj;
        }
        /// <summary>
        /// 从行中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.FTPInformation_Entity GetEntity(DataRow row)
        {
            Entity.FTPInformation_Entity _obj = new Entity.FTPInformation_Entity();
            if (!row["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(row["ID"]);
            if (!row["FTPAddress"].Equals(DBNull.Value))
                _obj.FTPAddress = Convert.ToString(row["FTPAddress"]);
            if (!row["FTPUsername"].Equals(DBNull.Value))
                _obj.FTPUsername = Convert.ToString(row["FTPUsername"]);
            if (!row["FTPPassword"].Equals(DBNull.Value))
                _obj.FTPPassword = Convert.ToString(row["FTPPassword"]);
            if (!row["data_Key"].Equals(DBNull.Value))
                _obj.data_Key = Convert.ToString(row["data_Key"]);
            if (!row["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(row["AgencyNo"]);


            return _obj;
        }
        /// <summary>
        /// 该数据访问对象的属性值装载到数据库更新参数数组Insert用
        /// </summary>
        /// <remarks></remarks>
        private static IDbDataParameter[] ValueParas(Entity.FTPInformation_Entity _obj)
        {
            SqlParameter[] paras = new SqlParameter[6];
            paras[0] = new SqlParameter("@ID", _obj.ID);
            paras[1] = new SqlParameter("@FTPAddress", _obj.FTPAddress);
            paras[2] = new SqlParameter("@FTPUsername", _obj.FTPUsername);
            paras[3] = new SqlParameter("@FTPPassword", _obj.FTPPassword);
            paras[4] = new SqlParameter("@data_Key", _obj.data_Key);
            paras[5] = new SqlParameter("@AgencyNo", _obj.AgencyNo);


            paras[0].DbType = DbType.Int32;
            paras[1].DbType = DbType.String;
            paras[2].DbType = DbType.String;
            paras[3].DbType = DbType.String;
            paras[4].DbType = DbType.String;
            paras[5].DbType = DbType.String;


            return paras;
        }
    }
    public class data_DAL
    {
        /// <summary>
                /// 添加新机构
                /// </summary>
                /// <param name="ai">机构信息实体</param>
                /// <returns>机构编号</returns>
        public static int Createdata(Entity.data_Entity ai)
        {
            string sequel = "DR_Insertdata";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        public static int deldata(int id)
        {
            string sequel = "DR_deletedata";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ID", id) };
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }

        /// <summary>
                /// 获取所有机构信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.data_Entity> GetAlldata()
        {
            string sequel = "DR_SelectAllFromdata";
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, null).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }
        /// <summary>
                /// 按机构号获取机构所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.data_Entity> GetdataByAgencyNo(string no)
        {
            string sequel = "DR_SelectAllFromdataByAgencyNo";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AgencyNo", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }
        /// <summary>
                /// 按机构号获取机构所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static Entity.data_Entity GetdataByAgencyNo1(string no)
        {
            string sequel = "DR_SelectAllFromdataByAgencyNo";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AgencyNo", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataRow row = dt.Rows[0];
                return GetEntity(row);
            }
        }

        public static Entity.data_Entity GetdataByID(string no)
        {
            string sequel = "DR_SelectAllFromdataByID";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ID", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];

            DataRow row = dt.Rows[0];
            return GetEntity(row);
        }
        /// <summary>
                /// 更新机构信息
                /// </summary>
                /// <param name="ai">用户实体</param>
                /// <returns>影响行数</returns>
        public static int Updatedata(Entity.data_Entity ai)
        {
            string sequel = "DR_Updatedata";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }

        /// <summary>
        /// 将DataView转换为泛型实体对象
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <returns>泛型实体对象</returns>
        private static List<Entity.data_Entity> LoadListFromDataView(DataView dv)
        {
            List<Entity.data_Entity> list = new List<Entity.data_Entity>();
            for (int index = 0; index <= dv.Count - 1; index++)
            {
                list.Add(GetEntity(dv[index].Row));
            }
            return list;
        }
        /// <summary>
        /// 从DataReader中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.data_Entity GetEntity(IDataReader dataReader)
        {
            Entity.data_Entity _obj = new Entity.data_Entity();
            if (!dataReader["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(dataReader["ID"]);
            if (!dataReader["inputTime"].Equals(DBNull.Value))
                _obj.inputTime = Convert.ToDateTime(dataReader["inputTime"]);
            if (!dataReader["dta_Position"].Equals(DBNull.Value))
                _obj.data_Position = Convert.ToString(dataReader["data_Position"]);
            if (!dataReader["dataName"].Equals(DBNull.Value))
                _obj.dataName = Convert.ToString(dataReader["dataName"]);
            if (!dataReader["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(dataReader["AgencyNo"]);
            if (!dataReader["datatype"].Equals(DBNull.Value))
                _obj.datatype = Convert.ToString(dataReader["datatype"]);
            if (!dataReader["upload"].Equals(DBNull.Value))
                _obj.upload = Convert.ToString(dataReader["upload"]);
            return _obj;
        }
        /// <summary>
        /// 从行中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.data_Entity GetEntity(DataRow row)
        {
            Entity.data_Entity _obj = new Entity.data_Entity();
            if (!row["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(row["ID"]);
            if (!row["inputTime"].Equals(DBNull.Value))
                _obj.inputTime = Convert.ToDateTime(row["inputTime"]);
            if (!row["data_Position"].Equals(DBNull.Value))
                _obj.data_Position = Convert.ToString(row["data_Position"]);
            if (!row["dataName"].Equals(DBNull.Value))
                _obj.dataName = Convert.ToString(row["dataName"]);
            if (!row["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(row["AgencyNo"]);
            if (!row["datatype"].Equals(DBNull.Value))
                _obj.datatype = Convert.ToString(row["datatype"]);
            if (!row["upload"].Equals(DBNull.Value))
                _obj.upload = Convert.ToString(row["upload"]);

            return _obj;
        }


        /// <summary>
        /// 该数据访问对象的属性值装载到数据库更新参数数组Insert用
        /// </summary>
        /// <remarks></remarks>
        private static IDbDataParameter[] ValueParas(Entity.data_Entity _obj)
        {
            SqlParameter[] paras = new SqlParameter[7];
            paras[0] = new SqlParameter("@ID", _obj.ID);
            paras[1] = new SqlParameter("@inputTime", _obj.inputTime);
            paras[2] = new SqlParameter("@data_Position", _obj.data_Position);
            paras[3] = new SqlParameter("@dataName", _obj.dataName);
            paras[4] = new SqlParameter("@AgencyNo", _obj.AgencyNo);
            paras[5] = new SqlParameter("@datatype", _obj.datatype);
            paras[6] = new SqlParameter("@upload", _obj.upload);

            paras[0].DbType = DbType.Int32;
            paras[1].DbType = DbType.DateTime;
            paras[2].DbType = DbType.String;
            paras[3].DbType = DbType.String;
            paras[4].DbType = DbType.String;
            paras[5].DbType = DbType.String;
            paras[6].DbType = DbType.String;
            return paras;
        }
    }
    public class logs_DAL
    {
        /// <summary>
                /// 添加新机构
                /// </summary>
                /// <param name="ai">机构信息实体</param>
                /// <returns>机构编号</returns>
        public static int Createlog(Entity.logs_Entity ai)
        {
            string sequel = "DR_Insertlogs";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        public static int dellog()
        {
            string sequel = "DR_deletelogs";
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, null);
            return result;
        }
        /// <summary>
                /// 获取所有机构信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.logs_Entity> GetAlllogs()
        {
            string sequel = "DR_SelectAllFromlogs";
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, null).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }
        /// <summary>
                /// 按机构号获取机构所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.logs_Entity> GetlogsByAgencyNo(string no)
        {
            string sequel = "DR_SelectAllFromlogsByAgencyNo";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AgencyNo", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }
        /// <summary>
                /// 更新机构信息
                /// </summary>
                /// <param name="ai">用户实体</param>
                /// <returns>影响行数</returns>
        public static int Updatelogs(Entity.logs_Entity ai)
        {
            string sequel = "DR_Updatelogs";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }

        /// <summary>
        /// 将DataView转换为泛型实体对象
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <returns>泛型实体对象</returns>
        private static List<Entity.logs_Entity> LoadListFromDataView(DataView dv)
        {
            List<Entity.logs_Entity> list = new List<Entity.logs_Entity>();
            for (int index = 0; index <= dv.Count - 1; index++)
            {
                list.Add(GetEntity(dv[index].Row));
            }
            return list;
        }
        /// <summary>
        /// 从DataReader中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.logs_Entity GetEntity(IDataReader dataReader)
        {
            Entity.logs_Entity _obj = new Entity.logs_Entity();
            if (!dataReader["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(dataReader["ID"]);
            if (!dataReader["inputTime"].Equals(DBNull.Value))
                _obj.inputTime = Convert.ToDateTime(dataReader["inputTime"]);
            if (!dataReader["succeed"].Equals(DBNull.Value))
                _obj.succeed = Convert.ToString(dataReader["succeed"]);
            if (!dataReader["logName"].Equals(DBNull.Value))
                _obj.logName = Convert.ToString(dataReader["logName"]);
            if (!dataReader["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(dataReader["AgencyNo"]);
            if (!dataReader["log_No"].Equals(DBNull.Value))
                _obj.log_No = Convert.ToString(dataReader["log_No"]);
            return _obj;
        }
        /// <summary>
        /// 从行中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.logs_Entity GetEntity(DataRow row)
        {
            Entity.logs_Entity _obj = new Entity.logs_Entity();
            if (!row["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(row["ID"]);
            if (!row["inputTime"].Equals(DBNull.Value))
                _obj.inputTime = Convert.ToDateTime(row["inputTime"]);
            if (!row["succeed"].Equals(DBNull.Value))
                _obj.succeed = Convert.ToString(row["succeed"]);
            if (!row["logName"].Equals(DBNull.Value))
                _obj.logName = Convert.ToString(row["logName"]);
            if (!row["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(row["AgencyNo"]);
            if (!row["log_No"].Equals(DBNull.Value))
                _obj.log_No = Convert.ToString(row["log_No"]);

            return _obj;
        }


        /// <summary>
        /// 该数据访问对象的属性值装载到数据库更新参数数组Insert用
        /// </summary>
        /// <remarks></remarks>
        private static IDbDataParameter[] ValueParas(Entity.logs_Entity _obj)
        {
            SqlParameter[] paras = new SqlParameter[6];
            paras[0] = new SqlParameter("@ID", _obj.ID);
            paras[1] = new SqlParameter("@inputTime", _obj.inputTime);
            paras[2] = new SqlParameter("@succeed", _obj.succeed);
            paras[3] = new SqlParameter("@logName", _obj.logName);
            paras[4] = new SqlParameter("@AgencyNo", _obj.AgencyNo);
            paras[5] = new SqlParameter("@log_No", _obj.log_No);

            paras[0].DbType = DbType.Int32;
            paras[1].DbType = DbType.DateTime;
            paras[2].DbType = DbType.String;
            paras[3].DbType = DbType.String;
            paras[4].DbType = DbType.String;
            paras[5].DbType = DbType.String;

            return paras;
        }
    }
public class account_DAL
    {/// <summary>
    /// 删除账户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
        public static int deleteaccount(int id)
        {
            string sequel = "DR_deleteaccount";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ID", id) };
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        /// <summary>
                /// 添加新账户
                /// </summary>
                /// <param name="ai">机构信息实体</param>
                /// <returns>机构编号</returns>
        public static int Createaccount(Entity.account_Entity ai)
        {
            string sequel = "DR_Insertaccount";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }
        /// <summary>
                /// 获取所有账户信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.account_Entity> GetAllaccount()
        {
            string sequel = "DR_SelectAllFromaccount";
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, null).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }

        /// <summary>
                /// 按机构号获取账户所有信息
                /// </summary>
                /// <returns>泛型实体</returns>
        public static List<Entity.account_Entity> GetaccountByAgencyNo(string no)
        {
            string sequel = "DR_SelectAllFromaccountByAgencyNo";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AgencyNo", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];

            return LoadListFromDataView(dt.DefaultView);
        }
        public static Entity.account_Entity GetaccountByID(string no)
        {
            string sequel = "DR_SelectAllFromaccountByID";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ID", no) };
            DataTable dt = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sequel, paras).Tables[0];

            DataRow row = dt.Rows[0];
            return GetEntity(row); 
        }
        /// <summary>
                /// 更新账户信息
                /// </summary>
                /// <param name="ai">用户实体</param>
                /// <returns>影响行数</returns>
        public static int Updateaccount(Entity.account_Entity ai)
        {
            string sequel = "DR_Updateaccount";
            SqlParameter[] paras = (SqlParameter[])ValueParas(ai);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sequel, paras);
            return result;
        }

        /// <summary>
        /// 将DataView转换为泛型实体对象
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <returns>泛型实体对象</returns>
        private static List<Entity.account_Entity> LoadListFromDataView(DataView dv)
        {
            List<Entity.account_Entity> list = new List<Entity.account_Entity>();
            for (int index = 0; index <= dv.Count - 1; index++)
            {
                list.Add(GetEntity(dv[index].Row));
            }
            return list;
        }
        /// <summary>
        /// 从DataReader中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.account_Entity GetEntity(IDataReader dataReader)
        {
            Entity.account_Entity _obj = new Entity.account_Entity();
            if (!dataReader["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(dataReader["ID"]);
            if (!dataReader["account_no"].Equals(DBNull.Value))
                _obj.account_no = Convert.ToString(dataReader["account_no"]);
            if (!dataReader["account_name"].Equals(DBNull.Value))
                _obj.account_name= Convert.ToString(dataReader["account_name"]);
            if (!dataReader["rg_code"].Equals(DBNull.Value))
                _obj.rg_code = Convert.ToString(dataReader["rg_code"]);
            if (!dataReader["type_code"].Equals(DBNull.Value))
                _obj.type_code = Convert.ToString(dataReader["type_code"]);
            if (!dataReader["type_name"].Equals(DBNull.Value))
                _obj.type_name = Convert.ToString(dataReader["type_name"]);
            if (!dataReader["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(dataReader["AgencyNo"]);

            return _obj;
        }
        /// <summary>
        /// 从行中读取数据映射到实体类的属性中
        /// </summary>
        /// <remarks></remarks>
        private static Entity.account_Entity GetEntity(DataRow row)
        {
            Entity.account_Entity _obj = new Entity.account_Entity();
            if (!row["ID"].Equals(DBNull.Value))
                _obj.ID = Convert.ToInt32(row["ID"]);
            if (!row["account_no"].Equals(DBNull.Value))
                _obj.account_no = Convert.ToString(row["account_no"]);
            if (!row["account_name"].Equals(DBNull.Value))
                _obj.account_name = Convert.ToString(row["account_name"]);
            if (!row["rg_code"].Equals(DBNull.Value))
                _obj.rg_code = Convert.ToString(row["rg_code"]);
            if (!row["type_code"].Equals(DBNull.Value))
                _obj.type_code = Convert.ToString(row["type_code"]);
            if (!row["type_name"].Equals(DBNull.Value))
                _obj.type_name = Convert.ToString(row["type_name"]);
            if (!row["AgencyNo"].Equals(DBNull.Value))
                _obj.AgencyNo = Convert.ToString(row["AgencyNo"]);


            return _obj;
        }


        /// <summary>
        /// 该数据访问对象的属性值装载到数据库更新参数数组Insert用
        /// </summary>
        /// <remarks></remarks>
        private static IDbDataParameter[] ValueParas(Entity.account_Entity _obj)
        {
            SqlParameter[] paras = new SqlParameter[7];
            paras[0] = new SqlParameter("@ID", _obj.ID);
            paras[1] = new SqlParameter("@account_no", _obj.account_no);
            paras[2] = new SqlParameter("@account_name", _obj.account_name);
            paras[3] = new SqlParameter("@rg_code", _obj.rg_code);
            paras[4] = new SqlParameter("@type_code", _obj.type_code);
            paras[5] = new SqlParameter("@type_name", _obj.type_name);
            paras[6] = new SqlParameter("@AgencyNo", _obj.AgencyNo);


            paras[0].DbType = DbType.Int32;
            paras[1].DbType = DbType.String;
            paras[2].DbType = DbType.String;
            paras[3].DbType = DbType.String;
            paras[4].DbType = DbType.String;
            paras[5].DbType = DbType.String;
            paras[6].DbType = DbType.String;

            return paras;
        }
    }

    public class  admin_DAL
    {

    }
}
