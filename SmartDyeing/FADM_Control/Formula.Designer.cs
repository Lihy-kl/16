using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class Formula
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formula));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grp_BatchData = new System.Windows.Forms.GroupBox();
            this.dgv_BatchData = new System.Windows.Forms.DataGridView();
            this.btn_FormulaCodeAdd = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_BatchAdd = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.Btn_WaitList = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_NotDrip = new System.Windows.Forms.Button();
            this.grp_FormulaBrowse = new System.Windows.Forms.GroupBox();
            this.txt_Browse_Operator = new System.Windows.Forms.ComboBox();
            this.rdo_Browse_condition = new System.Windows.Forms.RadioButton();
            this.dgv_FormulaBrowse = new System.Windows.Forms.DataGridView();
            this.dt_Browse_End = new System.Windows.Forms.DateTimePicker();
            this.dt_Browse_Start = new System.Windows.Forms.DateTimePicker();
            this.txt_Browse_Code = new System.Windows.Forms.TextBox();
            this.btn_Browse_Delete = new System.Windows.Forms.Button();
            this.btn_Browse_Select = new System.Windows.Forms.Button();
            this.rdo_Browse_NoDrop = new System.Windows.Forms.RadioButton();
            this.rdo_Browse_All = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.grp_FormulaData = new System.Windows.Forms.GroupBox();
            this.chk_Auto = new System.Windows.Forms.CheckBox();
            this.txt_FormulaName = new System.Windows.Forms.TextBox();
            this.txt_ClothNum = new System.Windows.Forms.TextBox();
            this.lab_FormulaName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lab_CupNum = new System.Windows.Forms.Label();
            this.txt_CupNum = new System.Windows.Forms.TextBox();
            this.txt_DyeingCode = new System.Windows.Forms.ComboBox();
            this.lab_DyeingCode = new System.Windows.Forms.Label();
            this.txt_Operator = new System.Windows.Forms.TextBox();
            this.txt_FormulaGroup = new System.Windows.Forms.ComboBox();
            this.lab_FormulaGroup = new System.Windows.Forms.Label();
            this.txt_AnhydrationWR = new System.Windows.Forms.TextBox();
            this.lab_AnhydrationWR = new System.Windows.Forms.Label();
            this.txt_Non_AnhydrationWR = new System.Windows.Forms.TextBox();
            this.lab_Non_AnhydrationWR = new System.Windows.Forms.Label();
            this.txt_ClothType = new System.Windows.Forms.TextBox();
            this.txt_CreateTime = new System.Windows.Forms.TextBox();
            this.txt_TotalWeight = new System.Windows.Forms.TextBox();
            this.txt_BathRatio = new System.Windows.Forms.TextBox();
            this.txt_ClothWeight = new System.Windows.Forms.TextBox();
            this.chk_AddWaterChoose = new System.Windows.Forms.CheckBox();
            this.txt_Customer = new System.Windows.Forms.TextBox();
            this.txt_State = new System.Windows.Forms.TextBox();
            this.txt_VersionNum = new System.Windows.Forms.TextBox();
            this.txt_FormulaCode = new System.Windows.Forms.TextBox();
            this.lab_CreateTime = new System.Windows.Forms.Label();
            this.lab_Operator = new System.Windows.Forms.Label();
            this.lab_TotalWeight = new System.Windows.Forms.Label();
            this.lab_BathRatio = new System.Windows.Forms.Label();
            this.lab_ClothWeight = new System.Windows.Forms.Label();
            this.lab_Customer = new System.Windows.Forms.Label();
            this.lab_ClothType = new System.Windows.Forms.Label();
            this.lab_FormulaCode = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_pre = new System.Windows.Forms.Button();
            this.btn_upd = new System.Windows.Forms.Button();
            this.dgv_FormulaData = new SmartDyeing.FADM_Object.MyDataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssistantCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssistantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormulaDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SettingConcentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealConcentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectDropWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealDropWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BottleSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grp_BatchData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BatchData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.grp_FormulaBrowse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaBrowse)).BeginInit();
            this.panel1.SuspendLayout();
            this.grp_FormulaData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaData)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_BatchData
            // 
            this.grp_BatchData.Controls.Add(this.dgv_BatchData);
            resources.ApplyResources(this.grp_BatchData, "grp_BatchData");
            this.grp_BatchData.Name = "grp_BatchData";
            this.grp_BatchData.TabStop = false;
            // 
            // dgv_BatchData
            // 
            this.dgv_BatchData.AllowUserToAddRows = false;
            this.dgv_BatchData.AllowUserToOrderColumns = true;
            this.dgv_BatchData.AllowUserToResizeColumns = false;
            this.dgv_BatchData.AllowUserToResizeRows = false;
            this.dgv_BatchData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_BatchData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_BatchData, "dgv_BatchData");
            this.dgv_BatchData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_BatchData.Name = "dgv_BatchData";
            this.dgv_BatchData.ReadOnly = true;
            this.dgv_BatchData.RowHeadersVisible = false;
            this.dgv_BatchData.RowTemplate.Height = 23;
            this.dgv_BatchData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_BatchData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_BatchData_CellMouseDown);
            this.dgv_BatchData.CurrentCellChanged += new System.EventHandler(this.dgv_BatchData_CurrentCellChanged);
            this.dgv_BatchData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_BatchData_DataBindingComplete);
            this.dgv_BatchData.Enter += new System.EventHandler(this.dgv_BatchData_Enter);
            this.dgv_BatchData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_BatchData_KeyDown);
            // 
            // btn_FormulaCodeAdd
            // 
            resources.ApplyResources(this.btn_FormulaCodeAdd, "btn_FormulaCodeAdd");
            this.btn_FormulaCodeAdd.Name = "btn_FormulaCodeAdd";
            this.btn_FormulaCodeAdd.UseVisualStyleBackColor = true;
            this.btn_FormulaCodeAdd.Click += new System.EventHandler(this.btn_FormulaCodeAdd_Click);
            // 
            // btn_Save
            // 
            resources.ApplyResources(this.btn_Save, "btn_Save");
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            this.btn_Save.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Save_KeyDown);
            // 
            // btn_BatchAdd
            // 
            resources.ApplyResources(this.btn_BatchAdd, "btn_BatchAdd");
            this.btn_BatchAdd.Name = "btn_BatchAdd";
            this.btn_BatchAdd.UseVisualStyleBackColor = true;
            this.btn_BatchAdd.Click += new System.EventHandler(this.btn_BatchAdd_Click);
            // 
            // btn_Start
            // 
            resources.ApplyResources(this.btn_Start, "btn_Start");
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            resources.ApplyResources(this.btn_Stop, "btn_Stop");
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // tmr
            // 
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // Btn_WaitList
            // 
            resources.ApplyResources(this.Btn_WaitList, "Btn_WaitList");
            this.Btn_WaitList.Name = "Btn_WaitList";
            this.Btn_WaitList.UseVisualStyleBackColor = true;
            this.Btn_WaitList.Click += new System.EventHandler(this.Btn_WaitList_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // tsm_Delete
            // 
            this.tsm_Delete.Name = "tsm_Delete";
            resources.ApplyResources(this.tsm_Delete, "tsm_Delete");
            this.tsm_Delete.Click += new System.EventHandler(this.tsm_Delete_Click);
            // 
            // btn_NotDrip
            // 
            resources.ApplyResources(this.btn_NotDrip, "btn_NotDrip");
            this.btn_NotDrip.Name = "btn_NotDrip";
            this.btn_NotDrip.UseVisualStyleBackColor = true;
            this.btn_NotDrip.Click += new System.EventHandler(this.btn_NotDrip_Click);
            // 
            // grp_FormulaBrowse
            // 
            this.grp_FormulaBrowse.Controls.Add(this.txt_Browse_Operator);
            this.grp_FormulaBrowse.Controls.Add(this.rdo_Browse_condition);
            this.grp_FormulaBrowse.Controls.Add(this.dgv_FormulaBrowse);
            this.grp_FormulaBrowse.Controls.Add(this.dt_Browse_End);
            this.grp_FormulaBrowse.Controls.Add(this.dt_Browse_Start);
            this.grp_FormulaBrowse.Controls.Add(this.txt_Browse_Code);
            this.grp_FormulaBrowse.Controls.Add(this.btn_Browse_Delete);
            this.grp_FormulaBrowse.Controls.Add(this.btn_Browse_Select);
            this.grp_FormulaBrowse.Controls.Add(this.rdo_Browse_NoDrop);
            this.grp_FormulaBrowse.Controls.Add(this.rdo_Browse_All);
            this.grp_FormulaBrowse.Controls.Add(this.label14);
            this.grp_FormulaBrowse.Controls.Add(this.label13);
            this.grp_FormulaBrowse.Controls.Add(this.label12);
            this.grp_FormulaBrowse.Controls.Add(this.label11);
            resources.ApplyResources(this.grp_FormulaBrowse, "grp_FormulaBrowse");
            this.grp_FormulaBrowse.Name = "grp_FormulaBrowse";
            this.grp_FormulaBrowse.TabStop = false;
            // 
            // txt_Browse_Operator
            // 
            resources.ApplyResources(this.txt_Browse_Operator, "txt_Browse_Operator");
            this.txt_Browse_Operator.FormattingEnabled = true;
            this.txt_Browse_Operator.Name = "txt_Browse_Operator";
            // 
            // rdo_Browse_condition
            // 
            resources.ApplyResources(this.rdo_Browse_condition, "rdo_Browse_condition");
            this.rdo_Browse_condition.Name = "rdo_Browse_condition";
            this.rdo_Browse_condition.UseVisualStyleBackColor = true;
            this.rdo_Browse_condition.CheckedChanged += new System.EventHandler(this.rdo_Browse_condition_CheckedChanged);
            // 
            // dgv_FormulaBrowse
            // 
            this.dgv_FormulaBrowse.AllowUserToAddRows = false;
            this.dgv_FormulaBrowse.AllowUserToDeleteRows = false;
            this.dgv_FormulaBrowse.AllowUserToResizeColumns = false;
            this.dgv_FormulaBrowse.AllowUserToResizeRows = false;
            this.dgv_FormulaBrowse.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_FormulaBrowse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_FormulaBrowse, "dgv_FormulaBrowse");
            this.dgv_FormulaBrowse.Name = "dgv_FormulaBrowse";
            this.dgv_FormulaBrowse.ReadOnly = true;
            this.dgv_FormulaBrowse.RowHeadersVisible = false;
            this.dgv_FormulaBrowse.RowTemplate.Height = 23;
            this.dgv_FormulaBrowse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_FormulaBrowse.CurrentCellChanged += new System.EventHandler(this.dgv_FormulaBrowse_CurrentCellChanged);
            this.dgv_FormulaBrowse.Enter += new System.EventHandler(this.dgv_FormulaBrowse_Enter_1);
            // 
            // dt_Browse_End
            // 
            resources.ApplyResources(this.dt_Browse_End, "dt_Browse_End");
            this.dt_Browse_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Browse_End.Name = "dt_Browse_End";
            this.dt_Browse_End.ShowUpDown = true;
            // 
            // dt_Browse_Start
            // 
            resources.ApplyResources(this.dt_Browse_Start, "dt_Browse_Start");
            this.dt_Browse_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Browse_Start.Name = "dt_Browse_Start";
            this.dt_Browse_Start.ShowUpDown = true;
            this.dt_Browse_Start.Value = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            // 
            // txt_Browse_Code
            // 
            resources.ApplyResources(this.txt_Browse_Code, "txt_Browse_Code");
            this.txt_Browse_Code.Name = "txt_Browse_Code";
            // 
            // btn_Browse_Delete
            // 
            resources.ApplyResources(this.btn_Browse_Delete, "btn_Browse_Delete");
            this.btn_Browse_Delete.Name = "btn_Browse_Delete";
            this.btn_Browse_Delete.UseVisualStyleBackColor = true;
            this.btn_Browse_Delete.Click += new System.EventHandler(this.btn_Browse_Delete_Click);
            // 
            // btn_Browse_Select
            // 
            resources.ApplyResources(this.btn_Browse_Select, "btn_Browse_Select");
            this.btn_Browse_Select.Name = "btn_Browse_Select";
            this.btn_Browse_Select.UseVisualStyleBackColor = true;
            this.btn_Browse_Select.Click += new System.EventHandler(this.btn_Browse_Select_Click);
            // 
            // rdo_Browse_NoDrop
            // 
            resources.ApplyResources(this.rdo_Browse_NoDrop, "rdo_Browse_NoDrop");
            this.rdo_Browse_NoDrop.Checked = true;
            this.rdo_Browse_NoDrop.Name = "rdo_Browse_NoDrop";
            this.rdo_Browse_NoDrop.TabStop = true;
            this.rdo_Browse_NoDrop.UseVisualStyleBackColor = true;
            this.rdo_Browse_NoDrop.CheckedChanged += new System.EventHandler(this.rdo_Browse_NoDrop_CheckedChanged);
            // 
            // rdo_Browse_All
            // 
            resources.ApplyResources(this.rdo_Browse_All, "rdo_Browse_All");
            this.rdo_Browse_All.Name = "rdo_Browse_All";
            this.rdo_Browse_All.UseVisualStyleBackColor = true;
            this.rdo_Browse_All.CheckedChanged += new System.EventHandler(this.rdo_Browse_All_CheckedChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grp_FormulaData);
            this.panel1.Controls.Add(this.panel2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // grp_FormulaData
            // 
            this.grp_FormulaData.Controls.Add(this.chk_Auto);
            this.grp_FormulaData.Controls.Add(this.txt_FormulaName);
            this.grp_FormulaData.Controls.Add(this.txt_ClothNum);
            this.grp_FormulaData.Controls.Add(this.lab_FormulaName);
            this.grp_FormulaData.Controls.Add(this.label1);
            this.grp_FormulaData.Controls.Add(this.lab_CupNum);
            this.grp_FormulaData.Controls.Add(this.txt_CupNum);
            this.grp_FormulaData.Controls.Add(this.txt_DyeingCode);
            this.grp_FormulaData.Controls.Add(this.lab_DyeingCode);
            this.grp_FormulaData.Controls.Add(this.txt_Operator);
            this.grp_FormulaData.Controls.Add(this.txt_FormulaGroup);
            this.grp_FormulaData.Controls.Add(this.lab_FormulaGroup);
            this.grp_FormulaData.Controls.Add(this.txt_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.txt_Non_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_Non_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.dgv_FormulaData);
            this.grp_FormulaData.Controls.Add(this.txt_ClothType);
            this.grp_FormulaData.Controls.Add(this.txt_CreateTime);
            this.grp_FormulaData.Controls.Add(this.txt_TotalWeight);
            this.grp_FormulaData.Controls.Add(this.txt_BathRatio);
            this.grp_FormulaData.Controls.Add(this.txt_ClothWeight);
            this.grp_FormulaData.Controls.Add(this.chk_AddWaterChoose);
            this.grp_FormulaData.Controls.Add(this.txt_Customer);
            this.grp_FormulaData.Controls.Add(this.txt_State);
            this.grp_FormulaData.Controls.Add(this.txt_VersionNum);
            this.grp_FormulaData.Controls.Add(this.txt_FormulaCode);
            this.grp_FormulaData.Controls.Add(this.lab_CreateTime);
            this.grp_FormulaData.Controls.Add(this.lab_Operator);
            this.grp_FormulaData.Controls.Add(this.lab_TotalWeight);
            this.grp_FormulaData.Controls.Add(this.lab_BathRatio);
            this.grp_FormulaData.Controls.Add(this.lab_ClothWeight);
            this.grp_FormulaData.Controls.Add(this.lab_Customer);
            this.grp_FormulaData.Controls.Add(this.lab_ClothType);
            this.grp_FormulaData.Controls.Add(this.lab_FormulaCode);
            resources.ApplyResources(this.grp_FormulaData, "grp_FormulaData");
            this.grp_FormulaData.Name = "grp_FormulaData";
            this.grp_FormulaData.TabStop = false;
            // 
            // chk_Auto
            // 
            resources.ApplyResources(this.chk_Auto, "chk_Auto");
            this.chk_Auto.Name = "chk_Auto";
            this.chk_Auto.UseVisualStyleBackColor = true;
            this.chk_Auto.Enter += new System.EventHandler(this.chk_Auto_Enter);
            this.chk_Auto.Leave += new System.EventHandler(this.chk_Auto_Leave);
            // 
            // txt_FormulaName
            // 
            resources.ApplyResources(this.txt_FormulaName, "txt_FormulaName");
            this.txt_FormulaName.Name = "txt_FormulaName";
            // 
            // txt_ClothNum
            // 
            resources.ApplyResources(this.txt_ClothNum, "txt_ClothNum");
            this.txt_ClothNum.Name = "txt_ClothNum";
            // 
            // lab_FormulaName
            // 
            resources.ApplyResources(this.lab_FormulaName, "lab_FormulaName");
            this.lab_FormulaName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_FormulaName.Name = "lab_FormulaName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lab_CupNum
            // 
            resources.ApplyResources(this.lab_CupNum, "lab_CupNum");
            this.lab_CupNum.Name = "lab_CupNum";
            // 
            // txt_CupNum
            // 
            resources.ApplyResources(this.txt_CupNum, "txt_CupNum");
            this.txt_CupNum.Name = "txt_CupNum";
            this.txt_CupNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_CupNum_KeyPress);
            this.txt_CupNum.Leave += new System.EventHandler(this.txt_CupNum_Leave);
            // 
            // txt_DyeingCode
            // 
            resources.ApplyResources(this.txt_DyeingCode, "txt_DyeingCode");
            this.txt_DyeingCode.FormattingEnabled = true;
            this.txt_DyeingCode.Name = "txt_DyeingCode";
            this.txt_DyeingCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_DyeingCode__KeyUp);
            // 
            // lab_DyeingCode
            // 
            resources.ApplyResources(this.lab_DyeingCode, "lab_DyeingCode");
            this.lab_DyeingCode.Name = "lab_DyeingCode";
            // 
            // txt_Operator
            // 
            resources.ApplyResources(this.txt_Operator, "txt_Operator");
            this.txt_Operator.Name = "txt_Operator";
            // 
            // txt_FormulaGroup
            // 
            resources.ApplyResources(this.txt_FormulaGroup, "txt_FormulaGroup");
            this.txt_FormulaGroup.FormattingEnabled = true;
            this.txt_FormulaGroup.Name = "txt_FormulaGroup";
            this.txt_FormulaGroup.SelectedIndexChanged += new System.EventHandler(this.txt_FormulaGroup_SelectedIndexChanged);
            this.txt_FormulaGroup.TextUpdate += new System.EventHandler(this.txt_FormulaGroup_TextUpdate);
            this.txt_FormulaGroup.Leave += new System.EventHandler(this.txt_FormulaGroup_Leave);
            // 
            // lab_FormulaGroup
            // 
            resources.ApplyResources(this.lab_FormulaGroup, "lab_FormulaGroup");
            this.lab_FormulaGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_FormulaGroup.Name = "lab_FormulaGroup";
            // 
            // txt_AnhydrationWR
            // 
            resources.ApplyResources(this.txt_AnhydrationWR, "txt_AnhydrationWR");
            this.txt_AnhydrationWR.Name = "txt_AnhydrationWR";
            this.txt_AnhydrationWR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_AnhydrationWR_KeyPress);
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
            this.txt_Non_AnhydrationWR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Non_AnhydrationWR_KeyPress);
            // 
            // lab_Non_AnhydrationWR
            // 
            resources.ApplyResources(this.lab_Non_AnhydrationWR, "lab_Non_AnhydrationWR");
            this.lab_Non_AnhydrationWR.Name = "lab_Non_AnhydrationWR";
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
            this.txt_BathRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_BathRatio_KeyPress);
            this.txt_BathRatio.Leave += new System.EventHandler(this.txt_BathRatio_Leave);
            // 
            // txt_ClothWeight
            // 
            resources.ApplyResources(this.txt_ClothWeight, "txt_ClothWeight");
            this.txt_ClothWeight.Name = "txt_ClothWeight";
            this.txt_ClothWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ClothWeight_KeyPress);
            this.txt_ClothWeight.Leave += new System.EventHandler(this.txt_ClothWeight_Leave);
            // 
            // chk_AddWaterChoose
            // 
            resources.ApplyResources(this.chk_AddWaterChoose, "chk_AddWaterChoose");
            this.chk_AddWaterChoose.BackColor = System.Drawing.SystemColors.Control;
            this.chk_AddWaterChoose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chk_AddWaterChoose.Name = "chk_AddWaterChoose";
            this.chk_AddWaterChoose.UseVisualStyleBackColor = false;
            this.chk_AddWaterChoose.Enter += new System.EventHandler(this.chk_AddWaterChoose_Enter);
            this.chk_AddWaterChoose.Leave += new System.EventHandler(this.chk_AddWaterChoose_Leave);
            // 
            // txt_Customer
            // 
            resources.ApplyResources(this.txt_Customer, "txt_Customer");
            this.txt_Customer.Name = "txt_Customer";
            // 
            // txt_State
            // 
            resources.ApplyResources(this.txt_State, "txt_State");
            this.txt_State.Name = "txt_State";
            // 
            // txt_VersionNum
            // 
            resources.ApplyResources(this.txt_VersionNum, "txt_VersionNum");
            this.txt_VersionNum.Name = "txt_VersionNum";
            // 
            // txt_FormulaCode
            // 
            resources.ApplyResources(this.txt_FormulaCode, "txt_FormulaCode");
            this.txt_FormulaCode.Name = "txt_FormulaCode";
            // 
            // lab_CreateTime
            // 
            resources.ApplyResources(this.lab_CreateTime, "lab_CreateTime");
            this.lab_CreateTime.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lab_CreateTime.Name = "lab_CreateTime";
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
            // lab_FormulaCode
            // 
            resources.ApplyResources(this.lab_FormulaCode, "lab_FormulaCode");
            this.lab_FormulaCode.Name = "lab_FormulaCode";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // btn_pre
            // 
            resources.ApplyResources(this.btn_pre, "btn_pre");
            this.btn_pre.Name = "btn_pre";
            this.btn_pre.UseVisualStyleBackColor = true;
            this.btn_pre.Click += new System.EventHandler(this.btn_pre_Click);
            // 
            // btn_upd
            // 
            resources.ApplyResources(this.btn_upd, "btn_upd");
            this.btn_upd.Name = "btn_upd";
            this.btn_upd.UseVisualStyleBackColor = true;
            this.btn_upd.Click += new System.EventHandler(this.btn_upd_Click);
            // 
            // dgv_FormulaData
            // 
            this.dgv_FormulaData.AllowUserToDeleteRows = false;
            this.dgv_FormulaData.AllowUserToResizeColumns = false;
            this.dgv_FormulaData.AllowUserToResizeRows = false;
            this.dgv_FormulaData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_FormulaData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_FormulaData.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_FormulaData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_FormulaData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FormulaData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.AssistantCode,
            this.AssistantName,
            this.FormulaDosage,
            this.UnitOfAccount,
            this.Column1,
            this.SettingConcentration,
            this.RealConcentration,
            this.ObjectDropWeight,
            this.RealDropWeight,
            this.BottleSelection});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_FormulaData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_FormulaData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_FormulaData, "dgv_FormulaData");
            this.dgv_FormulaData.MultiSelect = false;
            this.dgv_FormulaData.Name = "dgv_FormulaData";
            this.dgv_FormulaData.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_FormulaData.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_FormulaData.RowTemplate.Height = 30;
            this.dgv_FormulaData.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_FormulaData.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_FormulaData_EditingControlShowing);
            this.dgv_FormulaData.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_FormulaData_RowLeave);
            this.dgv_FormulaData.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_FormulaData_RowsAdded);
            this.dgv_FormulaData.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgv_FormulaData_RowsRemoved);
            this.dgv_FormulaData.SelectionChanged += new System.EventHandler(this.dgv_FormulaData_SelectionChanged);
            this.dgv_FormulaData.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgv_FormulaData_UserAddedRow);
            this.dgv_FormulaData.Leave += new System.EventHandler(this.dgv_FormulaData_Leave);
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
            // AssistantName
            // 
            this.AssistantName.FillWeight = 152.5202F;
            resources.ApplyResources(this.AssistantName, "AssistantName");
            this.AssistantName.Name = "AssistantName";
            this.AssistantName.ReadOnly = true;
            this.AssistantName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.UnitOfAccount.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.UnitOfAccount.FillWeight = 79.8297F;
            this.UnitOfAccount.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.UnitOfAccount, "UnitOfAccount");
            this.UnitOfAccount.Name = "UnitOfAccount";
            this.UnitOfAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            // Formula
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btn_upd);
            this.Controls.Add(this.Btn_WaitList);
            this.Controls.Add(this.btn_FormulaCodeAdd);
            this.Controls.Add(this.btn_NotDrip);
            this.Controls.Add(this.btn_pre);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_BatchAdd);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grp_FormulaBrowse);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.grp_BatchData);
            this.Name = "Formula";
            this.ShowIcon = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Formula_FormClosed);
            this.Load += new System.EventHandler(this.Formula_Load);
            this.LocationChanged += new System.EventHandler(this.Formula_LocationChanged);
            this.Leave += new System.EventHandler(this.Formula_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Formula_MouseDown);
            this.grp_BatchData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BatchData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.grp_FormulaBrowse.ResumeLayout(false);
            this.grp_FormulaBrowse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaBrowse)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grp_FormulaData.ResumeLayout(false);
            this.grp_FormulaData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grp_BatchData;
        private Button btn_Stop;
        private Button btn_FormulaCodeAdd;
        private Button btn_BatchAdd;
        public Button btn_Save;
        private DataGridView dgv_BatchData;
        private System.Windows.Forms.Timer tmr;
        private Button Btn_WaitList;
        public Button btn_Start;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem tsm_Delete;
        public Button btn_NotDrip;
        private GroupBox grp_FormulaBrowse;
        private ComboBox txt_Browse_Operator;
        private RadioButton rdo_Browse_condition;
        private DataGridView dgv_FormulaBrowse;
        private DateTimePicker dt_Browse_End;
        private DateTimePicker dt_Browse_Start;
        private TextBox txt_Browse_Code;
        private Button btn_Browse_Delete;
        private Button btn_Browse_Select;
        private RadioButton rdo_Browse_NoDrop;
        private RadioButton rdo_Browse_All;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private FlowLayoutPanel panel1;
        private FlowLayoutPanel panel2;
        public Button btn_pre;
        public GroupBox grp_FormulaData;
        public FADM_Object.MyDataGridView dgv_FormulaData;
        private DataGridViewTextBoxColumn Index;
        private DataGridViewTextBoxColumn AssistantCode;
        private DataGridViewTextBoxColumn AssistantName;
        private DataGridViewTextBoxColumn FormulaDosage;
        private DataGridViewComboBoxColumn UnitOfAccount;
        private DataGridViewComboBoxColumn Column1;
        private DataGridViewTextBoxColumn SettingConcentration;
        private DataGridViewTextBoxColumn RealConcentration;
        private DataGridViewTextBoxColumn ObjectDropWeight;
        private DataGridViewTextBoxColumn RealDropWeight;
        private DataGridViewCheckBoxColumn BottleSelection;
        public Button btn_upd;
        private ComboBox txt_DyeingCode;
        private Label lab_DyeingCode;
        private TextBox txt_Operator;
        private ComboBox txt_FormulaGroup;
        private Label lab_FormulaGroup;
        private TextBox txt_AnhydrationWR;
        private Label lab_AnhydrationWR;
        private TextBox txt_Non_AnhydrationWR;
        private Label lab_Non_AnhydrationWR;
        public TextBox txt_CupNum;
        private TextBox txt_ClothType;
        private TextBox txt_CreateTime;
        private TextBox txt_TotalWeight;
        private TextBox txt_BathRatio;
        private TextBox txt_ClothWeight;
        private CheckBox chk_AddWaterChoose;
        private TextBox txt_Customer;
        private TextBox txt_FormulaName;
        private TextBox txt_State;
        private TextBox txt_VersionNum;
        private TextBox txt_FormulaCode;
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
        public TextBox txt_ClothNum;
        private Label label1;
        private CheckBox chk_Auto;
    }
}
