using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

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
                                        if (this[3, this.CurrentRow.Index].Value!=null && this[3, this.CurrentRow.Index].Value.ToString() != "" && Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_DyeAlarmWeight)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("输入配方用量过大", "MyDataGridView", MessageBoxButtons.OK, true);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Input formula dosage is too large", "MyDataGridView", MessageBoxButtons.OK, true);

                                        }
                                    }
                                    else
                                    {
                                         
                                        if (this[3, this.CurrentRow.Index].Value!=null && this[3, this.CurrentRow.Index].Value.ToString()!="" && Convert.ToDouble(this[3, this.CurrentRow.Index].Value.ToString()) > Lib_Card.Configure.Parameter.Other_AdditivesAlarmWeight)
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

                            this.CurrentCell = this[i_col, i_row+1];

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
                            //        return true;
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
                                    this.CurrentRow.Cells[2].Value = dt_assistant.Rows[0]["AssistantName"].ToString();
                                    this.CurrentRow.Cells[4].Value = dt_assistant.Rows[0]["UnitOfAccount"].ToString();
                                }

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
                    return true;

                case Keys.Delete:
                    try
                    {
                        if (this.Name == "dgv_FormulaData" || this.Name == "dgv_FormulaGroup")
                            this.Rows.Remove(this.CurrentRow);
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
                                                return ;

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
                                                return ;

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
                                                        return ;
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
                            this.CurrentCell = this[i_col, i_row+1];
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
