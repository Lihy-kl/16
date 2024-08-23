using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class DyeingProcess
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
            this.grp_Browse = new System.Windows.Forms.GroupBox();
            this.txt_DyeingCode = new System.Windows.Forms.TextBox();
            this.btn_DyeingCodeDelete = new System.Windows.Forms.Button();
            this.lab_BrewProcessCode = new System.Windows.Forms.Label();
            this.btn_DyeingCodeAdd = new System.Windows.Forms.Button();
            this.dgv_DyeingCode = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.dgv_DyeDetails = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.gb_dye = new System.Windows.Forms.GroupBox();
            this.txt_Remark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_AddHandle = new System.Windows.Forms.Button();
            this.dgv_Dye_Code = new System.Windows.Forms.DataGridView();
            this.btn_DyeCodeDelete = new System.Windows.Forms.Button();
            this.btn_DyeCodeAdd = new System.Windows.Forms.Button();
            this.txt_Dye_Code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv_Child_DyeData = new System.Windows.Forms.DataGridView();
            this.btn_DyeingProcessAdd = new System.Windows.Forms.Button();
            this.btn_DyeingProcessUpdate = new System.Windows.Forms.Button();
            this.btn_DyeingProcessDelete = new System.Windows.Forms.Button();
            this.grp_Browse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DyeingCode)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DyeDetails)).BeginInit();
            this.gb_dye.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dye_Code)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Child_DyeData)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_Browse
            // 
            this.grp_Browse.Controls.Add(this.txt_DyeingCode);
            this.grp_Browse.Controls.Add(this.btn_DyeingCodeDelete);
            this.grp_Browse.Controls.Add(this.lab_BrewProcessCode);
            this.grp_Browse.Controls.Add(this.btn_DyeingCodeAdd);
            this.grp_Browse.Controls.Add(this.dgv_DyeingCode);
            this.grp_Browse.Font = new System.Drawing.Font("宋体", 10.5F);
            this.grp_Browse.Location = new System.Drawing.Point(0, 4);
            this.grp_Browse.Name = "grp_Browse";
            this.grp_Browse.Size = new System.Drawing.Size(452, 545);
            this.grp_Browse.TabIndex = 4;
            this.grp_Browse.TabStop = false;
            this.grp_Browse.Text = "浏览染固色流程代码";
            // 
            // txt_DyeingCode
            // 
            this.txt_DyeingCode.Enabled = false;
            this.txt_DyeingCode.Font = new System.Drawing.Font("宋体", 14.25F);
            this.txt_DyeingCode.Location = new System.Drawing.Point(229, 434);
            this.txt_DyeingCode.Name = "txt_DyeingCode";
            this.txt_DyeingCode.Size = new System.Drawing.Size(199, 29);
            this.txt_DyeingCode.TabIndex = 11;
            this.txt_DyeingCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_DyeingCode_KeyDown);
            // 
            // btn_DyeingCodeDelete
            // 
            this.btn_DyeingCodeDelete.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_DyeingCodeDelete.Location = new System.Drawing.Point(230, 479);
            this.btn_DyeingCodeDelete.Name = "btn_DyeingCodeDelete";
            this.btn_DyeingCodeDelete.Size = new System.Drawing.Size(198, 60);
            this.btn_DyeingCodeDelete.TabIndex = 5;
            this.btn_DyeingCodeDelete.Text = "删除";
            this.btn_DyeingCodeDelete.UseVisualStyleBackColor = true;
            this.btn_DyeingCodeDelete.Click += new System.EventHandler(this.btn_DyeingCodeDelete_Click);
            // 
            // lab_BrewProcessCode
            // 
            this.lab_BrewProcessCode.Font = new System.Drawing.Font("宋体", 14.25F);
            this.lab_BrewProcessCode.Location = new System.Drawing.Point(29, 426);
            this.lab_BrewProcessCode.Name = "lab_BrewProcessCode";
            this.lab_BrewProcessCode.Size = new System.Drawing.Size(192, 39);
            this.lab_BrewProcessCode.TabIndex = 10;
            this.lab_BrewProcessCode.Text = "染固色流程代码:";
            this.lab_BrewProcessCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_DyeingCodeAdd
            // 
            this.btn_DyeingCodeAdd.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_DyeingCodeAdd.Location = new System.Drawing.Point(21, 479);
            this.btn_DyeingCodeAdd.Name = "btn_DyeingCodeAdd";
            this.btn_DyeingCodeAdd.Size = new System.Drawing.Size(200, 60);
            this.btn_DyeingCodeAdd.TabIndex = 4;
            this.btn_DyeingCodeAdd.Text = "新增";
            this.btn_DyeingCodeAdd.UseVisualStyleBackColor = true;
            this.btn_DyeingCodeAdd.Click += new System.EventHandler(this.btn_DyeingCodeAdd_Click);
            // 
            // dgv_DyeingCode
            // 
            this.dgv_DyeingCode.AllowUserToAddRows = false;
            this.dgv_DyeingCode.AllowUserToDeleteRows = false;
            this.dgv_DyeingCode.AllowUserToResizeColumns = false;
            this.dgv_DyeingCode.AllowUserToResizeRows = false;
            this.dgv_DyeingCode.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_DyeingCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DyeingCode.Location = new System.Drawing.Point(4, 20);
            this.dgv_DyeingCode.MultiSelect = false;
            this.dgv_DyeingCode.Name = "dgv_DyeingCode";
            this.dgv_DyeingCode.ReadOnly = true;
            this.dgv_DyeingCode.RowHeadersVisible = false;
            this.dgv_DyeingCode.RowHeadersWidth = 62;
            this.dgv_DyeingCode.RowTemplate.Height = 23;
            this.dgv_DyeingCode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DyeingCode.Size = new System.Drawing.Size(444, 403);
            this.dgv_DyeingCode.TabIndex = 0;
            this.dgv_DyeingCode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DyeingCode_CellClick);
            this.dgv_DyeingCode.CurrentCellChanged += new System.EventHandler(this.dgv_DyeingCode_CurrentCellChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Save);
            this.groupBox1.Controls.Add(this.dgv_DyeDetails);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.groupBox1.Location = new System.Drawing.Point(4, 555);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 420);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "染固色流程代码详情";
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_Save.Location = new System.Drawing.Point(104, 352);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(200, 60);
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // dgv_DyeDetails
            // 
            this.dgv_DyeDetails.AllowUserToResizeColumns = false;
            this.dgv_DyeDetails.AllowUserToResizeRows = false;
            this.dgv_DyeDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_DyeDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DyeDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgv_DyeDetails.Location = new System.Drawing.Point(4, 20);
            this.dgv_DyeDetails.MultiSelect = false;
            this.dgv_DyeDetails.Name = "dgv_DyeDetails";
            this.dgv_DyeDetails.RowHeadersVisible = false;
            this.dgv_DyeDetails.RowHeadersWidth = 62;
            this.dgv_DyeDetails.RowTemplate.Height = 23;
            this.dgv_DyeDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_DyeDetails.Size = new System.Drawing.Size(440, 324);
            this.dgv_DyeDetails.TabIndex = 0;
            this.dgv_DyeDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DyeDetails_CellClick);
            this.dgv_DyeDetails.CurrentCellChanged += new System.EventHandler(this.dgv_DyeDetails_CurrentCellChanged);
            this.dgv_DyeDetails.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_DyeDetails_RowsAdded);
            this.dgv_DyeDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_DyeDetails_KeyDown);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Column1.HeaderText = "流程代码";
            this.Column1.Name = "Column1";
            // 
            // gb_dye
            // 
            this.gb_dye.Controls.Add(this.txt_Remark);
            this.gb_dye.Controls.Add(this.label1);
            this.gb_dye.Controls.Add(this.btn_AddHandle);
            this.gb_dye.Controls.Add(this.dgv_Dye_Code);
            this.gb_dye.Controls.Add(this.btn_DyeCodeDelete);
            this.gb_dye.Controls.Add(this.btn_DyeCodeAdd);
            this.gb_dye.Controls.Add(this.txt_Dye_Code);
            this.gb_dye.Controls.Add(this.label2);
            this.gb_dye.Controls.Add(this.dgv_Child_DyeData);
            this.gb_dye.Controls.Add(this.btn_DyeingProcessAdd);
            this.gb_dye.Controls.Add(this.btn_DyeingProcessUpdate);
            this.gb_dye.Controls.Add(this.btn_DyeingProcessDelete);
            this.gb_dye.Location = new System.Drawing.Point(463, 4);
            this.gb_dye.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_dye.Name = "gb_dye";
            this.gb_dye.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_dye.Size = new System.Drawing.Size(1438, 971);
            this.gb_dye.TabIndex = 9;
            this.gb_dye.TabStop = false;
            this.gb_dye.Text = "工艺";
            // 
            // txt_Remark
            // 
            this.txt_Remark.Enabled = false;
            this.txt_Remark.Font = new System.Drawing.Font("宋体", 14.25F);
            this.txt_Remark.Location = new System.Drawing.Point(687, 26);
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(161, 29);
            this.txt_Remark.TabIndex = 16;
            this.txt_Remark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Remark_KeyDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F);
            this.label1.Location = new System.Drawing.Point(554, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 38);
            this.label1.TabIndex = 15;
            this.label1.Text = "备注:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_AddHandle
            // 
            this.btn_AddHandle.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_AddHandle.Location = new System.Drawing.Point(1013, 17);
            this.btn_AddHandle.Name = "btn_AddHandle";
            this.btn_AddHandle.Size = new System.Drawing.Size(160, 60);
            this.btn_AddHandle.TabIndex = 14;
            this.btn_AddHandle.Text = "新增后处理工艺";
            this.btn_AddHandle.UseVisualStyleBackColor = true;
            this.btn_AddHandle.Click += new System.EventHandler(this.btn_AddHandle_Click);
            // 
            // dgv_Dye_Code
            // 
            this.dgv_Dye_Code.AllowUserToAddRows = false;
            this.dgv_Dye_Code.AllowUserToDeleteRows = false;
            this.dgv_Dye_Code.AllowUserToResizeColumns = false;
            this.dgv_Dye_Code.AllowUserToResizeRows = false;
            this.dgv_Dye_Code.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Dye_Code.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Dye_Code.Location = new System.Drawing.Point(6, 26);
            this.dgv_Dye_Code.MultiSelect = false;
            this.dgv_Dye_Code.Name = "dgv_Dye_Code";
            this.dgv_Dye_Code.ReadOnly = true;
            this.dgv_Dye_Code.RowHeadersVisible = false;
            this.dgv_Dye_Code.RowHeadersWidth = 62;
            this.dgv_Dye_Code.RowTemplate.Height = 23;
            this.dgv_Dye_Code.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv_Dye_Code.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Dye_Code.Size = new System.Drawing.Size(217, 869);
            this.dgv_Dye_Code.TabIndex = 13;
            this.dgv_Dye_Code.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Dye_Code_CellClick);
            this.dgv_Dye_Code.CurrentCellChanged += new System.EventHandler(this.dgv_Dye_Code_CurrentCellChanged);
            // 
            // btn_DyeCodeDelete
            // 
            this.btn_DyeCodeDelete.Enabled = false;
            this.btn_DyeCodeDelete.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_DyeCodeDelete.Location = new System.Drawing.Point(1179, 17);
            this.btn_DyeCodeDelete.Name = "btn_DyeCodeDelete";
            this.btn_DyeCodeDelete.Size = new System.Drawing.Size(150, 60);
            this.btn_DyeCodeDelete.TabIndex = 12;
            this.btn_DyeCodeDelete.Text = "删除工艺";
            this.btn_DyeCodeDelete.UseVisualStyleBackColor = true;
            this.btn_DyeCodeDelete.Click += new System.EventHandler(this.btn_DyeCodeDelete_Click);
            // 
            // btn_DyeCodeAdd
            // 
            this.btn_DyeCodeAdd.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_DyeCodeAdd.Location = new System.Drawing.Point(866, 17);
            this.btn_DyeCodeAdd.Name = "btn_DyeCodeAdd";
            this.btn_DyeCodeAdd.Size = new System.Drawing.Size(138, 60);
            this.btn_DyeCodeAdd.TabIndex = 11;
            this.btn_DyeCodeAdd.Text = "新增染色工艺";
            this.btn_DyeCodeAdd.UseVisualStyleBackColor = true;
            this.btn_DyeCodeAdd.Click += new System.EventHandler(this.btn_DyeCodeAdd_Click);
            // 
            // txt_Dye_Code
            // 
            this.txt_Dye_Code.Enabled = false;
            this.txt_Dye_Code.Font = new System.Drawing.Font("宋体", 14.25F);
            this.txt_Dye_Code.Location = new System.Drawing.Point(364, 26);
            this.txt_Dye_Code.Name = "txt_Dye_Code";
            this.txt_Dye_Code.Size = new System.Drawing.Size(182, 29);
            this.txt_Dye_Code.TabIndex = 10;
            this.txt_Dye_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Dye_Code_KeyDown);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F);
            this.label2.Location = new System.Drawing.Point(231, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 38);
            this.label2.TabIndex = 9;
            this.label2.Text = "工艺代码:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgv_Child_DyeData
            // 
            this.dgv_Child_DyeData.AllowUserToAddRows = false;
            this.dgv_Child_DyeData.AllowUserToDeleteRows = false;
            this.dgv_Child_DyeData.AllowUserToResizeColumns = false;
            this.dgv_Child_DyeData.AllowUserToResizeRows = false;
            this.dgv_Child_DyeData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Child_DyeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Child_DyeData.Location = new System.Drawing.Point(278, 94);
            this.dgv_Child_DyeData.MultiSelect = false;
            this.dgv_Child_DyeData.Name = "dgv_Child_DyeData";
            this.dgv_Child_DyeData.ReadOnly = true;
            this.dgv_Child_DyeData.RowHeadersVisible = false;
            this.dgv_Child_DyeData.RowHeadersWidth = 62;
            this.dgv_Child_DyeData.RowTemplate.Height = 23;
            this.dgv_Child_DyeData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_Child_DyeData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Child_DyeData.Size = new System.Drawing.Size(1051, 798);
            this.dgv_Child_DyeData.TabIndex = 5;
            // 
            // btn_DyeingProcessAdd
            // 
            this.btn_DyeingProcessAdd.Enabled = false;
            this.btn_DyeingProcessAdd.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_DyeingProcessAdd.Location = new System.Drawing.Point(1335, 94);
            this.btn_DyeingProcessAdd.Name = "btn_DyeingProcessAdd";
            this.btn_DyeingProcessAdd.Size = new System.Drawing.Size(97, 60);
            this.btn_DyeingProcessAdd.TabIndex = 5;
            this.btn_DyeingProcessAdd.Text = "添加";
            this.btn_DyeingProcessAdd.UseVisualStyleBackColor = true;
            this.btn_DyeingProcessAdd.Click += new System.EventHandler(this.btn_DyeingProcessAdd_Click);
            // 
            // btn_DyeingProcessUpdate
            // 
            this.btn_DyeingProcessUpdate.Enabled = false;
            this.btn_DyeingProcessUpdate.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_DyeingProcessUpdate.Location = new System.Drawing.Point(1335, 178);
            this.btn_DyeingProcessUpdate.Name = "btn_DyeingProcessUpdate";
            this.btn_DyeingProcessUpdate.Size = new System.Drawing.Size(97, 60);
            this.btn_DyeingProcessUpdate.TabIndex = 6;
            this.btn_DyeingProcessUpdate.Text = "修改";
            this.btn_DyeingProcessUpdate.UseVisualStyleBackColor = true;
            this.btn_DyeingProcessUpdate.Click += new System.EventHandler(this.btn_DyeingProcessUpdate_Click);
            // 
            // btn_DyeingProcessDelete
            // 
            this.btn_DyeingProcessDelete.Enabled = false;
            this.btn_DyeingProcessDelete.Font = new System.Drawing.Font("宋体", 14.25F);
            this.btn_DyeingProcessDelete.Location = new System.Drawing.Point(1335, 262);
            this.btn_DyeingProcessDelete.Name = "btn_DyeingProcessDelete";
            this.btn_DyeingProcessDelete.Size = new System.Drawing.Size(97, 60);
            this.btn_DyeingProcessDelete.TabIndex = 7;
            this.btn_DyeingProcessDelete.Text = "删除";
            this.btn_DyeingProcessDelete.UseVisualStyleBackColor = true;
            this.btn_DyeingProcessDelete.Click += new System.EventHandler(this.btn_DyeingProcessDelete_Click);
            // 
            // DyeingProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gb_dye);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grp_Browse);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DyeingProcess";
            this.Size = new System.Drawing.Size(1904, 980);
            this.grp_Browse.ResumeLayout(false);
            this.grp_Browse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DyeingCode)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DyeDetails)).EndInit();
            this.gb_dye.ResumeLayout(false);
            this.gb_dye.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Dye_Code)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Child_DyeData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grp_Browse;
        private DataGridView dgv_DyeingCode;
        private Button btn_DyeingCodeDelete;
        private Button btn_DyeingCodeAdd;
        private GroupBox groupBox1;
        private DataGridView dgv_DyeDetails;
        private GroupBox gb_dye;
        private DataGridView dgv_Dye_Code;
        private Button btn_DyeCodeDelete;
        private Button btn_DyeCodeAdd;
        private TextBox txt_Dye_Code;
        private Label label2;
        private DataGridView dgv_Child_DyeData;
        private Button btn_DyeingProcessAdd;
        private Button btn_DyeingProcessUpdate;
        private Button btn_DyeingProcessDelete;
        private Button btn_Save;
        private TextBox txt_DyeingCode;
        private Label lab_BrewProcessCode;
        private Button btn_AddHandle;
        private DataGridViewComboBoxColumn Column1;
        private TextBox txt_Remark;
        private Label label1;
    }
}
