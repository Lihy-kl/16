using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public partial class ReportTest : Form
    {
        private string _s_batchName = null;
        private string _s_cupNum = null;
        public ReportTest(string s_bN, string s_cupNum)
        {
            _s_batchName = s_bN;
            _s_cupNum = s_cupNum;
            InitializeComponent();
        }

        private Dictionary<(int, int), (int, int)> mergedCells = new Dictionary<(int, int), (int, int)>();

        private void ReportTest_Load(object sender, EventArgs e)
        {

            string s_path = Environment.CurrentDirectory + "\\Config\\DataBase.ini";
            Lib_DataBank.SQLServer.SQLServerCon con = new Lib_DataBank.SQLServer.SQLServerCon()
            {
                Server = Lib_File.Ini.GetIni("FADM", "Server", s_path),
                Port = Lib_File.Ini.GetIni("FADM", "Port", s_path),
                Database = Lib_File.Ini.GetIni("FADM", "Database", s_path),
                UserName = Lib_File.Ini.GetIni("FADM", "UserName", s_path),
                Password = Lib_File.Ini.GetIni("FADM", "Password", s_path)
            };

            FADM_Object.Communal._fadmSqlserver = new Lib_DataBank.SQLServer(con);
            FADM_Object.Communal._fadmSqlserver.Open();
            FADM_Object.Communal._fadmSqlserver.Close();

            string s_sql = "SELECT AssistantCode,AssistantName,FormulaDosage as SettingConcentration,RealConcentration,UnitOfAccount  FROM history_details WHERE BatchName = '" + _s_batchName + "' And CupNum = '" + _s_cupNum + "';";

            DataTable dt = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Details", dt));

            DataTable dt_fd = new DataTable();
            s_sql = "SELECT FormulaCode,FormulaName,FinishTime,CreateTime,'" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' as PrintTime  FROM history_head WHERE BatchName = '" + _s_batchName + "' And CupNum = '" + _s_cupNum + "';";
            dt_fd = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Head", dt_fd));

            this.reportViewer1.RefreshReport();
        }
    }
}
