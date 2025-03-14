using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class AbsCupDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbsCupDetails));
            this.dgv_CupDetails = new System.Windows.Forms.DataGridView();
            this.txt_CupNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CupDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_CupDetails
            // 
            this.dgv_CupDetails.AllowUserToAddRows = false;
            this.dgv_CupDetails.AllowUserToDeleteRows = false;
            this.dgv_CupDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_CupDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_CupDetails, "dgv_CupDetails");
            this.dgv_CupDetails.Name = "dgv_CupDetails";
            this.dgv_CupDetails.ReadOnly = true;
            this.dgv_CupDetails.RowHeadersVisible = false;
            this.dgv_CupDetails.RowTemplate.Height = 23;
            // 
            // txt_CupNum
            // 
            resources.ApplyResources(this.txt_CupNum, "txt_CupNum");
            this.txt_CupNum.Name = "txt_CupNum";
            this.txt_CupNum.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // AbsCupDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_CupNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgv_CupDetails);
            this.Name = "AbsCupDetails";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.DripCupDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CupDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridView dgv_CupDetails;
        private TextBox txt_CupNum;
        private Label label5;
    }
}