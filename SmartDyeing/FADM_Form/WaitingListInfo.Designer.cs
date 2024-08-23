using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class WaitingListInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitingListInfo));
            this.dgv_WaitList = new System.Windows.Forms.DataGridView();
            this.Btn_Up = new System.Windows.Forms.Button();
            this.Btn_Down = new System.Windows.Forms.Button();
            this.Btn_Del = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_WaitList1 = new System.Windows.Forms.DataGridView();
            this.Btn_Save1 = new System.Windows.Forms.Button();
            this.Btn_Up1 = new System.Windows.Forms.Button();
            this.Btn_Del1 = new System.Windows.Forms.Button();
            this.Btn_Down1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WaitList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WaitList1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_WaitList
            // 
            resources.ApplyResources(this.dgv_WaitList, "dgv_WaitList");
            this.dgv_WaitList.AllowUserToAddRows = false;
            this.dgv_WaitList.AllowUserToDeleteRows = false;
            this.dgv_WaitList.AllowUserToResizeColumns = false;
            this.dgv_WaitList.AllowUserToResizeRows = false;
            this.dgv_WaitList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_WaitList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_WaitList.MultiSelect = false;
            this.dgv_WaitList.Name = "dgv_WaitList";
            this.dgv_WaitList.ReadOnly = true;
            this.dgv_WaitList.RowHeadersVisible = false;
            this.dgv_WaitList.RowTemplate.Height = 23;
            this.dgv_WaitList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_WaitList.CurrentCellChanged += new System.EventHandler(this.dgv_WaitList_CurrentCellChanged);
            // 
            // Btn_Up
            // 
            resources.ApplyResources(this.Btn_Up, "Btn_Up");
            this.Btn_Up.Name = "Btn_Up";
            this.Btn_Up.UseVisualStyleBackColor = true;
            this.Btn_Up.Click += new System.EventHandler(this.Btn_Up_Click);
            // 
            // Btn_Down
            // 
            resources.ApplyResources(this.Btn_Down, "Btn_Down");
            this.Btn_Down.Name = "Btn_Down";
            this.Btn_Down.UseVisualStyleBackColor = true;
            this.Btn_Down.Click += new System.EventHandler(this.Btn_Down_Click);
            // 
            // Btn_Del
            // 
            resources.ApplyResources(this.Btn_Del, "Btn_Del");
            this.Btn_Del.Name = "Btn_Del";
            this.Btn_Del.UseVisualStyleBackColor = true;
            this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
            // 
            // Btn_Save
            // 
            resources.ApplyResources(this.Btn_Save, "Btn_Save");
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.dgv_WaitList);
            this.groupBox1.Controls.Add(this.Btn_Save);
            this.groupBox1.Controls.Add(this.Btn_Up);
            this.groupBox1.Controls.Add(this.Btn_Del);
            this.groupBox1.Controls.Add(this.Btn_Down);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.dgv_WaitList1);
            this.groupBox2.Controls.Add(this.Btn_Save1);
            this.groupBox2.Controls.Add(this.Btn_Up1);
            this.groupBox2.Controls.Add(this.Btn_Del1);
            this.groupBox2.Controls.Add(this.Btn_Down1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // dgv_WaitList1
            // 
            resources.ApplyResources(this.dgv_WaitList1, "dgv_WaitList1");
            this.dgv_WaitList1.AllowUserToAddRows = false;
            this.dgv_WaitList1.AllowUserToDeleteRows = false;
            this.dgv_WaitList1.AllowUserToResizeColumns = false;
            this.dgv_WaitList1.AllowUserToResizeRows = false;
            this.dgv_WaitList1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_WaitList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_WaitList1.MultiSelect = false;
            this.dgv_WaitList1.Name = "dgv_WaitList1";
            this.dgv_WaitList1.ReadOnly = true;
            this.dgv_WaitList1.RowHeadersVisible = false;
            this.dgv_WaitList1.RowTemplate.Height = 23;
            this.dgv_WaitList1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Btn_Save1
            // 
            resources.ApplyResources(this.Btn_Save1, "Btn_Save1");
            this.Btn_Save1.Name = "Btn_Save1";
            this.Btn_Save1.UseVisualStyleBackColor = true;
            this.Btn_Save1.Click += new System.EventHandler(this.Btn_Save1_Click);
            // 
            // Btn_Up1
            // 
            resources.ApplyResources(this.Btn_Up1, "Btn_Up1");
            this.Btn_Up1.Name = "Btn_Up1";
            this.Btn_Up1.UseVisualStyleBackColor = true;
            this.Btn_Up1.Click += new System.EventHandler(this.Btn_Up1_Click);
            // 
            // Btn_Del1
            // 
            resources.ApplyResources(this.Btn_Del1, "Btn_Del1");
            this.Btn_Del1.Name = "Btn_Del1";
            this.Btn_Del1.UseVisualStyleBackColor = true;
            this.Btn_Del1.Click += new System.EventHandler(this.Btn_Del1_Click);
            // 
            // Btn_Down1
            // 
            resources.ApplyResources(this.Btn_Down1, "Btn_Down1");
            this.Btn_Down1.Name = "Btn_Down1";
            this.Btn_Down1.UseVisualStyleBackColor = true;
            this.Btn_Down1.Click += new System.EventHandler(this.Btn_Down1_Click);
            // 
            // WaitingListInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WaitingListInfo";
            this.ShowIcon = false;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaitingListInfo_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WaitList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WaitList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgv_WaitList;
        private Button Btn_Up;
        private Button Btn_Down;
        private Button Btn_Del;
        private Button Btn_Save;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DataGridView dgv_WaitList1;
        private Button Btn_Save1;
        private Button Btn_Up1;
        private Button Btn_Del1;
        private Button Btn_Down1;
    }
}