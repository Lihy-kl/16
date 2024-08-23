using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class Run
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Run));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.dgv_Run = new System.Windows.Forms.DataGridView();
            this.bdn_Run = new System.Windows.Forms.BindingNavigator(this.components);
            this.tsbtn_ProcessFirstPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_ProcessUpPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tstxt_ProcessPageNow = new System.Windows.Forms.ToolStripTextBox();
            this.tslab = new System.Windows.Forms.ToolStripLabel();
            this.tslab_ProcessAllPage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_ProcessDownPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tslab_ProcessAllNum = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtn_ProcessEndPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.bds_Run = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Run)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdn_Run)).BeginInit();
            this.bdn_Run.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Run)).BeginInit();
            this.SuspendLayout();
            // 
            // tmr
            // 
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // dgv_Run
            // 
            resources.ApplyResources(this.dgv_Run, "dgv_Run");
            this.dgv_Run.AllowUserToAddRows = false;
            this.dgv_Run.AllowUserToDeleteRows = false;
            this.dgv_Run.AllowUserToResizeColumns = false;
            this.dgv_Run.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.dgv_Run.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Run.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Run.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Run.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Run.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Run.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Run.Name = "dgv_Run";
            this.dgv_Run.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_Run.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_Run.RowTemplate.Height = 27;
            // 
            // bdn_Run
            // 
            resources.ApplyResources(this.bdn_Run, "bdn_Run");
            this.bdn_Run.AddNewItem = null;
            this.bdn_Run.CountItem = null;
            this.bdn_Run.DeleteItem = null;
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
            this.bdn_Run.MoveFirstItem = null;
            this.bdn_Run.MoveLastItem = null;
            this.bdn_Run.MoveNextItem = null;
            this.bdn_Run.MovePreviousItem = null;
            this.bdn_Run.Name = "bdn_Run";
            this.bdn_Run.PositionItem = null;
            this.bdn_Run.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bdn_Run_ItemClicked);
            // 
            // tsbtn_ProcessFirstPage
            // 
            resources.ApplyResources(this.tsbtn_ProcessFirstPage, "tsbtn_ProcessFirstPage");
            this.tsbtn_ProcessFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_ProcessFirstPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_ProcessFirstPage.Name = "tsbtn_ProcessFirstPage";
            // 
            // toolStripSeparator8
            // 
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            // 
            // tsbtn_ProcessUpPage
            // 
            resources.ApplyResources(this.tsbtn_ProcessUpPage, "tsbtn_ProcessUpPage");
            this.tsbtn_ProcessUpPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_ProcessUpPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_ProcessUpPage.Name = "tsbtn_ProcessUpPage";
            // 
            // toolStripSeparator9
            // 
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            // 
            // tstxt_ProcessPageNow
            // 
            resources.ApplyResources(this.tstxt_ProcessPageNow, "tstxt_ProcessPageNow");
            this.tstxt_ProcessPageNow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tstxt_ProcessPageNow.Name = "tstxt_ProcessPageNow";
            this.tstxt_ProcessPageNow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstxt_ProcessPageNow_KeyPress);
            // 
            // tslab
            // 
            resources.ApplyResources(this.tslab, "tslab");
            this.tslab.Name = "tslab";
            // 
            // tslab_ProcessAllPage
            // 
            resources.ApplyResources(this.tslab_ProcessAllPage, "tslab_ProcessAllPage");
            this.tslab_ProcessAllPage.ForeColor = System.Drawing.Color.Red;
            this.tslab_ProcessAllPage.Name = "tslab_ProcessAllPage";
            // 
            // toolStripSeparator10
            // 
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            // 
            // tsbtn_ProcessDownPage
            // 
            resources.ApplyResources(this.tsbtn_ProcessDownPage, "tsbtn_ProcessDownPage");
            this.tsbtn_ProcessDownPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_ProcessDownPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_ProcessDownPage.Name = "tsbtn_ProcessDownPage";
            // 
            // toolStripSeparator11
            // 
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            // 
            // toolStripLabel3
            // 
            resources.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.Name = "toolStripLabel3";
            // 
            // tslab_ProcessAllNum
            // 
            resources.ApplyResources(this.tslab_ProcessAllNum, "tslab_ProcessAllNum");
            this.tslab_ProcessAllNum.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslab_ProcessAllNum.ForeColor = System.Drawing.Color.Red;
            this.tslab_ProcessAllNum.Name = "tslab_ProcessAllNum";
            // 
            // toolStripLabel5
            // 
            resources.ApplyResources(this.toolStripLabel5, "toolStripLabel5");
            this.toolStripLabel5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel5.Name = "toolStripLabel5";
            // 
            // tsbtn_ProcessEndPage
            // 
            resources.ApplyResources(this.tsbtn_ProcessEndPage, "tsbtn_ProcessEndPage");
            this.tsbtn_ProcessEndPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_ProcessEndPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_ProcessEndPage.Name = "tsbtn_ProcessEndPage";
            // 
            // toolStripSeparator12
            // 
            resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            // 
            // Run
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_Run);
            this.Controls.Add(this.bdn_Run);
            this.Name = "Run";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Run)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdn_Run)).EndInit();
            this.bdn_Run.ResumeLayout(false);
            this.bdn_Run.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Run)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmr;
        public System.Windows.Forms.DataGridView dgv_Run;
        public System.Windows.Forms.BindingNavigator bdn_Run;
        private System.Windows.Forms.ToolStripButton tsbtn_ProcessFirstPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbtn_ProcessUpPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        public System.Windows.Forms.ToolStripTextBox tstxt_ProcessPageNow;
        private System.Windows.Forms.ToolStripLabel tslab;
        public System.Windows.Forms.ToolStripLabel tslab_ProcessAllPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton tsbtn_ProcessDownPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        public System.Windows.Forms.ToolStripLabel tslab_ProcessAllNum;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton tsbtn_ProcessEndPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.BindingSource bds_Run;

    }
}
