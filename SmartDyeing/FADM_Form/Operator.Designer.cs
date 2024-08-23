using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class Operator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Operator));
            this.btn_save = new System.Windows.Forms.Button();
            this.dgv_Operator = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Operator)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.Name = "btn_save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // dgv_Operator
            // 
            resources.ApplyResources(this.dgv_Operator, "dgv_Operator");
            this.dgv_Operator.AllowUserToResizeColumns = false;
            this.dgv_Operator.AllowUserToResizeRows = false;
            this.dgv_Operator.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Operator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Operator.MultiSelect = false;
            this.dgv_Operator.Name = "dgv_Operator";
            this.dgv_Operator.RowHeadersVisible = false;
            this.dgv_Operator.RowTemplate.Height = 23;
            this.dgv_Operator.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Operator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_Operator);
            this.Controls.Add(this.btn_save);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Operator";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Operator_FormClosing);
            this.Load += new System.EventHandler(this.Operator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Operator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridView dgv_Operator;

    }
}