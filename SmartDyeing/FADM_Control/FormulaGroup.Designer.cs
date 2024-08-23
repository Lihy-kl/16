namespace SmartDyeing.FADM_Control
{
    partial class FormulaGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormulaGroup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_FormulaGroup = new SmartDyeing.FADM_Object.MyDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_FormulaCodeAdd = new System.Windows.Forms.Button();
            this.txt_GroupName = new System.Windows.Forms.TextBox();
            this.lab_FormulaName = new System.Windows.Forms.Label();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssistantCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssistantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_Assistant = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FG_dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaGroup)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Assistant)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FG_dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.dgv_FormulaGroup);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btn_Save);
            this.groupBox3.Controls.Add(this.btn_FormulaCodeAdd);
            this.groupBox3.Controls.Add(this.txt_GroupName);
            this.groupBox3.Controls.Add(this.lab_FormulaName);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // dgv_FormulaGroup
            // 
            resources.ApplyResources(this.dgv_FormulaGroup, "dgv_FormulaGroup");
            this.dgv_FormulaGroup.AllowUserToDeleteRows = false;
            this.dgv_FormulaGroup.AllowUserToResizeColumns = false;
            this.dgv_FormulaGroup.AllowUserToResizeRows = false;
            this.dgv_FormulaGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_FormulaGroup.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv_FormulaGroup.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_FormulaGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_FormulaGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FormulaGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_FormulaGroup.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_FormulaGroup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_FormulaGroup.MultiSelect = false;
            this.dgv_FormulaGroup.Name = "dgv_FormulaGroup";
            this.dgv_FormulaGroup.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dgv_FormulaGroup.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_FormulaGroup.RowTemplate.Height = 30;
            this.dgv_FormulaGroup.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_FormulaGroup.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_FormulaGroup_RowsAdded);
            this.dgv_FormulaGroup.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgv_FormulaGroup_RowsRemoved);
            this.dgv_FormulaGroup.SelectionChanged += new System.EventHandler(this.dgv_FormulaGroup_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 77.02372F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 104.9278F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 152.5202F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Save
            // 
            resources.ApplyResources(this.btn_Save, "btn_Save");
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_FormulaCodeAdd
            // 
            resources.ApplyResources(this.btn_FormulaCodeAdd, "btn_FormulaCodeAdd");
            this.btn_FormulaCodeAdd.Name = "btn_FormulaCodeAdd";
            this.btn_FormulaCodeAdd.UseVisualStyleBackColor = true;
            this.btn_FormulaCodeAdd.Click += new System.EventHandler(this.btn_FormulaCodeAdd_Click);
            // 
            // txt_GroupName
            // 
            resources.ApplyResources(this.txt_GroupName, "txt_GroupName");
            this.txt_GroupName.Name = "txt_GroupName";
            this.txt_GroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_GroupName_KeyDown);
            // 
            // lab_FormulaName
            // 
            resources.ApplyResources(this.lab_FormulaName, "lab_FormulaName");
            this.lab_FormulaName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_FormulaName.Name = "lab_FormulaName";
            // 
            // Index
            // 
            this.Index.FillWeight = 55.05777F;
            resources.ApplyResources(this.Index, "Index");
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AssistantCode
            // 
            this.AssistantCode.FillWeight = 92.54126F;
            resources.ApplyResources(this.AssistantCode, "AssistantCode");
            this.AssistantCode.Name = "AssistantCode";
            this.AssistantCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AssistantName
            // 
            this.AssistantName.FillWeight = 134.5157F;
            resources.ApplyResources(this.AssistantName, "AssistantName");
            this.AssistantName.Name = "AssistantName";
            this.AssistantName.ReadOnly = true;
            this.AssistantName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.dgv_Assistant);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // dgv_Assistant
            // 
            resources.ApplyResources(this.dgv_Assistant, "dgv_Assistant");
            this.dgv_Assistant.AllowUserToAddRows = false;
            this.dgv_Assistant.AllowUserToDeleteRows = false;
            this.dgv_Assistant.AllowUserToResizeColumns = false;
            this.dgv_Assistant.AllowUserToResizeRows = false;
            this.dgv_Assistant.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Assistant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Assistant.MultiSelect = false;
            this.dgv_Assistant.Name = "dgv_Assistant";
            this.dgv_Assistant.ReadOnly = true;
            this.dgv_Assistant.RowHeadersVisible = false;
            this.dgv_Assistant.RowTemplate.Height = 23;
            this.dgv_Assistant.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.FG_dataGridView1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // FG_dataGridView1
            // 
            resources.ApplyResources(this.FG_dataGridView1, "FG_dataGridView1");
            this.FG_dataGridView1.AllowUserToAddRows = false;
            this.FG_dataGridView1.AllowUserToDeleteRows = false;
            this.FG_dataGridView1.AllowUserToResizeColumns = false;
            this.FG_dataGridView1.AllowUserToResizeRows = false;
            this.FG_dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.FG_dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FG_dataGridView1.MultiSelect = false;
            this.FG_dataGridView1.Name = "FG_dataGridView1";
            this.FG_dataGridView1.ReadOnly = true;
            this.FG_dataGridView1.RowHeadersVisible = false;
            this.FG_dataGridView1.RowTemplate.Height = 23;
            this.FG_dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FG_dataGridView1.CurrentCellChanged += new System.EventHandler(this.FG_dataGridView1_CurrentCellChanged);
            // 
            // FormulaGroup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "FormulaGroup";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FormulaGroup)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Assistant)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FG_dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_FormulaCodeAdd;
        private System.Windows.Forms.TextBox txt_GroupName;
        private System.Windows.Forms.Label lab_FormulaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssistantCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssistantName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_Assistant;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView FG_dataGridView1;
        private FADM_Object.MyDataGridView dgv_FormulaGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}
