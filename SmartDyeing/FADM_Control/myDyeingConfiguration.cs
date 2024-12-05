using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class myDyeingConfiguration : UserControl
    {
        public myDyeingConfiguration()
        {
            InitializeComponent();

            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                //设置标题字体
                dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12F);

                //设置内容字体
                dgv_dyconfiglisg.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
            }
            else
            {

                //设置标题字体
                dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);

                //设置内容字体
                dgv_dyconfiglisg.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
            }
        }

     
    }
}
