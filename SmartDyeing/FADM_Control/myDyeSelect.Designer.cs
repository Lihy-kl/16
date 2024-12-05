namespace SmartDyeing.FADM_Control
{
    partial class myDyeSelect
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dy_type_comboBox1 = new System.Windows.Forms.ComboBox();
            this.dy_nodelist_comboBox2 = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1890, 86);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dy_type_comboBox1);
            this.groupBox1.Controls.Add(this.dy_nodelist_comboBox2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1872, 82);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工艺:";
            // 
            // dy_type_comboBox1
            // 
            this.dy_type_comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.dy_type_comboBox1.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dy_type_comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dy_type_comboBox1.FormattingEnabled = true;
            this.dy_type_comboBox1.Items.AddRange(new object[] {
            "",
            "染色工艺",
            "后处理工艺"});
            this.dy_type_comboBox1.Location = new System.Drawing.Point(26, 25);
            this.dy_type_comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dy_type_comboBox1.Name = "dy_type_comboBox1";
            this.dy_type_comboBox1.Size = new System.Drawing.Size(300, 32);
            this.dy_type_comboBox1.TabIndex = 49;
            // 
            // dy_nodelist_comboBox2
            // 
            this.dy_nodelist_comboBox2.Font = new System.Drawing.Font("宋体", 14.25F);
            this.dy_nodelist_comboBox2.FormattingEnabled = true;
            this.dy_nodelist_comboBox2.Location = new System.Drawing.Point(349, 25);
            this.dy_nodelist_comboBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dy_nodelist_comboBox2.Name = "dy_nodelist_comboBox2";
            this.dy_nodelist_comboBox2.Size = new System.Drawing.Size(300, 32);
            this.dy_nodelist_comboBox2.TabIndex = 50;
            // 
            // myDyeSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "myDyeSelect";
            this.Size = new System.Drawing.Size(1905, 101);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox dy_type_comboBox1;
        public System.Windows.Forms.ComboBox dy_nodelist_comboBox2;
    }
}
