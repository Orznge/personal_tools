using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using mshtml;
using System.IO;
using System.Text;


namespace PGC信息采集
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        public int Para = 1;
        public string[] Description_source;
        public string description = "";
        private void BT_Search_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://" + "movie.douban.com/subject_search?search_text="+TB_SearchWord.Text+"&cat=1002");
            
            Para = 1;
        }

        public void BT_Admit_Click(object sender, EventArgs e)
        {
            Index_Gen();
            //MessageBox.Show(sourcecode);
        }

        private void BT_MoreInfo_Click(object sender, EventArgs e)
        {
            //Para+=1;
            Index_Gen();
        }
        public void Index_Gen()
        {
            string sourcecode = webBrowser1.Document.Body.InnerText;
            string fore_title;//国家/类型
            string title = TB_SearchWord.Text;//标题用搜索关键字
            string rear_title;

            string[] Get_fore_title = Regex.Split(sourcecode, "类型: ", RegexOptions.IgnoreCase);
            fore_title = Get_fore_title[1];
            Get_fore_title = fore_title.Split('\r');
            fore_title = Get_fore_title[0];
            fore_title = Regex.Replace(fore_title, " ", "");
            if (fore_title.Length > 5) { fore_title = fore_title.Substring(0, 5); }
            fore_title = "【" + fore_title + "】";


            string[] Get_rear_title = sourcecode.Split('(');//年份
            rear_title = Get_rear_title[1];
            Get_rear_title = rear_title.Split(')');
            rear_title = "【" + Get_rear_title[0] + "】";

            
            String[] Get_description = Regex.Split(sourcecode, "简介  ·  ·  ·  ·  ·  ·\r");
            
            //Get_description = Regex.Split(description,"\r\n");
            //description = description.Substring(0,245);
            //Get_description = Get_description[].Trim();
            if  (Para == 1) { description = Get_description[1]; Description_source = Regex.Split(description, "\r\n"); description = "";  }
            //for (int i = 1; i < Para; i++)
            //{
                description += Description_source[Para-1].Trim();
            description = description.Replace(" ©豆瓣","");
                //description = Regex.Replace(description.Trim(), "", "");
            //}
            if (description.Length > 250)
                description = description.Substring(0, 245) + "...";

            string Index;
            //Index = "[basic]\r\n\r\ntitle = \"" + fore_title + title + rear_title + "\"\r\ncomefrom = \"转载\"\r\ntypeid = \"145\"\r\ntags = \"欧美电影\"\r\nkey_words_change = \"是\"\r\ndescription = \"" + description + "\"\r\n[eptitle]\r\n\r\n[login]\r\nmid=15773384\r\n\r\n[transcode]\r\n\r\nx264_binary = \"x264_64_kMod-8bit.exe\"\r\nx264_params = \"--threads 24 --tune film --ref 4 --bframes 4 --b-adapt 2 --qcomp 0.7 --rc-lookahead 70 --psy-rd 1.0:0 --keyint 250 --min-keyint 1 --aq-strength 1.0 --aq-mode 3 --me umh --subme 10 --merange 32 --fgo 5\"\r\nx264_params_1pass = \"--crf 24\"\r\nx264_params_2pass = \"--bitrate 1800\"\r\nx264_minrate = 1800\r\nx264_maxrate = 1800\r\nqaac_params = \"-c192 -q2\"";
            Index = "[basic]\r\n\r\ntitle = \"" + fore_title + title + rear_title + "\"\r\ncomefrom = \"转载\"\r\ntypeid = \"145\"\r\ntags = \"海外电影\"\r\nkey_words_change = \"是\"\r\ndescription = \"" + description + "\"\r\n[eptitle]\r\n\r\n[login]\r\nmid=\""+CB_Uploader.Text+"\"";
            Para += 1;//下一次加一段落文字

            //textBox2.Text = fore_title+title+rear_title;
            textBox2.Text = Index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Url=new Uri("http://pgc-encoder.bilibili.co/job/PGC%20Encode/build?delay=0sec");
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //IHTMLWindow2 win = (IHTMLWindow2)webBrowser1.Document.Window.DomWindow;
            ////string s = "function confirm() {return true;} function alert() {}"; //仅对 body 内有效
            //string s = @"window.alert = null; window.onerror = null; window.confirm = null; window.open = null; window.showModalDialog = null;";
            //win.execScript(s, "javascript");
        }

        private void TB_Batchlist_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
            else e.Effect = DragDropEffects.None;
        }


        private void TB_Batchlist_DragDrop(object sender, DragEventArgs e)
        {
           
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //MessageBox.Show(path);
            string[] sp = path.Split('\\');
            path = sp[sp.Length-1];
            LB_Batchlist.Text += (path+"\r\n");
            //String[] fileNames = (String[])e.Data.GetData(DataFormats.FileDrop);
            //int j = TB_Batchlist_DragDrop.Items.Count + 1;
            //for (int i = 0; i < fileNames.Length; i++)
            //{
            //    TB_Batchlist_DragDrop.Items.Add(String.Format("{0:D3} ", i + j) + fileNames[i]);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textbox版本
            //string[]sp= Regex.Split(TB_Batchlist.Text,Environment.NewLine);
            //if (TB_Batchlist.Text!="")

            //{
            //    string List_Next = sp[0];
            //    //TB_Batchlist.Text = TB_Batchlist.Text.Substring(1);
            //    TB_Batchlist.Text = TB_Batchlist.Text.Substring(List_Next.Length);
            //    TB_Batchlist.Text = TB_Batchlist.Text.Trim();
            //    webBrowser1.Url = new Uri("http://" + "movie.douban.com/subject_search?search_text=" + List_Next + "&cat=1002");

            //    Para = 1;
            //}
            //else { MessageBox.Show("U r out of bullet"); }
            if (LB_Batchlist.Items.Count > 0)
            {
                string List_Next = LB_Batchlist.Items[0].ToString();
                webBrowser1.Url = new Uri("http://" + "movie.douban.com/subject_search?search_text=" + List_Next + "&cat=1002");
                LB_Batchlist.Items.RemoveAt(0);
                TB_SearchWord.Text = List_Next;
                Para = 1;
            }
            else { MessageBox.Show("U r out of bullet"); }
        }
        
        
        //http://blog.csdn.net/zhenniubile/article/details/5782524
        private void LB_Batchlist_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
            else e.Effect = DragDropEffects.None;
        }

        private void LB_Batchlist_DragDrop(object sender, DragEventArgs e)
        {
            if (LB_Batchlist.Items.Count > 0)
                { 
                 LB_Batchlist.Items.RemoveAt(0);
                }
            //LB_Batchlist.Items.Clear();
            String[] fileNames = (String[])e.Data.GetData(DataFormats.FileDrop);
            int j = LB_Batchlist.Items.Count + 1;
            for (int i = 0; i < fileNames.Length; i++)
            {
                string[] sp = fileNames[i].Split('\\');
                string path = sp[sp.Length - 1];
                //TB_Batchlist.Text += (path + "\r\n");
                //LB_Batchlist.Items.Add(String.Format("{0:D3} ", i + j) + fileNames[i]);
                LB_Batchlist.Items.Add(/*String.Format("{0:D3} ", i + j) +*/ path);
            }
    }

        //http://blog.csdn.net/zhenniubile/article/details/5782524

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CB_RandomUID.Checked) { Random num1 = new Random();int i = num1.Next(8); CB_Uploader.Text = CB_Uploader.Items[i].ToString(); }
            //MessageBox.Show(@TB_Path_Output.Text + "\\" + TB_SearchWord.Text + "\\index.txt");


            File.WriteAllText(@TB_Path_Output.Text + "\\" + TB_SearchWord.Text + "\\index.txt", textBox2.Text, Encoding.UTF8);
            
            
            //nextline succeeded
            //File.WriteAllText(@"\\172.18.24.14\copyright\encoded\huangcheng\film\佳韵社海外不好意思打水印部分\0Output\八个杀手\index.txt", "hello", Encoding.UTF8);
            //StreamWriter sr = new StreamWriter(fs);
            //sr.WriteLine(textBox2.Text,Encoding.UTF8);//开始写入值
            //sr.Close();
            //fs.Close();
        }
    }
}
