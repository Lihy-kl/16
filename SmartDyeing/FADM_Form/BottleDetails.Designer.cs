using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class BottleDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BottleDetails));
            this.grp_BottleDetails = new System.Windows.Forms.GroupBox();
            this.txt_SelfChecking4 = new System.Windows.Forms.TextBox();
            this.lb_SelfChecking4 = new System.Windows.Forms.Label();
            this.txt_SelfChecking3 = new System.Windows.Forms.TextBox();
            this.lb_SelfChecking3 = new System.Windows.Forms.Label();
            this.txt_SelfChecking2 = new System.Windows.Forms.TextBox();
            this.lb_SelfChecking2 = new System.Windows.Forms.Label();
            this.txt_SelfChecking1 = new System.Windows.Forms.TextBox();
            this.lb_SelfChecking1 = new System.Windows.Forms.Label();
            this.txt_SettingConcentration = new System.Windows.Forms.TextBox();
            this.txt_RealConcentration = new System.Windows.Forms.TextBox();
            this.txt_CurrentWeight = new System.Windows.Forms.TextBox();
            this.txt_BrewingCode = new System.Windows.Forms.TextBox();
            this.txt_LastAdjustWeight = new System.Windows.Forms.TextBox();
            this.txt_CurrentAdjustWeight = new System.Windows.Forms.TextBox();
            this.txt_BrewingData = new System.Windows.Forms.TextBox();
            this.txt_AdjustValue = new System.Windows.Forms.TextBox();
            this.txt_AssistantCode = new System.Windows.Forms.TextBox();
            this.txt_BottleNum = new System.Windows.Forms.TextBox();
            this.lab_AdjustValue = new System.Windows.Forms.Label();
            this.txt_AllowMaxWeight = new System.Windows.Forms.TextBox();
            this.lab_CurrentAdjustWeight = new System.Windows.Forms.Label();
            this.lab_LastAdjustWeight = new System.Windows.Forms.Label();
            this.lab_AssistantCode = new System.Windows.Forms.Label();
            this.lab_BrewingData = new System.Windows.Forms.Label();
            this.lab_RealConcentration = new System.Windows.Forms.Label();
            this.lab_CurrentWeight = new System.Windows.Forms.Label();
            this.lab_BrewingCode = new System.Windows.Forms.Label();
            this.lab_SettingConcentration = new System.Windows.Forms.Label();
            this.lab_BottleNum = new System.Windows.Forms.Label();
            this.lab_AllowMaxWeight = new System.Windows.Forms.Label();
            this.grp_AdjustParameters = new System.Windows.Forms.GroupBox();
            this.txt_UnitOfAccount = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_AllowMaxColoringConcentration = new System.Windows.Forms.TextBox();
            this.txt_TermOfValidity = new System.Windows.Forms.TextBox();
            this.txt_AllowMinColoringConcentration = new System.Windows.Forms.TextBox();
            this.txt_Cost = new System.Windows.Forms.TextBox();
            this.txt_AssistantType = new System.Windows.Forms.TextBox();
            this.txt_Intensity = new System.Windows.Forms.TextBox();
            this.txt_AssistantName = new System.Windows.Forms.TextBox();
            this.lab_AssistantName = new System.Windows.Forms.Label();
            this.lab_Intensity = new System.Windows.Forms.Label();
            this.lab_Cost = new System.Windows.Forms.Label();
            this.lab_AllowMinColoringConcentration = new System.Windows.Forms.Label();
            this.lab_AllowMaxColoringConcentration = new System.Windows.Forms.Label();
            this.lab_TermOfValidity = new System.Windows.Forms.Label();
            this.lab_AssistantType = new System.Windows.Forms.Label();
            this.grp_BottleDetails.SuspendLayout();
            this.grp_AdjustParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_BottleDetails
            // 
            resources.ApplyResources(this.grp_BottleDetails, "grp_BottleDetails");
            this.grp_BottleDetails.Controls.Add(this.txt_SelfChecking4);
            this.grp_BottleDetails.Controls.Add(this.lb_SelfChecking4);
            this.grp_BottleDetails.Controls.Add(this.txt_SelfChecking3);
            this.grp_BottleDetails.Controls.Add(this.lb_SelfChecking3);
            this.grp_BottleDetails.Controls.Add(this.txt_SelfChecking2);
            this.grp_BottleDetails.Controls.Add(this.lb_SelfChecking2);
            this.grp_BottleDetails.Controls.Add(this.txt_SelfChecking1);
            this.grp_BottleDetails.Controls.Add(this.lb_SelfChecking1);
            this.grp_BottleDetails.Controls.Add(this.txt_SettingConcentration);
            this.grp_BottleDetails.Controls.Add(this.txt_RealConcentration);
            this.grp_BottleDetails.Controls.Add(this.txt_CurrentWeight);
            this.grp_BottleDetails.Controls.Add(this.txt_BrewingCode);
            this.grp_BottleDetails.Controls.Add(this.txt_LastAdjustWeight);
            this.grp_BottleDetails.Controls.Add(this.txt_CurrentAdjustWeight);
            this.grp_BottleDetails.Controls.Add(this.txt_BrewingData);
            this.grp_BottleDetails.Controls.Add(this.txt_AdjustValue);
            this.grp_BottleDetails.Controls.Add(this.txt_AssistantCode);
            this.grp_BottleDetails.Controls.Add(this.txt_BottleNum);
            this.grp_BottleDetails.Controls.Add(this.lab_AdjustValue);
            this.grp_BottleDetails.Controls.Add(this.txt_AllowMaxWeight);
            this.grp_BottleDetails.Controls.Add(this.lab_CurrentAdjustWeight);
            this.grp_BottleDetails.Controls.Add(this.lab_LastAdjustWeight);
            this.grp_BottleDetails.Controls.Add(this.lab_AssistantCode);
            this.grp_BottleDetails.Controls.Add(this.lab_BrewingData);
            this.grp_BottleDetails.Controls.Add(this.lab_RealConcentration);
            this.grp_BottleDetails.Controls.Add(this.lab_CurrentWeight);
            this.grp_BottleDetails.Controls.Add(this.lab_BrewingCode);
            this.grp_BottleDetails.Controls.Add(this.lab_SettingConcentration);
            this.grp_BottleDetails.Controls.Add(this.lab_BottleNum);
            this.grp_BottleDetails.Controls.Add(this.lab_AllowMaxWeight);
            this.grp_BottleDetails.Name = "grp_BottleDetails";
            this.grp_BottleDetails.TabStop = false;
            // 
            // txt_SelfChecking4
            // 
            resources.ApplyResources(this.txt_SelfChecking4, "txt_SelfChecking4");
            this.txt_SelfChecking4.Name = "txt_SelfChecking4";
            this.txt_SelfChecking4.ReadOnly = true;
            this.txt_SelfChecking4.TabStop = false;
            // 
            // lb_SelfChecking4
            // 
            resources.ApplyResources(this.lb_SelfChecking4, "lb_SelfChecking4");
            this.lb_SelfChecking4.Name = "lb_SelfChecking4";
            // 
            // txt_SelfChecking3
            // 
            resources.ApplyResources(this.txt_SelfChecking3, "txt_SelfChecking3");
            this.txt_SelfChecking3.Name = "txt_SelfChecking3";
            this.txt_SelfChecking3.ReadOnly = true;
            this.txt_SelfChecking3.TabStop = false;
            // 
            // lb_SelfChecking3
            // 
            resources.ApplyResources(this.lb_SelfChecking3, "lb_SelfChecking3");
            this.lb_SelfChecking3.Name = "lb_SelfChecking3";
            // 
            // txt_SelfChecking2
            // 
            resources.ApplyResources(this.txt_SelfChecking2, "txt_SelfChecking2");
            this.txt_SelfChecking2.Name = "txt_SelfChecking2";
            this.txt_SelfChecking2.ReadOnly = true;
            this.txt_SelfChecking2.TabStop = false;
            // 
            // lb_SelfChecking2
            // 
            resources.ApplyResources(this.lb_SelfChecking2, "lb_SelfChecking2");
            this.lb_SelfChecking2.Name = "lb_SelfChecking2";
            // 
            // txt_SelfChecking1
            // 
            resources.ApplyResources(this.txt_SelfChecking1, "txt_SelfChecking1");
            this.txt_SelfChecking1.Name = "txt_SelfChecking1";
            this.txt_SelfChecking1.ReadOnly = true;
            this.txt_SelfChecking1.TabStop = false;
            // 
            // lb_SelfChecking1
            // 
            resources.ApplyResources(this.lb_SelfChecking1, "lb_SelfChecking1");
            this.lb_SelfChecking1.Name = "lb_SelfChecking1";
            // 
            // txt_SettingConcentration
            // 
            resources.ApplyResources(this.txt_SettingConcentration, "txt_SettingConcentration");
            this.txt_SettingConcentration.Name = "txt_SettingConcentration";
            this.txt_SettingConcentration.ReadOnly = true;
            this.txt_SettingConcentration.TabStop = false;
            // 
            // txt_RealConcentration
            // 
            resources.ApplyResources(this.txt_RealConcentration, "txt_RealConcentration");
            this.txt_RealConcentration.Name = "txt_RealConcentration";
            this.txt_RealConcentration.ReadOnly = true;
            this.txt_RealConcentration.TabStop = false;
            // 
            // txt_CurrentWeight
            // 
            resources.ApplyResources(this.txt_CurrentWeight, "txt_CurrentWeight");
            this.txt_CurrentWeight.Name = "txt_CurrentWeight";
            this.txt_CurrentWeight.ReadOnly = true;
            this.txt_CurrentWeight.TabStop = false;
            // 
            // txt_BrewingCode
            // 
            resources.ApplyResources(this.txt_BrewingCode, "txt_BrewingCode");
            this.txt_BrewingCode.Name = "txt_BrewingCode";
            this.txt_BrewingCode.ReadOnly = true;
            this.txt_BrewingCode.TabStop = false;
            // 
            // txt_LastAdjustWeight
            // 
            resources.ApplyResources(this.txt_LastAdjustWeight, "txt_LastAdjustWeight");
            this.txt_LastAdjustWeight.Name = "txt_LastAdjustWeight";
            this.txt_LastAdjustWeight.ReadOnly = true;
            this.txt_LastAdjustWeight.TabStop = false;
            // 
            // txt_CurrentAdjustWeight
            // 
            resources.ApplyResources(this.txt_CurrentAdjustWeight, "txt_CurrentAdjustWeight");
            this.txt_CurrentAdjustWeight.Name = "txt_CurrentAdjustWeight";
            this.txt_CurrentAdjustWeight.ReadOnly = true;
            this.txt_CurrentAdjustWeight.TabStop = false;
            // 
            // txt_BrewingData
            // 
            resources.ApplyResources(this.txt_BrewingData, "txt_BrewingData");
            this.txt_BrewingData.Name = "txt_BrewingData";
            this.txt_BrewingData.ReadOnly = true;
            this.txt_BrewingData.TabStop = false;
            // 
            // txt_AdjustValue
            // 
            resources.ApplyResources(this.txt_AdjustValue, "txt_AdjustValue");
            this.txt_AdjustValue.Name = "txt_AdjustValue";
            this.txt_AdjustValue.ReadOnly = true;
            this.txt_AdjustValue.TabStop = false;
            // 
            // txt_AssistantCode
            // 
            resources.ApplyResources(this.txt_AssistantCode, "txt_AssistantCode");
            this.txt_AssistantCode.Name = "txt_AssistantCode";
            this.txt_AssistantCode.ReadOnly = true;
            this.txt_AssistantCode.TabStop = false;
            // 
            // txt_BottleNum
            // 
            resources.ApplyResources(this.txt_BottleNum, "txt_BottleNum");
            this.txt_BottleNum.Name = "txt_BottleNum";
            this.txt_BottleNum.ReadOnly = true;
            this.txt_BottleNum.TabStop = false;
            // 
            // lab_AdjustValue
            // 
            resources.ApplyResources(this.lab_AdjustValue, "lab_AdjustValue");
            this.lab_AdjustValue.Name = "lab_AdjustValue";
            // 
            // txt_AllowMaxWeight
            // 
            resources.ApplyResources(this.txt_AllowMaxWeight, "txt_AllowMaxWeight");
            this.txt_AllowMaxWeight.Name = "txt_AllowMaxWeight";
            this.txt_AllowMaxWeight.ReadOnly = true;
            this.txt_AllowMaxWeight.TabStop = false;
            // 
            // lab_CurrentAdjustWeight
            // 
            resources.ApplyResources(this.lab_CurrentAdjustWeight, "lab_CurrentAdjustWeight");
            this.lab_CurrentAdjustWeight.Name = "lab_CurrentAdjustWeight";
            // 
            // lab_LastAdjustWeight
            // 
            resources.ApplyResources(this.lab_LastAdjustWeight, "lab_LastAdjustWeight");
            this.lab_LastAdjustWeight.Name = "lab_LastAdjustWeight";
            // 
            // lab_AssistantCode
            // 
            resources.ApplyResources(this.lab_AssistantCode, "lab_AssistantCode");
            this.lab_AssistantCode.Name = "lab_AssistantCode";
            // 
            // lab_BrewingData
            // 
            resources.ApplyResources(this.lab_BrewingData, "lab_BrewingData");
            this.lab_BrewingData.Name = "lab_BrewingData";
            // 
            // lab_RealConcentration
            // 
            resources.ApplyResources(this.lab_RealConcentration, "lab_RealConcentration");
            this.lab_RealConcentration.Name = "lab_RealConcentration";
            // 
            // lab_CurrentWeight
            // 
            resources.ApplyResources(this.lab_CurrentWeight, "lab_CurrentWeight");
            this.lab_CurrentWeight.Name = "lab_CurrentWeight";
            // 
            // lab_BrewingCode
            // 
            resources.ApplyResources(this.lab_BrewingCode, "lab_BrewingCode");
            this.lab_BrewingCode.Name = "lab_BrewingCode";
            // 
            // lab_SettingConcentration
            // 
            resources.ApplyResources(this.lab_SettingConcentration, "lab_SettingConcentration");
            this.lab_SettingConcentration.Name = "lab_SettingConcentration";
            // 
            // lab_BottleNum
            // 
            resources.ApplyResources(this.lab_BottleNum, "lab_BottleNum");
            this.lab_BottleNum.Name = "lab_BottleNum";
            // 
            // lab_AllowMaxWeight
            // 
            resources.ApplyResources(this.lab_AllowMaxWeight, "lab_AllowMaxWeight");
            this.lab_AllowMaxWeight.Name = "lab_AllowMaxWeight";
            // 
            // grp_AdjustParameters
            // 
            resources.ApplyResources(this.grp_AdjustParameters, "grp_AdjustParameters");
            this.grp_AdjustParameters.Controls.Add(this.txt_UnitOfAccount);
            this.grp_AdjustParameters.Controls.Add(this.label19);
            this.grp_AdjustParameters.Controls.Add(this.txt_AllowMaxColoringConcentration);
            this.grp_AdjustParameters.Controls.Add(this.txt_TermOfValidity);
            this.grp_AdjustParameters.Controls.Add(this.txt_AllowMinColoringConcentration);
            this.grp_AdjustParameters.Controls.Add(this.txt_Cost);
            this.grp_AdjustParameters.Controls.Add(this.txt_AssistantType);
            this.grp_AdjustParameters.Controls.Add(this.txt_Intensity);
            this.grp_AdjustParameters.Controls.Add(this.txt_AssistantName);
            this.grp_AdjustParameters.Controls.Add(this.lab_AssistantName);
            this.grp_AdjustParameters.Controls.Add(this.lab_Intensity);
            this.grp_AdjustParameters.Controls.Add(this.lab_Cost);
            this.grp_AdjustParameters.Controls.Add(this.lab_AllowMinColoringConcentration);
            this.grp_AdjustParameters.Controls.Add(this.lab_AllowMaxColoringConcentration);
            this.grp_AdjustParameters.Controls.Add(this.lab_TermOfValidity);
            this.grp_AdjustParameters.Controls.Add(this.lab_AssistantType);
            this.grp_AdjustParameters.Name = "grp_AdjustParameters";
            this.grp_AdjustParameters.TabStop = false;
            // 
            // txt_UnitOfAccount
            // 
            resources.ApplyResources(this.txt_UnitOfAccount, "txt_UnitOfAccount");
            this.txt_UnitOfAccount.Name = "txt_UnitOfAccount";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // txt_AllowMaxColoringConcentration
            // 
            resources.ApplyResources(this.txt_AllowMaxColoringConcentration, "txt_AllowMaxColoringConcentration");
            this.txt_AllowMaxColoringConcentration.Name = "txt_AllowMaxColoringConcentration";
            this.txt_AllowMaxColoringConcentration.ReadOnly = true;
            this.txt_AllowMaxColoringConcentration.TabStop = false;
            // 
            // txt_TermOfValidity
            // 
            resources.ApplyResources(this.txt_TermOfValidity, "txt_TermOfValidity");
            this.txt_TermOfValidity.Name = "txt_TermOfValidity";
            this.txt_TermOfValidity.ReadOnly = true;
            this.txt_TermOfValidity.TabStop = false;
            // 
            // txt_AllowMinColoringConcentration
            // 
            resources.ApplyResources(this.txt_AllowMinColoringConcentration, "txt_AllowMinColoringConcentration");
            this.txt_AllowMinColoringConcentration.Name = "txt_AllowMinColoringConcentration";
            this.txt_AllowMinColoringConcentration.ReadOnly = true;
            this.txt_AllowMinColoringConcentration.TabStop = false;
            // 
            // txt_Cost
            // 
            resources.ApplyResources(this.txt_Cost, "txt_Cost");
            this.txt_Cost.Name = "txt_Cost";
            this.txt_Cost.ReadOnly = true;
            this.txt_Cost.TabStop = false;
            // 
            // txt_AssistantType
            // 
            resources.ApplyResources(this.txt_AssistantType, "txt_AssistantType");
            this.txt_AssistantType.Name = "txt_AssistantType";
            this.txt_AssistantType.ReadOnly = true;
            this.txt_AssistantType.TabStop = false;
            // 
            // txt_Intensity
            // 
            resources.ApplyResources(this.txt_Intensity, "txt_Intensity");
            this.txt_Intensity.Name = "txt_Intensity";
            this.txt_Intensity.ReadOnly = true;
            this.txt_Intensity.TabStop = false;
            // 
            // txt_AssistantName
            // 
            resources.ApplyResources(this.txt_AssistantName, "txt_AssistantName");
            this.txt_AssistantName.Name = "txt_AssistantName";
            this.txt_AssistantName.ReadOnly = true;
            this.txt_AssistantName.TabStop = false;
            // 
            // lab_AssistantName
            // 
            resources.ApplyResources(this.lab_AssistantName, "lab_AssistantName");
            this.lab_AssistantName.Name = "lab_AssistantName";
            // 
            // lab_Intensity
            // 
            resources.ApplyResources(this.lab_Intensity, "lab_Intensity");
            this.lab_Intensity.Name = "lab_Intensity";
            // 
            // lab_Cost
            // 
            resources.ApplyResources(this.lab_Cost, "lab_Cost");
            this.lab_Cost.Name = "lab_Cost";
            // 
            // lab_AllowMinColoringConcentration
            // 
            resources.ApplyResources(this.lab_AllowMinColoringConcentration, "lab_AllowMinColoringConcentration");
            this.lab_AllowMinColoringConcentration.Name = "lab_AllowMinColoringConcentration";
            // 
            // lab_AllowMaxColoringConcentration
            // 
            resources.ApplyResources(this.lab_AllowMaxColoringConcentration, "lab_AllowMaxColoringConcentration");
            this.lab_AllowMaxColoringConcentration.Name = "lab_AllowMaxColoringConcentration";
            // 
            // lab_TermOfValidity
            // 
            resources.ApplyResources(this.lab_TermOfValidity, "lab_TermOfValidity");
            this.lab_TermOfValidity.Name = "lab_TermOfValidity";
            // 
            // lab_AssistantType
            // 
            resources.ApplyResources(this.lab_AssistantType, "lab_AssistantType");
            this.lab_AssistantType.Name = "lab_AssistantType";
            // 
            // BottleDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_AdjustParameters);
            this.Controls.Add(this.grp_BottleDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BottleDetails";
            this.ShowIcon = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BottleDetails_Load);
            this.grp_BottleDetails.ResumeLayout(false);
            this.grp_BottleDetails.PerformLayout();
            this.grp_AdjustParameters.ResumeLayout(false);
            this.grp_AdjustParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_BottleDetails;
        private System.Windows.Forms.TextBox txt_SettingConcentration;
        private System.Windows.Forms.TextBox txt_RealConcentration;
        private System.Windows.Forms.TextBox txt_CurrentWeight;
        private System.Windows.Forms.TextBox txt_BrewingCode;
        private System.Windows.Forms.TextBox txt_LastAdjustWeight;
        private System.Windows.Forms.TextBox txt_CurrentAdjustWeight;
        private System.Windows.Forms.TextBox txt_BrewingData;
        private System.Windows.Forms.TextBox txt_AdjustValue;
        private System.Windows.Forms.TextBox txt_AssistantCode;
        private System.Windows.Forms.TextBox txt_AllowMaxWeight;
        private System.Windows.Forms.TextBox txt_BottleNum;
        private System.Windows.Forms.Label lab_AdjustValue;
        private System.Windows.Forms.Label lab_CurrentAdjustWeight;
        private System.Windows.Forms.Label lab_LastAdjustWeight;
        private System.Windows.Forms.Label lab_AssistantCode;
        private System.Windows.Forms.Label lab_BrewingCode;
        private System.Windows.Forms.Label lab_BrewingData;
        private System.Windows.Forms.Label lab_CurrentWeight;
        private System.Windows.Forms.Label lab_AllowMaxWeight;
        private System.Windows.Forms.Label lab_RealConcentration;
        private System.Windows.Forms.Label lab_SettingConcentration;
        private System.Windows.Forms.Label lab_BottleNum;
        private System.Windows.Forms.GroupBox grp_AdjustParameters;
        private System.Windows.Forms.Label lab_AssistantName;
        private System.Windows.Forms.Label lab_Intensity;
        private System.Windows.Forms.Label lab_Cost;
        private System.Windows.Forms.Label lab_AllowMinColoringConcentration;
        private System.Windows.Forms.Label lab_AllowMaxColoringConcentration;
        private System.Windows.Forms.Label lab_TermOfValidity;
        private System.Windows.Forms.Label lab_AssistantType;
        private System.Windows.Forms.TextBox txt_AllowMaxColoringConcentration;
        private System.Windows.Forms.TextBox txt_TermOfValidity;
        private System.Windows.Forms.TextBox txt_AllowMinColoringConcentration;
        private System.Windows.Forms.TextBox txt_Cost;
        private System.Windows.Forms.TextBox txt_AssistantType;
        private System.Windows.Forms.TextBox txt_Intensity;
        private System.Windows.Forms.TextBox txt_AssistantName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label txt_UnitOfAccount;
        private TextBox txt_SelfChecking3;
        private Label lb_SelfChecking3;
        private TextBox txt_SelfChecking2;
        private Label lb_SelfChecking2;
        private TextBox txt_SelfChecking1;
        private Label lb_SelfChecking1;
        private TextBox txt_SelfChecking4;
        private Label lb_SelfChecking4;
    }
}