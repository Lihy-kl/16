namespace SmartDyeing.FADM_Form
{
    partial class Check
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Check));
            this.BtnBottleCheckStop = new System.Windows.Forms.Button();
            this.BtnBottleCheckPause = new System.Windows.Forms.Button();
            this.BtnBottleCheckStart = new System.Windows.Forms.Button();
            this.TxtCheckBottleNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Check
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnBottleCheckStop);
            this.Controls.Add(this.BtnBottleCheckPause);
            this.Controls.Add(this.BtnBottleCheckStart);
            this.Controls.Add(this.TxtCheckBottleNo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Check";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnBottleCheckStop;
        private System.Windows.Forms.Button BtnBottleCheckPause;
        private System.Windows.Forms.Button BtnBottleCheckStart;
        private System.Windows.Forms.TextBox TxtCheckBottleNo;
        private System.Windows.Forms.Label label1;
    }
}