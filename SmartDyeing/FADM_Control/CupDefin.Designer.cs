using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class CupDefin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CupDefin));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Enable = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PnlCup = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_IsFixed = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_IsUsing = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chk_IsUsing = new System.Windows.Forms.CheckBox();
            this.chk_IsFixed = new System.Windows.Forms.CheckBox();
            this.chk_Enable = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txt_Enable);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txt_Enable
            // 
            resources.ApplyResources(this.txt_Enable, "txt_Enable");
            this.txt_Enable.BackColor = System.Drawing.SystemColors.Control;
            this.txt_Enable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Enable.Name = "txt_Enable";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.PnlCup);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // PnlCup
            // 
            resources.ApplyResources(this.PnlCup, "PnlCup");
            this.PnlCup.Name = "PnlCup";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.txt_IsFixed);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txt_IsFixed
            // 
            resources.ApplyResources(this.txt_IsFixed, "txt_IsFixed");
            this.txt_IsFixed.BackColor = System.Drawing.SystemColors.Control;
            this.txt_IsFixed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_IsFixed.Name = "txt_IsFixed";
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.txt_IsUsing);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // txt_IsUsing
            // 
            resources.ApplyResources(this.txt_IsUsing, "txt_IsUsing");
            this.txt_IsUsing.BackColor = System.Drawing.SystemColors.Control;
            this.txt_IsUsing.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_IsUsing.Name = "txt_IsUsing";
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.chk_IsUsing);
            this.groupBox5.Controls.Add(this.chk_IsFixed);
            this.groupBox5.Controls.Add(this.chk_Enable);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // chk_IsUsing
            // 
            resources.ApplyResources(this.chk_IsUsing, "chk_IsUsing");
            this.chk_IsUsing.Name = "chk_IsUsing";
            this.chk_IsUsing.UseVisualStyleBackColor = true;
            // 
            // chk_IsFixed
            // 
            resources.ApplyResources(this.chk_IsFixed, "chk_IsFixed");
            this.chk_IsFixed.Name = "chk_IsFixed";
            this.chk_IsFixed.UseVisualStyleBackColor = true;
            // 
            // chk_Enable
            // 
            resources.ApplyResources(this.chk_Enable, "chk_Enable");
            this.chk_Enable.Name = "chk_Enable";
            this.chk_Enable.UseVisualStyleBackColor = true;
            // 
            // CupDefin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CupDefin";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Panel PnlCup;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private TextBox txt_Enable;
        private TextBox txt_IsFixed;
        private TextBox txt_IsUsing;
        private CheckBox chk_IsUsing;
        private CheckBox chk_IsFixed;
        private CheckBox chk_Enable;
    }
}
