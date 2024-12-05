namespace SmartDyeing.FADM_Form
{
    partial class InsertAbs
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
            this.BtnBottleCheckPause = new System.Windows.Forms.Button();
            this.BtnBottleCheckStart = new System.Windows.Forms.Button();
            this.TxtCheckBottleNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(131, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "瓶号：";
            // 
            // BtnBottleCheckPause
            // 
            this.BtnBottleCheckPause.Font = new System.Drawing.Font("宋体", 14.25F);
            this.BtnBottleCheckPause.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnBottleCheckPause.Location = new System.Drawing.Point(79, 155);
            this.BtnBottleCheckPause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnBottleCheckPause.Name = "BtnBottleCheckPause";
            this.BtnBottleCheckPause.Size = new System.Drawing.Size(153, 35);
            this.BtnBottleCheckPause.TabIndex = 11;
            this.BtnBottleCheckPause.Text = "删除等待列表";
            this.BtnBottleCheckPause.UseVisualStyleBackColor = true;
            this.BtnBottleCheckPause.Click += new System.EventHandler(this.BtnBottleCheckPause_Click);
            // 
            // BtnBottleCheckStart
            // 
            this.BtnBottleCheckStart.Font = new System.Drawing.Font("宋体", 14.25F);
            this.BtnBottleCheckStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnBottleCheckStart.Location = new System.Drawing.Point(79, 105);
            this.BtnBottleCheckStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnBottleCheckStart.Name = "BtnBottleCheckStart";
            this.BtnBottleCheckStart.Size = new System.Drawing.Size(153, 35);
            this.BtnBottleCheckStart.TabIndex = 10;
            this.BtnBottleCheckStart.Text = "启 动";
            this.BtnBottleCheckStart.UseVisualStyleBackColor = true;
            this.BtnBottleCheckStart.Click += new System.EventHandler(this.BtnBottleCheckStart_Click);
            // 
            // TxtCheckBottleNo
            // 
            this.TxtCheckBottleNo.Font = new System.Drawing.Font("宋体", 14.25F);
            this.TxtCheckBottleNo.Location = new System.Drawing.Point(54, 61);
            this.TxtCheckBottleNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtCheckBottleNo.Name = "TxtCheckBottleNo";
            this.TxtCheckBottleNo.Size = new System.Drawing.Size(220, 29);
            this.TxtCheckBottleNo.TabIndex = 9;
            // 
            // InsertAbs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 293);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnBottleCheckPause);
            this.Controls.Add(this.BtnBottleCheckStart);
            this.Controls.Add(this.TxtCheckBottleNo);
            this.Name = "InsertAbs";
            this.Text = "吸光度测量";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnBottleCheckPause;
        private System.Windows.Forms.Button BtnBottleCheckStart;
        private System.Windows.Forms.TextBox TxtCheckBottleNo;
    }
}