
using System;
using System.Drawing;
//using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class HistoryAbsData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryAbsData));
            this.grp_DropRecord = new System.Windows.Forms.GroupBox();
            this.Btn_SetStand = new System.Windows.Forms.Button();
            this.txt_Record_CupNum = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rdo_Record_condition = new System.Windows.Forms.RadioButton();
            this.dt_Record_End = new System.Windows.Forms.DateTimePicker();
            this.dt_Record_Start = new System.Windows.Forms.DateTimePicker();
            this.btn_Record_Delete = new System.Windows.Forms.Button();
            this.btn_Record_Select = new System.Windows.Forms.Button();
            this.rdo_Record_All = new System.Windows.Forms.RadioButton();
            this.rdo_Record_Now = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_DropRecord = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgv_Details = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grp_FormulaData = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_BrewingData = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_RealConcentration = new System.Windows.Forms.TextBox();
            this.txt_BottleNum = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_dL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_dA = new System.Windows.Forms.TextBox();
            this.txt_dE = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_dB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_PB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_PA = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_PL = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_SB = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_SA = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_SL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.grp_DropRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DropRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Details)).BeginInit();
            this.grp_FormulaData.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_DropRecord
            // 
            this.grp_DropRecord.Controls.Add(this.Btn_SetStand);
            this.grp_DropRecord.Controls.Add(this.txt_Record_CupNum);
            this.grp_DropRecord.Controls.Add(this.label10);
            this.grp_DropRecord.Controls.Add(this.rdo_Record_condition);
            this.grp_DropRecord.Controls.Add(this.dt_Record_End);
            this.grp_DropRecord.Controls.Add(this.dt_Record_Start);
            this.grp_DropRecord.Controls.Add(this.btn_Record_Delete);
            this.grp_DropRecord.Controls.Add(this.btn_Record_Select);
            this.grp_DropRecord.Controls.Add(this.rdo_Record_All);
            this.grp_DropRecord.Controls.Add(this.rdo_Record_Now);
            this.grp_DropRecord.Controls.Add(this.label2);
            this.grp_DropRecord.Controls.Add(this.label3);
            this.grp_DropRecord.Controls.Add(this.dgv_DropRecord);
            resources.ApplyResources(this.grp_DropRecord, "grp_DropRecord");
            this.grp_DropRecord.Name = "grp_DropRecord";
            this.grp_DropRecord.TabStop = false;
            // 
            // Btn_SetStand
            // 
            resources.ApplyResources(this.Btn_SetStand, "Btn_SetStand");
            this.Btn_SetStand.Name = "Btn_SetStand";
            this.Btn_SetStand.UseVisualStyleBackColor = true;
            this.Btn_SetStand.Click += new System.EventHandler(this.Btn_SetStand_Click);
            // 
            // txt_Record_CupNum
            // 
            resources.ApplyResources(this.txt_Record_CupNum, "txt_Record_CupNum");
            this.txt_Record_CupNum.Name = "txt_Record_CupNum";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // rdo_Record_condition
            // 
            resources.ApplyResources(this.rdo_Record_condition, "rdo_Record_condition");
            this.rdo_Record_condition.Name = "rdo_Record_condition";
            this.rdo_Record_condition.UseVisualStyleBackColor = true;
            this.rdo_Record_condition.CheckedChanged += new System.EventHandler(this.rdo_Record_condition_CheckedChanged);
            // 
            // dt_Record_End
            // 
            resources.ApplyResources(this.dt_Record_End, "dt_Record_End");
            this.dt_Record_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Record_End.Name = "dt_Record_End";
            this.dt_Record_End.ShowUpDown = true;
            // 
            // dt_Record_Start
            // 
            resources.ApplyResources(this.dt_Record_Start, "dt_Record_Start");
            this.dt_Record_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Record_Start.Name = "dt_Record_Start";
            this.dt_Record_Start.ShowUpDown = true;
            this.dt_Record_Start.Value = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            // 
            // btn_Record_Delete
            // 
            resources.ApplyResources(this.btn_Record_Delete, "btn_Record_Delete");
            this.btn_Record_Delete.Name = "btn_Record_Delete";
            this.btn_Record_Delete.UseVisualStyleBackColor = true;
            this.btn_Record_Delete.Click += new System.EventHandler(this.btn_Record_Delete_Click);
            // 
            // btn_Record_Select
            // 
            resources.ApplyResources(this.btn_Record_Select, "btn_Record_Select");
            this.btn_Record_Select.Name = "btn_Record_Select";
            this.btn_Record_Select.UseVisualStyleBackColor = true;
            this.btn_Record_Select.Click += new System.EventHandler(this.btn_Record_Select_Click);
            // 
            // rdo_Record_All
            // 
            resources.ApplyResources(this.rdo_Record_All, "rdo_Record_All");
            this.rdo_Record_All.Name = "rdo_Record_All";
            this.rdo_Record_All.UseVisualStyleBackColor = true;
            this.rdo_Record_All.CheckedChanged += new System.EventHandler(this.rdo_Record_All_CheckedChanged);
            // 
            // rdo_Record_Now
            // 
            resources.ApplyResources(this.rdo_Record_Now, "rdo_Record_Now");
            this.rdo_Record_Now.Checked = true;
            this.rdo_Record_Now.Name = "rdo_Record_Now";
            this.rdo_Record_Now.TabStop = true;
            this.rdo_Record_Now.UseVisualStyleBackColor = true;
            this.rdo_Record_Now.CheckedChanged += new System.EventHandler(this.rdo_Record_Now_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dgv_DropRecord
            // 
            this.dgv_DropRecord.AllowUserToAddRows = false;
            this.dgv_DropRecord.AllowUserToDeleteRows = false;
            this.dgv_DropRecord.AllowUserToResizeColumns = false;
            this.dgv_DropRecord.AllowUserToResizeRows = false;
            this.dgv_DropRecord.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_DropRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_DropRecord, "dgv_DropRecord");
            this.dgv_DropRecord.MultiSelect = false;
            this.dgv_DropRecord.Name = "dgv_DropRecord";
            this.dgv_DropRecord.ReadOnly = true;
            this.dgv_DropRecord.RowHeadersVisible = false;
            this.dgv_DropRecord.RowTemplate.Height = 23;
            this.dgv_DropRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DropRecord.CurrentCellChanged += new System.EventHandler(this.dgv_DropRecord_CurrentCellChanged);
            this.dgv_DropRecord.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_DropRecord_DataBindingComplete);
            // 
            // dgv_Details
            // 
            this.dgv_Details.AllowUserToAddRows = false;
            this.dgv_Details.AllowUserToDeleteRows = false;
            this.dgv_Details.AllowUserToResizeColumns = false;
            this.dgv_Details.AllowUserToResizeRows = false;
            this.dgv_Details.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgv_Details, "dgv_Details");
            this.dgv_Details.MultiSelect = false;
            this.dgv_Details.Name = "dgv_Details";
            this.dgv_Details.ReadOnly = true;
            this.dgv_Details.RowHeadersVisible = false;
            this.dgv_Details.RowTemplate.Height = 23;
            this.dgv_Details.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // grp_FormulaData
            // 
            this.grp_FormulaData.Controls.Add(this.textBox4);
            this.grp_FormulaData.Controls.Add(this.label23);
            this.grp_FormulaData.Controls.Add(this.textBox2);
            this.grp_FormulaData.Controls.Add(this.textBox3);
            this.grp_FormulaData.Controls.Add(this.label21);
            this.grp_FormulaData.Controls.Add(this.label22);
            this.grp_FormulaData.Controls.Add(this.txt_BrewingData);
            this.grp_FormulaData.Controls.Add(this.label20);
            this.grp_FormulaData.Controls.Add(this.txt_RealConcentration);
            this.grp_FormulaData.Controls.Add(this.txt_BottleNum);
            this.grp_FormulaData.Controls.Add(this.label19);
            this.grp_FormulaData.Controls.Add(this.label18);
            this.grp_FormulaData.Controls.Add(this.label17);
            this.grp_FormulaData.Controls.Add(this.groupBox3);
            this.grp_FormulaData.Controls.Add(this.groupBox2);
            this.grp_FormulaData.Controls.Add(this.groupBox1);
            this.grp_FormulaData.Controls.Add(this.label1);
            this.grp_FormulaData.Controls.Add(this.textBox1);
            this.grp_FormulaData.Controls.Add(this.panel1);
            this.grp_FormulaData.Controls.Add(this.dgv_Details);
            resources.ApplyResources(this.grp_FormulaData, "grp_FormulaData");
            this.grp_FormulaData.Name = "grp_FormulaData";
            this.grp_FormulaData.TabStop = false;
            // 
            // textBox4
            // 
            resources.ApplyResources(this.textBox4, "textBox4");
            this.textBox4.Name = "textBox4";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // txt_BrewingData
            // 
            resources.ApplyResources(this.txt_BrewingData, "txt_BrewingData");
            this.txt_BrewingData.Name = "txt_BrewingData";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // txt_RealConcentration
            // 
            resources.ApplyResources(this.txt_RealConcentration, "txt_RealConcentration");
            this.txt_RealConcentration.Name = "txt_RealConcentration";
            // 
            // txt_BottleNum
            // 
            resources.ApplyResources(this.txt_BottleNum, "txt_BottleNum");
            this.txt_BottleNum.Name = "txt_BottleNum";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox6);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.textBox5);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txt_dL);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txt_dA);
            this.groupBox3.Controls.Add(this.txt_dE);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txt_dB);
            this.groupBox3.Controls.Add(this.label6);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txt_dL
            // 
            resources.ApplyResources(this.txt_dL, "txt_dL");
            this.txt_dL.Name = "txt_dL";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txt_dA
            // 
            resources.ApplyResources(this.txt_dA, "txt_dA");
            this.txt_dA.Name = "txt_dA";
            // 
            // txt_dE
            // 
            resources.ApplyResources(this.txt_dE, "txt_dE");
            this.txt_dE.Name = "txt_dE";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txt_dB
            // 
            resources.ApplyResources(this.txt_dB, "txt_dB");
            this.txt_dB.Name = "txt_dB";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_PB);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txt_PA);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txt_PL);
            this.groupBox2.Controls.Add(this.label14);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txt_PB
            // 
            resources.ApplyResources(this.txt_PB, "txt_PB");
            this.txt_PB.Name = "txt_PB";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // txt_PA
            // 
            resources.ApplyResources(this.txt_PA, "txt_PA");
            this.txt_PA.Name = "txt_PA";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // txt_PL
            // 
            resources.ApplyResources(this.txt_PL, "txt_PL");
            this.txt_PL.Name = "txt_PL";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_SB);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_SA);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_SL);
            this.groupBox1.Controls.Add(this.label8);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txt_SB
            // 
            resources.ApplyResources(this.txt_SB, "txt_SB");
            this.txt_SB.Name = "txt_SB";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txt_SA
            // 
            resources.ApplyResources(this.txt_SA, "txt_SA");
            this.txt_SA.Name = "txt_SA";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txt_SL
            // 
            resources.ApplyResources(this.txt_SL, "txt_SL");
            this.txt_SL.Name = "txt_SL";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // textBox5
            // 
            resources.ApplyResources(this.textBox5, "textBox5");
            this.textBox5.Name = "textBox5";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // textBox6
            // 
            resources.ApplyResources(this.textBox6, "textBox6");
            this.textBox6.Name = "textBox6";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // HistoryAbsData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_DropRecord);
            this.Controls.Add(this.grp_FormulaData);
            this.Name = "HistoryAbsData";
            this.grp_DropRecord.ResumeLayout(false);
            this.grp_DropRecord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DropRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Details)).EndInit();
            this.grp_FormulaData.ResumeLayout(false);
            this.grp_FormulaData.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grp_DropRecord;
        private System.Windows.Forms.RadioButton rdo_Record_condition;
        private System.Windows.Forms.DateTimePicker dt_Record_End;
        private System.Windows.Forms.DateTimePicker dt_Record_Start;
        private System.Windows.Forms.Button btn_Record_Delete;
        private System.Windows.Forms.Button btn_Record_Select;
        private System.Windows.Forms.RadioButton rdo_Record_All;
        private System.Windows.Forms.RadioButton rdo_Record_Now;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv_DropRecord;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txt_Record_CupNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgv_Details;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grp_FormulaData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txt_dA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_dL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_dB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_dE;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Btn_SetStand;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_SL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_PB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_PA;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_PL;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_SB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_SA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_BrewingData;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_RealConcentration;
        private System.Windows.Forms.TextBox txt_BottleNum;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label24;
    }
}
