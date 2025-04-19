namespace SmartDyeing.FADM_Control
{
    partial class PostTreatmentConfiguration
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostTreatmentConfiguration));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_Dye_Code = new System.Windows.Forms.DataGridView();
            this.btn_DyeingCodeDelete = new System.Windows.Forms.Button();
            this.btn_DyeingCodeAdd = new System.Windows.Forms.Button();
            this.txt_Dye_Code = new System.Windows.Forms.TextBox();
            this.lab_BrewProcessCode = new System.Windows.Forms.Label();
            this.grp_Dyeing = new System.Windows.Forms.GroupBox();
            this.txt_Notes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_DyeingProcessDelete = new System.Windows.Forms.Button();
            this.btn_DyeingProcessUpdate = new System.Windows.Forms.Button();
            this.btn_DyeingProcessAdd = new System.Windows.Forms.Button();
            this.grp_BrewingProcess = new System.Windows.Forms.GroupBox();
            this.btn_Insert = new System.Windows.Forms.Button();
            this.txt_Template = new System.Windows.Forms.ComboBox();
            this.lab_DyeingCode = new System.Windows.Forms.Label();
            this.btn_Copy = new System.Windows.Forms.Button();
            this.dgv_Child_DyeData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dye_Code)).BeginInit();
            this.grp_Dyeing.SuspendLayout();
            this.grp_BrewingProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Child_DyeData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Dye_Code
            // 
            this.dgv_Dye_Code.AllowUserToAddRows = false;
            this.dgv_Dye_Code.AllowUserToDeleteRows = false;
            this.dgv_Dye_Code.AllowUserToResizeColumns = false;
            this.dgv_Dye_Code.AllowUserToResizeRows = false;
            this.dgv_Dye_Code.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Dye_Code.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Dye_Code.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_Dye_Code, "dgv_Dye_Code");
            this.dgv_Dye_Code.MultiSelect = false;
            this.dgv_Dye_Code.Name = "dgv_Dye_Code";
            this.dgv_Dye_Code.ReadOnly = true;
            this.dgv_Dye_Code.RowHeadersVisible = false;
            this.dgv_Dye_Code.RowTemplate.Height = 23;
            this.dgv_Dye_Code.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Dye_Code.CurrentCellChanged += new System.EventHandler(this.dgv_Dye_Code_CurrentCellChanged);
            // 
            // btn_DyeingCodeDelete
            // 
            resources.ApplyResources(this.btn_DyeingCodeDelete, "btn_DyeingCodeDelete");
            this.btn_DyeingCodeDelete.Name = "btn_DyeingCodeDelete";
            this.btn_DyeingCodeDelete.UseVisualStyleBackColor = true;
            this.btn_DyeingCodeDelete.Click += new System.EventHandler(this.btn_DyeingCodeDelete_Click);
            // 
            // btn_DyeingCodeAdd
            // 
            resources.ApplyResources(this.btn_DyeingCodeAdd, "btn_DyeingCodeAdd");
            this.btn_DyeingCodeAdd.Name = "btn_DyeingCodeAdd";
            this.btn_DyeingCodeAdd.UseVisualStyleBackColor = true;
            this.btn_DyeingCodeAdd.Click += new System.EventHandler(this.btn_DyeingCodeAdd_Click);
            // 
            // txt_Dye_Code
            // 
            resources.ApplyResources(this.txt_Dye_Code, "txt_Dye_Code");
            this.txt_Dye_Code.Name = "txt_Dye_Code";
            this.txt_Dye_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Dye_Code_KeyDown);
            // 
            // lab_BrewProcessCode
            // 
            resources.ApplyResources(this.lab_BrewProcessCode, "lab_BrewProcessCode");
            this.lab_BrewProcessCode.Name = "lab_BrewProcessCode";
            // 
            // grp_Dyeing
            // 
            this.grp_Dyeing.Controls.Add(this.dgv_Dye_Code);
            resources.ApplyResources(this.grp_Dyeing, "grp_Dyeing");
            this.grp_Dyeing.Name = "grp_Dyeing";
            this.grp_Dyeing.TabStop = false;
            // 
            // txt_Notes
            // 
            resources.ApplyResources(this.txt_Notes, "txt_Notes");
            this.txt_Notes.Name = "txt_Notes";
            this.txt_Notes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Notes_KeyDown);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btn_DyeingProcessDelete
            // 
            resources.ApplyResources(this.btn_DyeingProcessDelete, "btn_DyeingProcessDelete");
            this.btn_DyeingProcessDelete.Name = "btn_DyeingProcessDelete";
            this.btn_DyeingProcessDelete.UseVisualStyleBackColor = true;
            this.btn_DyeingProcessDelete.Click += new System.EventHandler(this.btn_DyeingProcessDelete_Click);
            // 
            // btn_DyeingProcessUpdate
            // 
            resources.ApplyResources(this.btn_DyeingProcessUpdate, "btn_DyeingProcessUpdate");
            this.btn_DyeingProcessUpdate.Name = "btn_DyeingProcessUpdate";
            this.btn_DyeingProcessUpdate.UseVisualStyleBackColor = true;
            this.btn_DyeingProcessUpdate.Click += new System.EventHandler(this.btn_DyeingProcessUpdate_Click);
            // 
            // btn_DyeingProcessAdd
            // 
            resources.ApplyResources(this.btn_DyeingProcessAdd, "btn_DyeingProcessAdd");
            this.btn_DyeingProcessAdd.Name = "btn_DyeingProcessAdd";
            this.btn_DyeingProcessAdd.UseVisualStyleBackColor = true;
            this.btn_DyeingProcessAdd.Click += new System.EventHandler(this.btn_DyeingProcessAdd_Click);
            // 
            // grp_BrewingProcess
            // 
            this.grp_BrewingProcess.Controls.Add(this.btn_Insert);
            this.grp_BrewingProcess.Controls.Add(this.txt_Template);
            this.grp_BrewingProcess.Controls.Add(this.lab_DyeingCode);
            this.grp_BrewingProcess.Controls.Add(this.btn_Copy);
            this.grp_BrewingProcess.Controls.Add(this.txt_Notes);
            this.grp_BrewingProcess.Controls.Add(this.label1);
            this.grp_BrewingProcess.Controls.Add(this.btn_DyeingProcessDelete);
            this.grp_BrewingProcess.Controls.Add(this.btn_DyeingProcessUpdate);
            this.grp_BrewingProcess.Controls.Add(this.btn_DyeingProcessAdd);
            this.grp_BrewingProcess.Controls.Add(this.dgv_Child_DyeData);
            this.grp_BrewingProcess.Controls.Add(this.btn_DyeingCodeDelete);
            this.grp_BrewingProcess.Controls.Add(this.btn_DyeingCodeAdd);
            this.grp_BrewingProcess.Controls.Add(this.txt_Dye_Code);
            this.grp_BrewingProcess.Controls.Add(this.lab_BrewProcessCode);
            resources.ApplyResources(this.grp_BrewingProcess, "grp_BrewingProcess");
            this.grp_BrewingProcess.Name = "grp_BrewingProcess";
            this.grp_BrewingProcess.TabStop = false;
            // 
            // btn_Insert
            // 
            resources.ApplyResources(this.btn_Insert, "btn_Insert");
            this.btn_Insert.Name = "btn_Insert";
            this.btn_Insert.UseVisualStyleBackColor = true;
            this.btn_Insert.Click += new System.EventHandler(this.btn_Insert_Click);
            // 
            // txt_Template
            // 
            resources.ApplyResources(this.txt_Template, "txt_Template");
            this.txt_Template.FormattingEnabled = true;
            this.txt_Template.Items.AddRange(new object[] {
            resources.GetString("txt_Template.Items"),
            resources.GetString("txt_Template.Items1"),
            resources.GetString("txt_Template.Items2"),
            resources.GetString("txt_Template.Items3")});
            this.txt_Template.Name = "txt_Template";
            this.txt_Template.SelectedIndexChanged += new System.EventHandler(this.txt_Template_SelectedIndexChanged);
            this.txt_Template.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Template_KeyDown);
            // 
            // lab_DyeingCode
            // 
            resources.ApplyResources(this.lab_DyeingCode, "lab_DyeingCode");
            this.lab_DyeingCode.Name = "lab_DyeingCode";
            // 
            // btn_Copy
            // 
            resources.ApplyResources(this.btn_Copy, "btn_Copy");
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.UseVisualStyleBackColor = true;
            this.btn_Copy.Click += new System.EventHandler(this.btn_Copy_Click);
            // 
            // dgv_Child_DyeData
            // 
            this.dgv_Child_DyeData.AllowUserToAddRows = false;
            this.dgv_Child_DyeData.AllowUserToDeleteRows = false;
            this.dgv_Child_DyeData.AllowUserToResizeColumns = false;
            this.dgv_Child_DyeData.AllowUserToResizeRows = false;
            this.dgv_Child_DyeData.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Child_DyeData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Child_DyeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_Child_DyeData, "dgv_Child_DyeData");
            this.dgv_Child_DyeData.MultiSelect = false;
            this.dgv_Child_DyeData.Name = "dgv_Child_DyeData";
            this.dgv_Child_DyeData.ReadOnly = true;
            this.dgv_Child_DyeData.RowHeadersVisible = false;
            this.dgv_Child_DyeData.RowTemplate.Height = 23;
            this.dgv_Child_DyeData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Child_DyeData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Child_DyeData_CellClick);
            this.dgv_Child_DyeData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_Child_DyeData_MouseClick);
            // 
            // PostTreatmentConfiguration
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_Dyeing);
            this.Controls.Add(this.grp_BrewingProcess);
            this.Name = "PostTreatmentConfiguration";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dye_Code)).EndInit();
            this.grp_Dyeing.ResumeLayout(false);
            this.grp_BrewingProcess.ResumeLayout(false);
            this.grp_BrewingProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Child_DyeData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Dye_Code;
        private System.Windows.Forms.Button btn_DyeingCodeDelete;
        private System.Windows.Forms.Button btn_DyeingCodeAdd;
        private System.Windows.Forms.TextBox txt_Dye_Code;
        private System.Windows.Forms.Label lab_BrewProcessCode;
        private System.Windows.Forms.GroupBox grp_Dyeing;
        private System.Windows.Forms.TextBox txt_Notes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_DyeingProcessDelete;
        private System.Windows.Forms.Button btn_DyeingProcessUpdate;
        private System.Windows.Forms.Button btn_DyeingProcessAdd;
        private System.Windows.Forms.GroupBox grp_BrewingProcess;
        private System.Windows.Forms.DataGridView dgv_Child_DyeData;
        private System.Windows.Forms.Button btn_Copy;
        private System.Windows.Forms.ComboBox txt_Template;
        private System.Windows.Forms.Label lab_DyeingCode;
        private System.Windows.Forms.Button btn_Insert;
    }
}
