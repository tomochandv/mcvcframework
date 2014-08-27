using Oracle.DataAccess.Client;
/**
 *  날짜 :    2013.10 
 *  만든이 :  심민섭
 *  기능 :    Dbhelper
 *  설명 :    기존 디비연결은 System.Data.OraclClient를 사용. MSDN에서는 쓰지말라함. 그래서 디비연결을 위한 클래스를 만듬
 * */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TomochanLib
{
    public class DbHelper : DbAgent
    {
        public DbHelper()
        { }
        public DbHelper(Dbtype _type)
        {
            type = _type;
        }
        public DbHelper(Dbtype _type, string _connectionString)
        {
            type = _type;
            ConnectionString = _connectionString;
        }
        public enum Dbtype { oracle, mssql };
        public enum DbParameterType
        {
            Varchar,
            Nvarchar,
            Int,
            Double,
            Float,
            Text,
            Date,
            Char
        };

        private Dbtype type = ConfigurationManager.AppSettings["DBTYPE"] == "MS" ? Dbtype.mssql : Dbtype.oracle;
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        Parameter parameter = new Parameter();
        /// <summary>
        /// DataSet return
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public DataSet ExcuteToDataSet(string qry, List<Parameter> paramList)
        {
            DataSet ds = new DataSet();
            try
            {
                if (type == Dbtype.mssql)
                {
                    ds = SqlGetDataSet(ConnectionString, qry, parameter.SqlParameterList(paramList), CommandType.Text);
                }
                else
                {
                    ds = OracleGetDataSet(ConnectionString, qry, parameter.OracleParameterList(qry, paramList), CommandType.Text);
                }
            }
            catch (Exception ex)
            { throw ex; }
            return ds;
        }

        /// <summary>
        /// 저장,인서트,딜리트
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public int ExcuteNonQuery(string qry, List<Parameter> paramList)
        {
            int row = 0;
            try
            {
                if (type == Dbtype.mssql)
                {
                    row = SqlExcuteNonQuery(ConnectionString, qry, parameter.SqlParameterList(paramList), CommandType.Text);
                }
                else
                {
                    row = OracleExcuteNonQuery(ConnectionString, qry, parameter.OracleParameterList(qry, paramList), CommandType.Text);
                }
            }
            catch (Exception ex)
            { throw ex; }
            return row;
        }

        public int ExcuteNonQueryTran(List<string> qry, List<List<Parameter>> paramList)
        {
            int row = 0;
            try
            {
                if (type == Dbtype.mssql)
                {
                    List<List<SqlParameter>> list = new List<List<SqlParameter>>();
                    foreach(var param in paramList)
                    {
                        list.Add(parameter.SqlParameterList(param));
                    }
                    row = SqlExcuteNonQuery(ConnectionString, qry, list, CommandType.Text);
                }
                else
                {
                    List<List<OracleParameter>> list = new List<List<OracleParameter>>();
                    foreach (var param in paramList)
                    {
                        list.Add(parameter.OracleParameterList(param));
                    }
                    row = OracleExcuteNonQuery(ConnectionString, qry, list, CommandType.Text);
                }
            }
            catch (Exception ex)
            { throw ex; }
            return row;
        }

        /// <summary>
        /// 오브젝트 한개 리턴
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public object ExcuteScalar(string qry, List<Parameter> paramList)
        {
            object obj;
            try
            {
                if (type == Dbtype.mssql)
                {
                    obj = SqlGetScarlar(ConnectionString, qry, parameter.SqlParameterList(paramList), CommandType.Text);
                }
                else
                {
                    obj = OracleGetScarlar(ConnectionString, qry, parameter.OracleParameterList(qry, paramList), CommandType.Text);
                }
            }
            catch (Exception ex)
            { throw ex; }
            return obj;
        }

        public int GetToInt(object obj)
        {
            try
            {
                if (obj == null)
                { return 0; }
                else { return int.Parse(obj.ToString()); }
            }
            catch (Exception ex)
            { return 0; }
        }
    }

    #region Parameter Class
    public class Parameter
    {
        /// <summary>
        /// parameter
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="parameterName">parameter name</param>
        /// <param name="dbType">Column type</param>
        /// <param name="size">size</param>
        public Parameter(object value, string parameterName, SqlDbType dbType, int size)
        {
            Value = value;
            ParameterName = parameterName;
            DbType = dbType;
            Size = size;
        }
        /// <summary>
        /// parameter
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="parameterName">parameter name</param>
        /// <param name="dbType">Column type</param>
        public Parameter(object value, string parameterName, SqlDbType dbType)
        {
            Value = value;
            ParameterName = parameterName;
            DbType = dbType;
        }
        public Parameter()
        {

        }
        /// <summary>
        /// Value
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// Db type
        /// </summary>
        public SqlDbType DbType { get; set; }
        /// <summary>
        /// Size 
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Parameter Name
        /// </summary>
        public string ParameterName { get; set; }


        public List<OracleParameter> OracleParameterList(string qry, List<Parameter> paramList)
        {
            List<OracleParameter> paramCollection = new List<OracleParameter>();
            if (paramList != null)
            {
                foreach (var data in paramList)
                {
                    MatchCollection matches = Regex.Matches(qry, string.Format("@{0}", data.ParameterName));
                    int paramCount = matches.Count;

                    for (int i = 0; i < paramCount; i++)
                    {
                        OracleParameter param = new OracleParameter();
                        param.Value = data.Value;
                        param.ParameterName = data.ParameterName.Replace("@", ":");
                        if (data.Size != 0)
                        {
                            param.Size = data.Size;
                        }
                        param.OracleDbType = OracleDbTypeConvert(data.DbType);
                        paramCollection.Add(param);
                    }
                }
            }
            return paramCollection;
        }

        public List<OracleParameter> OracleParameterList(List<Parameter> paramList)
        {
            List<OracleParameter> paramCollection = new List<OracleParameter>();
            if (paramList != null)
            {
                foreach (var data in paramList)
                {
                    OracleParameter param = new OracleParameter();
                    param.Value = data.Value;
                    param.ParameterName = data.ParameterName.Replace("@", ":");
                    if (data.Size != 0)
                    {
                        param.Size = data.Size;
                    }
                    param.OracleDbType = OracleDbTypeConvert(data.DbType);
                    paramCollection.Add(param);
                }
            }
            return paramCollection;
        }

        public List<SqlParameter> SqlParameterList(List<Parameter> paramList)
        {
            List<SqlParameter> paramCollection = new List<SqlParameter>();
            if (paramList != null)
            {
                foreach (var data in paramList)
                {
                    SqlParameter param = new SqlParameter();
                    param.Value = data.Value;
                    param.ParameterName = data.ParameterName.Replace(":", "@");
                    if (data.Size != 0)
                    {
                        param.Size = data.Size;
                    }
                    param.SqlDbType = data.DbType;
                    paramCollection.Add(param);
                }
            }
            return paramCollection;
        }

        private OracleDbType OracleDbTypeConvert(System.Data.SqlDbType dbtype)
        {
            OracleDbType returnType;
            switch (dbtype)
            {
                case (SqlDbType.VarChar):
                    returnType = OracleDbType.Varchar2;
                    break;
                case (SqlDbType.NVarChar):
                    returnType = OracleDbType.NVarchar2;
                    break;
                case (SqlDbType.Char):
                    returnType = OracleDbType.Char;
                    break;
                case (SqlDbType.Int):
                    returnType = OracleDbType.Int32;
                    break;
                case (SqlDbType.DateTime):
                    returnType = OracleDbType.Date;
                    break;
                case (SqlDbType.Float):
                    returnType = OracleDbType.Decimal;
                    break;
                case (SqlDbType.Decimal):
                    returnType = OracleDbType.Double;
                    break;
                case (SqlDbType.Text):
                    returnType = OracleDbType.Clob;
                    break;
                default:
                    returnType = OracleDbType.Varchar2;
                    break;
            }
            return returnType;
        }



    }
    #endregion

    #region DbAgent Class
    public class DbAgent
    {
        #region Global Property
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand sqlCom = new SqlCommand();
        private SqlDataAdapter sqlAdapeter = new SqlDataAdapter();

        private OracleConnection oraCon = new OracleConnection();
        private OracleCommand oraCom = new OracleCommand();
        private OracleDataAdapter oraAdapeter = new OracleDataAdapter();

        #endregion

        /// <summary>
        /// Sql Transection
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="query">query</param>
        /// <param name="param">parameter</param>
        /// <param name="type">query type</param>
        /// <returns>effect rows</returns>
        #region protected int SqlExcuteNonQuery(string connectionString, string query, SqlParameterCollection param, CommandType type)
        protected int SqlExcuteNonQuery(string connectionString, string query, List<SqlParameter> param, CommandType type)
        {
            int result = 0;
            try
            {
                sqlCon.ConnectionString = connectionString;
                sqlCom.CommandText = Build(query, DbHelper.Dbtype.mssql);
                sqlCom.CommandType = type;
                sqlCom.Connection = sqlCon;
                sqlCom.Parameters.Clear();
                if (param != null)
                {
                    foreach (var data in param)
                    {
                        sqlCom.Parameters.Add(data);
                    }
                }

                sqlCon.Open();
                result = sqlCom.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Oracle Transection Query
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="query">query list</param>
        /// <param name="param">parameter list</param>
        /// <param name="type">query type</param>
        /// <returns></returns>
        #region protected int SqlExcuteNonQuery(string connectionString, string query, SqlParameterCollection param, CommandType type)
        protected int SqlExcuteNonQuery(string connectionString, List<string> query, List<List<SqlParameter>> param, CommandType type)
        {

            int result = 0;
            try
            {
                if (query.Count == param.Count)
                {
                    sqlCon.ConnectionString = connectionString;
                    sqlCon.Open();
                    sqlCom = sqlCon.CreateCommand();
                    SqlTransaction tran = sqlCon.BeginTransaction();
                    sqlCom.Transaction = tran;
                    for (int i = 0; i < query.Count; i++)
                    {
                        sqlCom.CommandText = Build(query[i], DbHelper.Dbtype.mssql);
                        sqlCom.CommandType = type;
                        if (param != null)
                        {
                            sqlCom.Parameters.Clear();
                            foreach (var data in param[i])
                            {
                                sqlCom.Parameters.Add(data);
                            }
                        }
                        result += sqlCom.ExecuteNonQuery();
                    }
                    sqlCom.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlCom.Transaction.Rollback();
                throw ex;
            }
            sqlCon.Close();
            return result;
        }
        #endregion

        /// <summary>
        /// Oracle Query
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="query">query</param>
        /// <param name="param">parameter</param>
        /// <param name="type">query type</param>
        /// <returns>effect rows</returns>
        #region protected int OracleExcuteNonQuery(string connectionString, string query, OracleParameterCollection param, CommandType type)
        protected int OracleExcuteNonQuery(string connectionString, string query, List<OracleParameter> param, CommandType type)
        {
            int result = 0;
            try
            {

                oraCon.ConnectionString = connectionString;
                oraCom.CommandText = Build(query, DbHelper.Dbtype.oracle);
                oraCom.CommandType = type;
                oraCom.Connection = oraCon;
                if (param != null)
                {
                    foreach (var data in param)
                    {
                        oraCom.Parameters.Add(data);
                    }
                }

                oraCon.Open();
                result = oraCom.ExecuteNonQuery();
                oraCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Oracle Transection Query
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="query">query list</param>
        /// <param name="param">parameter list</param>
        /// <param name="type">query type</param>
        /// <returns></returns>
        #region protected int OracleExcuteNonQuery(string connectionString, string query, OracleParameterCollection param, CommandType type)
        protected int OracleExcuteNonQuery(string connectionString, List<string> query, List<List<OracleParameter>> param, CommandType type)
        {

            int result = 0;
            try
            {
                if (query.Count == param.Count)
                {
                    oraCon.ConnectionString = connectionString;
                    oraCon.Open();
                    oraCom = oraCon.CreateCommand();
                    OracleTransaction tran = oraCon.BeginTransaction();
                    oraCom.Transaction = tran;
                    for (int i = 0; i < query.Count; i++)
                    {
                        oraCom.CommandText = Build(query[i], DbHelper.Dbtype.oracle);
                        oraCom.CommandType = type;
                        if (param != null)
                        {
                            oraCom.Parameters.Clear();
                            foreach (var data in param[i])
                            {
                                oraCom.Parameters.Add(data);
                            }
                        }
                        result += oraCom.ExecuteNonQuery();
                    }
                    oraCom.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                oraCom.Transaction.Rollback();
                throw ex;
            }
            oraCon.Close();
            return result;
        }
        #endregion

        /// <summary>
        /// Sql Get DataSet
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="query">query</param>
        /// <param name="param">parameter</param>
        /// <param name="type">query type</param>
        /// <returns>dataset</returns>
        #region protected DataSet SqlGetDataSet(string connectionString, string query, SqlParameterCollection param, CommandType type)
        protected DataSet SqlGetDataSet(string connectionString, string query, List<SqlParameter> param, CommandType type)
        {
            DataSet ds = new DataSet();
            try
            {
                sqlCon.ConnectionString = connectionString;
                sqlCom.CommandText = Build(query, DbHelper.Dbtype.mssql);
                sqlCom.CommandType = type;
                sqlCom.Connection = sqlCon;
                sqlCom.Parameters.Clear();
                if (param != null)
                {
                    foreach (var data in param)
                    {
                        sqlCom.Parameters.Add(data);
                    }
                }
                sqlAdapeter.SelectCommand = sqlCom;
                sqlCon.Open();
                sqlAdapeter.Fill(ds);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        /// <summary>
        /// oracle Get DataSet
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="query">query</param>
        /// <param name="param">parameter</param>
        /// <param name="type">query type</param>
        /// <returns>dataset</returns>
        #region protected DataSet OracleGetDataSet(string connectionString, string query, OracleParameterCollection param, CommandType type)
        protected DataSet OracleGetDataSet(string connectionString, string query, List<OracleParameter> param, CommandType type)
        {
            DataSet ds = new DataSet();
            try
            {
                oraCon.ConnectionString = connectionString;
                oraCom.CommandText = Build(query, DbHelper.Dbtype.oracle);
                oraCom.CommandType = type;
                oraCom.Connection = oraCon;

                if (param != null)
                {
                    foreach (var data in param)
                    {
                        oraCom.Parameters.Add(data);
                    }
                }

                oraAdapeter.SelectCommand = oraCom;
                oraCon.Open();
                oraAdapeter.Fill(ds);
                oraCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        /// <summary>
        /// Sql Get Scalar
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="query">query</param>
        /// <param name="param">parameter</param>
        /// <param name="type">query type</param>
        /// <returns>string data</returns>
        #region protected string SqlGetScarlar(string connectionString, string query, SqlParameterCollection param, CommandType type)
        protected string SqlGetScarlar(string connectionString, string query, List<SqlParameter> param, CommandType type)
        {
            string result = string.Empty;
            try
            {
                sqlCon.ConnectionString = connectionString;
                sqlCom.CommandText = Build(query, DbHelper.Dbtype.mssql);
                sqlCom.CommandType = type;
                sqlCom.Connection = sqlCon;
                sqlCom.Parameters.Clear();
                if (param != null)
                {
                    foreach (var data in param)
                    {
                        sqlCom.Parameters.Add(data);
                    }
                }

                sqlCon.Open();
                result = sqlCom.ExecuteScalar().ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Oracle Get Scalar
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="query">query</param>
        /// <param name="param">parameter</param>
        /// <param name="type">query type</param>
        /// <returns>string data</returns>
        #region protected string OracleGetScarlar(string connectionString, string query, OracleParameterCollection param, CommandType type)
        protected string OracleGetScarlar(string connectionString, string query, List<OracleParameter> param, CommandType type)
        {
            string result = string.Empty;
            try
            {
                oraCon.ConnectionString = connectionString;
                oraCom.CommandText = Build(query, DbHelper.Dbtype.oracle);
                oraCom.CommandType = type;
                oraCom.Connection = oraCon;
                if (param != null)
                {
                    foreach (var data in param)
                    {
                        oraCom.Parameters.Add(data);
                    }
                }

                oraCon.Open();
                result = oraCom.ExecuteScalar().ToString();
                oraCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// query convert
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        #region query convert
        private string Build(string qry, DbHelper.Dbtype dbType)
        {
            string returnQuery = string.Empty;
            if (dbType == DbHelper.Dbtype.oracle)
            {
                returnQuery = qry.ToUpper().
                    Replace("ISNULL", "NVL").
                    Replace("LEN", "LENGTH").
                    Replace("GETDATE()", "SYSDATE").
                    // Replace("''", "''").
                    Replace("+", "||").
                    Replace("@", ":").
                    Replace("\r\n", " ");
            }
            else
            {
                returnQuery = qry.ToUpper().
                    Replace("NVL", "ISNULL").
                    Replace("LENGTH", "LEN").
                    Replace("SYSDATE", "GETDATE()").
                    //Replace("' '", "''").
                    Replace("||", "+").
                    Replace(":", "@").
                    Replace("\r\n", " ");
            }

            return returnQuery;
        }
        #endregion
    }
    #endregion
}
