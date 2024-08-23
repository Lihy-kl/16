﻿using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnUserSwitching = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.MiBrewingProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.MiDyeingProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.DyeingProcessConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.PostTreatmentProcessConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.DyeingAndFixationProcessConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.MiAssistant = new System.Windows.Forms.ToolStripMenuItem();
            this.MiBottle = new System.Windows.Forms.ToolStripMenuItem();
            this.MiCup = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLimitSet = new System.Windows.Forms.ToolStripMenuItem();
            this.MiFormulaGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.MiOperator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButton6 = new System.Windows.Forms.ToolStripSplitButton();
            this.MiOut1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLow1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MiFullDrip1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.MiDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.MiRun = new System.Windows.Forms.ToolStripMenuItem();
            this.MiAlarm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnMain = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.DetailInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton5 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton4 = new System.Windows.Forms.ToolStripSplitButton();
            this.MiHistoryPage = new System.Windows.Forms.ToolStripMenuItem();
            this.MiFormulaPage = new System.Windows.Forms.ToolStripMenuItem();
            this.MiBrewPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnNTD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton3 = new System.Windows.Forms.ToolStripSplitButton();
            this.MiAbort = new System.Windows.Forms.ToolStripMenuItem();
            this.MiRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.LabStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnResetBrew = new System.Windows.Forms.ToolStripButton();
            this.PnlMain = new System.Windows.Forms.Panel();
            this.TmrMain = new System.Windows.Forms.Timer(this.components);
            this.TmrTemp = new System.Windows.Forms.Timer(this.components);
            this.TmrFGY = new System.Windows.Forms.Timer(this.components);
            this.timerReg = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnUserSwitching,
            this.toolStripSeparator1,
            this.toolStripSplitButton1,
            this.toolStripSplitButton6,
            this.toolStripSeparator2,
            this.toolStripSplitButton2,
            this.toolStripSeparator3,
            this.BtnMain,
            this.toolStripSeparator4,
            this.DetailInfo,
            this.toolStripSeparator9,
            this.toolStripSplitButton5,
            this.toolStripSeparator8,
            this.toolStripSplitButton4,
            this.toolStripSeparator6,
            this.BtnNTD,
            this.toolStripSeparator7,
            this.toolStripSplitButton3,
            this.LabStatus,
            this.toolStripLabel1,
            this.toolStripSeparator5,
            this.BtnResetBrew});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // BtnUserSwitching
            // 
            resources.ApplyResources(this.BtnUserSwitching, "BtnUserSwitching");
            this.BtnUserSwitching.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnUserSwitching.Name = "BtnUserSwitching";
            this.BtnUserSwitching.Click += new System.EventHandler(this.BtnUserSwitching_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripSplitButton1
            // 
            resources.ApplyResources(this.toolStripSplitButton1, "toolStripSplitButton1");
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiBrewingProcess,
            this.MiDyeingProcess,
            this.MiAssistant,
            this.MiBottle,
            this.MiCup,
            this.MiLimitSet,
            this.MiFormulaGroup,
            this.MiOperator});
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            // 
            // MiBrewingProcess
            // 
            resources.ApplyResources(this.MiBrewingProcess, "MiBrewingProcess");
            this.MiBrewingProcess.Name = "MiBrewingProcess";
            this.MiBrewingProcess.Click += new System.EventHandler(this.MiBrewingProcess_Click);
            // 
            // MiDyeingProcess
            // 
            resources.ApplyResources(this.MiDyeingProcess, "MiDyeingProcess");
            this.MiDyeingProcess.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DyeingProcessConfiguration,
            this.PostTreatmentProcessConfiguration,
            this.DyeingAndFixationProcessConfiguration});
            this.MiDyeingProcess.Name = "MiDyeingProcess";
            this.MiDyeingProcess.Click += new System.EventHandler(this.MiDyeingProcess_Click);
            // 
            // DyeingProcessConfiguration
            // 
            resources.ApplyResources(this.DyeingProcessConfiguration, "DyeingProcessConfiguration");
            this.DyeingProcessConfiguration.Name = "DyeingProcessConfiguration";
            this.DyeingProcessConfiguration.Click += new System.EventHandler(this.DyeingProcessConfiguration_Click);
            // 
            // PostTreatmentProcessConfiguration
            // 
            resources.ApplyResources(this.PostTreatmentProcessConfiguration, "PostTreatmentProcessConfiguration");
            this.PostTreatmentProcessConfiguration.Name = "PostTreatmentProcessConfiguration";
            this.PostTreatmentProcessConfiguration.Click += new System.EventHandler(this.PostTreatmentProcessConfiguration_Click);
            // 
            // DyeingAndFixationProcessConfiguration
            // 
            resources.ApplyResources(this.DyeingAndFixationProcessConfiguration, "DyeingAndFixationProcessConfiguration");
            this.DyeingAndFixationProcessConfiguration.Name = "DyeingAndFixationProcessConfiguration";
            this.DyeingAndFixationProcessConfiguration.Click += new System.EventHandler(this.DyeingAndFixationProcessConfiguration_Click);
            // 
            // MiAssistant
            // 
            resources.ApplyResources(this.MiAssistant, "MiAssistant");
            this.MiAssistant.Name = "MiAssistant";
            this.MiAssistant.Click += new System.EventHandler(this.MiAssistant_Click);
            // 
            // MiBottle
            // 
            resources.ApplyResources(this.MiBottle, "MiBottle");
            this.MiBottle.Name = "MiBottle";
            this.MiBottle.Click += new System.EventHandler(this.MiBottle_Click);
            // 
            // MiCup
            // 
            resources.ApplyResources(this.MiCup, "MiCup");
            this.MiCup.Name = "MiCup";
            this.MiCup.Click += new System.EventHandler(this.MiCup_Click);
            // 
            // MiLimitSet
            // 
            resources.ApplyResources(this.MiLimitSet, "MiLimitSet");
            this.MiLimitSet.Name = "MiLimitSet";
            this.MiLimitSet.Click += new System.EventHandler(this.MiLimitSet_Click);
            // 
            // MiFormulaGroup
            // 
            resources.ApplyResources(this.MiFormulaGroup, "MiFormulaGroup");
            this.MiFormulaGroup.Name = "MiFormulaGroup";
            this.MiFormulaGroup.Click += new System.EventHandler(this.MiFormulaGroup_Click);
            // 
            // MiOperator
            // 
            resources.ApplyResources(this.MiOperator, "MiOperator");
            this.MiOperator.Name = "MiOperator";
            this.MiOperator.Click += new System.EventHandler(this.MiOperator_Click);
            // 
            // toolStripSplitButton6
            // 
            resources.ApplyResources(this.toolStripSplitButton6, "toolStripSplitButton6");
            this.toolStripSplitButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiOut1,
            this.MiLow1,
            this.MiFullDrip1});
            this.toolStripSplitButton6.Name = "toolStripSplitButton6";
            // 
            // MiOut1
            // 
            resources.ApplyResources(this.MiOut1, "MiOut1");
            this.MiOut1.Name = "MiOut1";
            this.MiOut1.Click += new System.EventHandler(this.MiOut1_Click);
            // 
            // MiLow1
            // 
            resources.ApplyResources(this.MiLow1, "MiLow1");
            this.MiLow1.Name = "MiLow1";
            this.MiLow1.Click += new System.EventHandler(this.MiLow1_Click);
            // 
            // MiFullDrip1
            // 
            resources.ApplyResources(this.MiFullDrip1, "MiFullDrip1");
            this.MiFullDrip1.Name = "MiFullDrip1";
            this.MiFullDrip1.Click += new System.EventHandler(this.MiFullDrip1_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // toolStripSplitButton2
            // 
            resources.ApplyResources(this.toolStripSplitButton2, "toolStripSplitButton2");
            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiDebug,
            this.MiRun,
            this.MiAlarm});
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            // 
            // MiDebug
            // 
            resources.ApplyResources(this.MiDebug, "MiDebug");
            this.MiDebug.Name = "MiDebug";
            this.MiDebug.Click += new System.EventHandler(this.MiDebug_Click);
            // 
            // MiRun
            // 
            resources.ApplyResources(this.MiRun, "MiRun");
            this.MiRun.Name = "MiRun";
            this.MiRun.Click += new System.EventHandler(this.MiRun_Click);
            // 
            // MiAlarm
            // 
            resources.ApplyResources(this.MiAlarm, "MiAlarm");
            this.MiAlarm.Name = "MiAlarm";
            this.MiAlarm.Click += new System.EventHandler(this.MiAlarm_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // BtnMain
            // 
            resources.ApplyResources(this.BtnMain, "BtnMain");
            this.BtnMain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnMain.Name = "BtnMain";
            this.BtnMain.Click += new System.EventHandler(this.BtnMain_Click);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // DetailInfo
            // 
            resources.ApplyResources(this.DetailInfo, "DetailInfo");
            this.DetailInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DetailInfo.Name = "DetailInfo";
            this.DetailInfo.Click += new System.EventHandler(this.DetailInfo_Click);
            // 
            // toolStripSeparator9
            // 
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            // 
            // toolStripSplitButton5
            // 
            resources.ApplyResources(this.toolStripSplitButton5, "toolStripSplitButton5");
            this.toolStripSplitButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton5.Name = "toolStripSplitButton5";
            this.toolStripSplitButton5.DropDownOpening += new System.EventHandler(this.toolStripSplitButton5_DropDownOpening);
            this.toolStripSplitButton5.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripSplitButton5_DropDownItemClicked);
            this.toolStripSplitButton5.Click += new System.EventHandler(this.toolStripSplitButton5_Click);
            // 
            // toolStripSeparator8
            // 
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            // 
            // toolStripSplitButton4
            // 
            resources.ApplyResources(this.toolStripSplitButton4, "toolStripSplitButton4");
            this.toolStripSplitButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiHistoryPage,
            this.MiFormulaPage,
            this.MiBrewPage});
            this.toolStripSplitButton4.Name = "toolStripSplitButton4";
            // 
            // MiHistoryPage
            // 
            resources.ApplyResources(this.MiHistoryPage, "MiHistoryPage");
            this.MiHistoryPage.Name = "MiHistoryPage";
            this.MiHistoryPage.Click += new System.EventHandler(this.MiHistoryPage_Click);
            // 
            // MiFormulaPage
            // 
            resources.ApplyResources(this.MiFormulaPage, "MiFormulaPage");
            this.MiFormulaPage.Name = "MiFormulaPage";
            this.MiFormulaPage.Click += new System.EventHandler(this.MiFormulaPage_Click);
            // 
            // MiBrewPage
            // 
            resources.ApplyResources(this.MiBrewPage, "MiBrewPage");
            this.MiBrewPage.Name = "MiBrewPage";
            this.MiBrewPage.Click += new System.EventHandler(this.MiBrewPage_Click);
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // BtnNTD
            // 
            resources.ApplyResources(this.BtnNTD, "BtnNTD");
            this.BtnNTD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnNTD.Name = "BtnNTD";
            this.BtnNTD.Click += new System.EventHandler(this.BtnNTD_Click);
            // 
            // toolStripSeparator7
            // 
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            // 
            // toolStripSplitButton3
            // 
            resources.ApplyResources(this.toolStripSplitButton3, "toolStripSplitButton3");
            this.toolStripSplitButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiAbort,
            this.MiRegister});
            this.toolStripSplitButton3.Name = "toolStripSplitButton3";
            // 
            // MiAbort
            // 
            resources.ApplyResources(this.MiAbort, "MiAbort");
            this.MiAbort.Name = "MiAbort";
            this.MiAbort.Click += new System.EventHandler(this.MiAbort_Click);
            // 
            // MiRegister
            // 
            resources.ApplyResources(this.MiRegister, "MiRegister");
            this.MiRegister.Name = "MiRegister";
            this.MiRegister.Click += new System.EventHandler(this.MiRegister_Click);
            // 
            // LabStatus
            // 
            resources.ApplyResources(this.LabStatus, "LabStatus");
            this.LabStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.LabStatus.ForeColor = System.Drawing.Color.Red;
            this.LabStatus.Name = "LabStatus";
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // BtnResetBrew
            // 
            resources.ApplyResources(this.BtnResetBrew, "BtnResetBrew");
            this.BtnResetBrew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnResetBrew.ForeColor = System.Drawing.Color.Red;
            this.BtnResetBrew.Name = "BtnResetBrew";
            this.BtnResetBrew.Click += new System.EventHandler(this.BtnResetBrew_Click);
            // 
            // PnlMain
            // 
            resources.ApplyResources(this.PnlMain, "PnlMain");
            this.PnlMain.Name = "PnlMain";
            // 
            // TmrMain
            // 
            this.TmrMain.Enabled = true;
            this.TmrMain.Tick += new System.EventHandler(this.TmrMain_Tick);
            // 
            // TmrTemp
            // 
            this.TmrTemp.Enabled = true;
            this.TmrTemp.Interval = 30000;
            this.TmrTemp.Tick += new System.EventHandler(this.TmrTemp_Tick);
            // 
            // TmrFGY
            // 
            this.TmrFGY.Interval = 1000;
            this.TmrFGY.Tick += new System.EventHandler(this.TmrFGY_Tick);
            // 
            // timerReg
            // 
            this.timerReg.Enabled = true;
            this.timerReg.Interval = 3600000;
            this.timerReg.Tick += new System.EventHandler(this.timerReg_Tick);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.Name = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        public ToolStripButton BtnUserSwitching;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton BtnResetBrew;
        private ToolStripLabel LabStatus;
        private Panel PnlMain;
        private ToolStripMenuItem MiBrewingProcess;
        private ToolStripMenuItem MiDyeingProcess;
        private ToolStripMenuItem MiAssistant;
        private ToolStripMenuItem MiBottle;
        private ToolStripMenuItem MiDebug;
        private ToolStripMenuItem MiRun;
        private ToolStripMenuItem MiAlarm;
        private ToolStripSplitButton toolStripSplitButton3;
        private ToolStripMenuItem MiAbort;
        public ToolStripSplitButton toolStripSplitButton1;
        public ToolStripSplitButton toolStripSplitButton2;
        public ToolStripMenuItem MiRegister;
        private System.Windows.Forms.Timer TmrMain;
        private ToolStripMenuItem MiCup;
        public ToolStripButton BtnMain;
        private ToolStripButton DetailInfo;
        private System.Windows.Forms.Timer TmrTemp;
        private ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.Timer TmrFGY;
        private ToolStripMenuItem MiLimitSet;
        private ToolStripSplitButton toolStripSplitButton4;
        private ToolStripMenuItem MiHistoryPage;
        private ToolStripMenuItem MiFormulaPage;
        private ToolStripMenuItem MiOperator;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripSplitButton toolStripSplitButton5;
        private ToolStripLabel toolStripLabel1;
        private Timer timerReg;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton BtnNTD;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem MiFormulaGroup;
        private ToolStripMenuItem DyeingProcessConfiguration;
        private ToolStripMenuItem PostTreatmentProcessConfiguration;
        private ToolStripMenuItem DyeingAndFixationProcessConfiguration;
        private ToolStripMenuItem MiBrewPage;
        private ToolStripMenuItem MiOut1;
        private ToolStripMenuItem MiLow1;
        private ToolStripMenuItem MiFullDrip1;
        public ToolStripSplitButton toolStripSplitButton6;
    }
}