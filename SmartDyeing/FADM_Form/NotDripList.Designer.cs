namespace SmartDyeing.FADM_Form
{
    partial class NotDripList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotDripList));
            this.grp_BatchData = new System.Windows.Forms.GroupBox();
            this.btn_Refalsh = new System.Windows.Forms.Button();
            this.rdo_Record_All = new System.Windows.Forms.RadioButton();
            this.rdo_Record_Now = new System.Windows.Forms.RadioButton();
            this.dgv_BatchData = new System.Windows.Forms.DataGridView();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grp_BatchData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BatchData)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_BatchData
            // 
            resources.ApplyResources(this.grp_BatchData, "grp_BatchData");
            this.grp_BatchData.Controls.Add(this.btn_Refalsh);
            this.grp_BatchData.Controls.Add(this.rdo_Record_All);
            this.grp_BatchData.Controls.Add(this.rdo_Record_Now);
            this.grp_BatchData.Controls.Add(this.dgv_BatchData);
            this.grp_BatchData.Name = "grp_BatchData";
            this.grp_BatchData.TabStop = false;
            // 
            // btn_Refalsh
            // 
            resources.ApplyResources(this.btn_Refalsh, "btn_Refalsh");
            this.btn_Refalsh.Name = "btn_Refalsh";
            this.btn_Refalsh.UseVisualStyleBackColor = true;
            this.btn_Refalsh.Click += new System.EventHandler(this.btn_Refalsh_Click);
            // 
            // rdo_Record_All
            // 
            resources.ApplyResources(this.rdo_Record_All, "rdo_Record_All");
            this.rdo_Record_All.Name = "rdo_Record_All";
            this.rdo_Record_All.UseVisualStyleBackColor = true;
            // 
            // rdo_Record_Now
            // 
            resources.ApplyResources(this.rdo_Record_Now, "rdo_Record_Now");
            this.rdo_Record_Now.Checked = true;
            this.rdo_Record_Now.Name = "rdo_Record_Now";
            this.rdo_Record_Now.TabStop = true;
            this.rdo_Record_Now.UseVisualStyleBackColor = true;
            // 
            // dgv_BatchData
            // 
            resources.ApplyResources(this.dgv_BatchData, "dgv_BatchData");
            this.dgv_BatchData.AllowUserToAddRows = false;
            this.dgv_BatchData.AllowUserToDeleteRows = false;
            this.dgv_BatchData.AllowUserToResizeColumns = false;
            this.dgv_BatchData.AllowUserToResizeRows = false;
            this.dgv_BatchData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_BatchData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_BatchData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_BatchData.Name = "dgv_BatchData";
            this.dgv_BatchData.ReadOnly = true;
            this.dgv_BatchData.RowHeadersVisible = false;
            this.dgv_BatchData.RowTemplate.Height = 23;
            this.dgv_BatchData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btn_Delete
            // 
            resources.ApplyResources(this.btn_Delete, "btn_Delete");
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Add
            // 
            resources.ApplyResources(this.btn_Add, "btn_Add");
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NotDripList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.grp_BatchData);
            this.Name = "NotDripList";
            this.ShowIcon = false;
            this.TopMost = true;
            this.grp_BatchData.ResumeLayout(false);
            this.grp_BatchData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BatchData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_BatchData;
        private System.Windows.Forms.DataGridView dgv_BatchData;
        private System.Windows.Forms.RadioButton rdo_Record_All;
        private System.Windows.Forms.RadioButton rdo_Record_Now;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Refalsh;
        private System.Windows.Forms.Timer timer1;
    }
}