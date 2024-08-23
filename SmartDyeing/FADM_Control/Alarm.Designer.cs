using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class Alarm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alarm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.dgv_Alarm = new System.Windows.Forms.DataGridView();
            this.bdn_Alarm = new System.Windows.Forms.BindingNavigator(this.components);
            this.tsbtn_AlarmFirstPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_AlarmUpPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tstxt_AlarmPageNow = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tslab_AlarmAllPage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_AlarmDownPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tslab_AlarmAllNum = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtn_AlarmEndPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.bds_Alarm = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Alarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdn_Alarm)).BeginInit();
            this.bdn_Alarm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Alarm)).BeginInit();
            this.SuspendLayout();
            // 
            // tmr
            // 
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // dgv_Alarm
            // 
            resources.ApplyResources(this.dgv_Alarm, "dgv_Alarm");
            this.dgv_Alarm.AllowUserToAddRows = false;
            this.dgv_Alarm.AllowUserToDeleteRows = false;
            this.dgv_Alarm.AllowUserToResizeColumns = false;
            this.dgv_Alarm.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.dgv_Alarm.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Alarm.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Alarm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Alarm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Alarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Alarm.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Alarm.Name = "dgv_Alarm";
            this.dgv_Alarm.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_Alarm.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_Alarm.RowTemplate.Height = 27;
            // 
            // bdn_Alarm
            // 
            resources.ApplyResources(this.bdn_Alarm, "bdn_Alarm");
            this.bdn_Alarm.AddNewItem = null;
            this.bdn_Alarm.CountItem = null;
            this.bdn_Alarm.DeleteItem = null;
            this.bdn_Alarm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_AlarmFirstPage,
            this.toolStripSeparator1,
            this.tsbtn_AlarmUpPage,
            this.toolStripSeparator2,
            this.tstxt_AlarmPageNow,
            this.toolStripLabel1,
            this.tslab_AlarmAllPage,
            this.toolStripSeparator3,
            this.tsbtn_AlarmDownPage,
            this.toolStripSeparator13,
            this.tslab_AlarmAllNum,
            this.toolStripLabel9,
            this.tsbtn_AlarmEndPage,
            this.toolStripSeparator14});
            this.bdn_Alarm.MoveFirstItem = null;
            this.bdn_Alarm.MoveLastItem = null;
            this.bdn_Alarm.MoveNextItem = null;
            this.bdn_Alarm.MovePreviousItem = null;
            this.bdn_Alarm.Name = "bdn_Alarm";
            this.bdn_Alarm.PositionItem = null;
            this.bdn_Alarm.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bdn_Alarm_ItemClicked);
            // 
            // tsbtn_AlarmFirstPage
            // 
            resources.ApplyResources(this.tsbtn_AlarmFirstPage, "tsbtn_AlarmFirstPage");
            this.tsbtn_AlarmFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_AlarmFirstPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_AlarmFirstPage.Name = "tsbtn_AlarmFirstPage";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // tsbtn_AlarmUpPage
            // 
            resources.ApplyResources(this.tsbtn_AlarmUpPage, "tsbtn_AlarmUpPage");
            this.tsbtn_AlarmUpPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_AlarmUpPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_AlarmUpPage.Name = "tsbtn_AlarmUpPage";
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // tstxt_AlarmPageNow
            // 
            resources.ApplyResources(this.tstxt_AlarmPageNow, "tstxt_AlarmPageNow");
            this.tstxt_AlarmPageNow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tstxt_AlarmPageNow.Name = "tstxt_AlarmPageNow";
            this.tstxt_AlarmPageNow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstxt_AlarmPageNow_KeyPress);
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // tslab_AlarmAllPage
            // 
            resources.ApplyResources(this.tslab_AlarmAllPage, "tslab_AlarmAllPage");
            this.tslab_AlarmAllPage.ForeColor = System.Drawing.Color.Red;
            this.tslab_AlarmAllPage.Name = "tslab_AlarmAllPage";
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // tsbtn_AlarmDownPage
            // 
            resources.ApplyResources(this.tsbtn_AlarmDownPage, "tsbtn_AlarmDownPage");
            this.tsbtn_AlarmDownPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_AlarmDownPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_AlarmDownPage.Name = "tsbtn_AlarmDownPage";
            // 
            // toolStripSeparator13
            // 
            resources.ApplyResources(this.toolStripSeparator13, "toolStripSeparator13");
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            // 
            // tslab_AlarmAllNum
            // 
            resources.ApplyResources(this.tslab_AlarmAllNum, "tslab_AlarmAllNum");
            this.tslab_AlarmAllNum.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslab_AlarmAllNum.ForeColor = System.Drawing.Color.Red;
            this.tslab_AlarmAllNum.Name = "tslab_AlarmAllNum";
            // 
            // toolStripLabel9
            // 
            resources.ApplyResources(this.toolStripLabel9, "toolStripLabel9");
            this.toolStripLabel9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel9.Name = "toolStripLabel9";
            // 
            // tsbtn_AlarmEndPage
            // 
            resources.ApplyResources(this.tsbtn_AlarmEndPage, "tsbtn_AlarmEndPage");
            this.tsbtn_AlarmEndPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_AlarmEndPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tsbtn_AlarmEndPage.Name = "tsbtn_AlarmEndPage";
            // 
            // toolStripSeparator14
            // 
            resources.ApplyResources(this.toolStripSeparator14, "toolStripSeparator14");
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            // 
            // Alarm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_Alarm);
            this.Controls.Add(this.bdn_Alarm);
            this.Name = "Alarm";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Alarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdn_Alarm)).EndInit();
            this.bdn_Alarm.ResumeLayout(false);
            this.bdn_Alarm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Alarm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmr;
        public System.Windows.Forms.DataGridView dgv_Alarm;
        public System.Windows.Forms.BindingNavigator bdn_Alarm;
        private System.Windows.Forms.ToolStripButton tsbtn_AlarmFirstPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtn_AlarmUpPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripTextBox tstxt_AlarmPageNow;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.ToolStripLabel tslab_AlarmAllPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtn_AlarmDownPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        public System.Windows.Forms.ToolStripLabel tslab_AlarmAllNum;
        private System.Windows.Forms.ToolStripLabel toolStripLabel9;
        private System.Windows.Forms.ToolStripButton tsbtn_AlarmEndPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.BindingSource bds_Alarm;





    }
}
