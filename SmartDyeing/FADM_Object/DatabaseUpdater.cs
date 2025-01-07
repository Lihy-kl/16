using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDyeing.FADM_Object
{
    public class DatabaseUpdater
    {
        private string _connectionString;



        public DatabaseUpdater(Lib_DataBank.SQLServer.SQLServerCon s_SQLServerCon)
        {
            _connectionString = "Server=" + s_SQLServerCon.Server + "," + s_SQLServerCon.Port +
                ";Database=" + s_SQLServerCon.Database +
                ";uid=" + s_SQLServerCon.UserName +
                ";pwd=" + s_SQLServerCon.Password;
        }
        public DatabaseUpdater(String conPar)
        {
            _connectionString = conPar;
        }
        public void CreateTableIfNotExists(string tableName, string columnDefinitions)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // 检查表是否存在
                string checkTableQuery = $@"
                IF NOT EXISTS (
                    SELECT * 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = '{tableName}'
                )
                BEGIN
                    CREATE TABLE {tableName} ({columnDefinitions});
                END";

                using (SqlCommand command = new SqlCommand(checkTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddColumnIfNotExists(string tableName, string columnName, string columnType)
        {
            try {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // 检查列是否存在
                    string checkColumnQuery = $@"
                IF NOT EXISTS (
                    SELECT * 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = '{tableName}' AND COLUMN_NAME = '{columnName}'
                )
                BEGIN
                    ALTER TABLE {tableName} ADD {columnName} {columnType};
                END";
                    using (SqlCommand command = new SqlCommand(checkColumnQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
           
        }

        /// <summary>
        /// SQLServer配置参数
        /// </summary>
        
    }
}
