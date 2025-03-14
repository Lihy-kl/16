using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Windows.Forms.VisualStyles;
using SmartDyeing.FADM_Control;
using System.Drawing;

namespace SmartDyeing.FADM_Object
{
    public class MyDataGridView : DataGridView
    {
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    
                    this.EndEdit();
                    if (this.Name == "dgv_FormulaData")
                    {
                        int i_col = this.CurrentCell.ColumnIndex;

                        int i_row = this.CurrentCell.RowIndex;

                        if (i_col >= 3)
                        {
                            if (i_col == 3)
                            {
                                if (this.CurrentCell.Value != null && this[4, this.CurrentRow.Index].Value != null && this[3, this.CurrentRow.Index].Value != null)
                                {
                                    if (this[4, this.CurrentRow.Index].Value.ToString() == "%")
                                    {
                                        if (this[3, this.CurrentRow.Index].Value != null && this[3, this.CurrentRow.Index].Value.ToString() != "" && Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_DyeAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }
                                    else
                                    {

                                        if (this[3, this.CurrentRow.Index].Value != null && this[3, this.CurrentRow.Index].Value.ToString() != "" && Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_AdditivesAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }
                                    bool b = this[4, this.CurrentRow.Index] is DataGridViewComboBoxCell;
                                    if (b) {
                                        //把手动选瓶去掉
                                        DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)this[10, this.CurrentRow.Index];
                                        dc.Value = 0;
                                    }

                                    

                                }
                            }
                            i_col = 0;

                            if (i_row == this.NewRowIndex)
                            {
                                //判断上一行是否为空行
                                string s_1 = null;
                                string s_2 = null;
                                try
                                {
                                    if (this.CurrentCell.Value != null)
                                        s_1 = this.CurrentCell.Value.ToString();
                                    if (this[1, this.CurrentRow.Index].Value != null)
                                        s_2 = this[1, this.CurrentRow.Index].Value.ToString();
                                }
                                catch
                                {

                                }
                                if (s_1 != null || s_2 != null)
                                {
                                    this.Rows.Add();
                                }
                                else
                                {
                                    this.CurrentCell.Selected = false;
                                    return true;
                                }

                            }
                            i_row++;
                            this[i_col, i_row].Value = i_row + 1;
                            this.CurrentCell = this[i_col + 1, i_row];
                        }
                        else if (i_col == 1)
                        {

                            try
                            {
                                if (this.CurrentRow.Cells[1].Value != null)
                                {
                                    //判断该染助剂是否存在
                                    string s_sql = "SELECT *  FROM assistant_details WHERE" +
                                                       " AssistantCode = '" + this.CurrentRow.Cells[1].Value.ToString() + "' ; ";

                                    DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                    if (dt_assistant.Rows.Count <= 0)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        {
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                            "染助剂代码不存在,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                            {
                                                for (int i = 1; i < this.Columns.Count; i++)
                                                {
                                                    this.CurrentRow.Cells[i].Value = null;
                                                }
                                                return true;

                                            }
                                        }
                                        else
                                        {
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                            " Dyeing agent code does not exist, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                            {
                                                for (int i = 1; i < this.Columns.Count; i++)
                                                {
                                                    this.CurrentRow.Cells[i].Value = null;
                                                }
                                                return true;

                                            }
                                        }

                                    }

                                    //判断是否已经输过相同的染助剂代码
                                    foreach (DataGridViewRow dgr in this.Rows)
                                    {
                                        if (this.CurrentRow.Cells[1].Value != null && dgr.Cells[1].Value != null)
                                        {
                                            if ((dgr.Cells[1].Value.ToString().ToLower() == this.CurrentRow.Cells[1].Value.ToString().ToLower()) &&
                                                dgr.Index != this.CurrentRow.Index)
                                            {
                                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                                    "染助剂代码重复,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                                    {
                                                        for (int i = 1; i < this.Columns.Count; i++)
                                                        {
                                                            this.CurrentRow.Cells[i].Value = null;
                                                        }
                                                        return true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                                    " Dyeing agent code is duplicate, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                                    {
                                                        for (int i = 1; i < this.Columns.Count; i++)
                                                        {
                                                            this.CurrentRow.Cells[i].Value = null;
                                                        }
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (dt_assistant.Rows.Count > 0)
                                    {
                                        List<string> lis_bottleNum = new List<string>();
                                        this.CurrentRow.Cells[2].Value = dt_assistant.Rows[0]["AssistantName"].ToString();
                                        string UnitOfAccount = dt_assistant.Rows[0]["UnitOfAccount"].ToString();

                                        bool b = this[4, i_row] is DataGridViewComboBoxCell;
                                        if (b)
                                        {
                                            DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)this[4, i_row];
                                            if (FADM_Object.Communal._b_isUnitChange)
                                            {
                                                if (UnitOfAccount.Equals("%"))
                                                {
                                                    lis_bottleNum.Add("%");
                                                    dd.Value = null;
                                                    dd.DataSource = lis_bottleNum;
                                                    dd.Value = lis_bottleNum[0].ToString();
                                                }
                                                else if (UnitOfAccount.Equals("g/l"))
                                                {
                                                    if (dd.Value == null)
                                                    {
                                                        lis_bottleNum.Add("g/l");
                                                        lis_bottleNum.Add("%");
                                                        dd.DataSource = lis_bottleNum;
                                                        dd.Value = lis_bottleNum[0].ToString();
                                                    }
                                                    else
                                                    {
                                                        if (dd.Value.Equals("%"))
                                                        {
                                                            lis_bottleNum.Add("%");
                                                            lis_bottleNum.Add("g/l");
                                                            dd.DataSource = lis_bottleNum;
                                                            dd.Value = lis_bottleNum[0].ToString();
                                                        }


                                                    }

                                                }
                                                else
                                                {
                                                    lis_bottleNum.Add(UnitOfAccount);
                                                    dd.DataSource = lis_bottleNum;
                                                    dd.Value = lis_bottleNum[0].ToString();
                                                }
                                            }
                                            else
                                            {
                                                lis_bottleNum.Add(UnitOfAccount);
                                                dd.DataSource = lis_bottleNum;
                                                dd.Value = lis_bottleNum[0].ToString();
                                            }
                                        }
                                        else {
                                            this.CurrentRow.Cells[4].Value = UnitOfAccount;
                                        }
                                        
                                        
                                        


                                       

                                    }
                                }
                            }
                            catch
                            {

                            }

                            this.CurrentCell = this[i_col + 2, i_row];

                        }
                    }
                    else if (this.Name == "dgv_FormulaGroup")
                    {
                        int i_col = this.CurrentCell.ColumnIndex;

                        int i_row = this.CurrentCell.RowIndex;

                        if (i_col == 1)
                        {
                            if (i_row == this.NewRowIndex)
                            {
                                //判断上一行是否为空行
                                string s_1 = null;
                                try
                                {
                                    if (this.CurrentCell.Value != null)
                                        s_1 = this.CurrentCell.Value.ToString();
                                }
                                catch
                                {

                                }
                                if (s_1 != null)
                                {
                                    this.Rows.Add();
                                }
                                else
                                {
                                    this.CurrentCell.Selected = false;
                                    return true;
                                }

                            }
                            try
                            {
                                if (this.CurrentRow.Cells[1].Value != null)
                                {
                                    //判断该染助剂是否存在
                                    string s_sql = "SELECT *  FROM assistant_details WHERE" +
                                                       " AssistantCode = '" + this.CurrentRow.Cells[1].Value.ToString() + "' ; ";

                                    DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                    if (dt_assistant.Rows.Count <= 0)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        {
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                            "染助剂代码不存在,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                            {
                                                for (int i = 1; i < this.Columns.Count; i++)
                                                {
                                                    this.CurrentRow.Cells[i].Value = null;
                                                }
                                                return true;

                                            }
                                        }
                                        else
                                        {
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                            " Dyeing agent code does not exist, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                            {
                                                for (int i = 1; i < this.Columns.Count; i++)
                                                {
                                                    this.CurrentRow.Cells[i].Value = null;
                                                }
                                                return true;

                                            }
                                        }

                                    }

                                    //判断是否已经输过相同的染助剂代码
                                    foreach (DataGridViewRow dgr in this.Rows)
                                    {
                                        if (this.CurrentRow.Cells[1].Value != null && dgr.Cells[1].Value != null)
                                        {
                                            if ((dgr.Cells[1].Value.ToString().ToLower() == this.CurrentRow.Cells[1].Value.ToString().ToLower()) &&
                                                dgr.Index != this.CurrentRow.Index)
                                            {
                                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                                    "染助剂代码重复,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                                    {
                                                        for (int i = 1; i < this.Columns.Count; i++)
                                                        {
                                                            this.CurrentRow.Cells[i].Value = null;
                                                        }
                                                        return true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                                    " Dyeing agent code is duplicate, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                                    {
                                                        for (int i = 1; i < this.Columns.Count; i++)
                                                        {
                                                            this.CurrentRow.Cells[i].Value = null;
                                                        }
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (dt_assistant.Rows.Count > 0)
                                    {
                                        this.CurrentRow.Cells[2].Value = dt_assistant.Rows[0]["AssistantName"].ToString();
                                        this.CurrentRow.Cells[3].Value = dt_assistant.Rows[0]["UnitOfAccount"].ToString();
                                    }
                                }
                            }
                            catch
                            {

                            }

                            this.CurrentCell = this[i_col, i_row + 1];

                        }
                    }
                    else if (this.Name.Contains("dgv_dyconfiglisg")) //工艺步骤这里跳表格要判断下 不同工艺
                    {  //处理工艺步骤填写内容
                        //Console.WriteLine(1);
                        int i_col = this.CurrentCell.ColumnIndex;  //列
                        int i_row = this.CurrentCell.RowIndex;  //行
                        ;
                        int R = i_row;
                        int C = i_col;
                        for (int i = 0; i < this.Rows.Count; i++)
                        {
                            if (R > this.Rows.Count - 1)
                            {
                                //最后一行最后一个格子结束
                                break;
                            }

                            string cellV = this[1, R].Value.ToString();//当前行的步骤名称
                            if ("放布".Equals(cellV))
                            {
                                R = R + 1;
                                C = 1;
                                continue;
                            }
                            else if ("冷行".Equals(cellV))
                            {

                                if (C == 4)
                                {
                                    C = 5;
                                    break;
                                }
                                else if (C == 5)
                                {
                                    R = R + 1;
                                    C = 1;
                                    continue;
                                }
                                else
                                { //不是4或者5的列，就跳到4上
                                    C = 4;
                                    break;
                                }
                            }
                            else if ("温控".Equals(cellV))
                            {
                                if (C == 2)
                                {
                                    C = 3;
                                    break;
                                }
                                else if (C == 3)
                                {
                                    C = 4;
                                    break;
                                }
                                else if (C == 4)
                                {
                                    C = 5;
                                    break;
                                }
                                else if (C == 5)
                                {
                                    R = R + 1;
                                    C = 1;
                                    continue;
                                }
                                else
                                {
                                    C = 2;
                                    break;
                                }
                            }
                            else if (cellV.Trim().Equals("加A") || cellV.Trim().Equals("加B") || cellV.Trim().Equals("加C") || cellV.Trim().Equals("加D") || cellV.Trim().Equals("加E") || cellV.Trim().Equals("加F") || cellV.Trim().Equals("加G") || cellV.Trim().Equals("加H") || cellV.Trim().Equals("加I") || cellV.Trim().Equals("加J") || cellV.Trim().Equals("加K") || cellV.Trim().Equals("加L") || cellV.Trim().Equals("加M") || cellV.Trim().Equals("加N"))
                            {
                                /* if (C == 4)
                                 {
                                     C = 5;
                                     break;
                                 }*/
                                if (C == 5)
                                {
                                    R = R + 1;
                                    C = 1;
                                    continue;
                                }
                                else
                                { //不是4或者5的列，就跳到4上
                                    C = 5;
                                    break;
                                }
                            }
                            else if ("加水".Equals(cellV))
                            {
                                if (C == 4)
                                {
                                    C = 5;
                                    break;
                                }
                                else if (C == 5)
                                {
                                    R = R + 1;
                                    C = 1;
                                    continue;
                                }
                                else
                                { //不是4或者5的列，就跳到4上
                                    C = 4;
                                    break;
                                }
                            }
                            else if ("搅拌".Equals(cellV))
                            {
                                if (C == 4)
                                {
                                    C = 5;
                                    break;
                                }
                                else if (C == 5)
                                {
                                    R = R + 1;
                                    C = 1;
                                    continue;
                                }
                                else
                                { //不是4或者5的列，就跳到4上
                                    C = 4;
                                    break;
                                }
                            }
                            else if ("排液".Equals(cellV))
                            {
                                if (C == 5)
                                {
                                    R = R + 1;
                                    C = 1;
                                    continue;
                                }
                                else
                                { //不是4或者5的列，就跳到4上
                                    C = 5;
                                    break;
                                }
                            }
                            else if ("出布".Equals(cellV))
                            {
                                R = R + 1;
                                C = 1;
                                continue;
                            }
                            else if ("洗杯".Equals(cellV))
                            {
                                if (C == 4)
                                {
                                    C = 5;
                                    break;
                                }
                                else if (C == 5)
                                {
                                    R = R + 1;
                                    C = 1;
                                    continue;
                                }
                                else
                                { //不是4或者5的列，就跳到4上
                                    C = 4;
                                    break;
                                }
                            }
                        }

                        if (R > this.Rows.Count - 1)
                        {
                            //最后一行最后一个格子结束
                            //this.Enabled = false;
                            if (false)
                            {

                            }
                            else {
                                string s_temp = this.Name.Split('_')[2];
                                FADM_Control.myDyeingConfiguration s = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp) - 1).ToString()];
                                if (s.dgv_Dye.Rows.Count != 0)
                                {
                                    s.dgv_Dye.Enabled = true;
                                    s.dgv_Dye.CurrentCell = s.dgv_Dye[1, 0];
                                    s.dgv_Dye.Focus();
                                }
                                else
                                {
                                    //没有需要填写加A加B 就跳到下一个工艺选择 com上
                                    //==0个
                                    myDyeSelect d = FADM_Control.Formula.myDyeSelectList[Convert.ToInt32(s_temp)];
                                    d.dy_type_comboBox1.Focus();
                                }

                            }

                           

                        }
                        else
                        {
                            this.CurrentCell = this[C, R];
                        }


                    } ///------------------------------------
                    else
                    {
                        int i_col = this.CurrentCell.ColumnIndex;

                        int i_row = this.CurrentCell.RowIndex;

                        if (i_col >= 3)
                        {
                            if (i_col == 3)
                            {
                                if (this.CurrentCell.Value != null && this[4, this.CurrentRow.Index].Value != null && this[3, this.CurrentRow.Index].Value != null)
                                {
                                    if (this[4, this.CurrentRow.Index].Value.ToString() == "%")
                                    {
                                        if (Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_DyeAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_AdditivesAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }

                                    //把手动选瓶去掉
                                    DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)this[10, this.CurrentRow.Index];
                                    dc.Value = 0;

                                }
                            }

                            i_col = 0;

                            //if (_i_row == this.NewRowIndex)
                            //{
                            //    //判断上一行是否为空行
                            //    string s_1 = null;
                            //    string s_2 = null;
                            //    try
                            //    {
                            //        if (this.CurrentCell.Value != null)
                            //            s_1 = this.CurrentCell.Value.ToString();
                            //        if (this[1, this.CurrentRow.Index].Value != null)
                            //            s_2 = this[1, this.CurrentRow.Index].Value.ToString();
                            //    }
                            //    catch
                            //    {

                            //    }
                            //    if (s_1 != null || s_2 != null)
                            //    {
                            //        this.Rows.Add();
                            //    }
                            //    else
                            //    {
                            //        this.CurrentCell.Selected = false;
                            //        return true;
                            //    }

                            //}

                            if (this.Parent.Text.Contains("后处理工艺"))
                            {
                                if (this[1, i_row].Value == null)
                                {
                                    if (i_row != this.Rows.Count - 1)
                                    {
                                        i_row++;
                                        this.CurrentCell = this[i_col + 1, i_row];
                                    }
                                    else
                                    {
                                        //是不是直接跳到保存上
                                        if (false)
                                        {
                                            
                                        }
                                        else {
                                            string s_temp = this.Name;
                                            myDyeSelect d = FADM_Control.Formula.myDyeSelectList[Convert.ToInt32(s_temp)];
                                            d.dy_type_comboBox1.Focus();

                                            if (FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Visible)
                                            { //隐藏

                                                Label la = (Label)FADM_Control.Formula.isHiSo[Convert.ToInt32(s_temp) - 1];
                                                string s_temp2 = la.Name;
                                                if (FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Visible)
                                                { //隐藏
                                                    /*FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Hide();
                                                    la.Text = "▲                                                                                  ";*/

                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Hide();
                                                    Point xy = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Location;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_Dye.Location = xy;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].grp_Dye.Height = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].grp_Dye.Height - FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Height = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Height - FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height;
                                                    la.Text = "▲                                                                                  ";

                                                }
                                                else
                                                {
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Height = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Height + FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].grp_Dye.Height = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].grp_Dye.Height + FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Show();
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_Dye.Location = new Point(FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_Dye.Location.X, FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Location.Y + FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height);

                                                    //FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Show();
                                                    la.Text = "▼                                                                                  ";
                                                }
                                                // FADM_Control.Formula.DyeingConHS(FADM_Control.Formula.isHiSo[Convert.ToInt32(s_temp)-1], null);

                                            }

                                        }
                                        

                                    }
                                }
                                else
                                {
                                    i_row++;
                                    //回车不跳到下一行 新增一行 如果这一行都没有内容就跳到加B上
                                    this.Rows.Insert(i_row);
                                    this.CurrentCell = this[1, i_row];
                                    this.Height = this.Height + 30;
                                    this.Parent.Height = this.Parent.Height + 30;
                                    this.Parent.Parent.Height = this.Parent.Parent.Height + 30;
                                }

                            }
                            else
                            {
                                if (i_row != this.Rows.Count - 1)
                                {
                                    i_row++;
                                    this.CurrentCell = this[i_col + 1, i_row];
                                }
                                else
                                {
                                    this.CurrentCell.Selected = false;

                                    if (this.Parent.Text.Contains("染色工艺"))
                                    {
                                        if (false)
                                        {

                                            

                                        }
                                        else {

                                            string s_temp = this.Name;
                                            myDyeSelect d = FADM_Control.Formula.myDyeSelectList[Convert.ToInt32(s_temp)];
                                            d.dy_type_comboBox1.Focus();
                                            if (FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Visible)
                                            { //隐藏

                                                Label la = (Label)FADM_Control.Formula.isHiSo[Convert.ToInt32(s_temp) - 1];
                                                string s_temp2 = la.Name;
                                                if (FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Visible)
                                                { //隐藏
                                                  //FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Hide();
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Hide();
                                                    Point xy = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Location;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_Dye.Location = xy;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].grp_Dye.Height = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].grp_Dye.Height - FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Height = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Height - FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height;

                                                    la.Text = "▲                                                                                  ";
                                                }
                                                else
                                                {

                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Height = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Height + FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].grp_Dye.Height = FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].grp_Dye.Height + FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height;
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Show();
                                                    FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_Dye.Location = new Point(FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_Dye.Location.X, FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Location.Y + FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].dgv_dyconfiglisg.Height);
                                                    //FADM_Control.Formula.mymap[(Convert.ToInt32(s_temp2) - 1).ToString()].Show();
                                                    la.Text = "▼                                                                                  ";
                                                }
                                            }
                                        }


                                        

                                    }


                                }
                            }




                        }
                        else if (i_col == 1)
                        {
                            //输入助剂代码后自己选瓶
                            if (this.CurrentRow.Cells[1].Value != null)
                            {
                                string s_sql = "SELECT *  FROM assistant_details WHERE" +
                                              " AssistantCode = '" + this.CurrentRow.Cells[1].Value.ToString() + "' ; ";

                                DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                if (dt_assistant.Rows.Count <= 0)
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    {
                                        if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() + "染助剂代码不存在！", "输入异常", MessageBoxButtons.OK, false))
                                        {
                                            this.CurrentRow.Cells[1].Value = null;
                                            return true;
                                        }
                                    }
                                    else
                                    {
                                        if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() + " Dyeing agent code does not exist！", "Input exception", MessageBoxButtons.OK, false))
                                        {
                                            this.CurrentRow.Cells[1].Value = null;
                                            return true;
                                        }
                                    }
                                }

                                //s_sql = "SELECT top 1 *   FROM bottle_details WHERE" +
                                //              " AssistantCode = '" + this.CurrentRow.Cells[1].Value.ToString() + "' ; ";
                                //dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                //if (dt_assistant.Rows.Count <= 0)
                                //{
                                //    if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() + "不存在对应母液瓶！", "输入异常", MessageBoxButtons.OK,false))
                                //    {
                                //        this.CurrentRow.Cells[1].Value = null;
                                //        return true;
                                //    }
                                //}
                                //else
                                //{
                                //    List<string> _lis_bottleNum = new List<string>();
                                //    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)this[i_col + 3, _i_row];
                                //    _lis_bottleNum.Add(dt_assistant.Rows[0]["BottleNum"].ToString());
                                //    dd.Value = null;
                                //    dd.DataSource = _lis_bottleNum;
                                //    dd.Value = dt_assistant.Rows[0]["BottleNum"].ToString();
                                //}


                                if (dt_assistant.Rows.Count > 0)
                                {
                                    List<string> lis_bottleNum = new List<string>();
                                    this.CurrentRow.Cells[2].Value = dt_assistant.Rows[0]["AssistantName"].ToString();
                                    string UnitOfAccount = dt_assistant.Rows[0]["UnitOfAccount"].ToString();

                                    bool b = this[4, i_row] is DataGridViewComboBoxCell;
                                    if (b)
                                    {
                                        DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)this[4, i_row];
                                        if (FADM_Object.Communal._b_isUnitChange)
                                        {
                                            if (UnitOfAccount.Equals("%"))
                                            {
                                                lis_bottleNum.Add("%");
                                                dd.Value = null;
                                                dd.DataSource = lis_bottleNum;
                                                dd.Value = lis_bottleNum[0].ToString();
                                            }
                                            else if (UnitOfAccount.Equals("g/l"))
                                            {
                                                if (dd.Value == null)
                                                {
                                                    lis_bottleNum.Add("g/l");
                                                    lis_bottleNum.Add("%");
                                                    dd.DataSource = lis_bottleNum;
                                                    dd.Value = lis_bottleNum[0].ToString();
                                                }
                                                else
                                                {
                                                    if (dd.Value.Equals("%"))
                                                    {
                                                        lis_bottleNum.Add("%");
                                                        lis_bottleNum.Add("g/l");
                                                        dd.DataSource = lis_bottleNum;
                                                        dd.Value = lis_bottleNum[0].ToString();
                                                    }


                                                }

                                            }
                                            else
                                            {
                                                lis_bottleNum.Add(UnitOfAccount);
                                                dd.DataSource = lis_bottleNum;
                                                dd.Value = lis_bottleNum[0].ToString();
                                            }
                                        }
                                        else
                                        {
                                            lis_bottleNum.Add(UnitOfAccount);
                                            dd.Value = null;
                                            dd.DataSource = lis_bottleNum;
                                            dd.Value = lis_bottleNum[0].ToString();
                                        }

                                    }
                                    else {
                                        this.CurrentRow.Cells[4].Value = UnitOfAccount;
                                    }


                                    
                                }

                                this.CurrentCell = this[i_col + 2, i_row];
                            }
                            else if (this.Parent.Text.Contains("后处理工艺"))
                            {
                                this.CurrentCell = this[i_col + 2, i_row];
                                if (i_row != this.Rows.Count - 1)
                                {
                                    // i_row++;

                                }
                                else
                                {
                                    if (this.CurrentRow.Cells[0].Value != null && this.CurrentRow.Cells[0].Value.ToString() != "")
                                    {

                                    }
                                    else
                                    {
                                        this.CurrentCell.Selected = false;
                                        //  this.Enabled = false;//这里直接退出？
                                    }
                                    // this.CurrentCell.Selected = false;
                                }
                            }

                        }
                        else
                        {
                            if (i_row != this.Rows.Count - 1)
                            {
                                i_row++;
                                this.CurrentCell = this[i_col + 1, i_row];
                            }
                            else
                            {
                                this.CurrentCell.Selected = false;
                            }
                        }
                    }
                    return true;

                case Keys.Delete:
                    try
                    {
                        if (this.Name == "dgv_FormulaData" || this.Name == "dgv_FormulaGroup")
                        {
                            this.Rows.Remove(this.CurrentRow);
                        }
                        else if (this.AccessibleName == "dye" && this.Parent.Text.Contains("后处理"))
                        { //第一个单元格没有加A 就证明可以删除
                            int i_col = this.CurrentCell.ColumnIndex;
                            int i_row = this.CurrentCell.RowIndex;
                            if (this.CurrentRow.Cells[0].Value == null || this.CurrentRow.Cells[0].Value.ToString().Length == 0)
                            {
                                this.Rows.Remove(this.CurrentRow);
                            }
                            else
                            {
                                //遍历看下 是否有重复的
                                string cellV = this.CurrentRow.Cells[0].Value.ToString();
                                int count = 0;
                                foreach (DataGridViewRow dgvr in this.Rows)
                                {
                                    if (dgvr.Cells[0].Value != null && dgvr.Cells[0].Value.ToString().Length > 0 && dgvr.Cells[0].Value.ToString().Equals(cellV))
                                    {
                                        count++;
                                    }
                                }
                                if (count >= 2)
                                {
                                    this.Rows.Remove(this.CurrentRow);
                                }
                            }
                        }



                    }
                    catch
                    {

                    }
                    return true;

                case Keys.Insert:
                    try
                    {
                        if (this.Name == "dgv_FormulaData")
                            this.Rows.Insert(this.CurrentRow.Index);
                    }
                    catch
                    {

                    }
                    return true;


                default:

                    return base.ProcessDialogKey(keyData);

            }


        }



        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:


                    this.EndEdit();
                    if (this.Name == "dgv_FormulaData")
                    {
                        int i_col = this.CurrentCell.ColumnIndex;

                        int i_row = this.CurrentCell.RowIndex;

                        if (i_col >= 3)
                        {
                            if (i_col == 3)
                            {
                                if (this.CurrentCell.Value != null && this[4, this.CurrentRow.Index].Value != null && this[3, this.CurrentRow.Index].Value != null)
                                {
                                    if (this[4, this.CurrentRow.Index].Value.ToString() == "%")
                                    {
                                        if (Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_DyeAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_AdditivesAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }
                                }
                            }
                            i_col = 0;

                            if (i_row == this.NewRowIndex)
                            {
                                //判断上一行是否为空行
                                string s_1 = null;
                                string s_2 = null;
                                try
                                {
                                    if (this.CurrentCell.Value != null)
                                        s_1 = this.CurrentCell.Value.ToString();
                                    if (this[1, this.CurrentRow.Index].Value != null)
                                        s_2 = this[1, this.CurrentRow.Index].Value.ToString();
                                }
                                catch
                                {

                                }
                                if (s_1 != null || s_2 != null)
                                {
                                    this.Rows.Add();
                                }
                                else
                                {
                                    this.CurrentCell.Selected = false;
                                    return;
                                }
                            }
                            i_row++;
                            this[i_col, i_row].Value = i_row + 1;
                            this.CurrentCell = this[i_col + 1, i_row];
                        }
                        else if (i_col == 1)
                        {

                            try
                            {
                                if (this.CurrentRow.Cells[1].Value != null)
                                {
                                    //判断该染助剂是否存在
                                    string s_sql = "SELECT *  FROM assistant_details WHERE" +
                                                  " AssistantCode = '" + this.CurrentRow.Cells[1].Value.ToString() + "' ; ";

                                    DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                    if (dt_assistant.Rows.Count <= 0)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        {
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                            "染助剂代码不存在,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                            {
                                                for (int i = 1; i < this.Columns.Count; i++)
                                                {
                                                    this.CurrentRow.Cells[i].Value = null;
                                                }
                                                return;

                                            }
                                        }
                                        else
                                        {
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                            " Dyeing agent code does not exist, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                            {
                                                for (int i = 1; i < this.Columns.Count; i++)
                                                {
                                                    this.CurrentRow.Cells[i].Value = null;
                                                }
                                                return;

                                            }
                                        }

                                    }

                                    //判断是否已经输过相同的染助剂代码
                                    foreach (DataGridViewRow dgr in this.Rows)
                                    {
                                        if (dgr.Cells[1].Value != null && this.CurrentRow.Cells[1].Value != null)
                                        {
                                            if ((dgr.Cells[1].Value.ToString().ToLower() == this.CurrentRow.Cells[1].Value.ToString().ToLower()) && dgr.Index != this.CurrentRow.Index)
                                            {
                                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                                    "染助剂代码重复,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                                    {
                                                        for (int i = 1; i < this.Columns.Count; i++)
                                                        {
                                                            this.CurrentRow.Cells[i].Value = null;
                                                        }
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                                    " Dyeing agent code is duplicate, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                                    {
                                                        for (int i = 1; i < this.Columns.Count; i++)
                                                        {
                                                            this.CurrentRow.Cells[i].Value = null;
                                                        }
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (dt_assistant.Rows.Count > 0)
                                    {
                                        this.CurrentRow.Cells[2].Value = dt_assistant.Rows[0]["AssistantName"].ToString();
                                        this.CurrentRow.Cells[4].Value = dt_assistant.Rows[0]["UnitOfAccount"].ToString();
                                    }
                                }
                            }
                            catch
                            {

                            }
                            this.CurrentCell = this[i_col + 2, i_row];
                        }
                    }
                    else if (this.Name == "dgv_FormulaGroup")
                    {
                        int i_col = this.CurrentCell.ColumnIndex;

                        int i_row = this.CurrentCell.RowIndex;

                        if (i_col == 1)
                        {
                            if (i_row == this.NewRowIndex)
                            {
                                //判断上一行是否为空行
                                string s1 = null;
                                try
                                {
                                    if (this.CurrentCell.Value != null)
                                        s1 = this.CurrentCell.Value.ToString();
                                }
                                catch
                                {

                                }
                                if (s1 != null)
                                {
                                    this.Rows.Add();
                                }
                                else
                                {
                                    this.CurrentCell.Selected = false;
                                    return;
                                }

                            }

                            try
                            {
                                if (this.CurrentRow.Cells[1].Value != null)
                                {
                                    //判断该染助剂是否存在
                                    string s_sql = "SELECT *  FROM assistant_details WHERE" +
                                                  " AssistantCode = '" + this.CurrentRow.Cells[1].Value.ToString() + "' ; ";

                                    DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                    if (dt_assistant.Rows.Count <= 0)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        {
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                            "染助剂代码不存在,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                            {
                                                for (int i = 1; i < this.Columns.Count; i++)
                                                {
                                                    this.CurrentRow.Cells[i].Value = null;
                                                }
                                                return;

                                            }
                                        }
                                        else
                                        {
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                            " Dyeing agent code does not exist, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                            {
                                                for (int i = 1; i < this.Columns.Count; i++)
                                                {
                                                    this.CurrentRow.Cells[i].Value = null;
                                                }
                                                return;

                                            }
                                        }

                                    }

                                    //判断是否已经输过相同的染助剂代码
                                    foreach (DataGridViewRow dgr in this.Rows)
                                    {
                                        if (dgr.Cells[1].Value != null && this.CurrentRow.Cells[1].Value != null)
                                        {
                                            if ((dgr.Cells[1].Value.ToString().ToLower() == this.CurrentRow.Cells[1].Value.ToString().ToLower()) && dgr.Index != this.CurrentRow.Index)
                                            {
                                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                                    "染助剂代码重复,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                                    {
                                                        for (int i = 1; i < this.Columns.Count; i++)
                                                        {
                                                            this.CurrentRow.Cells[i].Value = null;
                                                        }
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() +
                                                    " Dyeing agent code is duplicate, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                                    {
                                                        for (int i = 1; i < this.Columns.Count; i++)
                                                        {
                                                            this.CurrentRow.Cells[i].Value = null;
                                                        }
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (dt_assistant.Rows.Count > 0)
                                    {
                                        this.CurrentRow.Cells[2].Value = dt_assistant.Rows[0]["AssistantName"].ToString();
                                        this.CurrentRow.Cells[3].Value = dt_assistant.Rows[0]["UnitOfAccount"].ToString();
                                    }
                                }
                            }
                            catch
                            {

                            }
                            this.CurrentCell = this[i_col, i_row + 1];
                        }
                    }
                    else
                    {
                        int i_col = this.CurrentCell.ColumnIndex;

                        int i_row = this.CurrentCell.RowIndex;

                        if (i_col >= 3)
                        {
                            if (i_col == 3)
                            {
                                if (this.CurrentCell.Value != null && this[4, this.CurrentRow.Index].Value != null && this[3, this.CurrentRow.Index].Value != null)
                                {
                                    if (this[4, this.CurrentRow.Index].Value.ToString() == "%")
                                    {
                                        if (Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_DyeAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_AdditivesAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }
                                }
                            }


                            i_col = 0;

                            //if (_i_row == this.NewRowIndex)
                            //{
                            //    //判断上一行是否为空行
                            //    string s_1 = null;
                            //    string s_2 = null;
                            //    try
                            //    {
                            //        if (this.CurrentCell.Value != null)
                            //            s_1 = this.CurrentCell.Value.ToString();
                            //        if (this[1, this.CurrentRow.Index].Value != null)
                            //            s_2 = this[1, this.CurrentRow.Index].Value.ToString();
                            //    }
                            //    catch
                            //    {

                            //    }
                            //    if (s_1 != null || s_2 != null)
                            //    {
                            //        this.Rows.Add();
                            //    }
                            //    else
                            //    {
                            //        this.CurrentCell.Selected = false;
                            //        return ;
                            //    }

                            //}
                            if (i_row != this.Rows.Count - 1)
                            {
                                i_row++;
                                this.CurrentCell = this[i_col + 1, i_row];
                            }
                            else
                            {
                                this.CurrentCell.Selected = false;
                            }
                        }
                        else if (i_col == 1)
                        {
                            //输入助剂代码后自己选瓶
                            if (this.CurrentRow.Cells[1].Value != null)
                            {
                                string s_sql = "SELECT *  FROM assistant_details WHERE" +
                                              " AssistantCode = '" + this.CurrentRow.Cells[1].Value.ToString() + "' ; ";

                                DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                if (dt_assistant.Rows.Count <= 0)
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    {
                                        if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() + "染助剂代码不存在！", "输入异常", MessageBoxButtons.OK, false))
                                        {
                                            this.CurrentRow.Cells[1].Value = null;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() + " Dyeing agent code does not exist！", "Input exception", MessageBoxButtons.OK, false))
                                        {
                                            this.CurrentRow.Cells[1].Value = null;
                                            return;
                                        }
                                    }
                                }
                                if (dt_assistant.Rows.Count > 0)
                                {
                                    this.CurrentRow.Cells[2].Value = dt_assistant.Rows[0]["AssistantName"].ToString();
                                    this.CurrentRow.Cells[4].Value = dt_assistant.Rows[0]["UnitOfAccount"].ToString();
                                }

                                //s_sql = "SELECT top 1 *   FROM bottle_details WHERE" +
                                //              " AssistantCode = '" + this.CurrentRow.Cells[1].Value.ToString() + "' ; ";
                                //dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                //if (dt_assistant.Rows.Count <= 0)
                                //{
                                //    if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show(this.CurrentRow.Cells[1].Value.ToString() + "不存在对应母液瓶！", "输入异常", MessageBoxButtons.OK,false))
                                //    {
                                //        this.CurrentRow.Cells[1].Value = null;
                                //        return;
                                //    }
                                //}
                                //else
                                //{
                                //    List<string> _lis_bottleNum = new List<string>();
                                //    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)this[i_col + 3, _i_row];
                                //    _lis_bottleNum.Add(dt_assistant.Rows[0]["BottleNum"].ToString());
                                //    dd.Value = null;
                                //    dd.DataSource = _lis_bottleNum;
                                //    dd.Value = dt_assistant.Rows[0]["BottleNum"].ToString();
                                //}

                                this.CurrentCell = this[i_col + 2, i_row];
                            }

                        }
                        else
                        {
                            if (i_row != this.Rows.Count - 1)
                            {
                                i_row++;
                                this.CurrentCell = this[i_col + 1, i_row];
                            }
                            else
                            {
                                this.CurrentCell.Selected = false;
                            }
                        }
                    }
                    return;

                case Keys.Delete:

                    try
                    {
                        if (this.Name == "dgv_FormulaData" || this.Name == "dgv_FormulaGroup")
                            this.Rows.Remove(this.CurrentRow);
                    }
                    catch
                    {

                    }
                    break;
                default:

                    base.OnKeyDown(e);
                    break;
            }

        }

    }
}
