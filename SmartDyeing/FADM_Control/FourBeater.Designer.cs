using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class FourBeater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FourBeater));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_AllOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_AllOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cup1 = new SmartDyeing.FADM_Control.Cup();
            this.cup2 = new SmartDyeing.FADM_Control.Cup();
            this.cup3 = new SmartDyeing.FADM_Control.Cup();
            this.cup4 = new SmartDyeing.FADM_Control.Cup();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_Online = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Offline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_IsFix = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_HighWash = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.ContextMenuStrip = this.contextMenuStrip2;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cup1);
            this.groupBox1.Controls.Add(this.cup2);
            this.groupBox1.Controls.Add(this.cup3);
            this.groupBox1.Controls.Add(this.cup4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_AllOnline,
            this.tsm_AllOffline});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            // 
            // tsm_AllOnline
            // 
            resources.ApplyResources(this.tsm_AllOnline, "tsm_AllOnline");
            this.tsm_AllOnline.Name = "tsm_AllOnline";
            this.tsm_AllOnline.Click += new System.EventHandler(this.tsm_AllOnline_Click);
            // 
            // tsm_AllOffline
            // 
            resources.ApplyResources(this.tsm_AllOffline, "tsm_AllOffline");
            this.tsm_AllOffline.Name = "tsm_AllOffline";
            this.tsm_AllOffline.Click += new System.EventHandler(this.tsm_AllOffline_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.AutoEllipsis = true;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.AutoEllipsis = true;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.AutoEllipsis = true;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.AutoEllipsis = true;
            this.label4.Name = "label4";
            // 
            // cup1
            // 
            resources.ApplyResources(this.cup1, "cup1");
            this.cup1.BackColor = System.Drawing.Color.Transparent;
            this.cup1.BottleColor = System.Drawing.Color.Black;
            this.cup1.LiquidColor = System.Drawing.Color.Transparent;
            this.cup1.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cup1.Name = "cup1";
            this.cup1.NO = "1";
            this.cup1.Title = "";
            this.cup1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cup2
            // 
            resources.ApplyResources(this.cup2, "cup2");
            this.cup2.BackColor = System.Drawing.Color.Transparent;
            this.cup2.BottleColor = System.Drawing.Color.Black;
            this.cup2.LiquidColor = System.Drawing.Color.Transparent;
            this.cup2.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cup2.Name = "cup2";
            this.cup2.NO = "2";
            this.cup2.Title = "";
            this.cup2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cup3
            // 
            resources.ApplyResources(this.cup3, "cup3");
            this.cup3.BackColor = System.Drawing.Color.Transparent;
            this.cup3.BottleColor = System.Drawing.Color.Black;
            this.cup3.LiquidColor = System.Drawing.Color.Transparent;
            this.cup3.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cup3.Name = "cup3";
            this.cup3.NO = "3";
            this.cup3.Title = "";
            this.cup3.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cup4
            // 
            resources.ApplyResources(this.cup4, "cup4");
            this.cup4.BackColor = System.Drawing.Color.Transparent;
            this.cup4.BottleColor = System.Drawing.Color.Black;
            this.cup4.LiquidColor = System.Drawing.Color.Transparent;
            this.cup4.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cup4.Name = "cup4";
            this.cup4.NO = "4";
            this.cup4.Title = "";
            this.cup4.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Online,
            this.tsm_Offline,
            this.tsm_Stop,
            this.tsm_IsFix,
            this.tsm_HighWash});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsm_Online
            // 
            resources.ApplyResources(this.tsm_Online, "tsm_Online");
            this.tsm_Online.Name = "tsm_Online";
            this.tsm_Online.Click += new System.EventHandler(this.tsm_Online_Click);
            // 
            // tsm_Offline
            // 
            resources.ApplyResources(this.tsm_Offline, "tsm_Offline");
            this.tsm_Offline.Name = "tsm_Offline";
            this.tsm_Offline.Click += new System.EventHandler(this.tsm_Offline_Click);
            // 
            // tsm_Stop
            // 
            resources.ApplyResources(this.tsm_Stop, "tsm_Stop");
            this.tsm_Stop.Name = "tsm_Stop";
            this.tsm_Stop.Click += new System.EventHandler(this.tsm_Stop_Click);
            // 
            // tsm_IsFix
            // 
            resources.ApplyResources(this.tsm_IsFix, "tsm_IsFix");
            this.tsm_IsFix.Name = "tsm_IsFix";
            this.tsm_IsFix.Click += new System.EventHandler(this.tsm_IsFix_Click);
            // 
            // tsm_HighWash
            // 
            resources.ApplyResources(this.tsm_HighWash, "tsm_HighWash");
            this.tsm_HighWash.Name = "tsm_HighWash";
            this.tsm_HighWash.Click += new System.EventHandler(this.tsm_HighWash_Click);
            // 
            // FourBeater
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FourBeater";
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Cup cup4;
        private Cup cup3;
        private Cup cup2;
        private Cup cup1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem tsm_Online;
        private ToolStripMenuItem tsm_Offline;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem tsm_AllOnline;
        private ToolStripMenuItem tsm_AllOffline;
        private ToolStripMenuItem tsm_Stop;
        private ToolStripMenuItem tsm_IsFix;
        private ToolStripMenuItem tsm_HighWash;
    }
}
