namespace SmartDyeing.FADM_Form
{
    partial class Self
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Self));
            this.BtnBottleSelfStop = new System.Windows.Forms.Button();
            this.BtnBottleSelfPause = new System.Windows.Forms.Button();
            this.BtnBottleSelfStart = new System.Windows.Forms.Button();
            this.TxtSelfBottleNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // _da_self
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnBottleSelfStop);
            this.Controls.Add(this.BtnBottleSelfPause);
            this.Controls.Add(this.BtnBottleSelfStart);
            this.Controls.Add(this.TxtSelfBottleNo);
            this.Name = "Self";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnBottleSelfStop;
        private System.Windows.Forms.Button BtnBottleSelfPause;
        private System.Windows.Forms.Button BtnBottleSelfStart;
        private System.Windows.Forms.TextBox TxtSelfBottleNo;
        private System.Windows.Forms.Label label1;
    }
}