using Newtonsoft.Json.Linq;
using SmartDyeing.FADM_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmartDyeing.FADM_Control
{
    public partial class myDyeSelect : UserControl
    {


        public static Dictionary<int,NoActivateForm> noActivateFormsList = new Dictionary<int,NoActivateForm>();

        private static IList<string> temData;

        public myDyeSelect()
        {
            InitializeComponent();
            dy_type_comboBox1.MouseWheel += new MouseEventHandler(comboBox1_MouseWheel);
            dy_nodelist_comboBox2.MouseWheel += new MouseEventHandler(comboBox2_MouseWheel);
            dy_type_comboBox1.KeyPress += comboBox1KeyPress;

            dy_nodelist_comboBox2.KeyDown += new KeyEventHandler(comboBox2_KeyDown);

            dy_nodelist_comboBox2.Enter += new EventHandler(comboBox2_Enter);

            dy_nodelist_comboBox2.Leave += new EventHandler(comboBox2_Leave);
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cc = (System.Windows.Forms.ComboBox)sender;
            if (noActivateFormsList.ContainsKey(Convert.ToInt32(cc.Name))) {
                noActivateFormsList[Convert.ToInt32(cc.Name)].Visible = false;
                noActivateFormsList[Convert.ToInt32(cc.Name)].Close();
            }
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            Console.WriteLine(123);
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void comboBox1KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void dy_nodelist_comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.ComboBox cc = (System.Windows.Forms.ComboBox)sender;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (noActivateFormsList[Convert.ToInt32(cc.Name)].lb_End_Stations.SelectedIndex > 0)
                    noActivateFormsList[Convert.ToInt32(cc.Name)].lb_End_Stations.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (noActivateFormsList[Convert.ToInt32(cc.Name)].lb_End_Stations.SelectedIndex < noActivateFormsList[Convert.ToInt32(cc.Name)].lb_End_Stations.Items.Count - 1)
                    noActivateFormsList[Convert.ToInt32(cc.Name)].lb_End_Stations.SelectedIndex++;
            }//回车
            else if (e.KeyCode == Keys.Enter)
            {
                if (noActivateFormsList.ContainsKey(Convert.ToInt32(cc.Name)) && noActivateFormsList[Convert.ToInt32(cc.Name)].Focused && noActivateFormsList[Convert.ToInt32(cc.Name)].Visible) {
                    string info = noActivateFormsList[Convert.ToInt32(cc.Name)].lb_End_Stations.SelectedItem as string;
                    dy_nodelist_comboBox2.Text = info;

                } else if (noActivateFormsList.ContainsKey(Convert.ToInt32(cc.Name)) && !noActivateFormsList[Convert.ToInt32(cc.Name)].Focused) {
                    //noActivateFormsList[Convert.ToInt32(cc.Name)].TopMost = true;
                    //noActivateFormsList[Convert.ToInt32(cc.Name)].Focus();
                    noActivateFormsList[Convert.ToInt32(cc.Name)].Visible = false;
                    noActivateFormsList[Convert.ToInt32(cc.Name)].Close();
                    NoActivateForm n = null;
                    n = new NoActivateForm();
                    Point button1ScreenPos = cc.PointToScreen(Point.Empty);
                    n.lb_End_Stations.AccessibleName = cc.Name;
                    n.lb_End_Stations.KeyDown += new KeyEventHandler(tttt);
                    n.lb_End_Stations.DoubleClick += new EventHandler(listBox1_DoubleClick);
                    n.StartPosition = FormStartPosition.Manual;
                    n.Location = new Point(button1ScreenPos.X, button1ScreenPos.Y + 28);
                    n.Show();
                    noActivateFormsList[Convert.ToInt32(cc.Name)] = n;
                    IList<string> list = GetStations(cc.Text);
                    if (list.Count > 0)
                    {
                        n.lb_End_Stations.DataSource = list;
                    }
                }
                else if (!noActivateFormsList.ContainsKey(Convert.ToInt32(cc.Name)))
                {
                    NoActivateForm n = null;
                    n = new NoActivateForm();
                    n.lb_End_Stations.AccessibleName = cc.Name;
                    Point button1ScreenPos = cc.PointToScreen(Point.Empty);
                    n.lb_End_Stations.AccessibleName = cc.Name;
                    n.lb_End_Stations.KeyDown += new KeyEventHandler(tttt);
                    n.lb_End_Stations.DoubleClick += new EventHandler(listBox1_DoubleClick);
                    n.StartPosition = FormStartPosition.Manual;
                    n.Location = new Point(button1ScreenPos.X, button1ScreenPos.Y + 28);
                    n.Show();
                    noActivateFormsList.Add(Convert.ToInt32(cc.Name), n);
                    IList<string> list = GetStations(cc.Text);
                    if (list.Count > 0)
                    {
                        n.lb_End_Stations.DataSource = list;
                    }

                }
            } 
            else {
                Point button1ScreenPos = cc.PointToScreen(Point.Empty);
                NoActivateForm n = null;
                //noActivateFormsList.Add(Convert.ToInt32(cc.Name), n);

                if (noActivateFormsList.ContainsKey(Convert.ToInt32(cc.Name)))
                {
                    noActivateFormsList[Convert.ToInt32(cc.Name)].Visible = false;
                    noActivateFormsList[Convert.ToInt32(cc.Name)].Close();

                    n = noActivateFormsList[Convert.ToInt32(cc.Name)];
                    n = new NoActivateForm();
                    n.lb_End_Stations.AccessibleName = cc.Name;
                    noActivateFormsList[Convert.ToInt32(cc.Name)] = n;
                }
                else
                {
                    n = new NoActivateForm();
                    n.lb_End_Stations.AccessibleName = cc.Name;
                    noActivateFormsList.Add(Convert.ToInt32(cc.Name), n);
                }

                //n.lb_End_Stations.DrawItem += new DrawItemEventHandler(ListBox_StationDatas_DrawItem);
                //n.Focus();
                n.lb_End_Stations.KeyDown += new KeyEventHandler(tttt);
                n.lb_End_Stations.DoubleClick += new EventHandler(listBox1_DoubleClick);
                n.StartPosition = FormStartPosition.Manual;
                n.Location = new Point(button1ScreenPos.X, button1ScreenPos.Y + 28);
                n.Show();

                IList<string> list = GetStations(cc.Text);
                if (list.Count > 0)
                {
                    n.lb_End_Stations.DataSource = list;
                }
            }
        }

        private void tttt(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                System.Windows.Forms.ListBox cc = (System.Windows.Forms.ListBox)sender;
                string info = noActivateFormsList[Convert.ToInt32(cc.AccessibleName)].lb_End_Stations.SelectedItem as string;
                dy_nodelist_comboBox2.Text = info;
                noActivateFormsList[Convert.ToInt32(cc.AccessibleName)].Visible = false;
                noActivateFormsList[Convert.ToInt32(cc.AccessibleName)].Close();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            System.Windows.Forms.ListBox cc = (System.Windows.Forms.ListBox)sender;
            // 确保双击的是ListBox中的项
            if (cc.SelectedIndex != -1)
            {
                // 获取被双击的项
                string selectedItem = cc.SelectedItem.ToString();

                dy_nodelist_comboBox2.Text = selectedItem;
                noActivateFormsList[Convert.ToInt32(cc.AccessibleName)].Visible = false;
                noActivateFormsList[Convert.ToInt32(cc.AccessibleName)].Close() ;
                // 可以在这里添加代码来处理双击事件，例如弹出消息框显示项
            }
        }

        private void ListBox_StationDatas_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString((sender as ListBox).Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor),
                                    e.Bounds);
        }

        public  IList<string> GetStations(string filter)
        {
            IList<string> results = new List<string>();
            if (dy_type_comboBox1.Text.Equals("染色工艺"))
            {
                string s_sql = "SELECT Code  FROM dyeing_process where Type = 1 group by Code ;";
                DataTable dt_dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                foreach (DataRow dr in dt_dyeingcode.Rows)
                {
                    results.Add(Convert.ToString(dr[0]));
                }
            }
            else if (dy_type_comboBox1.Text.Equals("后处理工艺"))
            {
                string s_sql = "SELECT Code  FROM dyeing_process where Type = 2 group by Code ;";
                DataTable dt_dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                foreach (DataRow dr in dt_dyeingcode.Rows)
                {
                    results.Add(Convert.ToString(dr[0]));
                }
            }
            /*return results.Where(
                f =>
                (f.Substring(0, filter.Length) == filter)).ToList<string>();*/

            return results.Where(item => item.Contains(filter)).ToList();

        }


    }
}
