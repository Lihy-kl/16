namespace SmartDyeing.FADM_Control
{
    partial class BrewRun
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bds_Run = new System.Windows.Forms.BindingSource(this.components);
            this.tsbtn_ProcessEndPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tslab_ProcessAllNum = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_ProcessDownPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tslab_ProcessAllPage = new System.Windows.Forms.ToolStripLabel();
            this.tstxt_ProcessPageNow = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_ProcessUpPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_ProcessFirstPage = new System.Windows.Forms.ToolStripButton();
            this.dgv_Run = new System.Windows.Forms.DataGridView();
            this.bdn_Run = new System.Windows.Forms.BindingNavigator(this.components);
            this.tslab = new System.Windows.Forms.ToolStripLabel();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bds_Run)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Run)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdn_Run)).BeginInit();
            this.bdn_Run.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsbtn_ProcessEndPage
            // 
            this.tsbtn_ProcessEndPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_ProcessEndPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_ProcessEndPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_ProcessEndPage.Name = "tsbtn_ProcessEndPage";
            this.tsbtn_ProcessEndPage.Size = new System.Drawing.Size(51, 32);
            this.tsbtn_ProcessEndPage.Text = "尾页";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(28, 32);
            this.toolStripLabel5.Text = "共";
            // 
            // tslab_ProcessAllNum
            // 
            this.tslab_ProcessAllNum.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslab_ProcessAllNum.ForeColor = System.Drawing.Color.Red;
            this.tslab_ProcessAllNum.Name = "tslab_ProcessAllNum";
            this.tslab_ProcessAllNum.Size = new System.Drawing.Size(19, 32);
            this.tslab_ProcessAllNum.Text = "0";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(58, 32);
            this.toolStripLabel3.Text = "条   ";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbtn_ProcessDownPage
            // 
            this.tsbtn_ProcessDownPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_ProcessDownPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_ProcessDownPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_ProcessDownPage.Name = "tsbtn_ProcessDownPage";
            this.tsbtn_ProcessDownPage.Size = new System.Drawing.Size(70, 32);
            this.tsbtn_ProcessDownPage.Text = "下一页";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 35);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 35);
            // 
            // tslab_ProcessAllPage
            // 
            this.tslab_ProcessAllPage.ForeColor = System.Drawing.Color.Red;
            this.tslab_ProcessAllPage.Name = "tslab_ProcessAllPage";
            this.tslab_ProcessAllPage.Size = new System.Drawing.Size(19, 32);
            this.tslab_ProcessAllPage.Text = "0";
            // 
            // tstxt_ProcessPageNow
            // 
            this.tstxt_ProcessPageNow.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.tstxt_ProcessPageNow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tstxt_ProcessPageNow.Name = "tstxt_ProcessPageNow";
            this.tstxt_ProcessPageNow.Size = new System.Drawing.Size(76, 35);
            this.tstxt_ProcessPageNow.Text = "0";
            this.tstxt_ProcessPageNow.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tstxt_ProcessPageNow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstxt_ProcessPageNow_KeyPress);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbtn_ProcessUpPage
            // 
            this.tsbtn_ProcessUpPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_ProcessUpPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_ProcessUpPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_ProcessUpPage.Name = "tsbtn_ProcessUpPage";
            this.tsbtn_ProcessUpPage.Size = new System.Drawing.Size(70, 32);
            this.tsbtn_ProcessUpPage.Text = "上一页";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbtn_ProcessFirstPage
            // 
            this.tsbtn_ProcessFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_ProcessFirstPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_ProcessFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_ProcessFirstPage.Name = "tsbtn_ProcessFirstPage";
            this.tsbtn_ProcessFirstPage.Size = new System.Drawing.Size(51, 32);
            this.tsbtn_ProcessFirstPage.Text = "首页";
            // 
            // dgv_Run
            // 
            this.dgv_Run.AllowUserToAddRows = false;
            this.dgv_Run.AllowUserToDeleteRows = false;
            this.dgv_Run.AllowUserToResizeColumns = false;
            this.dgv_Run.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Gainsboro;
            this.dgv_Run.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgv_Run.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Run.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Run.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgv_Run.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Run.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgv_Run.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Run.Enabled = false;
            this.dgv_Run.Location = new System.Drawing.Point(0, 35);
            this.dgv_Run.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_Run.Name = "dgv_Run";
            this.dgv_Run.RowHeadersVisible = false;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_Run.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgv_Run.RowTemplate.Height = 27;
            this.dgv_Run.Size = new System.Drawing.Size(1904, 945);
            this.dgv_Run.TabIndex = 6;
            // 
            // bdn_Run
            // 
            this.bdn_Run.AddNewItem = null;
            this.bdn_Run.CountItem = null;
            this.bdn_Run.DeleteItem = null;
            this.bdn_Run.Font = new System.Drawing.Font("宋体", 14.25F);
            this.bdn_Run.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_ProcessFirstPage,
            this.toolStripSeparator8,
            this.tsbtn_ProcessUpPage,
            this.toolStripSeparator9,
            this.tstxt_ProcessPageNow,
            this.tslab,
            this.tslab_ProcessAllPage,
            this.toolStripSeparator10,
            this.tsbtn_ProcessDownPage,
            this.toolStripSeparator11,
            this.toolStripLabel3,
            this.tslab_ProcessAllNum,
            this.toolStripLabel5,
            this.tsbtn_ProcessEndPage,
            this.toolStripSeparator12});
            this.bdn_Run.Location = new System.Drawing.Point(0, 0);
            this.bdn_Run.MoveFirstItem = null;
            this.bdn_Run.MoveLastItem = null;
            this.bdn_Run.MoveNextItem = null;
            this.bdn_Run.MovePreviousItem = null;
            this.bdn_Run.Name = "bdn_Run";
            this.bdn_Run.PositionItem = null;
            this.bdn_Run.Size = new System.Drawing.Size(1904, 35);
            this.bdn_Run.TabIndex = 5;
            this.bdn_Run.Text = "1";
            this.bdn_Run.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bdn_Run_ItemClicked);
            // 
            // tslab
            // 
            this.tslab.Name = "tslab";
            this.tslab.Size = new System.Drawing.Size(19, 32);
            this.tslab.Text = "/";
            // 
            // tmr
            // 
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // BrewRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_Run);
            this.Controls.Add(this.bdn_Run);
            this.Name = "BrewRun";
            this.Size = new System.Drawing.Size(1904, 980);
            ((System.ComponentModel.ISupportInitialize)(this.bds_Run)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Run)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdn_Run)).EndInit();
            this.bdn_Run.ResumeLayout(false);
            this.bdn_Run.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bds_Run;
        private System.Windows.Forms.ToolStripButton tsbtn_ProcessEndPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        public System.Windows.Forms.ToolStripLabel tslab_ProcessAllNum;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton tsbtn_ProcessDownPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        public System.Windows.Forms.ToolStripLabel tslab_ProcessAllPage;
        public System.Windows.Forms.ToolStripTextBox tstxt_ProcessPageNow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton tsbtn_ProcessUpPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbtn_ProcessFirstPage;
        public System.Windows.Forms.DataGridView dgv_Run;
        public System.Windows.Forms.BindingNavigator bdn_Run;
        private System.Windows.Forms.ToolStripLabel tslab;
        private System.Windows.Forms.Timer tmr;
    }
}
