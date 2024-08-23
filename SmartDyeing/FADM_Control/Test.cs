using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class Test : UserControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)] 
        internal static extern IntPtr GetFocus();

        List<Control> l =new List<Control>();

        public double dClothWeight;
        public double dTotalWeight;
        public Test()
        {
            InitializeComponent();
            dClothWeight = 5;
            dTotalWeight = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FADM_Control.DyeAndHandleFormulas s = new DyeAndHandleFormulas();
            s.Location = new Point(10, (Convert.ToInt32(textBox1.Text) - 1) * 150);
            this.panel1.Controls.Add(s);
            s.dgv_Dye.Name = textBox1.Text;
            l.Add(s.dgv_Dye);
            s.dgv_Dye.SelectionChanged += dgv_Dye_SelectionChanged;
            s.dgv_Dye.EditingControlShowing += dgv_Dye_EditingControlShowing;
            s.dgv_Dye.RowLeave += dgv_Dye_RowLeave;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
        }

        //声明瓶号列
        List<string> bottleNum = new List<string>();

        private void dgv_Dye_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)sender;
                if (((FADM_Object.MyDataGridView)sender).CurrentCell.ColumnIndex == 5)
                {
                    ((DataGridViewComboBoxEditingControl)e.Control).SelectedValueChanged -= dgv_Dye_SelectedValueChanged;
                    ((DataGridViewComboBoxEditingControl)e.Control).SelectedValueChanged += dgv_Dye_SelectedValueChanged;
                    //((DataGridViewComboBoxEditingControl)e.Control).Enter += new EventHandler(Page_Formula_Enter);
                    ((DataGridViewComboBoxEditingControl)e.Control).DropDown -= dgv_Dye_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).DropDown += dgv_Dye_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).GotFocus -= dgv_Dye_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).GotFocus += dgv_Dye_DropDown;
                }
                if (dgv_Dye.CurrentCell.ColumnIndex == 3)
                {
                    ((DataGridViewTextBoxEditingControl)e.Control).KeyPress -= dgv_Dye_KeyPress;
                    ((DataGridViewTextBoxEditingControl)e.Control).KeyPress += dgv_Dye_KeyPress;
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_Dye_EditingControlShowing", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_Dye_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)sender;
            dgv_Dye.EndEdit();
            if (dgv_Dye[1, dgv_Dye.CurrentRow.Index].Value == null ||
                dgv_Dye[3, dgv_Dye.CurrentRow.Index].Value == null)
            {
                return;
            }
            UpdataDyeAndHandle(dgv_Dye, dgv_Dye.CurrentRow.Index);
        }

        //Combobox下拉时事件
        void dgv_Dye_DropDown(object sender, EventArgs e)
        {

        }

        //配方用量输入检查
        void dgv_Dye_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)(((DataGridViewTextBoxEditingControl)sender).Parent.Parent);
                if (dgv_Dye.CurrentCell.ColumnIndex == 3)
                {
                    e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
                }
            }
            catch { }
        }

        //瓶号选择修改事件
        void dgv_Dye_SelectedValueChanged(object sender, EventArgs e)
        {

            try
            {
                FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)(((DataGridViewComboBoxEditingControl)sender).Parent.Parent);
                if (dgv_Dye.CurrentCell.ColumnIndex == 5)
                {
                    DataGridViewComboBoxEditingControl dd = (DataGridViewComboBoxEditingControl)sender;

                    bool b = false;

                    //获取当前染助剂所有母液瓶资料
                    string P_str_sql = "SELECT BottleNum, SettingConcentration ,RealConcentration, DropMinWeight" +
                                       " FROM bottle_details WHERE" +
                                       " AssistantCode = '" + dgv_Dye.CurrentRow.Cells[1].Value.ToString() + "'" +
                                       " AND RealConcentration != 0 ORDER BY BottleNum ;";
                    DataTable P_dt_currentassistantcodeallbottlenum = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                    if (P_dt_currentassistantcodeallbottlenum.Rows.Count > 0)
                    {
                        foreach (DataRow mdr in P_dt_currentassistantcodeallbottlenum.Rows)
                        {
                            if (bottleNum[1] == mdr[0].ToString())
                            {
                                b = true;
                                break;
                            }
                        }

                        foreach (DataRow mdr in P_dt_currentassistantcodeallbottlenum.Rows)
                        {
                            if (dd.Text.ToString() == mdr[0].ToString())
                            {
                                dgv_Dye.CurrentRow.Cells[5].Value = mdr[0].ToString();
                                dgv_Dye.CurrentRow.Cells[6].Value = mdr[1].ToString();
                                dgv_Dye.CurrentRow.Cells[7].Value = mdr[2].ToString();
                                break;
                            }
                        }



                        if (bottleNum[0] == dgv_Dye.CurrentRow.Index.ToString() && bottleNum[1] != dgv_Dye.CurrentRow.Cells[5].Value.ToString() && b)
                        {
                            //设置手动选瓶标志位
                            dgv_Dye.CurrentRow.Cells[10].Value = 1;
                        }



                        //计算目标滴液量
                        double objectDropWeight = 0;
                        if (dgv_Dye.CurrentRow.Cells[4].Value.ToString() == "%")
                        {
                            //染料
                            objectDropWeight = (dClothWeight * Convert.ToDouble(dgv_Dye.CurrentRow.Cells[3].Value.ToString()) / Convert.ToDouble(dgv_Dye.CurrentRow.Cells[7].Value.ToString()));
                        }
                        else
                        {
                            //助剂
                            objectDropWeight = (dTotalWeight * Convert.ToDouble(dgv_Dye.CurrentRow.Cells[3].Value.ToString()) / Convert.ToDouble(dgv_Dye.CurrentRow.Cells[7].Value.ToString()));

                        }

                        dgv_Dye.CurrentRow.Cells[8].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", objectDropWeight) : String.Format("{0:F3}", objectDropWeight);
                    }

                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_Dye_SelectedValueChanged", MessageBoxButtons.OK, true);
            }

        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    for (int i = 0; i < l.Count; i++)
                    {
                        try
                        {
                            TextBox txt = (TextBox)sender;
                        }
                        catch
                        {
                            try
                            {
                                CheckBox chk = (CheckBox)sender;
                            }
                            catch
                            {
                                try
                                {
                                    ComboBox cbo = (ComboBox)sender;
                                }
                                catch
                                {
                                    //btn_Save.Focus();
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void UpdataDyeAndHandle(FADM_Object.MyDataGridView dgv, int _CurrentRowIndex)
        {
            try
            {
                if (/*txt_ClothWeight.Text == "" || txt_TotalWeight.Text == "" ||*/ dgv[3, _CurrentRowIndex].Value == null || dgv[3, _CurrentRowIndex].Value.ToString() == "")
                {
                    return;
                }

                DataTable P_dt_currentassistantcodeallbottlenum = new DataTable();

                if (_CurrentRowIndex >= dgv.Rows.Count)
                {
                    return;
                }
                string P_str_sql = null;



                //获取染助剂资料
                P_str_sql = "SELECT *  FROM assistant_details WHERE" +
                            " AssistantCode = '" + dgv[1, _CurrentRowIndex].Value.ToString() + "' ; ";

                DataTable P_dt_assistantdetails = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                if (P_dt_assistantdetails.Rows.Count > 0)
                {
                    dgv[4, _CurrentRowIndex].Value = (P_dt_assistantdetails.Rows[0][5].ToString());
                    dgv[2, _CurrentRowIndex].Value = P_dt_assistantdetails.Rows[0][3].ToString();
                    dgv[9, _CurrentRowIndex].Value = "0.00";
                    //获取当前染助剂所有母液瓶资料
                    P_str_sql = "SELECT  BottleNum, SettingConcentration ,RealConcentration, DropMinWeight" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv[1, _CurrentRowIndex].Value.ToString() + "'" +
                                "  AND RealConcentration != 0  ORDER BY BottleNum ;";
                    P_dt_currentassistantcodeallbottlenum = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                    //未找到一个合适的瓶
                    if (P_dt_currentassistantcodeallbottlenum.Rows.Count == 0)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("当前染助剂代码未发现母液瓶！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("No mother liquor bottle found for the current dyeing agent code！", "Tips", MessageBoxButtons.OK, false);

                        for (int i = 1; i < dgv.Columns.Count - 1; i++)
                        {
                            dgv.CurrentRow.Cells[i].Value = null;
                        }

                        return;
                    }
                    List<string> bottleNum = new List<string>();
                    foreach (DataRow mdr in P_dt_currentassistantcodeallbottlenum.Rows)
                    {
                        bottleNum.Add(mdr[0].ToString());
                    }
                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv[5, _CurrentRowIndex];
                    for (int j = 0; j < bottleNum.Count; j++)
                    {
                        for (int i = 0; i < dd.Items.Count; i++)
                        {
                            if (dd.Items[i].ToString() == bottleNum[j])
                            {
                                goto next;
                            }
                        }
                        dd.Value = null;
                        dd.DataSource = bottleNum;
                        break;
                    next:
                        continue;
                    }

                    P_dt_currentassistantcodeallbottlenum.Clear();
                    //跟据设定浓度重新排序
                    P_str_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight,SyringeType" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv[1, _CurrentRowIndex].Value.ToString() + "'" +
                                " AND RealConcentration != 0 ORDER BY SettingConcentration DESC;";

                    P_dt_currentassistantcodeallbottlenum = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);



                    for (int i = 0; i < P_dt_currentassistantcodeallbottlenum.Rows.Count; i++)
                    {
                        double objectDropWeight = 0;
                        //判断是否需要自动选瓶
                        if (dgv[10, _CurrentRowIndex].Value == null ||
                            dgv[10, _CurrentRowIndex].Value.ToString() == "0")
                        {
                            //需要自动选瓶
                            if (dgv.Rows[_CurrentRowIndex].Cells[4].Value != null)
                            {
                                if (dgv.Rows[_CurrentRowIndex].Cells[4].Value.ToString() == "%")
                                {
                                    //染料
                                    objectDropWeight = (dClothWeight *
                                        Convert.ToDouble(dgv[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(P_dt_currentassistantcodeallbottlenum.Rows[i][2].ToString()));
                                }
                                else
                                {
                                    //助剂
                                    objectDropWeight = (dTotalWeight *
                                        Convert.ToDouble(dgv[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(P_dt_currentassistantcodeallbottlenum.Rows[i][2].ToString()));

                                }
                                if (Convert.ToDouble(String.Format("{0:F3}", objectDropWeight)) >=
                                    Convert.ToDouble(String.Format("{0:F3}", P_dt_currentassistantcodeallbottlenum.Rows[i][3])))
                                {

                                    dd.Value = P_dt_currentassistantcodeallbottlenum.Rows[i][0].ToString();
                                    dgv[6, _CurrentRowIndex].Value = P_dt_currentassistantcodeallbottlenum.Rows[i][1].ToString();
                                    dgv[7, _CurrentRowIndex].Value = P_dt_currentassistantcodeallbottlenum.Rows[i][2].ToString();
                                    dgv[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", objectDropWeight) : String.Format("{0:F3}", objectDropWeight);
                                    break;
                                }
                                else
                                {
                                    if (i == P_dt_currentassistantcodeallbottlenum.Rows.Count - 1)
                                    {
                                        if (objectDropWeight >= 0.1)
                                        {
                                            dd.Value = P_dt_currentassistantcodeallbottlenum.Rows[i][0].ToString();
                                            dgv[6, _CurrentRowIndex].Value = P_dt_currentassistantcodeallbottlenum.Rows[i][1].ToString();
                                            dgv[7, _CurrentRowIndex].Value = P_dt_currentassistantcodeallbottlenum.Rows[i][2].ToString();
                                            dgv[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", objectDropWeight) : String.Format("{0:F3}", objectDropWeight);
                                        }
                                        else
                                        {
                                            dd.Value = null;
                                            dgv[6, _CurrentRowIndex].Value = null;
                                            dgv[7, _CurrentRowIndex].Value = null;
                                            dgv[8, _CurrentRowIndex].Value = null;
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            //不需要自动选瓶

                            //获取当前染助剂所有母液瓶资料
                            foreach (DataRow mdr in P_dt_currentassistantcodeallbottlenum.Rows)
                            {
                                if (dd.Value.ToString() == mdr[0].ToString())
                                {
                                    dgv[5, _CurrentRowIndex].Value = mdr[0].ToString();
                                    dgv[6, _CurrentRowIndex].Value = mdr[1].ToString();
                                    dgv[7, _CurrentRowIndex].Value = mdr[2].ToString();
                                    break;
                                }
                            }

                            //计算目标滴液量
                            if (dgv[4, _CurrentRowIndex].Value != null)
                            {
                                if (dgv[4, _CurrentRowIndex].Value.ToString() == "%")
                                {
                                    //染料
                                    objectDropWeight = (dClothWeight *
                                        Convert.ToDouble(dgv[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(dgv[7, _CurrentRowIndex].Value.ToString()));
                                }
                                else
                                {
                                    //助剂
                                    objectDropWeight = (dTotalWeight *
                                        Convert.ToDouble(dgv[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(dgv[7, _CurrentRowIndex].Value.ToString()));

                                }

                                dgv[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", objectDropWeight) : String.Format("{0:F3}", objectDropWeight);
                                break;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "UpdataDyeAndHandle", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_Dye_SelectionChanged(object sender, EventArgs e)
        {

            FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)sender;
            if (dgv_Dye.Rows.Count >= 1)
            {
                int col = dgv_Dye.CurrentCell.ColumnIndex;

                int row = dgv_Dye.CurrentCell.RowIndex;

                if (col == 3)
                {
                    if (row == dgv_Dye.Rows.Count - 1)
                    {
                        if(Convert.ToInt32(dgv_Dye.Name) <l.Count)
                        {
                            ((FADM_Object.MyDataGridView)(l[Convert.ToInt32(dgv_Dye.Name)])).CurrentCell = ((FADM_Object.MyDataGridView)(l[Convert.ToInt32(dgv_Dye.Name)]))[1, 0];
                            ((FADM_Object.MyDataGridView)(l[Convert.ToInt32(dgv_Dye.Name)])).Focus();
                               
                        }
                        else
                        {
                            button1.Focus();
                        }

                    }

                }
                if (dgv_Dye.CurrentCell.ColumnIndex == 5)
                {
                    try
                    {

                        bottleNum.Clear();
                        bottleNum.Add(dgv_Dye.CurrentRow.Index.ToString());
                        if (dgv_Dye.CurrentRow.Cells[5].Value != null)
                        {
                            bottleNum.Add(dgv_Dye.CurrentRow.Cells[5].Value.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_Dye_SelectionChanged", MessageBoxButtons.OK, true);
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((FADM_Object.MyDataGridView)l[l.Count - 1]).Rows[0].Cells[1].Value = "022";
        }

        ///<summary>
        /// 获取 当前拥有焦点的控件 
        /// </summary>
        /// <returns></returns>
        private Control GetFocusedControl()
        {
            Control focusedControl = null;
            // To get hold of the focused control:           
            IntPtr focusedHandle = GetFocus();
            if (focusedHandle != IntPtr.Zero)
                //focusedControl = Control.FromHandle(focusedHandle);
                focusedControl = Control.FromChildHandle(focusedHandle);
            return focusedControl;
        }
    }
}
