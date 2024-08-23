using System.Data;
using MySql.Data.MySqlClient;

namespace Lib_DataBank.MySQL
{
    public class MySQL
    {
        private string Con { get; set; }

        private MySqlConnection m_Connection;

        /// <summary>
        /// MySQL配置参数
        /// </summary>
        public struct MySQLCon
        {
            /// <summary>
            /// 数据库IP
            /// </summary>
            public string DataSource { get; set; }

            /// <summary>
            /// 端口号
            /// </summary>
            public string Port { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }
        }

        public MySQL(MySQLCon s_MySQLCon)
        {
            Con = "DataSource=" + s_MySQLCon.DataSource +
                ";Port=" + s_MySQLCon.Port +
                ";UserName=" + s_MySQLCon.UserName +
                ";Password=" + s_MySQLCon.Password;
        }

        /// <summary>
        /// 数据库打开
        /// </summary>
        public void Open()
        {
            m_Connection = new MySqlConnection(Con);
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
        /// 获取数据
        /// </summary>
        /// <param name="sSql">sql语句</param>
        /// <returns></returns>
        public DataTable GetData(string sSql)
        {
            Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sSql, m_Connection);
            DataTable dt = new DataTable();
            mySqlDataAdapter.Fill(dt);
            Close();
            return dt;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="sSql">sql语句</param>
        public void ReviseData(string sSql)
        {
            Open();
            MySqlCommand mySqlCommand = new MySqlCommand(sSql, m_Connection);
            mySqlCommand.ExecuteNonQuery();
            mySqlCommand.Dispose();
            Close();
        }

    }
}
