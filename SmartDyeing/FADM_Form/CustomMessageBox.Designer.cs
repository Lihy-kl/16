namespace SmartDyeing.FADM_Form
{
    partial class CustomMessageBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Yes = new System.Windows.Forms.Button();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Btn_No = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(58, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(689, 302);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_Yes
            // 
            this.Btn_Yes.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_Yes.Location = new System.Drawing.Point(58, 341);
            this.Btn_Yes.Name = "Btn_Yes";
            this.Btn_Yes.Size = new System.Drawing.Size(127, 68);
            this.Btn_Yes.TabIndex = 1;
            this.Btn_Yes.Text = "Yes";
            this.Btn_Yes.UseVisualStyleBackColor = true;
            this.Btn_Yes.Click += new System.EventHandler(this.Btn_Yes_Click);
            // 
            // Btn_OK
            // 
            this.Btn_OK.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_OK.Location = new System.Drawing.Point(339, 341);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(127, 68);
            this.Btn_OK.TabIndex = 2;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = true;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Btn_No
            // 
            this.Btn_No.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_No.Location = new System.Drawing.Point(620, 341);
            this.Btn_No.Name = "Btn_No";
            this.Btn_No.Size = new System.Drawing.Size(127, 68);
            this.Btn_No.TabIndex = 3;
            this.Btn_No.Text = "NO";
            this.Btn_No.UseVisualStyleBackColor = true;
            this.Btn_No.Click += new System.EventHandler(this.Btn_No_Click);
            // 
            // CustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_No);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.Btn_Yes);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomMessageBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Info";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Yes;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Button Btn_No;
    }
}