//using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace Lib_DataBank
{
    public class SQLServer
    {
        private string Con { get; set; }

        private SqlConnection m_Connection;


        /// <summary>
        /// SQLServer配置参数
        /// </summary>
        public struct SQLServerCon
        {
            /// <summary>
            /// 数据库IP
            /// </summary>
            public string Server { get; set; }

            /// <summary>
            /// 端口号
            /// </summary>
            public string Port { get; set; }

            /// <summary>
            /// 数据库名称
            /// </summary>
            public string Database { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }
        }
        public SQLServer(SQLServerCon s_SQLServerCon)
        {
            Con = "Server=" + s_SQLServerCon.Server + "," + s_SQLServerCon.Port +
                ";Database=" + s_SQLServerCon.Database +
                ";uid=" + s_SQLServerCon.UserName +
                ";pwd=" + s_SQLServerCon.Password;
        }

        public SQLServer(String  conPar)
        {
            Con = conPar;
        }

        /// <summary>
        /// 数据库打开
        /// </summary>
        public void Open()
        {
            m_Connection = new SqlConnection(Con);
            m_Connection.Open();
        }

        /// <summary>
        /// 数据库关闭
        /// </summary>
        public void Close()
        {
            if (m_Connection != null)
            {
                if (m_Connection.State == ConnectionState.Open)
                {
                    m_Connection.Close();
                    m_Connection.Dispose();
                }
            }
        }

        /// <summary>
        /// 插入播报内容
        /// </summary>
        /// <param name="sSql">sql语句</param>
        /// <returns></returns>
        public void InsertSpeechInfo(string sInfo)
        {
            sInfo = sInfo.Replace(",", ";");
            sInfo = sInfo.Replace("，", ";");
            DataTable dt = GetData("SELECT * FROM SpeechInfo WHERE Info = '" + sInfo + "'");

            if (dt.Rows.Count == 0)
            {
                string sSql = "Insert Into SpeechInfo(Time, Info,IsFinished) values('" + DateTime.Now.ToLongTimeString() + "','" + sInfo + "',0);";
                ReviseData(sSql);
            }
        }


        /// <summary>
        /// 删除播报内容
        /// </summary>
        /// <param name="sSql">sql语句</param>
        /// <returns></returns>
        public void DeleteSpeechInfo(string sInfo)
        {
            sInfo = sInfo.Replace(",", ";");
            sInfo = sInfo.Replace("，", ";");
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    DataTable dt = GetData("SELECT * FROM SpeechInfo WHERE Info = '" + sInfo + "'");

                    if (dt.Rows.Count == 0)
                    {
                        break;
                    }
                    if (Convert.ToInt16(dt.Rows[0]["IsFinished"]) == 1)
                    {
                        string sSql = "Delete from SpeechInfo where Info = '" + sInfo + "';";
                        ReviseData(sSql);
                    }
                    Thread.Sleep(1);
                }
            });

            thread.Start();

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sSql">sql语句</param>
        /// <returns></returns>
        public DataTable GetData(string sSql)
        {
            lock (this)
            {
                DataTable dt = new DataTable();
                try
                {
                    Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sSql, m_Connection);

                    sqlDataAdapter.Fill(dt);
                    Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    Lib_Log.Log.writeLogException(ex + ":" + sSql);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="sSql">sql语句</param>
        public void ReviseData(string sSql)
        {
            lock (this)
            {
                try
                {
                    Open();
                    SqlCommand sqlCommand = new SqlCommand(sSql, m_Connection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    Close();
                }
                catch (Exception ex)
                {
                    Lib_Log.Log.writeLogException(ex + ":" + sSql);

                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="s">过程温度数据</param>
        public void SetImage(string s, int CupNum, string BatchName)
        {
            lock (this)
            {
                try
                {
                    if (s == "")
                        return;
                    Open();
                    byte[] bytStr = System.Text.Encoding.Default.GetBytes(Base64Encrypt(s));
                    string sql = "Update history_head set ProcessData = @Image where BatchName = '" + BatchName + "' and CupNum = " + CupNum.ToString() + ";";

                    SqlCommand sqlCommand = new SqlCommand(sql, m_Connection);
                    sqlCommand.Parameters.Add("@Image", SqlDbType.Image);
                    sqlCommand.Parameters["@Image"].Value = bytStr;
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    Close();
                }
                catch
                {

                }
            }
        }

        public string GetImage(int CupNum, string BatchName)
        {
            lock (this)
            {
                try
                {
                    Open();
                    string sql = "select * from history_head where BatchName = '" + BatchName + "' and CupNum = " + CupNum.ToString() + ";";
                    SqlCommand sqlCommand = new SqlCommand(sql, m_Connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();
                    if (reader["ProcessData"] is DBNull)
                        return "";
                    byte[] bytStr = (byte[])reader["ProcessData"];
                    string s = Base64Decrypt(System.Text.Encoding.Default.GetString(bytStr));
                    Close();
                    return s;
                }
                catch { return ""; }
            }
        }

        #region Base64加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
        #endregion

        /// <summary>
        /// 插入运行表
        /// </summary>
        /// <param name="sName">字段名</param>
        /// <param name="sValues">内容</param>
        public void InsertRun(string sName, string sValues)
        {
            Thread thread = new Thread(() =>
            {
                lock (this)
                {
                    try
                    {
                        DataTable dataTable = GetData("SELECT * FROM run_table order by MyID DESC;");
                        if (dataTable.Rows.Count > 0)
                        {
                            string sLast = string.Format("{0:d}", dataTable.Rows[0]["MyDate"]);
                            string sNow = string.Format("{0:d}", DateTime.Now);
                            if (sLast != sNow)
                                ReviseData("TRUNCATE TABLE run_table;");
                        }

                        string sSql = "INSERT INTO run_table ( MyDate, MyTime, " + sName + ")" +
                            " VALUES('" + String.Format("{0:d}", DateTime.Now) + "', '" +
                            String.Format("{0:T}", DateTime.Now) + "', '" + sValues + "');";
                        ReviseData(sSql);
                    }
                    catch (Exception ex)
                    {
                        Lib_Log.Log.writeLogException(ex);

                    }


                }
            });
            thread.Start();
        }
    }
}