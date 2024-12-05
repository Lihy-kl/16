using Lib_DataBank.MySQL;
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
    public partial class ReportPrint : Form
    {
        private string _s_batchName = null;
        private string _s_cupNum = null;
        public ReportPrint(string s_bN,string s_cupNum)
        {
            _s_batchName = s_bN;
            _s_cupNum=s_cupNum;
            InitializeComponent();
        }

        private void ReportPrint_Load(object sender, EventArgs e)
        {
            //string s_path = Environment.CurrentDirectory + "\\Config\\DataBase.ini";
            //Lib_DataBank.SQLServer.SQLServerCon con = new Lib_DataBank.SQLServer.SQLServerCon()
            //{
            //    Server = Lib_File.Ini.GetIni("FADM", "Server", s_path),
            //    Port = Lib_File.Ini.GetIni("FADM", "Port", s_path),
            //    Database = Lib_File.Ini.GetIni("FADM", "Database", s_path),
            //    UserName = Lib_File.Ini.GetIni("FADM", "UserName", s_path),
            //    Password = Lib_File.Ini.GetIni("FADM", "Password", s_path)
            //};

            //FADM_Object.Communal._fadmSqlserver = new Lib_DataBank.SQLServer(con);
            //FADM_Object.Communal._fadmSqlserver.Open();
            //FADM_Object.Communal._fadmSqlserver.Close();

            string s_sql = "SELECT AssistantCode,AssistantName,FormulaDosage as SettingConcentration,RealConcentration,UnitOfAccount  FROM history_details WHERE BatchName = '" + _s_batchName + "' And CupNum = '" + _s_cupNum + "';";

            DataTable dt = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Details", dt));

            DataTable dt_fd = new DataTable();
            s_sql = "SELECT FormulaCode,FormulaName,FinishTime,CreateTime,'" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' as PrintTime  FROM history_head WHERE BatchName = '" + _s_batchName + "' And CupNum = '" + _s_cupNum + "';";
            dt_fd = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Head", dt_fd));

            this.reportViewer1.RefreshReport();

        }

        private void LoadReport()
        {
            // 创建一个新的 LocalReport 对象
            LocalReport report = new LocalReport();
            report.ReportEmbeddedResource = "Report2.rdlc"; // 指定嵌入的 RDLC 文件路径

            // 创建一个 DataTable 并添加一些示例数据
            DataTable dataTable = new DataTable("NewDataSet");
            dataTable.Columns.Add("Column1", typeof(string));
            dataTable.Columns.Add("Column2", typeof(string));
            dataTable.Rows.Add("数据1", "数据2");
            dataTable.Rows.Add("数据3", "数据4");

            // 创建一个 ReportDataSource 并将 DataTable 绑定到报表
            ReportDataSource rds = new ReportDataSource("NewDataSet", dataTable);
            report.DataSources.Add(rds);
            ReportDataSource rds1 = new ReportDataSource("DataSet1", dataTable);
            report.DataSources.Add(rds1);

            // 动态生成表格
            string rdlcContent = GenerateRdlcContent();
            report.LoadReportDefinition(new System.IO.StringReader(rdlcContent));

            // 配置 ReportViewer 控件
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = null; // 清除现有路径
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rds1);
            reportViewer1.LocalReport.LoadReportDefinition(new System.IO.StringReader(rdlcContent));
            reportViewer1.RefreshReport();
        }

        private string GenerateRdlcContent()
        {
            // 动态生成 RDLC 内容
            return @"
<Report xmlns:rd='http://schemas.microsoft.com/SQLServer/reporting/reportdesigner' xmlns='http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition'>
  <DataSources>
    <DataSource Name='NewDataSet'>
      <ConnectionProperties>
        <DataProvider>System.Data.DataSet</DataProvider>
        <ConnectString>/* Local Connection */</ConnectString>
      </ConnectionProperties>
    </DataSource>
  </DataSources>
  <Body>
    <ReportItems>
      <Tablix Name='Tablix1'>
        <TablixBody>
          <TablixColumns>
            <TablixColumn>
              <Width>2in</Width>
            </TablixColumn>
            <TablixColumn>
              <Width>2in</Width>
            </TablixColumn>
          </TablixColumns>
          <TablixRows>
            <TablixRow>
              <Height>0.25in</Height>
              <TablixCells>
                <TablixCell>
                  <CellContents>
                    <Textbox Name='Textbox1'>
                      <Value>=Fields!Column1.Value</Value>
                    </Textbox>
                  </CellContents>
                </TablixCell>
                <TablixCell>
                  <CellContents>
                    <Textbox Name='Textbox2'>
                      <Value>=Fields!Column2.Value</Value>
                    </Textbox>
                  </CellContents>
                </TablixCell>
              </TablixCells>
            </TablixRow>
          </TablixRows>
        </TablixBody>
        <TablixColumnHierarchy>
          <TablixMembers>
            <TablixMember />
            <TablixMember />
          </TablixMembers>
        </TablixColumnHierarchy>
        <TablixRowHierarchy>
          <TablixMembers>
            <TablixMember />
          </TablixMembers>
        </TablixRowHierarchy>
        <DataSetName>NewDataSet</DataSetName>
      </Tablix>
    </ReportItems>
    <Height>2in</Height>
  </Body>
  <Width>4in</Width>
  <Page>
    <PageHeight>11in</PageHeight>
    <PageWidth>8.5in</PageWidth>
    <LeftMargin>1in</LeftMargin>
    <RightMargin>1in</RightMargin>
    <TopMargin>1in</TopMargin>
    <BottomMargin>1in</BottomMargin>
  </Page>
</Report>";
        }
    }
}
