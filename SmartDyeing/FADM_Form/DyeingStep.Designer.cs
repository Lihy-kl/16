using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class DyeingStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DyeingStep));
            this.cbo_TechnologyName = new System.Windows.Forms.ComboBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.txt_ProportionOrTime = new System.Windows.Forms.TextBox();
            this.txt_StepNum = new System.Windows.Forms.TextBox();
            this.lab_ProportionOrTime = new System.Windows.Forms.Label();
            this.lab_TechnologyName = new System.Windows.Forms.Label();
            this.lab_StepNum = new System.Windows.Forms.Label();
            this.txt_Temp = new System.Windows.Forms.TextBox();
            this.lab_Temp = new System.Windows.Forms.Label();
            this.txt_Rate = new System.Windows.Forms.TextBox();
            this.lab_Rate = new System.Windows.Forms.Label();
            this.txt_Rev = new System.Windows.Forms.TextBox();
            this.lab_Rev = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            resources.GetString("cbo_TechnologyName.Items6"),
            resources.GetString("cbo_TechnologyName.Items7"),
            resources.GetString("cbo_TechnologyName.Items8"),
            resources.GetString("cbo_TechnologyName.Items9"),
            resources.GetString("cbo_TechnologyName.Items10"),
            resources.GetString("cbo_TechnologyName.Items11"),
            resources.GetString("cbo_TechnologyName.Items12"),
            resources.GetString("cbo_TechnologyName.Items13"),
            resources.GetString("cbo_TechnologyName.Items14")});
            this.cbo_TechnologyName.Name = "cbo_TechnologyName";
            this.cbo_TechnologyName.SelectedIndexChanged += new System.EventHandler(this.cbo_TechnologyName_SelectedIndexChanged);
            this.cbo_TechnologyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbo_TechnologyName_KeyDown);
            // 
            // btn_Save
            // 
            resources.ApplyResources(this.btn_Save, "btn_Save");
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // txt_ProportionOrTime
            // 
            resources.ApplyResources(this.txt_ProportionOrTime, "txt_ProportionOrTime");
            this.txt_ProportionOrTime.Name = "txt_ProportionOrTime";
            this.txt_ProportionOrTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ProportionOrTime_KeyDown);
            // 
            // txt_StepNum
            // 
            resources.ApplyResources(this.txt_StepNum, "txt_StepNum");
            this.txt_StepNum.Name = "txt_StepNum";
            // 
            // lab_ProportionOrTime
            // 
            resources.ApplyResources(this.lab_ProportionOrTime, "lab_ProportionOrTime");
            this.lab_ProportionOrTime.Name = "lab_ProportionOrTime";
            // 
            // lab_TechnologyName
            // 
            resources.ApplyResources(this.lab_TechnologyName, "lab_TechnologyName");
            this.lab_TechnologyName.Name = "lab_TechnologyName";
            // 
            // lab_StepNum
            // 
            resources.ApplyResources(this.lab_StepNum, "lab_StepNum");
            this.lab_StepNum.Name = "lab_StepNum";
            // 
            // txt_Temp
            // 
            resources.ApplyResources(this.txt_Temp, "txt_Temp");
            this.txt_Temp.Name = "txt_Temp";
            this.txt_Temp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Temp_KeyDown);
            // 
            // lab_Temp
            // 
            resources.ApplyResources(this.lab_Temp, "lab_Temp");
            this.lab_Temp.Name = "lab_Temp";
            // 
            // txt_Rate
            // 
            resources.ApplyResources(this.txt_Rate, "txt_Rate");
            this.txt_Rate.Name = "txt_Rate";
            this.txt_Rate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Rate_KeyDown);
            // 
            // lab_Rate
            // 
            resources.ApplyResources(this.lab_Rate, "lab_Rate");
            this.lab_Rate.Name = "lab_Rate";
            // 
            // txt_Rev
            // 
            resources.ApplyResources(this.txt_Rev, "txt_Rev");
            this.txt_Rev.Name = "txt_Rev";
            this.txt_Rev.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Rev_KeyDown);
            // 
            // lab_Rev
            // 
            resources.ApplyResources(this.lab_Rev, "lab_Rev");
            this.lab_Rev.Name = "lab_Rev";
            // 
            // DyeingStep
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_Rev);
            this.Controls.Add(this.lab_Rev);
            this.Controls.Add(this.txt_Rate);
            this.Controls.Add(this.lab_Rate);
            this.Controls.Add(this.txt_Temp);
            this.Controls.Add(this.lab_Temp);
            this.Controls.Add(this.cbo_TechnologyName);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txt_ProportionOrTime);
            this.Controls.Add(this.txt_StepNum);
            this.Controls.Add(this.lab_ProportionOrTime);
            this.Controls.Add(this.lab_TechnologyName);
            this.Controls.Add(this.lab_StepNum);
            this.Name = "DyeingStep";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.DyeingStep_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo_TechnologyName;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox txt_ProportionOrTime;
        private System.Windows.Forms.TextBox txt_StepNum;
        private System.Windows.Forms.Label lab_ProportionOrTime;
        private System.Windows.Forms.Label lab_TechnologyName;
        private System.Windows.Forms.Label lab_StepNum;
        private TextBox txt_Temp;
        private Label lab_Temp;
        private TextBox txt_Rate;
        private Label lab_Rate;
        private TextBox txt_Rev;
        private Label lab_Rev;
    }
}