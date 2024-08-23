using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    partial class BrewingProcess
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrewingProcess));
            this.grp_Browse = new System.Windows.Forms.GroupBox();
            this.dgv_BrewCode = new System.Windows.Forms.DataGridView();
            this.grp_BrewingProcess = new System.Windows.Forms.GroupBox();
            this.btn_BrewingProcessDelete = new System.Windows.Forms.Button();
            this.btn_BrewingProcessUpdate = new System.Windows.Forms.Button();
            this.btn_BrewingProcessAdd = new System.Windows.Forms.Button();
            this.dgv_BrewProcess = new System.Windows.Forms.DataGridView();
            this.btn_BrewingCodeDelete = new System.Windows.Forms.Button();
            this.btn_BrewingCodeAdd = new System.Windows.Forms.Button();
            this.txt_BrewCode = new System.Windows.Forms.TextBox();
            this.lab_BrewProcessCode = new System.Windows.Forms.Label();
            this.grp_Browse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BrewCode)).BeginInit();
            this.grp_BrewingProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BrewProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_Browse
            // 
            resources.ApplyResources(this.grp_Browse, "grp_Browse");
            this.grp_Browse.Controls.Add(this.dgv_BrewCode);
            this.grp_Browse.Name = "grp_Browse";
            this.grp_Browse.TabStop = false;
            // 
            // dgv_BrewCode
            // 
            resources.ApplyResources(this.dgv_BrewCode, "dgv_BrewCode");
            this.dgv_BrewCode.AllowUserToAddRows = false;
            this.dgv_BrewCode.AllowUserToDeleteRows = false;
            this.dgv_BrewCode.AllowUserToResizeColumns = false;
            this.dgv_BrewCode.AllowUserToResizeRows = false;
            this.dgv_BrewCode.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_BrewCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_BrewCode.MultiSelect = false;
            this.dgv_BrewCode.Name = "dgv_BrewCode";
            this.dgv_BrewCode.ReadOnly = true;
            this.dgv_BrewCode.RowHeadersVisible = false;
            this.dgv_BrewCode.RowTemplate.Height = 23;
            this.dgv_BrewCode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_BrewCode.CurrentCellChanged += new System.EventHandler(this.dgv_BrewCode_CurrentCellChanged);
            // 
            // grp_BrewingProcess
            // 
            resources.ApplyResources(this.grp_BrewingProcess, "grp_BrewingProcess");
            this.grp_BrewingProcess.Controls.Add(this.btn_BrewingProcessDelete);
            this.grp_BrewingProcess.Controls.Add(this.btn_BrewingProcessUpdate);
            this.grp_BrewingProcess.Controls.Add(this.btn_BrewingProcessAdd);
            this.grp_BrewingProcess.Controls.Add(this.dgv_BrewProcess);
            this.grp_BrewingProcess.Controls.Add(this.btn_BrewingCodeDelete);
            this.grp_BrewingProcess.Controls.Add(this.btn_BrewingCodeAdd);
            this.grp_BrewingProcess.Controls.Add(this.txt_BrewCode);
            this.grp_BrewingProcess.Controls.Add(this.lab_BrewProcessCode);
            this.grp_BrewingProcess.Name = "grp_BrewingProcess";
            this.grp_BrewingProcess.TabStop = false;
            // 
            // btn_BrewingProcessDelete
            // 
            resources.ApplyResources(this.btn_BrewingProcessDelete, "btn_BrewingProcessDelete");
            this.btn_BrewingProcessDelete.Name = "btn_BrewingProcessDelete";
            this.btn_BrewingProcessDelete.UseVisualStyleBackColor = true;
            this.btn_BrewingProcessDelete.Click += new System.EventHandler(this.btn_BrewingProcessDelete_Click);
            // 
            // btn_BrewingProcessUpdate
            // 
            resources.ApplyResources(this.btn_BrewingProcessUpdate, "btn_BrewingProcessUpdate");
            this.btn_BrewingProcessUpdate.Name = "btn_BrewingProcessUpdate";
            this.btn_BrewingProcessUpdate.UseVisualStyleBackColor = true;
            this.btn_BrewingProcessUpdate.Click += new System.EventHandler(this.btn_BrewingProcessUpdate_Click);
            // 
            // btn_BrewingProcessAdd
            // 
            resources.ApplyResources(this.btn_BrewingProcessAdd, "btn_BrewingProcessAdd");
            this.btn_BrewingProcessAdd.Name = "btn_BrewingProcessAdd";
            this.btn_BrewingProcessAdd.UseVisualStyleBackColor = true;
            this.btn_BrewingProcessAdd.Click += new System.EventHandler(this.btn_BrewingProcessAdd_Click);
            // 
            // dgv_BrewProcess
            // 
            resources.ApplyResources(this.dgv_BrewProcess, "dgv_BrewProcess");
            this.dgv_BrewProcess.AllowUserToAddRows = false;
            this.dgv_BrewProcess.AllowUserToDeleteRows = false;
            this.dgv_BrewProcess.AllowUserToResizeColumns = false;
            this.dgv_BrewProcess.AllowUserToResizeRows = false;
            this.dgv_BrewProcess.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_BrewProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_BrewProcess.MultiSelect = false;
            this.dgv_BrewProcess.Name = "dgv_BrewProcess";
            this.dgv_BrewProcess.ReadOnly = true;
            this.dgv_BrewProcess.RowHeadersVisible = false;
            this.dgv_BrewProcess.RowTemplate.Height = 23;
            this.dgv_BrewProcess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btn_BrewingCodeDelete
            // 
            resources.ApplyResources(this.btn_BrewingCodeDelete, "btn_BrewingCodeDelete");
            this.btn_BrewingCodeDelete.Name = "btn_BrewingCodeDelete";
            this.btn_BrewingCodeDelete.UseVisualStyleBackColor = true;
            this.btn_BrewingCodeDelete.Click += new System.EventHandler(this.btn_BrewingCodeDelete_Click);
            // 
            // btn_BrewingCodeAdd
            // 
            resources.ApplyResources(this.btn_BrewingCodeAdd, "btn_BrewingCodeAdd");
            this.btn_BrewingCodeAdd.Name = "btn_BrewingCodeAdd";
            this.btn_BrewingCodeAdd.UseVisualStyleBackColor = true;
            this.btn_BrewingCodeAdd.Click += new System.EventHandler(this.btn_BrewingCodeAdd_Click);
            // 
            // txt_BrewCode
            // 
            resources.ApplyResources(this.txt_BrewCode, "txt_BrewCode");
            this.txt_BrewCode.Name = "txt_BrewCode";
            this.txt_BrewCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_BrewCode_KeyDown);
            // 
            // lab_BrewProcessCode
            // 
            resources.ApplyResources(this.lab_BrewProcessCode, "lab_BrewProcessCode");
            this.lab_BrewProcessCode.Name = "lab_BrewProcessCode";
            // 
            // BrewingProcess
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_BrewingProcess);
            this.Controls.Add(this.grp_Browse);
            this.Name = "BrewingProcess";
            this.grp_Browse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BrewCode)).EndInit();
            this.grp_BrewingProcess.ResumeLayout(false);
            this.grp_BrewingProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BrewProcess)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Browse;
        private System.Windows.Forms.DataGridView dgv_BrewCode;
        private System.Windows.Forms.GroupBox grp_BrewingProcess;
        private System.Windows.Forms.TextBox txt_BrewCode;
        private System.Windows.Forms.Label lab_BrewProcessCode;
        private System.Windows.Forms.Button btn_BrewingProcessDelete;
        private System.Windows.Forms.Button btn_BrewingProcessUpdate;
        private System.Windows.Forms.Button btn_BrewingProcessAdd;
        private System.Windows.Forms.Button btn_BrewingCodeDelete;
        private System.Windows.Forms.Button btn_BrewingCodeAdd;
        private System.Windows.Forms.DataGridView dgv_BrewProcess;

    }
}
