using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class P_MainBottle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_MainBottle));
            this.PnlBottle = new System.Windows.Forms.Panel();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.lab_Low = new System.Windows.Forms.Label();
            this.lab_Expire = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnRecheckStart = new System.Windows.Forms.Button();
            this.BtnWaterCheckStart = new System.Windows.Forms.Button();
            this.TxtWaterAddWeight = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnBottleSelfStop = new System.Windows.Forms.Button();
            this.BtnBottleSelfPause = new System.Windows.Forms.Button();
            this.BtnBottleSelfStart = new System.Windows.Forms.Button();
            this.TxtSelfBottleNo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnBottleCheckStop = new System.Windows.Forms.Button();
            this.BtnBottleCheckPause = new System.Windows.Forms.Button();
            this.BtnBottleCheckStart = new System.Windows.Forms.Button();
            this.TxtCheckBottleNo = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_SignUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Normal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Need = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_PreDrip = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlBottle
            // 
            resources.ApplyResources(this.PnlBottle, "PnlBottle");
            this.PnlBottle.Name = "PnlBottle";
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 1000;
            this.tmr.Tick += new System.EventHandler(this.Tmr_Tick);
            // 
            // lab_Low
            // 
            resources.ApplyResources(this.lab_Low, "lab_Low");
            this.lab_Low.AutoEllipsis = true;
            this.lab_Low.ForeColor = System.Drawing.Color.Red;
            this.lab_Low.Name = "lab_Low";
            // 
            // lab_Expire
            // 
            resources.ApplyResources(this.lab_Expire, "lab_Expire");
            this.lab_Expire.AutoEllipsis = true;
            this.lab_Expire.ForeColor = System.Drawing.Color.Red;
            this.lab_Expire.Name = "lab_Expire";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.BtnRecheckStart);
            this.groupBox3.Controls.Add(this.BtnWaterCheckStart);
            this.groupBox3.Controls.Add(this.TxtWaterAddWeight);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // BtnRecheckStart
            // 
            resources.ApplyResources(this.BtnRecheckStart, "BtnRecheckStart");
            this.BtnRecheckStart.Name = "BtnRecheckStart";
            this.BtnRecheckStart.UseVisualStyleBackColor = true;
            this.BtnRecheckStart.Click += new System.EventHandler(this.BtnRecheckStart_Click);
            // 
            // BtnWaterCheckStart
            // 
            resources.ApplyResources(this.BtnWaterCheckStart, "BtnWaterCheckStart");
            this.BtnWaterCheckStart.Name = "BtnWaterCheckStart";
            this.BtnWaterCheckStart.UseVisualStyleBackColor = true;
            this.BtnWaterCheckStart.Click += new System.EventHandler(this.BtnWaterCheckStart_Click);
            // 
            // TxtWaterAddWeight
            // 
            resources.ApplyResources(this.TxtWaterAddWeight, "TxtWaterAddWeight");
            this.TxtWaterAddWeight.Name = "TxtWaterAddWeight";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.BtnBottleSelfStop);
            this.groupBox2.Controls.Add(this.BtnBottleSelfPause);
            this.groupBox2.Controls.Add(this.BtnBottleSelfStart);
            this.groupBox2.Controls.Add(this.TxtSelfBottleNo);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // BtnBottleSelfStop
            // 
            resources.ApplyResources(this.BtnBottleSelfStop, "BtnBottleSelfStop");
            this.BtnBottleSelfStop.Name = "BtnBottleSelfStop";
            this.BtnBottleSelfStop.UseVisualStyleBackColor = true;
            this.BtnBottleSelfStop.Click += new System.EventHandler(this.BtnBottleSelfStop_Click);
            // 
            // BtnBottleSelfPause
            // 
            resources.ApplyResources(this.BtnBottleSelfPause, "BtnBottleSelfPause");
            this.BtnBottleSelfPause.Name = "BtnBottleSelfPause";
            this.BtnBottleSelfPause.UseVisualStyleBackColor = true;
            this.BtnBottleSelfPause.Click += new System.EventHandler(this.BtnBottleSelfPause_Click);
            // 
            // BtnBottleSelfStart
            // 
            resources.ApplyResources(this.BtnBottleSelfStart, "BtnBottleSelfStart");
            this.BtnBottleSelfStart.Name = "BtnBottleSelfStart";
            this.BtnBottleSelfStart.UseVisualStyleBackColor = true;
            this.BtnBottleSelfStart.Click += new System.EventHandler(this.BtnBottleSelfStart_Click);
            // 
            // TxtSelfBottleNo
            // 
            resources.ApplyResources(this.TxtSelfBottleNo, "TxtSelfBottleNo");
            this.TxtSelfBottleNo.Name = "TxtSelfBottleNo";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.BtnBottleCheckStop);
            this.groupBox1.Controls.Add(this.BtnBottleCheckPause);
            this.groupBox1.Controls.Add(this.BtnBottleCheckStart);
            this.groupBox1.Controls.Add(this.TxtCheckBottleNo);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // BtnBottleCheckStop
            // 
            resources.ApplyResources(this.BtnBottleCheckStop, "BtnBottleCheckStop");
            this.BtnBottleCheckStop.Name = "BtnBottleCheckStop";
            this.BtnBottleCheckStop.UseVisualStyleBackColor = true;
            this.BtnBottleCheckStop.Click += new System.EventHandler(this.BtnBottleCheckStop_Click);
            // 
            // BtnBottleCheckPause
            // 
            resources.ApplyResources(this.BtnBottleCheckPause, "BtnBottleCheckPause");
            this.BtnBottleCheckPause.Name = "BtnBottleCheckPause";
            this.BtnBottleCheckPause.UseVisualStyleBackColor = true;
            this.BtnBottleCheckPause.Click += new System.EventHandler(this.BtnBottleCheckPause_Click);
            // 
            // BtnBottleCheckStart
            // 
            resources.ApplyResources(this.BtnBottleCheckStart, "BtnBottleCheckStart");
            this.BtnBottleCheckStart.Name = "BtnBottleCheckStart";
            this.BtnBottleCheckStart.UseVisualStyleBackColor = true;
            this.BtnBottleCheckStart.Click += new System.EventHandler(this.BtnBottleCheckStart_Click);
            // 
            // TxtCheckBottleNo
            // 
            resources.ApplyResources(this.TxtCheckBottleNo, "TxtCheckBottleNo");
            this.TxtCheckBottleNo.Name = "TxtCheckBottleNo";
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_SignUpdate,
            this.tsm_Stop,
            this.tsm_Normal,
            this.tsm_Need,
            this.tsm_PreDrip});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // tsm_SignUpdate
            // 
            resources.ApplyResources(this.tsm_SignUpdate, "tsm_SignUpdate");
            this.tsm_SignUpdate.Name = "tsm_SignUpdate";
            this.tsm_SignUpdate.Click += new System.EventHandler(this.tsm_SignUpdate_Click);
            // 
            // tsm_Stop
            // 
            resources.ApplyResources(this.tsm_Stop, "tsm_Stop");
            this.tsm_Stop.Name = "tsm_Stop";
            this.tsm_Stop.Click += new System.EventHandler(this.tsm_Stop_Click);
            // 
            // tsm_Normal
            // 
            resources.ApplyResources(this.tsm_Normal, "tsm_Normal");
            this.tsm_Normal.Name = "tsm_Normal";
            this.tsm_Normal.Click += new System.EventHandler(this.tsm_Normal_Click);
            // 
            // tsm_Need
            // 
            resources.ApplyResources(this.tsm_Need, "tsm_Need");
            this.tsm_Need.Name = "tsm_Need";
            this.tsm_Need.Click += new System.EventHandler(this.tsm_Need_Click);
            // 
            // tsm_PreDrip
            // 
            resources.ApplyResources(this.tsm_PreDrip, "tsm_PreDrip");
            this.tsm_PreDrip.Name = "tsm_PreDrip";
            this.tsm_PreDrip.Click += new System.EventHandler(this.tsm_PreDrip_Click);
            // 
            // P_MainBottle
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lab_Expire);
            this.Controls.Add(this.lab_Low);
            this.Controls.Add(this.PnlBottle);
            this.Name = "P_MainBottle";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel PnlBottle;
        private System.Windows.Forms.Timer tmr;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tss_lab_bottlelow;
        private Label label1;
        private Label label2;
        private Label lab_Low;
        private Label lab_Expire;
        private Panel panel1;
        private GroupBox groupBox3;
        private Button BtnRecheckStart;
        private Button BtnWaterCheckStart;
        private TextBox TxtWaterAddWeight;
        private GroupBox groupBox2;
        private Button BtnBottleSelfStop;
        private Button BtnBottleSelfPause;
        private Button BtnBottleSelfStart;
        private TextBox TxtSelfBottleNo;
        private GroupBox groupBox1;
        private Button BtnBottleCheckStop;
        private Button BtnBottleCheckPause;
        private Button BtnBottleCheckStart;
        private TextBox TxtCheckBottleNo;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem tsm_SignUpdate;
        private ToolStripMenuItem tsm_Stop;
        private ToolStripMenuItem tsm_Normal;
        private ToolStripMenuItem tsm_Need;
        private ToolStripMenuItem tsm_PreDrip;
    }
}
