using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class CupInfo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CupInfo));
            this.txt_FoumulaCode = new System.Windows.Forms.TextBox();
            this.txt_DyeCode = new System.Windows.Forms.TextBox();
            this.lab_CupNum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_TotalTime = new System.Windows.Forms.TextBox();
            this.txt_RealTemp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Weight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_StepTotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Statues = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_SetTemp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_StepNum = new System.Windows.Forms.TextBox();
            this.txt_TechnologyName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_AllInLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_AllOffLine = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_CurrentStepTime = new System.Windows.Forms.TextBox();
            this.txt_SetTime = new System.Windows.Forms.TextBox();
            this.lab_OffLine = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_FoumulaCode
            // 
            this.txt_FoumulaCode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_FoumulaCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_FoumulaCode, "txt_FoumulaCode");
            this.txt_FoumulaCode.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_FoumulaCode.Name = "txt_FoumulaCode";
            this.txt_FoumulaCode.ReadOnly = true;
            // 
            // txt_DyeCode
            // 
            this.txt_DyeCode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_DyeCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_DyeCode, "txt_DyeCode");
            this.txt_DyeCode.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_DyeCode.Name = "txt_DyeCode";
            this.txt_DyeCode.ReadOnly = true;
            // 
            // lab_CupNum
            // 
            resources.ApplyResources(this.lab_CupNum, "lab_CupNum");
            this.lab_CupNum.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lab_CupNum.Name = "lab_CupNum";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txt_TotalTime
            // 
            this.txt_TotalTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_TotalTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_TotalTime, "txt_TotalTime");
            this.txt_TotalTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_TotalTime.Name = "txt_TotalTime";
            this.txt_TotalTime.ReadOnly = true;
            // 
            // txt_RealTemp
            // 
            this.txt_RealTemp.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_RealTemp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_RealTemp, "txt_RealTemp");
            this.txt_RealTemp.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_RealTemp.Name = "txt_RealTemp";
            this.txt_RealTemp.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txt_Weight
            // 
            this.txt_Weight.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_Weight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_Weight, "txt_Weight");
            this.txt_Weight.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_Weight.Name = "txt_Weight";
            this.txt_Weight.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txt_StepTotal
            // 
            this.txt_StepTotal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_StepTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_StepTotal, "txt_StepTotal");
            this.txt_StepTotal.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_StepTotal.Name = "txt_StepTotal";
            this.txt_StepTotal.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txt_Statues
            // 
            this.txt_Statues.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_Statues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_Statues, "txt_Statues");
            this.txt_Statues.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_Statues.Name = "txt_Statues";
            this.txt_Statues.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txt_SetTemp
            // 
            this.txt_SetTemp.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_SetTemp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_SetTemp, "txt_SetTemp");
            this.txt_SetTemp.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_SetTemp.Name = "txt_SetTemp";
            this.txt_SetTemp.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txt_StepNum
            // 
            this.txt_StepNum.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_StepNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_StepNum, "txt_StepNum");
            this.txt_StepNum.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_StepNum.Name = "txt_StepNum";
            this.txt_StepNum.ReadOnly = true;
            // 
            // txt_TechnologyName
            // 
            this.txt_TechnologyName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_TechnologyName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_TechnologyName, "txt_TechnologyName");
            this.txt_TechnologyName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_TechnologyName.Name = "txt_TechnologyName";
            this.txt_TechnologyName.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_AllInLine,
            this.tsm_AllOffLine});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // tsm_AllInLine
            // 
            this.tsm_AllInLine.Name = "tsm_AllInLine";
            resources.ApplyResources(this.tsm_AllInLine, "tsm_AllInLine");
            this.tsm_AllInLine.Click += new System.EventHandler(this.tsm_AllInLine_Click);
            // 
            // tsm_AllOffLine
            // 
            this.tsm_AllOffLine.Name = "tsm_AllOffLine";
            resources.ApplyResources(this.tsm_AllOffLine, "tsm_AllOffLine");
            this.tsm_AllOffLine.Click += new System.EventHandler(this.tsm_AllOffLine_Click);
            // 
            // txt_CurrentStepTime
            // 
            this.txt_CurrentStepTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_CurrentStepTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_CurrentStepTime, "txt_CurrentStepTime");
            this.txt_CurrentStepTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_CurrentStepTime.Name = "txt_CurrentStepTime";
            this.txt_CurrentStepTime.ReadOnly = true;
            // 
            // txt_SetTime
            // 
            this.txt_SetTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_SetTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_SetTime, "txt_SetTime");
            this.txt_SetTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txt_SetTime.Name = "txt_SetTime";
            this.txt_SetTime.ReadOnly = true;
            // 
            // lab_OffLine
            // 
            resources.ApplyResources(this.lab_OffLine, "lab_OffLine");
            this.lab_OffLine.ForeColor = System.Drawing.Color.Red;
            this.lab_OffLine.Name = "lab_OffLine";
            // 
            // CupInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lab_OffLine);
            this.Controls.Add(this.txt_SetTime);
            this.Controls.Add(this.txt_CurrentStepTime);
            this.Controls.Add(this.txt_TechnologyName);
            this.Controls.Add(this.txt_StepNum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_SetTemp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Statues);
            this.Controls.Add(this.txt_StepTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Weight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_RealTemp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_TotalTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_DyeCode);
            this.Controls.Add(this.txt_FoumulaCode);
            this.Controls.Add(this.lab_CupNum);
            resources.ApplyResources(this, "$this");
            this.Name = "CupInfo";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txt_FoumulaCode;
        private TextBox txt_DyeCode;
        private Label lab_CupNum;
        private Label label1;
        private TextBox txt_TotalTime;
        private TextBox txt_RealTemp;
        private Label label2;
        private TextBox txt_Weight;
        private Label label3;
        private TextBox txt_StepTotal;
        private Label label4;
        private TextBox txt_Statues;
        private Label label5;
        private TextBox txt_SetTemp;
        private Label label6;
        private TextBox txt_StepNum;
        private TextBox txt_TechnologyName;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem tsm_AllInLine;
        private ToolStripMenuItem tsm_AllOffLine;
        private TextBox txt_CurrentStepTime;
        private TextBox txt_SetTime;
        private Label lab_OffLine;
    }
}
