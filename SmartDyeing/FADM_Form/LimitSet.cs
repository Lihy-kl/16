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
    public partial class LimitSet : Form
    {
        public LimitSet()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            ShowData(1);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //先判断范围是否正确
            int i_columns = 0;
            for (int i = 1; i < dataGridView1.Columns.Count - 2; i++)
            {
                if (dataGridView1.Rows[0].Cells[i].Value == null || Convert.ToString(dataGridView1.Rows[0].Cells[i].Value) == "")
                {
                    if (dataGridView1.Rows[0].Cells[i + 1].Value != null && Convert.ToString(dataGridView1.Rows[0].Cells[i + 1].Value) != "")
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("范围录入错误", "保存按钮", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Range input error", "save button", MessageBoxButtons.OK, false);
                        return;
                    }
                }
            }

            //后一值要比前一值大
            for (int i = 2; i < dataGridView1.Columns.Count - 1; i++)
            {
                if (dataGridView1.Rows[0].Cells[i].Value == null || Convert.ToString(dataGridView1.Rows[0].Cells[i].Value) == "")
                {
                }
                else
                {
                    if (Convert.ToDouble(dataGridView1.Rows[0].Cells[i].Value) < Convert.ToDouble(dataGridView1.Rows[0].Cells[i - 1].Value))
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("范围后值小于前值", "保存按钮", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("The value after the range is smaller than the previous value", "save button", MessageBoxButtons.OK, false);
                        return;
                    }
                }
            }
            //计算总共列数
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Rows[0].Cells[i].Value == null || Convert.ToString(dataGridView1.Rows[0].Cells[i].Value) == "")
                {
                    break;
                }
                i_columns++;
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int p = 0; p < i_columns; p++)
                {
                    if (dataGridView1.Rows[i].Cells[p].Value == null || Convert.ToString(dataGridView1.Rows[i].Cells[p].Value) == "")
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("存在空值", "保存按钮", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("There is a null value present", "save button", MessageBoxButtons.OK, false);
                        return;
                    }
                }
            }

            if (radioButton1.Checked)
            {
                //先删除，再添加
                string s_sql = "Delete  FROM LimitTable WHERE Type = 1 ; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                try
                {
                    for (int i = 1; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int p = 1; p < i_columns; p++)
                        {
                            s_sql = "Insert into  LimitTable(Name,Type,Max,Value) Values('" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "',1," + dataGridView1.Rows[0].Cells[p].Value.ToString() + "," + dataGridView1.Rows[i].Cells[p].Value.ToString() + ");";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                    }
                }
                catch
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("保存失败，请重新点击保存", "保存按钮", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Save failed, please click save again", "save button", MessageBoxButtons.OK, false);
                }

            }
            else if (radioButton2.Checked)
            {
                //先删除，再添加
                string s_sql = "Delete  FROM LimitTable WHERE Type = 2 ; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                try
                {
                    for (int i = 1; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int p = 1; p < i_columns; p++)
                        {
                            s_sql = "Insert into  LimitTable(Name,Type,Max,Value) Values('" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "',2," + dataGridView1.Rows[0].Cells[p].Value.ToString() + "," + dataGridView1.Rows[i].Cells[p].Value.ToString() + ");";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                    }
                }
                catch
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("保存失败，请重新点击保存", "保存按钮", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Save failed, please click save again", "save button", MessageBoxButtons.OK, false);
                }
            }
            else if (radioButton3.Checked)
            {
                //先删除，再添加
                string s_sql = "Delete  FROM LimitTable WHERE Type = 3 ; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                try
                {
                    for (int i = 1; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int p = 1; p < i_columns; p++)
                        {
                            s_sql = "Insert into  LimitTable(Name,Type,Max,Value) Values('" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "',3," + dataGridView1.Rows[0].Cells[p].Value.ToString() + "," + dataGridView1.Rows[i].Cells[p].Value.ToString() + ");";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                    }
                }
                catch
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("保存失败，请重新点击保存", "保存按钮", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Save failed, please click save again", "save button", MessageBoxButtons.OK, false);
                }
            }
        }

        private void LimitSet_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Cells[0].ReadOnly = true;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                dataGridView1.Rows[0].Cells[0].Value = "染料浓度(%)";
            }
            else
            {
                dataGridView1.Rows[0].Cells[0].Value = "DyeConcentration(%)";
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {

                this.dataGridView1.Columns[i].Width = 100;
            }

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex > 0)
                {
                    ((DataGridViewTextBoxEditingControl)e.Control).KeyPress += dgv_KeyPress;
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dataGridView1_EditingControlShowing", MessageBoxButtons.OK, false);
            }
        }

        void dgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex > 0)
                e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void ShowData(int type)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add();
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                dataGridView1.Rows[0].Cells[0].Value = "染料浓度(%)";
            }
            else
            {
                dataGridView1.Rows[0].Cells[0].Value = "DyeConcentration(%)";
            }

            string s_sql = "SELECT Name  FROM LimitTable WHERE" +
                           " Type = '" + type.ToString() + "' group by Name; ";
            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            List<string> lis_name = new List<string>();

            for (int i = 0; i < dt_data.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i + 1].Cells[0].Value = dt_data.Rows[i]["Name"].ToString();
                lis_name.Add(dt_data.Rows[i]["Name"].ToString());
            }

            string s_sql1 = "SELECT Max  FROM LimitTable WHERE" +
                           " Type = '" + type.ToString() + "' group by Max order by Max; ";
            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);

            for (int i = 0; i < dt_data1.Rows.Count; i++)
            {
                dataGridView1.Rows[0].Cells[i + 1].Value = dt_data1.Rows[i]["Max"].ToString();
            }

            for (int i = 0; i < lis_name.Count; i++)
            {
                s_sql = "Select Value from LimitTable where " + " Type = '" + type.ToString() + "' and Name = '" + lis_name[i] + "' order by Max;";
                dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                for (int p = 0; p < dt_data.Rows.Count; p++)
                {
                    dataGridView1.Rows[i + 1].Cells[p + 1].Value = dt_data.Rows[p]["Value"].ToString();
                }
            }

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            ShowData(1);
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            ShowData(2);
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            ShowData(3);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {


                case Keys.Delete:

                    try
                    {
                        if (dataGridView1.CurrentCell.RowIndex > 0)
                        {
                            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        }
                    }
                    catch
                    {

                    }
                    break;
                default:
                    break;
            }
        }
    }
}
