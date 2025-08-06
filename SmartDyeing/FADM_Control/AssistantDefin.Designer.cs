using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class AssistantDefin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssistantDefin));
            this.grp_Browse = new System.Windows.Forms.GroupBox();
            this.dgv_Assistant = new System.Windows.Forms.DataGridView();
            this.grp_AssistantDetails = new System.Windows.Forms.GroupBox();
            this.cbo_Reweigh = new System.Windows.Forms.ComboBox();
            this.lab_Reweigh = new System.Windows.Forms.Label();
            this.txt_WavelengthInterval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_EndWavelength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_StartingWavelength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdo_4 = new System.Windows.Forms.RadioButton();
            this.rdo_3 = new System.Windows.Forms.RadioButton();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Insert = new System.Windows.Forms.Button();
            this.rdo_2 = new System.Windows.Forms.RadioButton();
            this.rdo_1 = new System.Windows.Forms.RadioButton();
            this.cbo_AssistantType = new System.Windows.Forms.ComboBox();
            this.txt_AssistantBarCode = new System.Windows.Forms.TextBox();
            this.txt_Cost = new System.Windows.Forms.TextBox();
            this.txt_Intensity = new System.Windows.Forms.TextBox();
            this.txt_TermOfValidity = new System.Windows.Forms.TextBox();
            this.txt_AllowMaxColoringConcentration = new System.Windows.Forms.TextBox();
            this.txt_AllowMinColoringConcentration = new System.Windows.Forms.TextBox();
            this.txt_AssistantName = new System.Windows.Forms.TextBox();
            this.txt_AssistantCode = new System.Windows.Forms.TextBox();
            this.lab_Cost = new System.Windows.Forms.Label();
            this.lab_Intensity = new System.Windows.Forms.Label();
            this.lab_TermOfValidity = new System.Windows.Forms.Label();
            this.lab_AllowMaxColoringConcentration = new System.Windows.Forms.Label();
            this.lab_AllowMinColoringConcentration = new System.Windows.Forms.Label();
            this.lab_UnitOfAccount = new System.Windows.Forms.Label();
            this.lab_AssistantType = new System.Windows.Forms.Label();
            this.lab_AssistantName = new System.Windows.Forms.Label();
            this.lab_AssistantBarCode = new System.Windows.Forms.Label();
            this.lab_AssistantCode = new System.Windows.Forms.Label();
            this.grp_Browse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Assistant)).BeginInit();
            this.grp_AssistantDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Browse
            // 
            this.grp_Browse.Controls.Add(this.dgv_Assistant);
            resources.ApplyResources(this.grp_Browse, "grp_Browse");
            this.grp_Browse.Name = "grp_Browse";
            this.grp_Browse.TabStop = false;
            // 
            // dgv_Assistant
            // 
            this.dgv_Assistant.AllowUserToAddRows = false;
            this.dgv_Assistant.AllowUserToDeleteRows = false;
            this.dgv_Assistant.AllowUserToResizeColumns = false;
            this.dgv_Assistant.AllowUserToResizeRows = false;
            this.dgv_Assistant.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Assistant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_Assistant, "dgv_Assistant");
            this.dgv_Assistant.MultiSelect = false;
            this.dgv_Assistant.Name = "dgv_Assistant";
            this.dgv_Assistant.ReadOnly = true;
            this.dgv_Assistant.RowHeadersVisible = false;
            this.dgv_Assistant.RowTemplate.Height = 23;
            this.dgv_Assistant.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Assistant.CurrentCellChanged += new System.EventHandler(this.dgv_Assistant_CurrentCellChanged);
            // 
            // grp_AssistantDetails
            // 
            this.grp_AssistantDetails.Controls.Add(this.cbo_Reweigh);
            this.grp_AssistantDetails.Controls.Add(this.lab_Reweigh);
            this.grp_AssistantDetails.Controls.Add(this.txt_WavelengthInterval);
            this.grp_AssistantDetails.Controls.Add(this.label4);
            this.grp_AssistantDetails.Controls.Add(this.txt_EndWavelength);
            this.grp_AssistantDetails.Controls.Add(this.label3);
            this.grp_AssistantDetails.Controls.Add(this.txt_StartingWavelength);
            this.grp_AssistantDetails.Controls.Add(this.label2);
            this.grp_AssistantDetails.Controls.Add(this.rdo_4);
            this.grp_AssistantDetails.Controls.Add(this.rdo_3);
            this.grp_AssistantDetails.Controls.Add(this.btn_Delete);
            this.grp_AssistantDetails.Controls.Add(this.btn_Save);
            this.grp_AssistantDetails.Controls.Add(this.btn_Insert);
            this.grp_AssistantDetails.Controls.Add(this.rdo_2);
            this.grp_AssistantDetails.Controls.Add(this.rdo_1);
            this.grp_AssistantDetails.Controls.Add(this.cbo_AssistantType);
            this.grp_AssistantDetails.Controls.Add(this.txt_AssistantBarCode);
            this.grp_AssistantDetails.Controls.Add(this.txt_Cost);
            this.grp_AssistantDetails.Controls.Add(this.txt_Intensity);
            this.grp_AssistantDetails.Controls.Add(this.txt_TermOfValidity);
            this.grp_AssistantDetails.Controls.Add(this.txt_AllowMaxColoringConcentration);
            this.grp_AssistantDetails.Controls.Add(this.txt_AllowMinColoringConcentration);
            this.grp_AssistantDetails.Controls.Add(this.txt_AssistantName);
            this.grp_AssistantDetails.Controls.Add(this.txt_AssistantCode);
            this.grp_AssistantDetails.Controls.Add(this.lab_Cost);
            this.grp_AssistantDetails.Controls.Add(this.lab_Intensity);
            this.grp_AssistantDetails.Controls.Add(this.lab_TermOfValidity);
            this.grp_AssistantDetails.Controls.Add(this.lab_AllowMaxColoringConcentration);
            this.grp_AssistantDetails.Controls.Add(this.lab_AllowMinColoringConcentration);
            this.grp_AssistantDetails.Controls.Add(this.lab_UnitOfAccount);
            this.grp_AssistantDetails.Controls.Add(this.lab_AssistantType);
            this.grp_AssistantDetails.Controls.Add(this.lab_AssistantName);
            this.grp_AssistantDetails.Controls.Add(this.lab_AssistantBarCode);
            this.grp_AssistantDetails.Controls.Add(this.lab_AssistantCode);
            resources.ApplyResources(this.grp_AssistantDetails, "grp_AssistantDetails");
            this.grp_AssistantDetails.Name = "grp_AssistantDetails";
            this.grp_AssistantDetails.TabStop = false;
            // 
            // cbo_Reweigh
            // 
            this.cbo_Reweigh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbo_Reweigh, "cbo_Reweigh");
            this.cbo_Reweigh.FormattingEnabled = true;
            this.cbo_Reweigh.Items.AddRange(new object[] {
            resources.GetString("cbo_Reweigh.Items"),
            resources.GetString("cbo_Reweigh.Items1")});
            this.cbo_Reweigh.Name = "cbo_Reweigh";
            // 
            // lab_Reweigh
            // 
            resources.ApplyResources(this.lab_Reweigh, "lab_Reweigh");
            this.lab_Reweigh.Name = "lab_Reweigh";
            // 
            // txt_WavelengthInterval
            // 
            resources.ApplyResources(this.txt_WavelengthInterval, "txt_WavelengthInterval");
            this.txt_WavelengthInterval.Name = "txt_WavelengthInterval";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txt_EndWavelength
            // 
            resources.ApplyResources(this.txt_EndWavelength, "txt_EndWavelength");
            this.txt_EndWavelength.Name = "txt_EndWavelength";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txt_StartingWavelength
            // 
            resources.ApplyResources(this.txt_StartingWavelength, "txt_StartingWavelength");
            this.txt_StartingWavelength.Name = "txt_StartingWavelength";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // rdo_4
            // 
            resources.ApplyResources(this.rdo_4, "rdo_4");
            this.rdo_4.Name = "rdo_4";
            this.rdo_4.TabStop = true;
            this.rdo_4.UseVisualStyleBackColor = true;
            this.rdo_4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdo_4_KeyDown);
            // 
            // rdo_3
            // 
            resources.ApplyResources(this.rdo_3, "rdo_3");
            this.rdo_3.Name = "rdo_3";
            this.rdo_3.TabStop = true;
            this.rdo_3.UseVisualStyleBackColor = true;
            this.rdo_3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdo_3_KeyDown);
            // 
            // btn_Delete
            // 
            resources.ApplyResources(this.btn_Delete, "btn_Delete");
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Save
            // 
            resources.ApplyResources(this.btn_Save, "btn_Save");
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Insert
            // 
            resources.ApplyResources(this.btn_Insert, "btn_Insert");
            this.btn_Insert.Name = "btn_Insert";
            this.btn_Insert.UseVisualStyleBackColor = true;
            this.btn_Insert.Click += new System.EventHandler(this.btn_Insert_Click);
            // 
            // rdo_2
            // 
            resources.ApplyResources(this.rdo_2, "rdo_2");
            this.rdo_2.Name = "rdo_2";
            this.rdo_2.TabStop = true;
            this.rdo_2.UseVisualStyleBackColor = true;
            this.rdo_2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdo_2_KeyDown);
            // 
            // rdo_1
            // 
            resources.ApplyResources(this.rdo_1, "rdo_1");
            this.rdo_1.Name = "rdo_1";
            this.rdo_1.UseVisualStyleBackColor = true;
            this.rdo_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdo_1_KeyDown);
            // 
            // cbo_AssistantType
            // 
            this.cbo_AssistantType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbo_AssistantType, "cbo_AssistantType");
            this.cbo_AssistantType.FormattingEnabled = true;
            this.cbo_AssistantType.Items.AddRange(new object[] {
            resources.GetString("cbo_AssistantType.Items"),
            resources.GetString("cbo_AssistantType.Items1"),
            resources.GetString("cbo_AssistantType.Items2"),
            resources.GetString("cbo_AssistantType.Items3"),
            resources.GetString("cbo_AssistantType.Items4"),
            resources.GetString("cbo_AssistantType.Items5"),
            resources.GetString("cbo_AssistantType.Items6"),
            resources.GetString("cbo_AssistantType.Items7"),
            resources.GetString("cbo_AssistantType.Items8"),
            resources.GetString("cbo_AssistantType.Items9")});
            this.cbo_AssistantType.Name = "cbo_AssistantType";
            // 
            // txt_AssistantBarCode
            // 
            resources.ApplyResources(this.txt_AssistantBarCode, "txt_AssistantBarCode");
            this.txt_AssistantBarCode.Name = "txt_AssistantBarCode";
            // 
            // txt_Cost
            // 
            resources.ApplyResources(this.txt_Cost, "txt_Cost");
            this.txt_Cost.Name = "txt_Cost";
            // 
            // txt_Intensity
            // 
            resources.ApplyResources(this.txt_Intensity, "txt_Intensity");
            this.txt_Intensity.Name = "txt_Intensity";
            // 
            // txt_TermOfValidity
            // 
            resources.ApplyResources(this.txt_TermOfValidity, "txt_TermOfValidity");
            this.txt_TermOfValidity.Name = "txt_TermOfValidity";
            this.txt_TermOfValidity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_TermOfValidity_KeyDown);
            // 
            // txt_AllowMaxColoringConcentration
            // 
            resources.ApplyResources(this.txt_AllowMaxColoringConcentration, "txt_AllowMaxColoringConcentration");
            this.txt_AllowMaxColoringConcentration.Name = "txt_AllowMaxColoringConcentration";
            // 
            // txt_AllowMinColoringConcentration
            // 
            resources.ApplyResources(this.txt_AllowMinColoringConcentration, "txt_AllowMinColoringConcentration");
            this.txt_AllowMinColoringConcentration.Name = "txt_AllowMinColoringConcentration";
            // 
            // txt_AssistantName
            // 
            resources.ApplyResources(this.txt_AssistantName, "txt_AssistantName");
            this.txt_AssistantName.Name = "txt_AssistantName";
            // 
            // txt_AssistantCode
            // 
            resources.ApplyResources(this.txt_AssistantCode, "txt_AssistantCode");
            this.txt_AssistantCode.Name = "txt_AssistantCode";
            this.txt_AssistantCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_AssistantCode_KeyDown);
            // 
            // lab_Cost
            // 
            resources.ApplyResources(this.lab_Cost, "lab_Cost");
            this.lab_Cost.Name = "lab_Cost";
            // 
            // lab_Intensity
            // 
            resources.ApplyResources(this.lab_Intensity, "lab_Intensity");
            this.lab_Intensity.Name = "lab_Intensity";
            // 
            // lab_TermOfValidity
            // 
            resources.ApplyResources(this.lab_TermOfValidity, "lab_TermOfValidity");
            this.lab_TermOfValidity.Name = "lab_TermOfValidity";
            // 
            // lab_AllowMaxColoringConcentration
            // 
            resources.ApplyResources(this.lab_AllowMaxColoringConcentration, "lab_AllowMaxColoringConcentration");
            this.lab_AllowMaxColoringConcentration.Name = "lab_AllowMaxColoringConcentration";
            // 
            // lab_AllowMinColoringConcentration
            // 
            resources.ApplyResources(this.lab_AllowMinColoringConcentration, "lab_AllowMinColoringConcentration");
            this.lab_AllowMinColoringConcentration.Name = "lab_AllowMinColoringConcentration";
            // 
            // lab_UnitOfAccount
            // 
            resources.ApplyResources(this.lab_UnitOfAccount, "lab_UnitOfAccount");
            this.lab_UnitOfAccount.Name = "lab_UnitOfAccount";
            // 
            // lab_AssistantType
            // 
            resources.ApplyResources(this.lab_AssistantType, "lab_AssistantType");
            this.lab_AssistantType.Name = "lab_AssistantType";
            // 
            // lab_AssistantName
            // 
            resources.ApplyResources(this.lab_AssistantName, "lab_AssistantName");
            this.lab_AssistantName.Name = "lab_AssistantName";
            // 
            // lab_AssistantBarCode
            // 
            resources.ApplyResources(this.lab_AssistantBarCode, "lab_AssistantBarCode");
            this.lab_AssistantBarCode.Name = "lab_AssistantBarCode";
            // 
            // lab_AssistantCode
            // 
            resources.ApplyResources(this.lab_AssistantCode, "lab_AssistantCode");
            this.lab_AssistantCode.Name = "lab_AssistantCode";
            // 
            // AssistantDefin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_AssistantDetails);
            this.Controls.Add(this.grp_Browse);
            this.Name = "AssistantDefin";
            this.grp_Browse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Assistant)).EndInit();
            this.grp_AssistantDetails.ResumeLayout(false);
            this.grp_AssistantDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Browse;
        private System.Windows.Forms.DataGridView dgv_Assistant;
        private System.Windows.Forms.GroupBox grp_AssistantDetails;
        private System.Windows.Forms.Label lab_Cost;
        private System.Windows.Forms.Label lab_Intensity;
        private System.Windows.Forms.Label lab_TermOfValidity;
        private System.Windows.Forms.Label lab_AllowMaxColoringConcentration;
        private System.Windows.Forms.Label lab_AllowMinColoringConcentration;
        private System.Windows.Forms.Label lab_UnitOfAccount;
        private System.Windows.Forms.Label lab_AssistantType;
        private System.Windows.Forms.Label lab_AssistantName;
        private System.Windows.Forms.Label lab_AssistantBarCode;
        private System.Windows.Forms.Label lab_AssistantCode;
        private System.Windows.Forms.TextBox txt_Cost;
        private System.Windows.Forms.TextBox txt_Intensity;
        private System.Windows.Forms.TextBox txt_TermOfValidity;
        private System.Windows.Forms.TextBox txt_AllowMaxColoringConcentration;
        private System.Windows.Forms.TextBox txt_AllowMinColoringConcentration;
        private System.Windows.Forms.TextBox txt_AssistantName;
        private System.Windows.Forms.TextBox txt_AssistantCode;
        private System.Windows.Forms.RadioButton rdo_2;
        private System.Windows.Forms.RadioButton rdo_1;
        private System.Windows.Forms.ComboBox cbo_AssistantType;
        private System.Windows.Forms.TextBox txt_AssistantBarCode;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Insert;
        private System.Windows.Forms.RadioButton rdo_4;
        private System.Windows.Forms.RadioButton rdo_3;
        private TextBox txt_WavelengthInterval;
        private Label label4;
        private TextBox txt_EndWavelength;
        private Label label3;
        private TextBox txt_StartingWavelength;
        private Label label2;
        private ComboBox cbo_Reweigh;
        private Label lab_Reweigh;
    }
}
