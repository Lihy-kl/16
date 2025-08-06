using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class DripCupDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DripCupDetails));
            this.dgv_CupDetails = new System.Windows.Forms.DataGridView();
            this.txt_realWater = new System.Windows.Forms.TextBox();
            this.txt_objectWater = new System.Windows.Forms.TextBox();
            this.txt_VersionNum = new System.Windows.Forms.TextBox();
            this.txt_FormulaCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            // txt_realWater
            // 
            resources.ApplyResources(this.txt_realWater, "txt_realWater");
            this.txt_realWater.Name = "txt_realWater";
            this.txt_realWater.ReadOnly = true;
            // 
            // txt_objectWater
            // 
            resources.ApplyResources(this.txt_objectWater, "txt_objectWater");
            this.txt_objectWater.Name = "txt_objectWater";
            this.txt_objectWater.ReadOnly = true;
            // 
            // txt_VersionNum
            // 
            resources.ApplyResources(this.txt_VersionNum, "txt_VersionNum");
            this.txt_VersionNum.Name = "txt_VersionNum";
            this.txt_VersionNum.ReadOnly = true;
            // 
            // txt_FormulaCode
            // 
            resources.ApplyResources(this.txt_FormulaCode, "txt_FormulaCode");
            this.txt_FormulaCode.Name = "txt_FormulaCode";
            this.txt_FormulaCode.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // DripCupDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_CupNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_realWater);
            this.Controls.Add(this.txt_objectWater);
            this.Controls.Add(this.txt_VersionNum);
            this.Controls.Add(this.txt_FormulaCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_CupDetails);
            this.Name = "DripCupDetails";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.DripCupDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CupDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridView dgv_CupDetails;
        private TextBox txt_realWater;
        private TextBox txt_objectWater;
        private TextBox txt_VersionNum;
        private TextBox txt_FormulaCode;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txt_CupNum;
        private Label label5;
    }
}