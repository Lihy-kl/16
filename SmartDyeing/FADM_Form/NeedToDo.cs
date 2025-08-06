using Newtonsoft.Json.Linq;
using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SmartDyeing.FADM_Form
{
    public partial class NeedToDo : Form
    {
        public static bool _b_showRun = false;

        public static int HANDER = 0;

        public NeedToDo()
        {
            try
            {
                _b_showRun = true;
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Lib_Log.Log.writeLogException("NeedToDo 构造函数：" + ex.ToString());
            }

        }
     


        private void InfoUpdate()
        {
            try
            {
                dataGridView1.Rows.Clear();

                foreach (string i in Lib_Card.CardObject.keyValuePairs.Keys)
                {
                    if (i == null)
                    {
                        try
                        {
                            Lib_Card.CardObject.keyValuePairs.Remove(i);

                        }
                        catch {
                            Lib_Log.Log.writeLogException("keyValuePairs 空值不能删除：" );
                        }
                        continue;
                    }

                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        if ((!Lib_Card.CardObject.keyValuePairs[i].Info.Contains("放布")) && (!Lib_Card.CardObject.keyValuePairs[i].Info.Contains("出布")))
                            dataGridView1.Rows.Add(i,
                                 Lib_Card.CardObject.keyValuePairs[i].Type, Lib_Card.CardObject.keyValuePairs[i].Info);
                    }
                    else
                    {
                        if ((!Lib_Card.CardObject.keyValuePairs[i].Info.Contains("cloth placement")) && (!Lib_Card.CardObject.keyValuePairs[i].Info.Contains("cup discharge")))
                            dataGridView1.Rows.Add(i,
                                 Lib_Card.CardObject.keyValuePairs[i].Type, Lib_Card.CardObject.keyValuePairs[i].Info);
                    }
                }
            }
            catch (Exception ex)
            {
                Lib_Log.Log.writeLogException("NeedToDo InfoUpdate：" + ex.ToString());
            }


        }

        private void NeedToDo_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                InfoUpdate();
            }
            catch (Exception ex)
            {
                Lib_Log.Log.writeLogException("NeedToDo NeedToDo_Load：" + ex.ToString());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //try
            //{

            //    timer1.Enabled = false;
            //    if (dataGridView1.Rows.Count >= 1)
            //    {
            //        int i_col = dataGridView1.CurrentCell.ColumnIndex;

            //        if (i_col == 3 || i_col == 4)
            //        {
            //            int row = dataGridView1.CurrentCell.RowIndex;
            //            string s = dataGridView1[0, row].Value.ToString();
            //            Lib_Card.CardObject.prompt prompt = new Lib_Card.CardObject.prompt();
            //            prompt = Lib_Card.CardObject.keyValuePairs[s];

            //            if (i_col == 3)
            //            {
            //                //确认
            //                prompt.Choose = 1;
            //                if (FADM_Object.Communal._b_isNetWork)
            //                {
            //                    sendState(FADM_Object.Communal._s_machineCode, s, 1);
            //                }

            //            }
            //            else if (i_col == 4)
            //            {

            //                if (prompt.Info.Contains("退出运行请点否")|| prompt.Info.Contains("Click No to exit the operation"))
            //                {
            //                    //弹出二次确认按钮
            //                    DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("是否需要退出运行，如果选择退出后，需要重启软件，正在滴液的配液杯也要重新滴液，请谨慎选择？", "温馨提示", MessageBoxButtons.YesNo, true);
            //                    if (dialogResult == DialogResult.Yes)
            //                    {
            //                        //否
            //                        prompt.Choose = 2;
            //                        if (FADM_Object.Communal._b_isNetWork)
            //                        {
            //                            sendState(FADM_Object.Communal._s_machineCode, s, 2);
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    //否
            //                    prompt.Choose = 2;
            //                    if (FADM_Object.Communal._b_isNetWork)
            //                    {
            //                        sendState(FADM_Object.Communal._s_machineCode, s, 2);
            //                    }
            //                }

            //            }
            //            Lib_Card.CardObject.keyValuePairs[s] = prompt;


            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Lib_Log.Log.writeLogException("NeedToDo dataGridView1_SelectionChanged：" + ex.ToString());
            //}
            //finally
            //{
            //    timer1.Enabled = true;
            //}
        }
        async public void sendState(string machineCode, string time,int choose) {
            try
            {
                Console.WriteLine("发送点击状态到sendState---------------------");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("machineCode", machineCode);
                parameters.Add("time", time);
                parameters.Add("state", Convert.ToString(choose));
                await Task.Run(() => {
                    HttpWebResponse response = HttpUtil.CreatePostHttpResponse(FADM_Object.Communal.URL+"/outer/product/updateBroadcastD", parameters, 15000, null, null);
                });
            }
            catch
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                InfoUpdate();
            }
            catch (Exception ex)
            {
                Lib_Log.Log.writeLogException("NeedToDo timer1_Tick：" + ex.ToString());
            }

        }

        private void NeedToDo_FormClosed(object sender, FormClosedEventArgs e)
        {
            _b_showRun = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                timer1.Enabled = false;
                if (dataGridView1.Rows.Count >= 1)
                {
                    int i_col = dataGridView1.CurrentCell.ColumnIndex;

                    if (i_col == 3 || i_col == 4)
                    {
                        int row = dataGridView1.CurrentCell.RowIndex;
                        string s = dataGridView1[0, row].Value.ToString();
                        Lib_Card.CardObject.prompt prompt = new Lib_Card.CardObject.prompt();
                        prompt = Lib_Card.CardObject.keyValuePairs[s];

                        if (i_col == 3)
                        {
                            //确认
                            prompt.Choose = 1;
                            if (FADM_Object.Communal._b_isNetWork)
                            {
                                sendState(FADM_Object.Communal._s_machineCode, s, 1);
                            }

                        }
                        else if (i_col == 4)
                        {

                            if (prompt.Info.Contains("退出运行请点否") || prompt.Info.Contains("Click No to exit the operation"))
                            {
                                //弹出二次确认按钮
                                DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("是否需要退出运行，如果选择退出后，需要重启软件，正在滴液的配液杯也要重新滴液，请谨慎选择？", "温馨提示", MessageBoxButtons.YesNo, true);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    //否
                                    prompt.Choose = 2;
                                    if (FADM_Object.Communal._b_isNetWork)
                                    {
                                        sendState(FADM_Object.Communal._s_machineCode, s, 2);
                                    }
                                }
                            }
                            else
                            {
                                //否
                                prompt.Choose = 2;
                                if (FADM_Object.Communal._b_isNetWork)
                                {
                                    sendState(FADM_Object.Communal._s_machineCode, s, 2);
                                }
                            }

                        }
                        Lib_Card.CardObject.keyValuePairs[s] = prompt;


                    }
                }

            }
            catch (Exception ex)
            {
                Lib_Log.Log.writeLogException("NeedToDo dataGridView1_CellClick：" + ex.ToString());
            }
            finally
            {
                timer1.Enabled = true;
            }
        }
    }
}
