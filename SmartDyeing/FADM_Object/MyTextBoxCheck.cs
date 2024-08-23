using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Object
{
    class MyTextBoxCheck
    {
        /*设置textBox只能输入数字（正数，小数）
        */
        public static bool NumberDotTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //允许输入数字、小数点、删除键和负号
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') )
            {
                return true;
            }
           
            //小数点只能输入一次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                return true;
            }
            //第一位不能为小数点
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                return true;
            }
            //第一位是0，第二位必须为小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                if (((TextBox)sender).SelectionLength > 0)
                {

                }
                else
                {
                    return true;
                }
            }
            

            return false;
        }

        public static bool NumberTextbox_KeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    return true;
                }
            }

            return false;
        }


        public static bool IntTextbox_KeyPress(object sender,KeyPressEventArgs e)
        {
            //允许输入数字、删除键和负号
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('-'))
            {
                return true;
            }

            //负号只能输入一次
            if (e.KeyChar == (char)('-') && ((TextBox)sender).Text.IndexOf('-') != -1)
            {
                return true;
            }
          
            return false;
        }

    }
}
