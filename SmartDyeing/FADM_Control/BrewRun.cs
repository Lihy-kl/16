using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class BrewRun : UserControl
    {
        public BrewRun()
        {
            InitializeComponent();
            //打开定时器
            this.tmr.Enabled = true;
        }

        private void tstxt_ProcessPageNow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (tstxt_ProcessPageNow.Text != "" && tstxt_ProcessPageNow.Text != null)
                {
                    try
                    {
                        int i_now = Convert.ToInt32(tstxt_ProcessPageNow.Text);
                        if (i_now < 1 || i_now > this._i_pagecount)
                        {
                            tstxt_ProcessPageNow.Text = "1";
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                throw new Exception("输入页号异常！");
                            else
                                throw new Exception("Input page number exception!");
                        }
                        else
                        {
                            this._i_pagenow = i_now;
                            this._i_rownow = this._i_pagesize * (this._i_pagenow - 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        FADM_Form.CustomMessageBox.Show(ex.Message, "tstxt_ProcessPageNow_KeyPress", MessageBoxButtons.OK, true);
                    }
                }
            }
        }

        /// <summary>
        /// 每页行数
        /// </summary>
        private int _i_pagesize = 34;

        /// <summary>
        /// 总记录数
        /// </summary>
        private int _i_max = 0;

        /// <summary>
        /// 总页数
        /// </summary>
        private int _i_pagecount = 0;

        /// <summary>
        /// 当前页号
        /// </summary>
        private int _i_pagenow = 1;

        /// <summary>
        /// 上次页号
        /// </summary>
        private int _i_pagelast = 1;

        /// <summary>
        /// 当前行号
        /// </summary>
        private int _i_rownow = 0;

        /// <summary>
        /// 上次行号
        /// </summary>
        private int _i_rowlast = 0;

        /// <summary>
        /// 运行表
        /// </summary>
        private DataTable _dt_data = null;

        /// <summary>
        /// 查询：
        /// true:正在查询
        /// false:未查询
        /// </summary>
        private bool _b_select = false;

        /// <summary>
        /// 复位标志位
        /// </summary>
        private bool _b_reset = false;

        /// <summary>
        /// 显示流程表
        /// </summary>
        private void table()
        {
            try
            {
                lock (this)
                {
                    //获取数据
                    if (_b_select == false)
                    {
                        string s_sql = "select * from brew_run_table order by MyID desc;";
                        _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    }

                    if (_dt_data.Rows.Count > 0 && (this._i_max != _dt_data.Rows.Count
                             || (this._b_select && this._i_pagenow != this._i_pagelast)
                             || this._b_reset))
                    {
                        if (this._i_pagenow == this._i_pagelast)
                        {
                            //计算总记录数
                            this._i_max = _dt_data.Rows.Count;

                            //计算总页数
                            this._i_pagecount = (this._i_max / this._i_pagesize);
                            if (this._i_max % this._i_pagesize > 0)
                            {
                                this._i_pagecount++;
                            }
                        }

                        //克隆结构框架
                        DataTable dt_temp = _dt_data.Clone();

                        int i_startpos = 0,
                            P_int_endpos = 0;
                        //计算结束行
                        if (this._i_pagenow == this._i_pagecount)
                        {
                            P_int_endpos = this._i_max;
                        }
                        else
                        {
                            P_int_endpos = this._i_pagesize * this._i_pagenow;
                        }

                        //计算开始行
                        if (this._i_pagelast != this._i_pagenow)
                        {
                            i_startpos = this._i_rownow;
                            this._i_rowlast = this._i_rownow;
                        }
                        else
                        {
                            i_startpos = this._i_rowlast;
                        }

                        //从数据源复制记录行
                        for (int i = i_startpos; i < P_int_endpos; i++)
                        {
                            dt_temp.ImportRow(_dt_data.Rows[i]);
                            this._i_rownow++;
                        }

                        //赋值
                        this.tslab_ProcessAllPage.Text = this._i_pagecount.ToString();

                        this.tstxt_ProcessPageNow.Text = this._i_pagenow.ToString();

                        this.tslab_ProcessAllNum.Text = this._i_max.ToString();

                        //绑定数据源
                        this.bds_Run.DataSource = dt_temp;

                        this.bdn_Run.BindingSource = this.bds_Run;

                        this.dgv_Run.DataSource = this.bds_Run;
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        {
                            //设置单元格格式
                            string[] sa_name = { "序号",  "时间", "状态", "信息" };
                            int[] ia_width = { 100, 300, 100, 1400 };
                            for (int i = 0; i < sa_name.Length; i++)
                            {
                                this.dgv_Run.Columns[i].HeaderCell.Value = sa_name[i];
                                this.dgv_Run.Columns[i].Width = ia_width[i];
                            }

                        }
                        else
                        {
                            //设置单元格格式
                            string[] sa_name = { "ID", "DateTime", "Status", "Info"};
                            int[] ia_width = { 100, 300, 100, 1400 };
                            for (int i = 0; i < sa_name.Length; i++)
                            {
                                this.dgv_Run.Columns[i].HeaderCell.Value = sa_name[i];
                                this.dgv_Run.Columns[i].Width = ia_width[i];
                            }
                        }
                        this._i_pagelast = this._i_pagenow;

                        if (this._b_reset)
                        {
                            this._b_reset = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "table", MessageBoxButtons.OK, true);
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            //显示表
            this.table();
        }

        private void bdn_Run_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "tsbtn_ProcessUpPage":
                    if (this._i_pagenow <= 1)
                    {

                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已经是首页！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("It's already the homepage！", "Tips", MessageBoxButtons.OK, false);
                    }
                    else
                    {
                        this._i_pagenow--;
                        this._i_rownow = this._i_pagesize * (this._i_pagenow - 1);
                    }
                    this._b_select = true;
                    break;

                case "tsbtn_ProcessDownPage":
                    if (this._i_pagenow >= this._i_pagecount)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已经是尾页！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("It's already the last page！", "Tips", MessageBoxButtons.OK, false);
                    }
                    else
                    {
                        this._i_pagenow++;
                        this._i_rownow = this._i_pagesize * (this._i_pagenow - 1);
                    }
                    this._b_select = true;
                    break;

                case "tsbtn_ProcessFirstPage":
                    if (this._i_pagenow <= 1)
                    {
                        if (this._b_select == false)
                        {

                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("已经是首页！", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("It's already the homepage！", "Tips", MessageBoxButtons.OK, false);
                        }
                    }
                    else
                    {
                        this._i_pagenow = 1;
                        this._i_rownow = 0;
                    }
                    this._b_select = false;
                    this._b_reset = true;
                    break;

                case "tsbtn_ProcessEndPage":
                    if (this._i_pagenow >= this._i_pagecount)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已经是尾页！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("It's already the last page！", "Tips", MessageBoxButtons.OK, false);
                    }
                    else
                    {
                        this._i_pagenow = this._i_pagecount;
                        this._i_rownow = this._i_pagesize * (this._i_pagenow - 1);
                    }
                    this._b_select = true;
                    break;

                default:
                    break;

            }
        }
    }
}
