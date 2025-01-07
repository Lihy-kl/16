
using System;
using System.Drawing;
//using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class HistoryData
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryData));
            this.txt_CupNum = new System.Windows.Forms.TextBox();
            this.txt_ClothType = new System.Windows.Forms.TextBox();
            this.txt_Record_Operator = new System.Windows.Forms.ComboBox();
            this.grp_DropRecord = new System.Windows.Forms.GroupBox();
            this.btn_Record_Print = new System.Windows.Forms.Button();
            this.txt_Record_CupNum = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Btn_Derive = new System.Windows.Forms.Button();
            this.txt_R = new System.Windows.Forms.TextBox();
            this.rdo_Record_condition = new System.Windows.Forms.RadioButton();
            this.dt_Record_End = new System.Windows.Forms.DateTimePicker();
            this.dt_Record_Start = new System.Windows.Forms.DateTimePicker();
            this.txt_Record_Code = new System.Windows.Forms.TextBox();
            this.btn_Record_Delete = new System.Windows.Forms.Button();
            this.btn_Record_Select = new System.Windows.Forms.Button();
            this.rdo_Record_All = new System.Windows.Forms.RadioButton();
            this.rdo_Record_Now = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgv_DropRecord = new System.Windows.Forms.DataGridView();
            this.txt_Operator = new System.Windows.Forms.TextBox();
            this.txt_TotalWeight = new System.Windows.Forms.TextBox();
            this.txt_BathRatio = new System.Windows.Forms.TextBox();
            this.txt_ClothWeight = new System.Windows.Forms.TextBox();
            this.chk_AddWaterChoose = new System.Windows.Forms.CheckBox();
            this.txt_Customer = new System.Windows.Forms.TextBox();
            this.txt_FormulaName = new System.Windows.Forms.TextBox();
            this.txt_State = new System.Windows.Forms.TextBox();
            this.txt_VersionNum = new System.Windows.Forms.TextBox();
            this.txt_FormulaCode = new System.Windows.Forms.TextBox();
            this.lab_FormulaCode = new System.Windows.Forms.Label();
            this.lab_FormulaName = new System.Windows.Forms.Label();
            this.lab_CupCode = new System.Windows.Forms.Label();
            this.lab_ClothType = new System.Windows.Forms.Label();
            this.lab_Operator = new System.Windows.Forms.Label();
            this.lab_Customer = new System.Windows.Forms.Label();
            this.lab_TotalWeight = new System.Windows.Forms.Label();
            this.lab_ClothWeight = new System.Windows.Forms.Label();
            this.lab_BathRatio = new System.Windows.Forms.Label();
            this.grp_FormulaData = new System.Windows.Forms.GroupBox();
            this.txt_CreateTime = new System.Windows.Forms.TextBox();
            this.lab_CreateTime = new System.Windows.Forms.Label();
            this.txt_Start = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_TotalTime = new System.Windows.Forms.TextBox();
            this.txt_RealAddWaterWeight = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ObjectAddWaterWeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_Details = new System.Windows.Forms.DataGridView();
            this.txt_DyeingCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_AnhydrationWR = new System.Windows.Forms.TextBox();
            this.lab_AnhydrationWR = new System.Windows.Forms.Label();
            this.txt_Non_AnhydrationWR = new System.Windows.Forms.TextBox();
            this.lab_txt_Non_AnhydrationWR = new System.Windows.Forms.Label();
            this.txt_HandleBathRatio = new System.Windows.Forms.TextBox();
            this.lab_HandleBathRatio = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.grp_DropRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DropRecord)).BeginInit();
            this.grp_FormulaData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Details)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_CupNum
            // 
            resources.ApplyResources(this.txt_CupNum, "txt_CupNum");
            this.txt_CupNum.Name = "txt_CupNum";
            this.txt_CupNum.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_CupNum, resources.GetString("txt_CupNum.ToolTip"));
            // 
            // txt_ClothType
            // 
            resources.ApplyResources(this.txt_ClothType, "txt_ClothType");
            this.txt_ClothType.Name = "txt_ClothType";
            this.txt_ClothType.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_ClothType, resources.GetString("txt_ClothType.ToolTip"));
            // 
            // txt_Record_Operator
            // 
            resources.ApplyResources(this.txt_Record_Operator, "txt_Record_Operator");
            this.txt_Record_Operator.FormattingEnabled = true;
            this.txt_Record_Operator.Name = "txt_Record_Operator";
            this.toolTip1.SetToolTip(this.txt_Record_Operator, resources.GetString("txt_Record_Operator.ToolTip"));
            // 
            // grp_DropRecord
            // 
            resources.ApplyResources(this.grp_DropRecord, "grp_DropRecord");
            this.grp_DropRecord.Controls.Add(this.btn_Record_Print);
            this.grp_DropRecord.Controls.Add(this.txt_Record_CupNum);
            this.grp_DropRecord.Controls.Add(this.label10);
            this.grp_DropRecord.Controls.Add(this.Btn_Derive);
            this.grp_DropRecord.Controls.Add(this.txt_R);
            this.grp_DropRecord.Controls.Add(this.txt_Record_Operator);
            this.grp_DropRecord.Controls.Add(this.rdo_Record_condition);
            this.grp_DropRecord.Controls.Add(this.dt_Record_End);
            this.grp_DropRecord.Controls.Add(this.dt_Record_Start);
            this.grp_DropRecord.Controls.Add(this.txt_Record_Code);
            this.grp_DropRecord.Controls.Add(this.btn_Record_Delete);
            this.grp_DropRecord.Controls.Add(this.btn_Record_Select);
            this.grp_DropRecord.Controls.Add(this.rdo_Record_All);
            this.grp_DropRecord.Controls.Add(this.rdo_Record_Now);
            this.grp_DropRecord.Controls.Add(this.label2);
            this.grp_DropRecord.Controls.Add(this.label3);
            this.grp_DropRecord.Controls.Add(this.label4);
            this.grp_DropRecord.Controls.Add(this.label5);
            this.grp_DropRecord.Controls.Add(this.dgv_DropRecord);
            this.grp_DropRecord.Name = "grp_DropRecord";
            this.grp_DropRecord.TabStop = false;
            this.toolTip1.SetToolTip(this.grp_DropRecord, resources.GetString("grp_DropRecord.ToolTip"));
            // 
            // btn_Record_Print
            // 
            resources.ApplyResources(this.btn_Record_Print, "btn_Record_Print");
            this.btn_Record_Print.Name = "btn_Record_Print";
            this.toolTip1.SetToolTip(this.btn_Record_Print, resources.GetString("btn_Record_Print.ToolTip"));
            this.btn_Record_Print.UseVisualStyleBackColor = true;
            this.btn_Record_Print.Click += new System.EventHandler(this.btn_Record_Print_Click);
            // 
            // txt_Record_CupNum
            // 
            resources.ApplyResources(this.txt_Record_CupNum, "txt_Record_CupNum");
            this.txt_Record_CupNum.Name = "txt_Record_CupNum";
            this.toolTip1.SetToolTip(this.txt_Record_CupNum, resources.GetString("txt_Record_CupNum.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // Btn_Derive
            // 
            resources.ApplyResources(this.Btn_Derive, "Btn_Derive");
            this.Btn_Derive.Name = "Btn_Derive";
            this.toolTip1.SetToolTip(this.Btn_Derive, resources.GetString("Btn_Derive.ToolTip"));
            this.Btn_Derive.UseVisualStyleBackColor = true;
            this.Btn_Derive.Click += new System.EventHandler(this.Btn_Derive_Click);
            // 
            // txt_R
            // 
            resources.ApplyResources(this.txt_R, "txt_R");
            this.txt_R.Name = "txt_R";
            this.txt_R.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_R, resources.GetString("txt_R.ToolTip"));
            // 
            // rdo_Record_condition
            // 
            resources.ApplyResources(this.rdo_Record_condition, "rdo_Record_condition");
            this.rdo_Record_condition.Name = "rdo_Record_condition";
            this.toolTip1.SetToolTip(this.rdo_Record_condition, resources.GetString("rdo_Record_condition.ToolTip"));
            this.rdo_Record_condition.UseVisualStyleBackColor = true;
            this.rdo_Record_condition.CheckedChanged += new System.EventHandler(this.rdo_Record_condition_CheckedChanged);
            // 
            // dt_Record_End
            // 
            resources.ApplyResources(this.dt_Record_End, "dt_Record_End");
            this.dt_Record_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Record_End.Name = "dt_Record_End";
            this.dt_Record_End.ShowUpDown = true;
            this.toolTip1.SetToolTip(this.dt_Record_End, resources.GetString("dt_Record_End.ToolTip"));
            // 
            // dt_Record_Start
            // 
            resources.ApplyResources(this.dt_Record_Start, "dt_Record_Start");
            this.dt_Record_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Record_Start.Name = "dt_Record_Start";
            this.dt_Record_Start.ShowUpDown = true;
            this.toolTip1.SetToolTip(this.dt_Record_Start, resources.GetString("dt_Record_Start.ToolTip"));
            this.dt_Record_Start.Value = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            // 
            // txt_Record_Code
            // 
            resources.ApplyResources(this.txt_Record_Code, "txt_Record_Code");
            this.txt_Record_Code.Name = "txt_Record_Code";
            this.toolTip1.SetToolTip(this.txt_Record_Code, resources.GetString("txt_Record_Code.ToolTip"));
            // 
            // btn_Record_Delete
            // 
            resources.ApplyResources(this.btn_Record_Delete, "btn_Record_Delete");
            this.btn_Record_Delete.Name = "btn_Record_Delete";
            this.toolTip1.SetToolTip(this.btn_Record_Delete, resources.GetString("btn_Record_Delete.ToolTip"));
            this.btn_Record_Delete.UseVisualStyleBackColor = true;
            this.btn_Record_Delete.Click += new System.EventHandler(this.btn_Record_Delete_Click);
            // 
            // btn_Record_Select
            // 
            resources.ApplyResources(this.btn_Record_Select, "btn_Record_Select");
            this.btn_Record_Select.Name = "btn_Record_Select";
            this.toolTip1.SetToolTip(this.btn_Record_Select, resources.GetString("btn_Record_Select.ToolTip"));
            this.btn_Record_Select.UseVisualStyleBackColor = true;
            this.btn_Record_Select.Click += new System.EventHandler(this.btn_Record_Select_Click);
            // 
            // rdo_Record_All
            // 
            resources.ApplyResources(this.rdo_Record_All, "rdo_Record_All");
            this.rdo_Record_All.Name = "rdo_Record_All";
            this.toolTip1.SetToolTip(this.rdo_Record_All, resources.GetString("rdo_Record_All.ToolTip"));
            this.rdo_Record_All.UseVisualStyleBackColor = true;
            this.rdo_Record_All.CheckedChanged += new System.EventHandler(this.rdo_Record_All_CheckedChanged);
            // 
            // rdo_Record_Now
            // 
            resources.ApplyResources(this.rdo_Record_Now, "rdo_Record_Now");
            this.rdo_Record_Now.Checked = true;
            this.rdo_Record_Now.Name = "rdo_Record_Now";
            this.rdo_Record_Now.TabStop = true;
            this.toolTip1.SetToolTip(this.rdo_Record_Now, resources.GetString("rdo_Record_Now.ToolTip"));
            this.rdo_Record_Now.UseVisualStyleBackColor = true;
            this.rdo_Record_Now.CheckedChanged += new System.EventHandler(this.rdo_Record_Now_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // dgv_DropRecord
            // 
            resources.ApplyResources(this.dgv_DropRecord, "dgv_DropRecord");
            this.dgv_DropRecord.AllowUserToAddRows = false;
            this.dgv_DropRecord.AllowUserToDeleteRows = false;
            this.dgv_DropRecord.AllowUserToResizeColumns = false;
            this.dgv_DropRecord.AllowUserToResizeRows = false;
            this.dgv_DropRecord.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_DropRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DropRecord.MultiSelect = false;
            this.dgv_DropRecord.Name = "dgv_DropRecord";
            this.dgv_DropRecord.ReadOnly = true;
            this.dgv_DropRecord.RowHeadersVisible = false;
            this.dgv_DropRecord.RowTemplate.Height = 23;
            this.dgv_DropRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dgv_DropRecord, resources.GetString("dgv_DropRecord.ToolTip"));
            this.dgv_DropRecord.CurrentCellChanged += new System.EventHandler(this.dgv_DropRecord_CurrentCellChanged);
            this.dgv_DropRecord.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_DropRecord_DataBindingComplete);
            // 
            // txt_Operator
            // 
            resources.ApplyResources(this.txt_Operator, "txt_Operator");
            this.txt_Operator.Name = "txt_Operator";
            this.txt_Operator.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_Operator, resources.GetString("txt_Operator.ToolTip"));
            // 
            // txt_TotalWeight
            // 
            resources.ApplyResources(this.txt_TotalWeight, "txt_TotalWeight");
            this.txt_TotalWeight.Name = "txt_TotalWeight";
            this.txt_TotalWeight.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_TotalWeight, resources.GetString("txt_TotalWeight.ToolTip"));
            // 
            // txt_BathRatio
            // 
            resources.ApplyResources(this.txt_BathRatio, "txt_BathRatio");
            this.txt_BathRatio.Name = "txt_BathRatio";
            this.txt_BathRatio.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_BathRatio, resources.GetString("txt_BathRatio.ToolTip"));
            // 
            // txt_ClothWeight
            // 
            resources.ApplyResources(this.txt_ClothWeight, "txt_ClothWeight");
            this.txt_ClothWeight.Name = "txt_ClothWeight";
            this.txt_ClothWeight.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_ClothWeight, resources.GetString("txt_ClothWeight.ToolTip"));
            // 
            // chk_AddWaterChoose
            // 
            resources.ApplyResources(this.chk_AddWaterChoose, "chk_AddWaterChoose");
            this.chk_AddWaterChoose.BackColor = System.Drawing.SystemColors.Control;
            this.chk_AddWaterChoose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chk_AddWaterChoose.Name = "chk_AddWaterChoose";
            this.toolTip1.SetToolTip(this.chk_AddWaterChoose, resources.GetString("chk_AddWaterChoose.ToolTip"));
            this.chk_AddWaterChoose.UseVisualStyleBackColor = false;
            // 
            // txt_Customer
            // 
            resources.ApplyResources(this.txt_Customer, "txt_Customer");
            this.txt_Customer.Name = "txt_Customer";
            this.txt_Customer.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_Customer, resources.GetString("txt_Customer.ToolTip"));
            // 
            // txt_FormulaName
            // 
            resources.ApplyResources(this.txt_FormulaName, "txt_FormulaName");
            this.txt_FormulaName.Name = "txt_FormulaName";
            this.txt_FormulaName.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_FormulaName, resources.GetString("txt_FormulaName.ToolTip"));
            // 
            // txt_State
            // 
            resources.ApplyResources(this.txt_State, "txt_State");
            this.txt_State.Name = "txt_State";
            this.txt_State.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_State, resources.GetString("txt_State.ToolTip"));
            // 
            // txt_VersionNum
            // 
            resources.ApplyResources(this.txt_VersionNum, "txt_VersionNum");
            this.txt_VersionNum.Name = "txt_VersionNum";
            this.txt_VersionNum.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_VersionNum, resources.GetString("txt_VersionNum.ToolTip"));
            // 
            // txt_FormulaCode
            // 
            resources.ApplyResources(this.txt_FormulaCode, "txt_FormulaCode");
            this.txt_FormulaCode.Name = "txt_FormulaCode";
            this.txt_FormulaCode.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_FormulaCode, resources.GetString("txt_FormulaCode.ToolTip"));
            // 
            // lab_FormulaCode
            // 
            resources.ApplyResources(this.lab_FormulaCode, "lab_FormulaCode");
            this.lab_FormulaCode.Name = "lab_FormulaCode";
            this.toolTip1.SetToolTip(this.lab_FormulaCode, resources.GetString("lab_FormulaCode.ToolTip"));
            // 
            // lab_FormulaName
            // 
            resources.ApplyResources(this.lab_FormulaName, "lab_FormulaName");
            this.lab_FormulaName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_FormulaName.Name = "lab_FormulaName";
            this.toolTip1.SetToolTip(this.lab_FormulaName, resources.GetString("lab_FormulaName.ToolTip"));
            // 
            // lab_CupCode
            // 
            resources.ApplyResources(this.lab_CupCode, "lab_CupCode");
            this.lab_CupCode.Name = "lab_CupCode";
            this.toolTip1.SetToolTip(this.lab_CupCode, resources.GetString("lab_CupCode.ToolTip"));
            // 
            // lab_ClothType
            // 
            resources.ApplyResources(this.lab_ClothType, "lab_ClothType");
            this.lab_ClothType.Name = "lab_ClothType";
            this.toolTip1.SetToolTip(this.lab_ClothType, resources.GetString("lab_ClothType.ToolTip"));
            // 
            // lab_Operator
            // 
            resources.ApplyResources(this.lab_Operator, "lab_Operator");
            this.lab_Operator.Name = "lab_Operator";
            this.toolTip1.SetToolTip(this.lab_Operator, resources.GetString("lab_Operator.ToolTip"));
            // 
            // lab_Customer
            // 
            resources.ApplyResources(this.lab_Customer, "lab_Customer");
            this.lab_Customer.Name = "lab_Customer";
            this.toolTip1.SetToolTip(this.lab_Customer, resources.GetString("lab_Customer.ToolTip"));
            // 
            // lab_TotalWeight
            // 
            resources.ApplyResources(this.lab_TotalWeight, "lab_TotalWeight");
            this.lab_TotalWeight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_TotalWeight.Name = "lab_TotalWeight";
            this.toolTip1.SetToolTip(this.lab_TotalWeight, resources.GetString("lab_TotalWeight.ToolTip"));
            // 
            // lab_ClothWeight
            // 
            resources.ApplyResources(this.lab_ClothWeight, "lab_ClothWeight");
            this.lab_ClothWeight.Name = "lab_ClothWeight";
            this.toolTip1.SetToolTip(this.lab_ClothWeight, resources.GetString("lab_ClothWeight.ToolTip"));
            // 
            // lab_BathRatio
            // 
            resources.ApplyResources(this.lab_BathRatio, "lab_BathRatio");
            this.lab_BathRatio.Name = "lab_BathRatio";
            this.toolTip1.SetToolTip(this.lab_BathRatio, resources.GetString("lab_BathRatio.ToolTip"));
            // 
            // grp_FormulaData
            // 
            resources.ApplyResources(this.grp_FormulaData, "grp_FormulaData");
            this.grp_FormulaData.Controls.Add(this.txt_CreateTime);
            this.grp_FormulaData.Controls.Add(this.lab_CreateTime);
            this.grp_FormulaData.Controls.Add(this.txt_Start);
            this.grp_FormulaData.Controls.Add(this.label9);
            this.grp_FormulaData.Controls.Add(this.btn_save);
            this.grp_FormulaData.Controls.Add(this.panel1);
            this.grp_FormulaData.Controls.Add(this.label8);
            this.grp_FormulaData.Controls.Add(this.txt_TotalTime);
            this.grp_FormulaData.Controls.Add(this.txt_RealAddWaterWeight);
            this.grp_FormulaData.Controls.Add(this.label7);
            this.grp_FormulaData.Controls.Add(this.txt_ObjectAddWaterWeight);
            this.grp_FormulaData.Controls.Add(this.label1);
            this.grp_FormulaData.Controls.Add(this.dgv_Details);
            this.grp_FormulaData.Controls.Add(this.txt_DyeingCode);
            this.grp_FormulaData.Controls.Add(this.label6);
            this.grp_FormulaData.Controls.Add(this.txt_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.txt_Non_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_txt_Non_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.txt_HandleBathRatio);
            this.grp_FormulaData.Controls.Add(this.lab_HandleBathRatio);
            this.grp_FormulaData.Controls.Add(this.txt_Operator);
            this.grp_FormulaData.Controls.Add(this.lab_FormulaCode);
            this.grp_FormulaData.Controls.Add(this.lab_BathRatio);
            this.grp_FormulaData.Controls.Add(this.lab_ClothWeight);
            this.grp_FormulaData.Controls.Add(this.txt_CupNum);
            this.grp_FormulaData.Controls.Add(this.lab_TotalWeight);
            this.grp_FormulaData.Controls.Add(this.lab_Customer);
            this.grp_FormulaData.Controls.Add(this.txt_ClothType);
            this.grp_FormulaData.Controls.Add(this.lab_Operator);
            this.grp_FormulaData.Controls.Add(this.lab_ClothType);
            this.grp_FormulaData.Controls.Add(this.lab_CupCode);
            this.grp_FormulaData.Controls.Add(this.lab_FormulaName);
            this.grp_FormulaData.Controls.Add(this.txt_TotalWeight);
            this.grp_FormulaData.Controls.Add(this.txt_FormulaCode);
            this.grp_FormulaData.Controls.Add(this.txt_BathRatio);
            this.grp_FormulaData.Controls.Add(this.txt_VersionNum);
            this.grp_FormulaData.Controls.Add(this.txt_State);
            this.grp_FormulaData.Controls.Add(this.txt_ClothWeight);
            this.grp_FormulaData.Controls.Add(this.txt_FormulaName);
            this.grp_FormulaData.Controls.Add(this.txt_Customer);
            this.grp_FormulaData.Controls.Add(this.chk_AddWaterChoose);
            this.grp_FormulaData.Name = "grp_FormulaData";
            this.grp_FormulaData.TabStop = false;
            this.toolTip1.SetToolTip(this.grp_FormulaData, resources.GetString("grp_FormulaData.ToolTip"));
            // 
            // txt_CreateTime
            // 
            resources.ApplyResources(this.txt_CreateTime, "txt_CreateTime");
            this.txt_CreateTime.Name = "txt_CreateTime";
            this.txt_CreateTime.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_CreateTime, resources.GetString("txt_CreateTime.ToolTip"));
            // 
            // lab_CreateTime
            // 
            resources.ApplyResources(this.lab_CreateTime, "lab_CreateTime");
            this.lab_CreateTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_CreateTime.Name = "lab_CreateTime";
            this.toolTip1.SetToolTip(this.lab_CreateTime, resources.GetString("lab_CreateTime.ToolTip"));
            // 
            // txt_Start
            // 
            resources.ApplyResources(this.txt_Start, "txt_Start");
            this.txt_Start.Name = "txt_Start";
            this.txt_Start.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_Start, resources.GetString("txt_Start.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Name = "label9";
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // btn_save
            // 
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.Name = "btn_save";
            this.toolTip1.SetToolTip(this.btn_save, resources.GetString("btn_save.ToolTip"));
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // txt_TotalTime
            // 
            resources.ApplyResources(this.txt_TotalTime, "txt_TotalTime");
            this.txt_TotalTime.Name = "txt_TotalTime";
            this.txt_TotalTime.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_TotalTime, resources.GetString("txt_TotalTime.ToolTip"));
            // 
            // txt_RealAddWaterWeight
            // 
            resources.ApplyResources(this.txt_RealAddWaterWeight, "txt_RealAddWaterWeight");
            this.txt_RealAddWaterWeight.Name = "txt_RealAddWaterWeight";
            this.txt_RealAddWaterWeight.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_RealAddWaterWeight, resources.GetString("txt_RealAddWaterWeight.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // txt_ObjectAddWaterWeight
            // 
            resources.ApplyResources(this.txt_ObjectAddWaterWeight, "txt_ObjectAddWaterWeight");
            this.txt_ObjectAddWaterWeight.Name = "txt_ObjectAddWaterWeight";
            this.txt_ObjectAddWaterWeight.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_ObjectAddWaterWeight, resources.GetString("txt_ObjectAddWaterWeight.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // dgv_Details
            // 
            resources.ApplyResources(this.dgv_Details, "dgv_Details");
            this.dgv_Details.AllowUserToAddRows = false;
            this.dgv_Details.AllowUserToDeleteRows = false;
            this.dgv_Details.AllowUserToResizeColumns = false;
            this.dgv_Details.AllowUserToResizeRows = false;
            this.dgv_Details.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Details.MultiSelect = false;
            this.dgv_Details.Name = "dgv_Details";
            this.dgv_Details.ReadOnly = true;
            this.dgv_Details.RowHeadersVisible = false;
            this.dgv_Details.RowTemplate.Height = 23;
            this.dgv_Details.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dgv_Details, resources.GetString("dgv_Details.ToolTip"));
            // 
            // txt_DyeingCode
            // 
            resources.ApplyResources(this.txt_DyeingCode, "txt_DyeingCode");
            this.txt_DyeingCode.Name = "txt_DyeingCode";
            this.txt_DyeingCode.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_DyeingCode, resources.GetString("txt_DyeingCode.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // txt_AnhydrationWR
            // 
            resources.ApplyResources(this.txt_AnhydrationWR, "txt_AnhydrationWR");
            this.txt_AnhydrationWR.Name = "txt_AnhydrationWR";
            this.txt_AnhydrationWR.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_AnhydrationWR, resources.GetString("txt_AnhydrationWR.ToolTip"));
            // 
            // lab_AnhydrationWR
            // 
            resources.ApplyResources(this.lab_AnhydrationWR, "lab_AnhydrationWR");
            this.lab_AnhydrationWR.Name = "lab_AnhydrationWR";
            this.toolTip1.SetToolTip(this.lab_AnhydrationWR, resources.GetString("lab_AnhydrationWR.ToolTip"));
            // 
            // txt_Non_AnhydrationWR
            // 
            resources.ApplyResources(this.txt_Non_AnhydrationWR, "txt_Non_AnhydrationWR");
            this.txt_Non_AnhydrationWR.Name = "txt_Non_AnhydrationWR";
            this.txt_Non_AnhydrationWR.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_Non_AnhydrationWR, resources.GetString("txt_Non_AnhydrationWR.ToolTip"));
            // 
            // lab_txt_Non_AnhydrationWR
            // 
            resources.ApplyResources(this.lab_txt_Non_AnhydrationWR, "lab_txt_Non_AnhydrationWR");
            this.lab_txt_Non_AnhydrationWR.Name = "lab_txt_Non_AnhydrationWR";
            this.toolTip1.SetToolTip(this.lab_txt_Non_AnhydrationWR, resources.GetString("lab_txt_Non_AnhydrationWR.ToolTip"));
            // 
            // txt_HandleBathRatio
            // 
            resources.ApplyResources(this.txt_HandleBathRatio, "txt_HandleBathRatio");
            this.txt_HandleBathRatio.Name = "txt_HandleBathRatio";
            this.txt_HandleBathRatio.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txt_HandleBathRatio, resources.GetString("txt_HandleBathRatio.ToolTip"));
            // 
            // lab_HandleBathRatio
            // 
            resources.ApplyResources(this.lab_HandleBathRatio, "lab_HandleBathRatio");
            this.lab_HandleBathRatio.Name = "lab_HandleBathRatio";
            this.toolTip1.SetToolTip(this.lab_HandleBathRatio, resources.GetString("lab_HandleBathRatio.ToolTip"));
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 6000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // HistoryData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_DropRecord);
            this.Controls.Add(this.grp_FormulaData);
            this.Name = "HistoryData";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.HistoryData_Load);
            this.grp_DropRecord.ResumeLayout(false);
            this.grp_DropRecord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DropRecord)).EndInit();
            this.grp_FormulaData.ResumeLayout(false);
            this.grp_FormulaData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Details)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_CupNum;
        private System.Windows.Forms.TextBox txt_ClothType;
        private System.Windows.Forms.ComboBox txt_Record_Operator;
        private System.Windows.Forms.GroupBox grp_DropRecord;
        private System.Windows.Forms.RadioButton rdo_Record_condition;
        private System.Windows.Forms.DateTimePicker dt_Record_End;
        private System.Windows.Forms.DateTimePicker dt_Record_Start;
        private System.Windows.Forms.TextBox txt_Record_Code;
        private System.Windows.Forms.Button btn_Record_Delete;
        private System.Windows.Forms.Button btn_Record_Select;
        private System.Windows.Forms.RadioButton rdo_Record_All;
        private System.Windows.Forms.RadioButton rdo_Record_Now;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgv_DropRecord;
        private System.Windows.Forms.TextBox txt_TotalWeight;
        private System.Windows.Forms.TextBox txt_BathRatio;
        private System.Windows.Forms.TextBox txt_ClothWeight;
        private System.Windows.Forms.CheckBox chk_AddWaterChoose;
        private System.Windows.Forms.TextBox txt_Customer;
        private System.Windows.Forms.TextBox txt_FormulaName;
        private System.Windows.Forms.TextBox txt_State;
        private System.Windows.Forms.TextBox txt_VersionNum;
        private System.Windows.Forms.TextBox txt_FormulaCode;
        private System.Windows.Forms.Label lab_CupCode;
        private System.Windows.Forms.Label lab_Operator;
        private System.Windows.Forms.Label lab_TotalWeight;
        private System.Windows.Forms.Label lab_BathRatio;
        private System.Windows.Forms.Label lab_ClothWeight;
        private System.Windows.Forms.GroupBox grp_FormulaData;
        private System.Windows.Forms.Label lab_Customer;
        private System.Windows.Forms.Label lab_ClothType;
        private System.Windows.Forms.Label lab_FormulaName;
        private System.Windows.Forms.Label lab_FormulaCode;
        private System.Windows.Forms.TextBox txt_Operator;
        private System.Windows.Forms.TextBox txt_AnhydrationWR;
        private System.Windows.Forms.Label lab_AnhydrationWR;
        private System.Windows.Forms.TextBox txt_Non_AnhydrationWR;
        private System.Windows.Forms.Label lab_txt_Non_AnhydrationWR;
        private System.Windows.Forms.TextBox txt_HandleBathRatio;
        private System.Windows.Forms.Label lab_HandleBathRatio;
        private System.Windows.Forms.DataGridView dgv_Details;
        private System.Windows.Forms.TextBox txt_DyeingCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_RealAddWaterWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_ObjectAddWaterWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_TotalTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txt_R;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Btn_Derive;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox txt_Start;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_CreateTime;
        private System.Windows.Forms.Label lab_CreateTime;
        private System.Windows.Forms.TextBox txt_Record_CupNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_Record_Print;
    }
}
