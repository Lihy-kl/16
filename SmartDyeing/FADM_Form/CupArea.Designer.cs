namespace SmartDyeing.FADM_Form
{
    partial class CupArea
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 1000;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Update,
            this.tsm_Reset});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // tsm_Update
            // 
            this.tsm_Update.Name = "tsm_Update";
            this.tsm_Update.Size = new System.Drawing.Size(180, 22);
            this.tsm_Update.Text = "修改";
            this.tsm_Update.Click += new System.EventHandler(this.tsm_Update_Click);
            // 
            // tsm_Reset
            // 
            this.tsm_Reset.Name = "tsm_Reset";
            this.tsm_Reset.Size = new System.Drawing.Size(180, 22);
            this.tsm_Reset.Text = "重置";
            this.tsm_Reset.Click += new System.EventHandler(this.tsm_Reset_Click);
            // 
            // CupArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1649, 637);
            this.Name = "CupArea";
            this.Text = "CupArea";
            this.Load += new System.EventHandler(this.CupArea_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsm_Update;
        private System.Windows.Forms.ToolStripMenuItem tsm_Reset;
    }
}