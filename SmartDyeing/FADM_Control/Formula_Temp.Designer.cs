using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class Formula_Temp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formula_Temp));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grp_BatchData = new System.Windows.Forms.GroupBox();
            this.dgv_BatchData = new System.Windows.Forms.DataGridView();
            this.grp_FormulaData = new System.Windows.Forms.GroupBox();
            this.txt_Operator = new System.Windows.Forms.TextBox();
            this.txt_FormulaGroup = new System.Windows.Forms.ComboBox();
            this.lab_FormulaGroup = new System.Windows.Forms.Label();
            this.txt_AnhydrationWR = new System.Windows.Forms.TextBox();
            this.lab_AnhydrationWR = new System.Windows.Forms.Label();
            this.txt_Non_AnhydrationWR = new System.Windows.Forms.TextBox();
            this.lab_Non_AnhydrationWR = new System.Windows.Forms.Label();
            this.txt_DyeingCode = new System.Windows.Forms.ComboBox();
            this.lab_DyeingCode = new System.Windows.Forms.Label();
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
            this.txt_VersionNum = new System.Windows.Forms.TextBox();
            this.txt_FormulaCode = new System.Windows.Forms.TextBox();
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
            this.btn_FormulaCodeAdd = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_BatchAdd = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.grp_Dye = new System.Windows.Forms.GroupBox();
            this.grp_Handle = new System.Windows.Forms.GroupBox();
            this.txt_Handle_Rev5 = new System.Windows.Forms.TextBox();
            this.lab_Handle_Rev5 = new System.Windows.Forms.Label();
            this.txt_Handle_Rev4 = new System.Windows.Forms.TextBox();
            this.lab_Handle_Rev4 = new System.Windows.Forms.Label();
            this.txt_Handle_Rev3 = new System.Windows.Forms.TextBox();
            this.lab_Handle_Rev3 = new System.Windows.Forms.Label();
            this.txt_Handle_Rev2 = new System.Windows.Forms.TextBox();
            this.lab_Handle_Rev2 = new System.Windows.Forms.Label();
            this.txt_Handle_Rev1 = new System.Windows.Forms.TextBox();
            this.lab_Handle_Rev1 = new System.Windows.Forms.Label();
            this.txt_HandleBathRatio = new System.Windows.Forms.TextBox();
            this.lab_HandleBathRatio = new System.Windows.Forms.Label();
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
            this.dgv_Handle5 = new SmartDyeing.FADM_Object.MyDataGridView();
            this.dataGridViewTextBoxColumn46 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn47 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_Handle4 = new SmartDyeing.FADM_Object.MyDataGridView();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_Handle3 = new SmartDyeing.FADM_Object.MyDataGridView();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_Handle2 = new SmartDyeing.FADM_Object.MyDataGridView();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_Handle1 = new SmartDyeing.FADM_Object.MyDataGridView();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_Dye = new SmartDyeing.FADM_Object.MyDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_FormulaData = new SmartDyeing.FADM_Object.MyDataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssistantCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssistantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormulaDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SettingConcentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealConcentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectDropWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealDropWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BottleSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grp_BatchData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BatchData)).BeginInit();
            this.grp_FormulaData.SuspendLayout();
            this.grp_Dye.SuspendLayout();
            this.grp_Handle.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.grp_FormulaBrowse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dye)).BeginInit();
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
            // grp_FormulaData
            // 
            this.grp_FormulaData.Controls.Add(this.txt_Operator);
            this.grp_FormulaData.Controls.Add(this.txt_FormulaGroup);
            this.grp_FormulaData.Controls.Add(this.lab_FormulaGroup);
            this.grp_FormulaData.Controls.Add(this.txt_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.txt_Non_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.lab_Non_AnhydrationWR);
            this.grp_FormulaData.Controls.Add(this.txt_DyeingCode);
            this.grp_FormulaData.Controls.Add(this.lab_DyeingCode);
            this.grp_FormulaData.Controls.Add(this.dgv_FormulaData);
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
            // txt_DyeingCode
            // 
            resources.ApplyResources(this.txt_DyeingCode, "txt_DyeingCode");
            this.txt_DyeingCode.FormattingEnabled = true;
            this.txt_DyeingCode.Name = "txt_DyeingCode";
            this.txt_DyeingCode.SelectedIndexChanged += new System.EventHandler(this.txt_DyeingCode_SelectedIndexChanged);
            this.txt_DyeingCode.TextUpdate += new System.EventHandler(this.txt_DyeingCode_TextUpdate);
            this.txt_DyeingCode.Leave += new System.EventHandler(this.txt_DyeingCode_Leave);
            // 
            // lab_DyeingCode
            // 
            resources.ApplyResources(this.lab_DyeingCode, "lab_DyeingCode");
            this.lab_DyeingCode.Name = "lab_DyeingCode";
            // 
            // txt_CupNum
            // 
            resources.ApplyResources(this.txt_CupNum, "txt_CupNum");
            this.txt_CupNum.Name = "txt_CupNum";
            this.txt_CupNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_CupNum_KeyPress);
            this.txt_CupNum.Leave += new System.EventHandler(this.txt_CupNum_Leave);
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
            // grp_Dye
            // 
            this.grp_Dye.Controls.Add(this.dgv_Dye);
            resources.ApplyResources(this.grp_Dye, "grp_Dye");
            this.grp_Dye.Name = "grp_Dye";
            this.grp_Dye.TabStop = false;
            // 
            // grp_Handle
            // 
            this.grp_Handle.Controls.Add(this.txt_Handle_Rev5);
            this.grp_Handle.Controls.Add(this.lab_Handle_Rev5);
            this.grp_Handle.Controls.Add(this.txt_Handle_Rev4);
            this.grp_Handle.Controls.Add(this.lab_Handle_Rev4);
            this.grp_Handle.Controls.Add(this.txt_Handle_Rev3);
            this.grp_Handle.Controls.Add(this.lab_Handle_Rev3);
            this.grp_Handle.Controls.Add(this.txt_Handle_Rev2);
            this.grp_Handle.Controls.Add(this.lab_Handle_Rev2);
            this.grp_Handle.Controls.Add(this.txt_Handle_Rev1);
            this.grp_Handle.Controls.Add(this.lab_Handle_Rev1);
            this.grp_Handle.Controls.Add(this.txt_HandleBathRatio);
            this.grp_Handle.Controls.Add(this.lab_HandleBathRatio);
            this.grp_Handle.Controls.Add(this.dgv_Handle5);
            this.grp_Handle.Controls.Add(this.dgv_Handle4);
            this.grp_Handle.Controls.Add(this.dgv_Handle3);
            this.grp_Handle.Controls.Add(this.dgv_Handle2);
            this.grp_Handle.Controls.Add(this.dgv_Handle1);
            resources.ApplyResources(this.grp_Handle, "grp_Handle");
            this.grp_Handle.Name = "grp_Handle";
            this.grp_Handle.TabStop = false;
            // 
            // txt_Handle_Rev5
            // 
            resources.ApplyResources(this.txt_Handle_Rev5, "txt_Handle_Rev5");
            this.txt_Handle_Rev5.Name = "txt_Handle_Rev5";
            this.txt_Handle_Rev5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Handle_Rev5_KeyPress);
            // 
            // lab_Handle_Rev5
            // 
            resources.ApplyResources(this.lab_Handle_Rev5, "lab_Handle_Rev5");
            this.lab_Handle_Rev5.Name = "lab_Handle_Rev5";
            // 
            // txt_Handle_Rev4
            // 
            resources.ApplyResources(this.txt_Handle_Rev4, "txt_Handle_Rev4");
            this.txt_Handle_Rev4.Name = "txt_Handle_Rev4";
            this.txt_Handle_Rev4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Handle_Rev4_KeyPress);
            // 
            // lab_Handle_Rev4
            // 
            resources.ApplyResources(this.lab_Handle_Rev4, "lab_Handle_Rev4");
            this.lab_Handle_Rev4.Name = "lab_Handle_Rev4";
            // 
            // txt_Handle_Rev3
            // 
            resources.ApplyResources(this.txt_Handle_Rev3, "txt_Handle_Rev3");
            this.txt_Handle_Rev3.Name = "txt_Handle_Rev3";
            this.txt_Handle_Rev3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Handle_Rev3_KeyPress);
            // 
            // lab_Handle_Rev3
            // 
            resources.ApplyResources(this.lab_Handle_Rev3, "lab_Handle_Rev3");
            this.lab_Handle_Rev3.Name = "lab_Handle_Rev3";
            // 
            // txt_Handle_Rev2
            // 
            resources.ApplyResources(this.txt_Handle_Rev2, "txt_Handle_Rev2");
            this.txt_Handle_Rev2.Name = "txt_Handle_Rev2";
            this.txt_Handle_Rev2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Handle_Rev2_KeyPress);
            // 
            // lab_Handle_Rev2
            // 
            resources.ApplyResources(this.lab_Handle_Rev2, "lab_Handle_Rev2");
            this.lab_Handle_Rev2.Name = "lab_Handle_Rev2";
            // 
            // txt_Handle_Rev1
            // 
            resources.ApplyResources(this.txt_Handle_Rev1, "txt_Handle_Rev1");
            this.txt_Handle_Rev1.Name = "txt_Handle_Rev1";
            this.txt_Handle_Rev1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Handle_Rev1_KeyPress);
            // 
            // lab_Handle_Rev1
            // 
            resources.ApplyResources(this.lab_Handle_Rev1, "lab_Handle_Rev1");
            this.lab_Handle_Rev1.Name = "lab_Handle_Rev1";
            // 
            // txt_HandleBathRatio
            // 
            resources.ApplyResources(this.txt_HandleBathRatio, "txt_HandleBathRatio");
            this.txt_HandleBathRatio.Name = "txt_HandleBathRatio";
            this.txt_HandleBathRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_HandleBathRatio_KeyPress);
            // 
            // lab_HandleBathRatio
            // 
            resources.ApplyResources(this.lab_HandleBathRatio, "lab_HandleBathRatio");
            this.lab_HandleBathRatio.Name = "lab_HandleBathRatio";
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
            // dgv_Handle5
            // 
            this.dgv_Handle5.AllowUserToAddRows = false;
            this.dgv_Handle5.AllowUserToDeleteRows = false;
            this.dgv_Handle5.AllowUserToResizeColumns = false;
            this.dgv_Handle5.AllowUserToResizeRows = false;
            this.dgv_Handle5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Handle5.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_Handle5.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle43.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle43.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle43.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle43.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle43.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle5.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle43;
            this.dgv_Handle5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Handle5.ColumnHeadersVisible = false;
            this.dgv_Handle5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn46,
            this.dataGridViewTextBoxColumn47,
            this.dataGridViewTextBoxColumn52,
            this.dataGridViewTextBoxColumn48,
            this.dataGridViewTextBoxColumn49,
            this.dataGridViewComboBoxColumn6,
            this.dataGridViewTextBoxColumn50,
            this.dataGridViewTextBoxColumn51,
            this.dataGridViewTextBoxColumn53,
            this.dataGridViewTextBoxColumn54,
            this.dataGridViewCheckBoxColumn6});
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle44.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle44.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle44.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle44.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle44.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Handle5.DefaultCellStyle = dataGridViewCellStyle44;
            this.dgv_Handle5.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_Handle5, "dgv_Handle5");
            this.dgv_Handle5.MultiSelect = false;
            this.dgv_Handle5.Name = "dgv_Handle5";
            this.dgv_Handle5.RowHeadersVisible = false;
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle45.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_Handle5.RowsDefaultCellStyle = dataGridViewCellStyle45;
            this.dgv_Handle5.RowTemplate.Height = 30;
            this.dgv_Handle5.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle5.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_Handle5_EditingControlShowing);
            this.dgv_Handle5.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle5_RowEnter);
            this.dgv_Handle5.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle5_RowLeave);
            this.dgv_Handle5.SelectionChanged += new System.EventHandler(this.dgv_Handle5_SelectionChanged);
            this.dgv_Handle5.Enter += new System.EventHandler(this.dgv_Handle5_Enter);
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
            // dataGridViewTextBoxColumn52
            // 
            this.dataGridViewTextBoxColumn52.FillWeight = 150.5132F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn52, "dataGridViewTextBoxColumn52");
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            this.dataGridViewTextBoxColumn52.ReadOnly = true;
            this.dataGridViewTextBoxColumn52.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // dgv_Handle4
            // 
            this.dgv_Handle4.AllowUserToAddRows = false;
            this.dgv_Handle4.AllowUserToDeleteRows = false;
            this.dgv_Handle4.AllowUserToResizeColumns = false;
            this.dgv_Handle4.AllowUserToResizeRows = false;
            this.dgv_Handle4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Handle4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_Handle4.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle46.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle46.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle46.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle46;
            this.dgv_Handle4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Handle4.ColumnHeadersVisible = false;
            this.dgv_Handle4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn43,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewComboBoxColumn5,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn44,
            this.dataGridViewTextBoxColumn45,
            this.dataGridViewCheckBoxColumn5});
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle47.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle47.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Handle4.DefaultCellStyle = dataGridViewCellStyle47;
            this.dgv_Handle4.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_Handle4, "dgv_Handle4");
            this.dgv_Handle4.MultiSelect = false;
            this.dgv_Handle4.Name = "dgv_Handle4";
            this.dgv_Handle4.RowHeadersVisible = false;
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle48.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_Handle4.RowsDefaultCellStyle = dataGridViewCellStyle48;
            this.dgv_Handle4.RowTemplate.Height = 30;
            this.dgv_Handle4.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle4.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_Handle4_EditingControlShowing);
            this.dgv_Handle4.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle4_RowEnter);
            this.dgv_Handle4.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle4_RowLeave);
            this.dgv_Handle4.SelectionChanged += new System.EventHandler(this.dgv_Handle4_SelectionChanged);
            this.dgv_Handle4.Enter += new System.EventHandler(this.dgv_Handle4_Enter);
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
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.FillWeight = 150.5132F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn43, "dataGridViewTextBoxColumn43");
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.ReadOnly = true;
            this.dataGridViewTextBoxColumn43.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // dgv_Handle3
            // 
            this.dgv_Handle3.AllowUserToAddRows = false;
            this.dgv_Handle3.AllowUserToDeleteRows = false;
            this.dgv_Handle3.AllowUserToResizeColumns = false;
            this.dgv_Handle3.AllowUserToResizeRows = false;
            this.dgv_Handle3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Handle3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_Handle3.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle49.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle49.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle49.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle49.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle49.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle49.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle49;
            this.dgv_Handle3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Handle3.ColumnHeadersVisible = false;
            this.dgv_Handle3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn30,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewComboBoxColumn4,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewCheckBoxColumn4});
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle50.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle50.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle50.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle50.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle50.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Handle3.DefaultCellStyle = dataGridViewCellStyle50;
            this.dgv_Handle3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_Handle3, "dgv_Handle3");
            this.dgv_Handle3.MultiSelect = false;
            this.dgv_Handle3.Name = "dgv_Handle3";
            this.dgv_Handle3.RowHeadersVisible = false;
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle51.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_Handle3.RowsDefaultCellStyle = dataGridViewCellStyle51;
            this.dgv_Handle3.RowTemplate.Height = 30;
            this.dgv_Handle3.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle3.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_Handle3_EditingControlShowing);
            this.dgv_Handle3.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle3_RowEnter);
            this.dgv_Handle3.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle3_RowLeave);
            this.dgv_Handle3.SelectionChanged += new System.EventHandler(this.dgv_Handle3_SelectionChanged);
            this.dgv_Handle3.Enter += new System.EventHandler(this.dgv_Handle3_Enter);
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
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.FillWeight = 150.5132F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn34, "dataGridViewTextBoxColumn34");
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // dgv_Handle2
            // 
            this.dgv_Handle2.AllowUserToAddRows = false;
            this.dgv_Handle2.AllowUserToDeleteRows = false;
            this.dgv_Handle2.AllowUserToResizeColumns = false;
            this.dgv_Handle2.AllowUserToResizeRows = false;
            this.dgv_Handle2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Handle2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_Handle2.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle52.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle52.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle52.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle52.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle52.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle52.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle52;
            this.dgv_Handle2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Handle2.ColumnHeadersVisible = false;
            this.dgv_Handle2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewComboBoxColumn3,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewCheckBoxColumn3});
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle53.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle53.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle53.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle53.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle53.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle53.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Handle2.DefaultCellStyle = dataGridViewCellStyle53;
            this.dgv_Handle2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_Handle2, "dgv_Handle2");
            this.dgv_Handle2.MultiSelect = false;
            this.dgv_Handle2.Name = "dgv_Handle2";
            this.dgv_Handle2.RowHeadersVisible = false;
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle54.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_Handle2.RowsDefaultCellStyle = dataGridViewCellStyle54;
            this.dgv_Handle2.RowTemplate.Height = 30;
            this.dgv_Handle2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_Handle2_EditingControlShowing);
            this.dgv_Handle2.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle2_RowEnter);
            this.dgv_Handle2.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle2_RowLeave);
            this.dgv_Handle2.SelectionChanged += new System.EventHandler(this.dgv_Handle2_SelectionChanged);
            this.dgv_Handle2.Enter += new System.EventHandler(this.dgv_Handle2_Enter);
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
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.FillWeight = 150.5132F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn25, "dataGridViewTextBoxColumn25");
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // dgv_Handle1
            // 
            this.dgv_Handle1.AllowUserToAddRows = false;
            this.dgv_Handle1.AllowUserToDeleteRows = false;
            this.dgv_Handle1.AllowUserToResizeColumns = false;
            this.dgv_Handle1.AllowUserToResizeRows = false;
            this.dgv_Handle1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Handle1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_Handle1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle55.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle55.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle55.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle55.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle55.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle55.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle55;
            this.dgv_Handle1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Handle1.ColumnHeadersVisible = false;
            this.dgv_Handle1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewComboBoxColumn2,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewCheckBoxColumn2});
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle56.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle56.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle56.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle56.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle56.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle56.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Handle1.DefaultCellStyle = dataGridViewCellStyle56;
            this.dgv_Handle1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_Handle1, "dgv_Handle1");
            this.dgv_Handle1.MultiSelect = false;
            this.dgv_Handle1.Name = "dgv_Handle1";
            this.dgv_Handle1.RowHeadersVisible = false;
            dataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle57.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_Handle1.RowsDefaultCellStyle = dataGridViewCellStyle57;
            this.dgv_Handle1.RowTemplate.Height = 30;
            this.dgv_Handle1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Handle1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_Handle1_EditingControlShowing);
            this.dgv_Handle1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle1_RowEnter);
            this.dgv_Handle1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Handle1_RowLeave);
            this.dgv_Handle1.SelectionChanged += new System.EventHandler(this.dgv_Handle1_SelectionChanged);
            this.dgv_Handle1.Enter += new System.EventHandler(this.dgv_Handle1_Enter);
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
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.FillWeight = 151.002F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // dgv_Dye
            // 
            this.dgv_Dye.AllowUserToAddRows = false;
            this.dgv_Dye.AllowUserToDeleteRows = false;
            this.dgv_Dye.AllowUserToResizeColumns = false;
            this.dgv_Dye.AllowUserToResizeRows = false;
            this.dgv_Dye.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Dye.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_Dye.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle58.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle58.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle58.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle58.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle58.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle58.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Dye.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle58;
            this.dgv_Dye.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Dye.ColumnHeadersVisible = false;
            this.dgv_Dye.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewCheckBoxColumn1});
            dataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle59.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle59.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle59.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle59.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle59.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle59.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Dye.DefaultCellStyle = dataGridViewCellStyle59;
            this.dgv_Dye.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_Dye, "dgv_Dye");
            this.dgv_Dye.MultiSelect = false;
            this.dgv_Dye.Name = "dgv_Dye";
            this.dgv_Dye.RowHeadersVisible = false;
            dataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle60.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_Dye.RowsDefaultCellStyle = dataGridViewCellStyle60;
            this.dgv_Dye.RowTemplate.Height = 30;
            this.dgv_Dye.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Dye.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_Dye_EditingControlShowing);
            this.dgv_Dye.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Dye_RowEnter);
            this.dgv_Dye.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Dye_RowLeave);
            this.dgv_Dye.SelectionChanged += new System.EventHandler(this.dgv_Dye_SelectionChanged);
            this.dgv_Dye.Enter += new System.EventHandler(this.dgv_Dye_Enter);
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
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 151.3145F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // dgv_FormulaData
            // 
            this.dgv_FormulaData.AllowUserToDeleteRows = false;
            this.dgv_FormulaData.AllowUserToResizeColumns = false;
            this.dgv_FormulaData.AllowUserToResizeRows = false;
            this.dgv_FormulaData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_FormulaData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_FormulaData.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle61.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle61.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle61.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle61.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle61.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle61.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_FormulaData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle61;
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
            dataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle62.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle62.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle62.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle62.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle62.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle62.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_FormulaData.DefaultCellStyle = dataGridViewCellStyle62;
            this.dgv_FormulaData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            resources.ApplyResources(this.dgv_FormulaData, "dgv_FormulaData");
            this.dgv_FormulaData.MultiSelect = false;
            this.dgv_FormulaData.Name = "dgv_FormulaData";
            this.dgv_FormulaData.RowHeadersVisible = false;
            dataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle63.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_FormulaData.RowsDefaultCellStyle = dataGridViewCellStyle63;
            this.dgv_FormulaData.RowTemplate.Height = 30;
            this.dgv_FormulaData.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_FormulaData.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_FormulaData_EditingControlShowing);
            this.dgv_FormulaData.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_FormulaData_RowLeave);
            this.dgv_FormulaData.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_FormulaData_RowsAdded);
            this.dgv_FormulaData.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgv_FormulaData_RowsRemoved);
            this.dgv_FormulaData.SelectionChanged += new System.EventHandler(this.dgv_FormulaData_SelectionChanged);
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
            this.Controls.Add(this.grp_FormulaBrowse);
            this.Controls.Add(this.btn_NotDrip);
            this.Controls.Add(this.Btn_WaitList);
            this.Controls.Add(this.grp_Handle);
            this.Controls.Add(this.grp_Dye);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.grp_FormulaData);
            this.Controls.Add(this.grp_BatchData);
            this.Controls.Add(this.btn_BatchAdd);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_FormulaCodeAdd);
            resources.ApplyResources(this, "$this");
            this.Name = "Formula";
            this.Load += new System.EventHandler(this.Formula_Load);
            this.grp_BatchData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BatchData)).EndInit();
            this.grp_FormulaData.ResumeLayout(false);
            this.grp_FormulaData.PerformLayout();
            this.grp_Dye.ResumeLayout(false);
            this.grp_Handle.ResumeLayout(false);
            this.grp_Handle.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.grp_FormulaBrowse.ResumeLayout(false);
            this.grp_FormulaBrowse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Handle1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grp_BatchData;
        private GroupBox grp_FormulaData;
        private Button btn_Stop;
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
        private TextBox txt_FormulaName;
        private TextBox txt_State;
        private TextBox txt_VersionNum;
        private TextBox txt_FormulaCode;
        private TextBox txt_CreateTime;
        private TextBox txt_TotalWeight;
        private TextBox txt_BathRatio;
        private TextBox txt_ClothWeight;
        private CheckBox chk_AddWaterChoose;
        private TextBox txt_Customer;
        private TextBox txt_CupNum;
        private TextBox txt_ClothType;
        private Button btn_FormulaCodeAdd;
        private Button btn_BatchAdd;
        public Button btn_Save;
        private DataGridView dgv_BatchData;
        private SmartDyeing.FADM_Object.MyDataGridView dgv_FormulaData;
        private System.Windows.Forms.Timer tmr;
        private ComboBox txt_DyeingCode;
        private Label lab_DyeingCode;
        private GroupBox grp_Dye;
        private FADM_Object.MyDataGridView dgv_Dye;
        private GroupBox grp_Handle;
        private FADM_Object.MyDataGridView dgv_Handle4;
        private FADM_Object.MyDataGridView dgv_Handle3;
        private FADM_Object.MyDataGridView dgv_Handle2;
        private FADM_Object.MyDataGridView dgv_Handle1;
        private FADM_Object.MyDataGridView dgv_Handle5;
        private TextBox txt_Handle_Rev5;
        private Label lab_Handle_Rev5;
        private TextBox txt_Handle_Rev4;
        private Label lab_Handle_Rev4;
        private TextBox txt_Handle_Rev3;
        private Label lab_Handle_Rev3;
        private TextBox txt_Handle_Rev2;
        private Label lab_Handle_Rev2;
        private TextBox txt_Handle_Rev1;
        private Label lab_Handle_Rev1;
        private TextBox txt_HandleBathRatio;
        private Label lab_HandleBathRatio;
        private TextBox txt_AnhydrationWR;
        private Label lab_AnhydrationWR;
        private TextBox txt_Non_AnhydrationWR;
        private Label lab_Non_AnhydrationWR;
        private Button Btn_WaitList;
        public Button btn_Start;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem tsm_Delete;
        private Label lab_FormulaGroup;
        private ComboBox txt_FormulaGroup;
        public Button btn_NotDrip;
        private TextBox txt_Operator;
        private DataGridViewTextBoxColumn Index;
        private DataGridViewTextBoxColumn AssistantCode;
        private DataGridViewTextBoxColumn AssistantName;
        private DataGridViewTextBoxColumn FormulaDosage;
        private DataGridViewTextBoxColumn UnitOfAccount;
        private DataGridViewComboBoxColumn Column1;
        private DataGridViewTextBoxColumn SettingConcentration;
        private DataGridViewTextBoxColumn RealConcentration;
        private DataGridViewTextBoxColumn ObjectDropWeight;
        private DataGridViewTextBoxColumn RealDropWeight;
        private DataGridViewCheckBoxColumn BottleSelection;
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
    }
}
