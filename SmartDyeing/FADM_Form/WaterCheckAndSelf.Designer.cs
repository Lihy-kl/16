namespace SmartDyeing.FADM_Form
{
    partial class WaterCheckAndSelf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaterCheckAndSelf));
            this.BtnRecheckStart = new System.Windows.Forms.Button();
            this.BtnWaterCheckStart = new System.Windows.Forms.Button();
            this.TxtWaterAddWeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // WaterCheckAndSelf
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnRecheckStart);
            this.Controls.Add(this.BtnWaterCheckStart);
            this.Controls.Add(this.TxtWaterAddWeight);
            this.Name = "WaterCheckAndSelf";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnRecheckStart;
        private System.Windows.Forms.Button BtnWaterCheckStart;
        private System.Windows.Forms.TextBox TxtWaterAddWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}