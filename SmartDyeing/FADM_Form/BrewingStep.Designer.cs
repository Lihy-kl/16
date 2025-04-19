using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class BrewingStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrewingStep));
            this.lab_StepNum = new System.Windows.Forms.Label();
            this.lab_TechnologyName = new System.Windows.Forms.Label();
            this.lab_ProportionOrTime = new System.Windows.Forms.Label();
            this.txt_StepNum = new System.Windows.Forms.TextBox();
            this.txt_ProportionOrTime = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.cbo_TechnologyName = new System.Windows.Forms.ComboBox();
            this.txt_Ratio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lab_StepNum
            // 
            resources.ApplyResources(this.lab_StepNum, "lab_StepNum");
            this.lab_StepNum.Name = "lab_StepNum";
            // 
            // lab_TechnologyName
            // 
            resources.ApplyResources(this.lab_TechnologyName, "lab_TechnologyName");
            this.lab_TechnologyName.Name = "lab_TechnologyName";
            // 
            // lab_ProportionOrTime
            // 
            resources.ApplyResources(this.lab_ProportionOrTime, "lab_ProportionOrTime");
            this.lab_ProportionOrTime.Name = "lab_ProportionOrTime";
            // 
            // txt_StepNum
            // 
            resources.ApplyResources(this.txt_StepNum, "txt_StepNum");
            this.txt_StepNum.Name = "txt_StepNum";
            // 
            // txt_ProportionOrTime
            // 
            resources.ApplyResources(this.txt_ProportionOrTime, "txt_ProportionOrTime");
            this.txt_ProportionOrTime.Name = "txt_ProportionOrTime";
            // 
            // btn_Save
            // 
            resources.ApplyResources(this.btn_Save, "btn_Save");
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // cbo_TechnologyName
            // 
            resources.ApplyResources(this.cbo_TechnologyName, "cbo_TechnologyName");
            this.cbo_TechnologyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_TechnologyName.FormattingEnabled = true;
            this.cbo_TechnologyName.Items.AddRange(new object[] {
            resources.GetString("cbo_TechnologyName.Items"),
            resources.GetString("cbo_TechnologyName.Items1"),
            resources.GetString("cbo_TechnologyName.Items2"),
            resources.GetString("cbo_TechnologyName.Items3"),
            resources.GetString("cbo_TechnologyName.Items4"),
            resources.GetString("cbo_TechnologyName.Items5"),
            resources.GetString("cbo_TechnologyName.Items6")});
            this.cbo_TechnologyName.Name = "cbo_TechnologyName";
            this.cbo_TechnologyName.SelectedIndexChanged += new System.EventHandler(this.cbo_TechnologyName_SelectedIndexChanged);
            // 
            // txt_Ratio
            // 
            resources.ApplyResources(this.txt_Ratio, "txt_Ratio");
            this.txt_Ratio.Name = "txt_Ratio";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // BrewingStep
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_Ratio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbo_TechnologyName);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txt_ProportionOrTime);
            this.Controls.Add(this.txt_StepNum);
            this.Controls.Add(this.lab_ProportionOrTime);
            this.Controls.Add(this.lab_TechnologyName);
            this.Controls.Add(this.lab_StepNum);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BrewingStep";
            this.ShowIcon = false;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_StepNum;
        private System.Windows.Forms.Label lab_TechnologyName;
        private System.Windows.Forms.Label lab_ProportionOrTime;
        private System.Windows.Forms.TextBox txt_StepNum;
        private System.Windows.Forms.TextBox txt_ProportionOrTime;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.ComboBox cbo_TechnologyName;
        private TextBox txt_Ratio;
        private Label label1;
    }
}