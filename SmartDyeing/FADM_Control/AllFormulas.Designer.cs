using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class AllFormulas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllFormulas));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_Details = new System.Windows.Forms.DataGridView();
            this.dgv_Dyeing = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_CupNum = new System.Windows.Forms.TextBox();
            this.txt_ClothType = new System.Windows.Forms.TextBox();
            this.txt_CreateTime = new System.Windows.Forms.TextBox();
            this.txt_TotalWeight = new System.Windows.Forms.TextBox();
            this.txt_BathRatio = new System.Windows.Forms.TextBox();
            this.txt_ClothWeight = new System.Windows.Forms.TextBox();
            this.chk_AddWaterChoose = new System.Windows.Forms.CheckBox();
            this.txt_Customer = new System.Windows.Forms.TextBox();
            this.txt_FormulaName = new System.Windows.Forms.TextBox();
            this.txt_State = new System.Windows.Forms.TextBox();
            this.txt_FormulaCode = new System.Windows.Forms.TextBox();
            this.grp_BatchData = new System.Windows.Forms.GroupBox();
            this.txt_Record_Operator = new System.Windows.Forms.ComboBox();
            this.dgv_BatchData = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dt_Record_End = new System.Windows.Forms.DateTimePicker();
            this.rdo_Record_Now = new System.Windows.Forms.RadioButton();
            this.rdo_Record_condition = new System.Windows.Forms.RadioButton();
            this.btn_Record_Select = new System.Windows.Forms.Button();
            this.dt_Record_Start = new System.Windows.Forms.DateTimePicker();
            this.rdo_Record_All = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Record_Code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grp_FormulaData = new System.Windows.Forms.GroupBox();
            this.txt_DyeingCode = new System.Windows.Forms.ComboBox();
            this.txt_Operator = new System.Windows.Forms.ComboBox();
            this.txt_AnhydrationWR = new System.Windows.Forms.TextBox();
            this.lab_AnhydrationWR = new System.Windows.Forms.Label();
            this.txt_Non_AnhydrationWR = new System.Windows.Forms.TextBox();
            this.lab_txt_Non_AnhydrationWR = new System.Windows.Forms.Label();
            this.lab_DyeingCode = new System.Windows.Forms.Label();
            this.txt_VersionNum = new System.Windows.Forms.TextBox();
            this.lab_CreateTime = new System.Windows.Forms.Label();
            this.lab_CupNum = new System.Windows.Forms.Label();
            this.lab_Operator = new System.Windows.Forms.Label();
            this.lab_TotalWeight = new System.Windows.Forms.Label();
            this.lab_BathRatio = new System.Windows.Forms.Label();
            this.lab_ClothWeight = new System.Windows.Forms.Label();
            this.lab_Customer = new System.Windows.Forms.Label();
            this.lab_ClothType = new System.Windows.Forms.Label();
            this.lab_FormulaName = new System.Windows.Forms.Label();
            this.lab_FormulaCode = new System.Windows.Forms.Label();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssistantCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormulaDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SettingConcentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealConcentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssistantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectDropWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealDropWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BottleSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn46 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn47 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_FormulaData = new SmartDyeing.FADM_Object.MyDataGridView();
            this.dataGridViewTextBoxColumn55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn57 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn58 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn59 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn61 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn62 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Details)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dyeing)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.grp_BatchData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BatchData)).BeginInit();
            this.grp_FormulaData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Details
            // 
            this.dgv_Details.AllowUserToAddRows = false;
            this.dgv_Details.AllowUserToDeleteRows = false;
            this.dgv_Details.AllowUserToResizeColumns = false;
            this.dgv_Details.AllowUserToResizeRows = false;
            this.dgv_Details.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_Details, "dgv_Details");
            this.dgv_Details.MultiSelect = false;
            this.dgv_Details.Name = "dgv_Details";
            this.dgv_Details.ReadOnly = true;
            this.dgv_Details.RowHeadersVisible = false;
            this.dgv_Details.RowTemplate.Height = 23;
            this.dgv_Details.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // dgv_Dyeing
            // 
            this.dgv_Dyeing.AllowUserToAddRows = false;
            this.dgv_Dyeing.AllowUserToDeleteRows = false;
            this.dgv_Dyeing.AllowUserToResizeColumns = false;
            this.dgv_Dyeing.AllowUserToResizeRows = false;
            this.dgv_Dyeing.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Dyeing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Dyeing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4});
            resources.ApplyResources(this.dgv_Dyeing, "dgv_Dyeing");
            this.dgv_Dyeing.MultiSelect = false;
            this.dgv_Dyeing.Name = "dgv_Dyeing";
            this.dgv_Dyeing.ReadOnly = true;
            this.dgv_Dyeing.RowHeadersVisible = false;
            this.dgv_Dyeing.RowTemplate.Height = 23;
            this.dgv_Dyeing.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Dyeing.CurrentCellChanged += new System.EventHandler(this.dgv_Dyeing_CurrentCellChanged);
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_Details);
            this.groupBox3.Controls.Add(this.dgv_Dyeing);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txt_CupNum
            // 
            resources.ApplyResources(this.txt_CupNum, "txt_CupNum");
            this.txt_CupNum.Name = "txt_CupNum";
            // 
            // txt_ClothType
            // 
            resources.ApplyResources(this.txt_ClothType, "txt_ClothType");
            this.txt_ClothType.Name = "txt_ClothType";
            // 
            // txt_CreateTime
            // 
            resources.ApplyResources(this.txt_CreateTime, "txt_CreateTime");
            this.txt_CreateTime.Name = "txt_CreateTime";
            // 
            // txt_TotalWeight
            // 
            resources.ApplyResources(this.txt_TotalWeight, "txt_TotalWeight");
            this.txt_TotalWeight.Name = "txt_TotalWeight";
            // 
            // txt_BathRatio
            // 
            resources.ApplyResources(this.txt_BathRatio, "txt_BathRatio");
            this.txt_BathRatio.Name = "txt_BathRatio";
            // 
            // txt_ClothWeight
            // 
            resources.ApplyResources(this.txt_ClothWeight, "txt_ClothWeight");
            this.txt_ClothWeight.Name = "txt_ClothWeight";
            // 
            // chk_AddWaterChoose
            // 
            resources.ApplyResources(this.chk_AddWaterChoose, "chk_AddWaterChoose");
            this.chk_AddWaterChoose.BackColor = System.Drawing.SystemColors.Control;
            this.chk_AddWaterChoose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chk_AddWaterChoose.Name = "chk_AddWaterChoose";
            this.chk_AddWaterChoose.UseVisualStyleBackColor = false;
            // 
            // txt_Customer
            // 
            resources.ApplyResources(this.txt_Customer, "txt_Customer");
            this.txt_Customer.Name = "txt_Customer";
            // 
            // txt_FormulaName
            // 
            resources.ApplyResources(this.txt_FormulaName, "txt_FormulaName");
            this.txt_FormulaName.Name = "txt_FormulaName";
            // 
            // txt_State
            // 
            resources.ApplyResources(this.txt_State, "txt_State");
            this.txt_State.Name = "txt_State";
            // 
            // txt_FormulaCode
            // 
            resources.ApplyResources(this.txt_FormulaCode, "txt_FormulaCode");
            this.txt_FormulaCode.Name = "txt_FormulaCode";
            // 
            // grp_BatchData
            // 
            this.grp_BatchData.Controls.Add(this.txt_Record_Operator);
            this.grp_BatchData.Controls.Add(this.dgv_BatchData);
            this.grp_BatchData.Controls.Add(this.dt_Record_End);
            this.grp_BatchData.Controls.Add(this.rdo_Record_Now);
            this.grp_BatchData.Controls.Add(this.rdo_Record_condition);
            this.grp_BatchData.Controls.Add(this.btn_Record_Select);
            this.grp_BatchData.Controls.Add(this.dt_Record_Start);
            this.grp_BatchData.Controls.Add(this.rdo_Record_All);
            this.grp_BatchData.Controls.Add(this.label5);
            this.grp_BatchData.Controls.Add(this.txt_Record_Code);
            this.grp_BatchData.Controls.Add(this.label4);
            this.grp_BatchData.Controls.Add(this.label3);
            this.grp_BatchData.Controls.Add(this.label2);
            resources.ApplyResources(this.grp_BatchData, "grp_BatchData");
            this.grp_BatchData.Name = "grp_BatchData";
            this.grp_BatchData.TabStop = false;
            // 
            // txt_Record_Operator
            // 
            resources.ApplyResources(this.txt_Record_Operator, "txt_Record_Operator");
            this.txt_Record_Operator.FormattingEnabled = true;
            this.txt_Record_Operator.Name = "txt_Record_Operator";
            // 
            // dgv_BatchData
            // 
            this.dgv_BatchData.AllowUserToAddRows = false;
            this.dgv_BatchData.AllowUserToOrderColumns = true;
            this.dgv_BatchData.AllowUserToResizeColumns = false;
            this.dgv_BatchData.AllowUserToResizeRows = false;
            this.dgv_BatchData.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_BatchData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_BatchData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_BatchData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_BatchData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_BatchData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_BatchData, "dgv_BatchData");
            this.dgv_BatchData.MultiSelect = false;
            this.dgv_BatchData.Name = "dgv_BatchData";
            this.dgv_BatchData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_BatchData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_BatchData.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_BatchData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_BatchData.RowTemplate.Height = 23;
            this.dgv_BatchData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_BatchData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_BatchData_CellClick);
            this.dgv_BatchData.CurrentCellChanged += new System.EventHandler(this.dgv_BatchData_CurrentCellChanged);
            // 
            // Column5
            // 
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Column7, "Column7");
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // dt_Record_End
            // 
            resources.ApplyResources(this.dt_Record_End, "dt_Record_End");
            this.dt_Record_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Record_End.Name = "dt_Record_End";
            this.dt_Record_End.ShowUpDown = true;
            // 
            // rdo_Record_Now
            // 
            resources.ApplyResources(this.rdo_Record_Now, "rdo_Record_Now");
            this.rdo_Record_Now.Checked = true;
            this.rdo_Record_Now.Name = "rdo_Record_Now";
            this.rdo_Record_Now.TabStop = true;
            this.rdo_Record_Now.UseVisualStyleBackColor = true;
            this.rdo_Record_Now.Click += new System.EventHandler(this.rdo_Record_Now_Click);
            // 
            // rdo_Record_condition
            // 
            resources.ApplyResources(this.rdo_Record_condition, "rdo_Record_condition");
            this.rdo_Record_condition.Name = "rdo_Record_condition";
            this.rdo_Record_condition.UseVisualStyleBackColor = true;
            this.rdo_Record_condition.Click += new System.EventHandler(this.rdo_Record_condition_Click);
            // 
            // btn_Record_Select
            // 
            resources.ApplyResources(this.btn_Record_Select, "btn_Record_Select");
            this.btn_Record_Select.Name = "btn_Record_Select";
            this.btn_Record_Select.UseVisualStyleBackColor = true;
            this.btn_Record_Select.Click += new System.EventHandler(this.btn_Record_Select_Click);
            // 
            // dt_Record_Start
            // 
            resources.ApplyResources(this.dt_Record_Start, "dt_Record_Start");
            this.dt_Record_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Record_Start.Name = "dt_Record_Start";
            this.dt_Record_Start.ShowUpDown = true;
            this.dt_Record_Start.Value = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            // 
            // rdo_Record_All
            // 
            resources.ApplyResources(this.rdo_Record_All, "rdo_Record_All");
            this.rdo_Record_All.Name = "rdo_Record_All";
            this.rdo_Record_All.UseVisualStyleBackColor = true;
            this.rdo_Record_All.Click += new System.EventHandler(this.rdo_Record_All_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txt_Record_Code
            // 
            resources.ApplyResources(this.txt_Record_Code, "txt_Record_Code");
            this.txt_Record_Code.Name = "txt_Record_Code";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // grp_FormulaData
            // 
            this.grp_FormulaData.Controls.Add(this.txt_DyeingCode);
            this.grp_FormulaData.Controls.Add(this.txt_Operator);
            this.grp_FormulaData.Controls.Add(this.dgv_FormulaData);
            this.grp_FormulaData.Controls.Add(this.txt_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.txt_Non_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_txt_Non_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_DyeingCode);
            this.grp_FormulaData.Controls.Add(this.txt_CupNum);
            this.grp_FormulaData.Controls.Add(this.txt_ClothType);
            this.grp_FormulaData.Controls.Add(this.txt_CreateTime);
            this.grp_FormulaData.Controls.Add(this.txt_TotalWeight);
            this.grp_FormulaData.Controls.Add(this.txt_BathRatio);
            this.grp_FormulaData.Controls.Add(this.txt_ClothWeight);
            this.grp_FormulaData.Controls.Add(this.chk_AddWaterChoose);
            this.grp_FormulaData.Controls.Add(this.txt_Customer);
            this.grp_FormulaData.Controls.Add(this.txt_FormulaName);
            this.grp_FormulaData.Controls.Add(this.txt_State);
            this.grp_FormulaData.Controls.Add(this.txt_VersionNum);
            this.grp_FormulaData.Controls.Add(this.txt_FormulaCode);
            this.grp_FormulaData.Controls.Add(this.lab_CreateTime);
            this.grp_FormulaData.Controls.Add(this.lab_CupNum);
            this.grp_FormulaData.Controls.Add(this.lab_Operator);
            this.grp_FormulaData.Controls.Add(this.lab_TotalWeight);
            this.grp_FormulaData.Controls.Add(this.lab_BathRatio);
            this.grp_FormulaData.Controls.Add(this.lab_ClothWeight);
            this.grp_FormulaData.Controls.Add(this.lab_Customer);
            this.grp_FormulaData.Controls.Add(this.lab_ClothType);
            this.grp_FormulaData.Controls.Add(this.lab_FormulaName);
            this.grp_FormulaData.Controls.Add(this.lab_FormulaCode);
            resources.ApplyResources(this.grp_FormulaData, "grp_FormulaData");
            this.grp_FormulaData.Name = "grp_FormulaData";
            this.grp_FormulaData.TabStop = false;
            // 
            // txt_DyeingCode
            // 
            resources.ApplyResources(this.txt_DyeingCode, "txt_DyeingCode");
            this.txt_DyeingCode.FormattingEnabled = true;
            this.txt_DyeingCode.Name = "txt_DyeingCode";
            this.txt_DyeingCode.SelectedIndexChanged += new System.EventHandler(this.txt_DyeingCode_SelectedIndexChanged);
            // 
            // txt_Operator
            // 
            this.txt_Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.txt_Operator, "txt_Operator");
            this.txt_Operator.FormattingEnabled = true;
            this.txt_Operator.Name = "txt_Operator";
            // 
            // txt_AnhydrationWR
            // 
            resources.ApplyResources(this.txt_AnhydrationWR, "txt_AnhydrationWR");
            this.txt_AnhydrationWR.Name = "txt_AnhydrationWR";
            // 
            // lab_AnhydrationWR
            // 
            resources.ApplyResources(this.lab_AnhydrationWR, "lab_AnhydrationWR");
            this.lab_AnhydrationWR.Name = "lab_AnhydrationWR";
            // 
            // txt_Non_AnhydrationWR
            // 
            resources.ApplyResources(this.txt_Non_AnhydrationWR, "txt_Non_AnhydrationWR");
            this.txt_Non_AnhydrationWR.Name = "txt_Non_AnhydrationWR";
            // 
            // lab_txt_Non_AnhydrationWR
            // 
            resources.ApplyResources(this.lab_txt_Non_AnhydrationWR, "lab_txt_Non_AnhydrationWR");
            this.lab_txt_Non_AnhydrationWR.Name = "lab_txt_Non_AnhydrationWR";
            // 
            // lab_DyeingCode
            // 
            resources.ApplyResources(this.lab_DyeingCode, "lab_DyeingCode");
            this.lab_DyeingCode.Name = "lab_DyeingCode";
            // 
            // txt_VersionNum
            // 
            resources.ApplyResources(this.txt_VersionNum, "txt_VersionNum");
            this.txt_VersionNum.Name = "txt_VersionNum";
            // 
            // lab_CreateTime
            // 
            resources.ApplyResources(this.lab_CreateTime, "lab_CreateTime");
            this.lab_CreateTime.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lab_CreateTime.Name = "lab_CreateTime";
            // 
            // lab_CupNum
            // 
            resources.ApplyResources(this.lab_CupNum, "lab_CupNum");
            this.lab_CupNum.Name = "lab_CupNum";
            // 
            // lab_Operator
            // 
            resources.ApplyResources(this.lab_Operator, "lab_Operator");
            this.lab_Operator.Name = "lab_Operator";
            // 
            // lab_TotalWeight
            // 
            resources.ApplyResources(this.lab_TotalWeight, "lab_TotalWeight");
            this.lab_TotalWeight.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lab_TotalWeight.Name = "lab_TotalWeight";
            // 
            // lab_BathRatio
            // 
            resources.ApplyResources(this.lab_BathRatio, "lab_BathRatio");
            this.lab_BathRatio.Name = "lab_BathRatio";
            // 
            // lab_ClothWeight
            // 
            resources.ApplyResources(this.lab_ClothWeight, "lab_ClothWeight");
            this.lab_ClothWeight.Name = "lab_ClothWeight";
            // 
            // lab_Customer
            // 
            resources.ApplyResources(this.lab_Customer, "lab_Customer");
            this.lab_Customer.Name = "lab_Customer";
            // 
            // lab_ClothType
            // 
            resources.ApplyResources(this.lab_ClothType, "lab_ClothType");
            this.lab_ClothType.Name = "lab_ClothType";
            // 
            // lab_FormulaName
            // 
            resources.ApplyResources(this.lab_FormulaName, "lab_FormulaName");
            this.lab_FormulaName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_FormulaName.Name = "lab_FormulaName";
            // 
            // lab_FormulaCode
            // 
            resources.ApplyResources(this.lab_FormulaCode, "lab_FormulaCode");
            this.lab_FormulaCode.Name = "lab_FormulaCode";
            // 
            // Index
            // 
            this.Index.FillWeight = 77.02372F;
            resources.ApplyResources(this.Index, "Index");
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AssistantCode
            // 
            this.AssistantCode.FillWeight = 104.9278F;
            resources.ApplyResources(this.AssistantCode, "AssistantCode");
            this.AssistantCode.Name = "AssistantCode";
            this.AssistantCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FormulaDosage
            // 
            this.FormulaDosage.FillWeight = 75.44542F;
            resources.ApplyResources(this.FormulaDosage, "FormulaDosage");
            this.FormulaDosage.Name = "FormulaDosage";
            this.FormulaDosage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UnitOfAccount
            // 
            this.UnitOfAccount.FillWeight = 79.8297F;
            resources.ApplyResources(this.UnitOfAccount, "UnitOfAccount");
            this.UnitOfAccount.Name = "UnitOfAccount";
            this.UnitOfAccount.ReadOnly = true;
            this.UnitOfAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column1.FillWeight = 60.30895F;
            this.Column1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            // 
            // SettingConcentration
            // 
            this.SettingConcentration.FillWeight = 116.9112F;
            resources.ApplyResources(this.SettingConcentration, "SettingConcentration");
            this.SettingConcentration.Name = "SettingConcentration";
            this.SettingConcentration.ReadOnly = true;
            this.SettingConcentration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RealConcentration
            // 
            this.RealConcentration.FillWeight = 122.0618F;
            resources.ApplyResources(this.RealConcentration, "RealConcentration");
            this.RealConcentration.Name = "RealConcentration";
            this.RealConcentration.ReadOnly = true;
            this.RealConcentration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AssistantName
            // 
            this.AssistantName.FillWeight = 152.5202F;
            resources.ApplyResources(this.AssistantName, "AssistantName");
            this.AssistantName.Name = "AssistantName";
            this.AssistantName.ReadOnly = true;
            this.AssistantName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ObjectDropWeight
            // 
            this.ObjectDropWeight.FillWeight = 138.4942F;
            resources.ApplyResources(this.ObjectDropWeight, "ObjectDropWeight");
            this.ObjectDropWeight.Name = "ObjectDropWeight";
            this.ObjectDropWeight.ReadOnly = true;
            this.ObjectDropWeight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RealDropWeight
            // 
            this.RealDropWeight.FillWeight = 147.1373F;
            resources.ApplyResources(this.RealDropWeight, "RealDropWeight");
            this.RealDropWeight.Name = "RealDropWeight";
            this.RealDropWeight.ReadOnly = true;
            this.RealDropWeight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BottleSelection
            // 
            this.BottleSelection.FillWeight = 69.70769F;
            resources.ApplyResources(this.BottleSelection, "BottleSelection");
            this.BottleSelection.Name = "BottleSelection";
            this.BottleSelection.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn46
            // 
            this.dataGridViewTextBoxColumn46.FillWeight = 90.15134F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn46, "dataGridViewTextBoxColumn46");
            this.dataGridViewTextBoxColumn46.Name = "dataGridViewTextBoxColumn46";
            this.dataGridViewTextBoxColumn46.ReadOnly = true;
            this.dataGridViewTextBoxColumn46.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn47
            // 
            this.dataGridViewTextBoxColumn47.FillWeight = 103.5471F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn47, "dataGridViewTextBoxColumn47");
            this.dataGridViewTextBoxColumn47.Name = "dataGridViewTextBoxColumn47";
            this.dataGridViewTextBoxColumn47.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn48
            // 
            this.dataGridViewTextBoxColumn48.FillWeight = 74.45265F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn48, "dataGridViewTextBoxColumn48");
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            this.dataGridViewTextBoxColumn48.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn49
            // 
            this.dataGridViewTextBoxColumn49.FillWeight = 78.77927F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn49, "dataGridViewTextBoxColumn49");
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            this.dataGridViewTextBoxColumn49.ReadOnly = true;
            this.dataGridViewTextBoxColumn49.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn6
            // 
            this.dataGridViewComboBoxColumn6.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridViewComboBoxColumn6.FillWeight = 59.51537F;
            this.dataGridViewComboBoxColumn6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.dataGridViewComboBoxColumn6, "dataGridViewComboBoxColumn6");
            this.dataGridViewComboBoxColumn6.Name = "dataGridViewComboBoxColumn6";
            // 
            // dataGridViewTextBoxColumn50
            // 
            this.dataGridViewTextBoxColumn50.FillWeight = 115.3728F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn50, "dataGridViewTextBoxColumn50");
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            this.dataGridViewTextBoxColumn50.ReadOnly = true;
            this.dataGridViewTextBoxColumn50.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn51
            // 
            this.dataGridViewTextBoxColumn51.FillWeight = 120.4557F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn51, "dataGridViewTextBoxColumn51");
            this.dataGridViewTextBoxColumn51.Name = "dataGridViewTextBoxColumn51";
            this.dataGridViewTextBoxColumn51.ReadOnly = true;
            this.dataGridViewTextBoxColumn51.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn52
            // 
            this.dataGridViewTextBoxColumn52.FillWeight = 150.5132F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn52, "dataGridViewTextBoxColumn52");
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            this.dataGridViewTextBoxColumn52.ReadOnly = true;
            this.dataGridViewTextBoxColumn52.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn53
            // 
            this.dataGridViewTextBoxColumn53.FillWeight = 136.6719F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn53, "dataGridViewTextBoxColumn53");
            this.dataGridViewTextBoxColumn53.Name = "dataGridViewTextBoxColumn53";
            this.dataGridViewTextBoxColumn53.ReadOnly = true;
            this.dataGridViewTextBoxColumn53.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.FillWeight = 145.2012F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn54, "dataGridViewTextBoxColumn54");
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            this.dataGridViewTextBoxColumn54.ReadOnly = true;
            this.dataGridViewTextBoxColumn54.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn6
            // 
            this.dataGridViewCheckBoxColumn6.FillWeight = 69.7077F;
            resources.ApplyResources(this.dataGridViewCheckBoxColumn6, "dataGridViewCheckBoxColumn6");
            this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
            this.dataGridViewCheckBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.FillWeight = 90.15134F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn37, "dataGridViewTextBoxColumn37");
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            this.dataGridViewTextBoxColumn37.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.FillWeight = 103.5471F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn38, "dataGridViewTextBoxColumn38");
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.FillWeight = 74.45265F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn39, "dataGridViewTextBoxColumn39");
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.FillWeight = 78.77927F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn40, "dataGridViewTextBoxColumn40");
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.ReadOnly = true;
            this.dataGridViewTextBoxColumn40.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn5
            // 
            this.dataGridViewComboBoxColumn5.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridViewComboBoxColumn5.FillWeight = 59.51537F;
            this.dataGridViewComboBoxColumn5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.dataGridViewComboBoxColumn5, "dataGridViewComboBoxColumn5");
            this.dataGridViewComboBoxColumn5.Name = "dataGridViewComboBoxColumn5";
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.FillWeight = 115.3728F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn41, "dataGridViewTextBoxColumn41");
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.ReadOnly = true;
            this.dataGridViewTextBoxColumn41.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.FillWeight = 120.4557F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn42, "dataGridViewTextBoxColumn42");
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.ReadOnly = true;
            this.dataGridViewTextBoxColumn42.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.FillWeight = 150.5132F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn43, "dataGridViewTextBoxColumn43");
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.ReadOnly = true;
            this.dataGridViewTextBoxColumn43.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.FillWeight = 136.6719F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn44, "dataGridViewTextBoxColumn44");
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.ReadOnly = true;
            this.dataGridViewTextBoxColumn44.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn45
            // 
            this.dataGridViewTextBoxColumn45.FillWeight = 145.2012F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn45, "dataGridViewTextBoxColumn45");
            this.dataGridViewTextBoxColumn45.Name = "dataGridViewTextBoxColumn45";
            this.dataGridViewTextBoxColumn45.ReadOnly = true;
            this.dataGridViewTextBoxColumn45.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn5
            // 
            this.dataGridViewCheckBoxColumn5.FillWeight = 69.7077F;
            resources.ApplyResources(this.dataGridViewCheckBoxColumn5, "dataGridViewCheckBoxColumn5");
            this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
            this.dataGridViewCheckBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.FillWeight = 90.15134F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn28, "dataGridViewTextBoxColumn28");
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.FillWeight = 103.5471F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn29, "dataGridViewTextBoxColumn29");
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.FillWeight = 74.45265F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn30, "dataGridViewTextBoxColumn30");
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.FillWeight = 78.77927F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn31, "dataGridViewTextBoxColumn31");
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            this.dataGridViewTextBoxColumn31.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn4
            // 
            this.dataGridViewComboBoxColumn4.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridViewComboBoxColumn4.FillWeight = 59.51537F;
            this.dataGridViewComboBoxColumn4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.dataGridViewComboBoxColumn4, "dataGridViewComboBoxColumn4");
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.FillWeight = 115.3728F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn32, "dataGridViewTextBoxColumn32");
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.FillWeight = 120.4557F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn33, "dataGridViewTextBoxColumn33");
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.FillWeight = 150.5132F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn34, "dataGridViewTextBoxColumn34");
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.FillWeight = 136.6719F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn35, "dataGridViewTextBoxColumn35");
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            this.dataGridViewTextBoxColumn35.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.FillWeight = 145.2012F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn36, "dataGridViewTextBoxColumn36");
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn4
            // 
            this.dataGridViewCheckBoxColumn4.FillWeight = 69.7077F;
            resources.ApplyResources(this.dataGridViewCheckBoxColumn4, "dataGridViewCheckBoxColumn4");
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.FillWeight = 90.15134F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn19, "dataGridViewTextBoxColumn19");
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.FillWeight = 103.5471F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn20, "dataGridViewTextBoxColumn20");
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.FillWeight = 74.45265F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn21, "dataGridViewTextBoxColumn21");
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.FillWeight = 78.77927F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn22, "dataGridViewTextBoxColumn22");
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridViewComboBoxColumn3.FillWeight = 59.51537F;
            this.dataGridViewComboBoxColumn3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.dataGridViewComboBoxColumn3, "dataGridViewComboBoxColumn3");
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.FillWeight = 115.3728F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn23, "dataGridViewTextBoxColumn23");
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.FillWeight = 120.4557F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn24, "dataGridViewTextBoxColumn24");
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.FillWeight = 150.5132F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn25, "dataGridViewTextBoxColumn25");
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.FillWeight = 136.6719F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn26, "dataGridViewTextBoxColumn26");
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.FillWeight = 145.2012F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn27, "dataGridViewTextBoxColumn27");
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.FillWeight = 69.7077F;
            resources.ApplyResources(this.dataGridViewCheckBoxColumn3, "dataGridViewCheckBoxColumn3");
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.FillWeight = 86.95465F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.FillWeight = 103.8833F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.FillWeight = 74.69441F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.FillWeight = 79.03506F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridViewComboBoxColumn2.FillWeight = 59.70861F;
            this.dataGridViewComboBoxColumn2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.dataGridViewComboBoxColumn2, "dataGridViewComboBoxColumn2");
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.FillWeight = 115.7474F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.FillWeight = 120.8468F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn15, "dataGridViewTextBoxColumn15");
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.FillWeight = 151.002F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.FillWeight = 137.1156F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn17, "dataGridViewTextBoxColumn17");
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.FillWeight = 145.6727F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn18, "dataGridViewTextBoxColumn18");
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.FillWeight = 69.7077F;
            resources.ApplyResources(this.dataGridViewCheckBoxColumn2, "dataGridViewCheckBoxColumn2");
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 87.13462F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 104.0983F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 74.84899F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 79.19863F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridViewComboBoxColumn1.FillWeight = 59.8322F;
            this.dataGridViewComboBoxColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.dataGridViewComboBoxColumn1, "dataGridViewComboBoxColumn1");
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 115.9869F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 121.0969F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 151.3145F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.FillWeight = 137.3994F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.FillWeight = 145.9742F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FillWeight = 67.48329F;
            resources.ApplyResources(this.dataGridViewCheckBoxColumn1, "dataGridViewCheckBoxColumn1");
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // dgv_FormulaData
            // 
            this.dgv_FormulaData.AllowUserToDeleteRows = false;
            this.dgv_FormulaData.AllowUserToResizeColumns = false;
            this.dgv_FormulaData.AllowUserToResizeRows = false;
            this.dgv_FormulaData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_FormulaData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_FormulaData.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_FormulaData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_FormulaData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FormulaData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn55,
            this.dataGridViewTextBoxColumn56,
            this.dataGridViewTextBoxColumn57,
            this.dataGridViewTextBoxColumn58,
            this.dataGridViewComboBoxColumn7,
            this.dataGridViewTextBoxColumn59,
            this.dataGridViewTextBoxColumn60,
            this.dataGridViewTextBoxColumn61,
            this.dataGridViewTextBoxColumn62,
            this.dataGridViewTextBoxColumn63,
            this.dataGridViewCheckBoxColumn7});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_FormulaData.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_FormulaData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_FormulaData, "dgv_FormulaData");
            this.dgv_FormulaData.MultiSelect = false;
            this.dgv_FormulaData.Name = "dgv_FormulaData";
            this.dgv_FormulaData.RowHeadersVisible = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_FormulaData.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_FormulaData.RowTemplate.Height = 30;
            this.dgv_FormulaData.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn55
            // 
            this.dataGridViewTextBoxColumn55.FillWeight = 77.02372F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn55, "dataGridViewTextBoxColumn55");
            this.dataGridViewTextBoxColumn55.Name = "dataGridViewTextBoxColumn55";
            this.dataGridViewTextBoxColumn55.ReadOnly = true;
            this.dataGridViewTextBoxColumn55.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn56
            // 
            this.dataGridViewTextBoxColumn56.FillWeight = 104.9278F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn56, "dataGridViewTextBoxColumn56");
            this.dataGridViewTextBoxColumn56.Name = "dataGridViewTextBoxColumn56";
            this.dataGridViewTextBoxColumn56.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn57
            // 
            this.dataGridViewTextBoxColumn57.FillWeight = 75.44542F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn57, "dataGridViewTextBoxColumn57");
            this.dataGridViewTextBoxColumn57.Name = "dataGridViewTextBoxColumn57";
            this.dataGridViewTextBoxColumn57.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn58
            // 
            this.dataGridViewTextBoxColumn58.FillWeight = 79.8297F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn58, "dataGridViewTextBoxColumn58");
            this.dataGridViewTextBoxColumn58.Name = "dataGridViewTextBoxColumn58";
            this.dataGridViewTextBoxColumn58.ReadOnly = true;
            this.dataGridViewTextBoxColumn58.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn7
            // 
            this.dataGridViewComboBoxColumn7.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridViewComboBoxColumn7.FillWeight = 60.30895F;
            this.dataGridViewComboBoxColumn7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.dataGridViewComboBoxColumn7, "dataGridViewComboBoxColumn7");
            this.dataGridViewComboBoxColumn7.Name = "dataGridViewComboBoxColumn7";
            // 
            // dataGridViewTextBoxColumn59
            // 
            this.dataGridViewTextBoxColumn59.FillWeight = 116.9112F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn59, "dataGridViewTextBoxColumn59");
            this.dataGridViewTextBoxColumn59.Name = "dataGridViewTextBoxColumn59";
            this.dataGridViewTextBoxColumn59.ReadOnly = true;
            this.dataGridViewTextBoxColumn59.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn60
            // 
            this.dataGridViewTextBoxColumn60.FillWeight = 122.0618F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn60, "dataGridViewTextBoxColumn60");
            this.dataGridViewTextBoxColumn60.Name = "dataGridViewTextBoxColumn60";
            this.dataGridViewTextBoxColumn60.ReadOnly = true;
            this.dataGridViewTextBoxColumn60.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn61
            // 
            this.dataGridViewTextBoxColumn61.FillWeight = 152.5202F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn61, "dataGridViewTextBoxColumn61");
            this.dataGridViewTextBoxColumn61.Name = "dataGridViewTextBoxColumn61";
            this.dataGridViewTextBoxColumn61.ReadOnly = true;
            this.dataGridViewTextBoxColumn61.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn62
            // 
            this.dataGridViewTextBoxColumn62.FillWeight = 138.4942F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn62, "dataGridViewTextBoxColumn62");
            this.dataGridViewTextBoxColumn62.Name = "dataGridViewTextBoxColumn62";
            this.dataGridViewTextBoxColumn62.ReadOnly = true;
            this.dataGridViewTextBoxColumn62.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn63
            // 
            this.dataGridViewTextBoxColumn63.FillWeight = 147.1373F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn63, "dataGridViewTextBoxColumn63");
            this.dataGridViewTextBoxColumn63.Name = "dataGridViewTextBoxColumn63";
            this.dataGridViewTextBoxColumn63.ReadOnly = true;
            this.dataGridViewTextBoxColumn63.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.FillWeight = 69.70769F;
            resources.ApplyResources(this.dataGridViewCheckBoxColumn7, "dataGridViewCheckBoxColumn7");
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            this.dataGridViewCheckBoxColumn7.ReadOnly = true;
            // 
            // AllFormulas
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grp_BatchData);
            this.Controls.Add(this.grp_FormulaData);
            this.Name = "AllFormulas";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Details)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dyeing)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.grp_BatchData.ResumeLayout(false);
            this.grp_BatchData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BatchData)).EndInit();
            this.grp_FormulaData.ResumeLayout(false);
            this.grp_FormulaData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private DataGridView dgv_Details;
        private DataGridView dgv_Dyeing;
        private GroupBox groupBox3;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private TextBox txt_CupNum;
        private TextBox txt_ClothType;
        private TextBox txt_CreateTime;
        private TextBox txt_TotalWeight;
       
        private TextBox txt_BathRatio;
        private TextBox txt_ClothWeight;
        private CheckBox chk_AddWaterChoose;
        private TextBox txt_Customer;
        private TextBox txt_FormulaName;
        private TextBox txt_State;
        private TextBox txt_FormulaCode;
        private GroupBox grp_BatchData;
        private DataGridView dgv_BatchData;
        private GroupBox grp_FormulaData;
        private TextBox txt_AnhydrationWR;
        private Label lab_AnhydrationWR;
        private TextBox txt_Non_AnhydrationWR;
        private Label lab_txt_Non_AnhydrationWR;
        private Label lab_DyeingCode;
       
        private TextBox txt_VersionNum;
        private Label lab_CreateTime;
        private Label lab_CupNum;
        private Label lab_Operator;
        private Label lab_TotalWeight;
        private Label lab_BathRatio;
        private Label lab_ClothWeight;
        private Label lab_Customer;
        private Label lab_ClothType;
        private Label lab_FormulaName;
        private Label lab_FormulaCode;
        
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.Timer tmr;
        private ComboBox txt_Record_Operator;
        private DateTimePicker dt_Record_End;
        private RadioButton rdo_Record_Now;
        private RadioButton rdo_Record_condition;
        private Button btn_Record_Select;
        private DateTimePicker dt_Record_Start;
        private RadioButton rdo_Record_All;
        private Label label5;
        private TextBox txt_Record_Code;
        private Label label4;
        private Label label3;
        private Label label2;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Index;
        private DataGridViewTextBoxColumn AssistantCode;
        private DataGridViewTextBoxColumn FormulaDosage;
        private DataGridViewTextBoxColumn UnitOfAccount;
        private DataGridViewComboBoxColumn Column1;
        private DataGridViewTextBoxColumn SettingConcentration;
        private DataGridViewTextBoxColumn RealConcentration;
        private DataGridViewTextBoxColumn AssistantName;
        private DataGridViewTextBoxColumn ObjectDropWeight;
        private DataGridViewTextBoxColumn RealDropWeight;
        private DataGridViewCheckBoxColumn BottleSelection;
        private FADM_Object.MyDataGridView dgv_FormulaData;
        private ComboBox txt_DyeingCode;
        private ComboBox txt_Operator;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn58;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn59;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn60;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn61;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn62;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private Panel panel1;
    }
}
