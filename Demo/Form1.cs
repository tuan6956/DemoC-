using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet.Net;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private bool flag = false;
        private void button1_Click(object sender, EventArgs e)
        {

            string[] arr = txtInput.Lines.Where(x => x.Contains(txtFilter.Text)).ToArray();
            txtOutput.Lines = arr;
            //foreach (var item in txtInput.Lines)
            //{
            //    if (item.Contains(txtFilter.Text))
            //    {
            //        txtOutput.Text += item + Environment.NewLine;

            //    }
            //}
            //foreach (var item in arr)
            //{
            //    string[] temp = item.Split('|');
            //    if (temp[3] == txtFilter.Text)
            //    {
            //        txtOutput.Text += item + Environment.NewLine;
            //    }
            //}

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //txtInput.Text = File.ReadAllText("ssh.txt");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtInput.Text = File.ReadAllText(open.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save.FileName, txtOutput.Text);
            }
        }
        private void Login()
        {
            try
            {
                //while (true)
                //{
                var doc = new HtmlAgilityPack.HtmlDocument();

                HttpRequest rq = new HttpRequest();
                rq.Cookies = new CookieDictionary();
                //rq.Proxy = new HttpProxyClient("127.0.0.1", 8888);
                string html = rq.Get("https://id.zing.vn/").ToString();
                rq.AddParam("pid", "38");
                rq.AddParam("u1", "https://id.zing.vn/v2/login/cb?apikey=92140c0e46c54994812403f564787c14&pid=38&_src=&utm_source=&utm_medium=&utm_term=&utm_content=&utm_campaign=&next=https%3A%2F%2Fid.zing.vn%2Fv2%2Finfosetting%3Fapikey%3D92140c0e46c54994812403f564787c14%26pid%3D38&referer=");
                rq.AddParam("fp", "https://id.zing.vn/v2/login/cb?apikey=92140c0e46c54994812403f564787c14&pid=38&_src=&utm_source=&utm_medium=&utm_term=&utm_content=&utm_campaign=&next=https%3A%2F%2Fid.zing.vn%2Fv2%2Finfosetting%3Fapikey%3D92140c0e46c54994812403f564787c14%26pid%3D38&referer=");
                rq.AddParam("apikey", "92140c0e46c54994812403f564787c14");
                rq.AddParam("u", "thanhps422");
                rq.AddParam("p", "123123123");
                rq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                rq.Referer = "https://id.zing.vn/v2/login?apikey=92140c0e46c54994812403f564787c14&data=V8qMSFbMj09UY_q9eg";
                html = rq.Post("https://sso3.zing.vn/alogin").ToString();
                string token = null;
                if (html.Contains("captcha2.zing.vn"))
                {
                    doc.LoadHtml(html);
                    string urlCaptcha = doc.DocumentNode.SelectSingleNode("//p[@class='catchacode']//img").Attributes["src"].Value;
                    token = doc.DocumentNode.SelectSingleNode("//input[@id='token']").Attributes["value"].Value;
                    var piccap = rq.Get(urlCaptcha).ToBytes();
                    picCaptcha.Image = Image.FromStream(new MemoryStream(piccap));
                    picCaptcha.Refresh();
                    Delay();

                    rq.AddParam("pid", "38");
                    rq.AddParam("u1", "https://id.zing.vn/v2/login/cb?apikey=92140c0e46c54994812403f564787c14&pid=38&_src=&utm_source=&utm_medium=&utm_term=&utm_content=&utm_campaign=&next=https%3A%2F%2Fid.zing.vn%2Fv2%2Finfosetting%3Fapikey%3D92140c0e46c54994812403f564787c14%26pid%3D38&referer=");
                    rq.AddParam("fp", "https://id.zing.vn/v2/login/cb?apikey=92140c0e46c54994812403f564787c14&pid=38&_src=&utm_source=&utm_medium=&utm_term=&utm_content=&utm_campaign=&next=https%3A%2F%2Fid.zing.vn%2Fv2%2Finfosetting%3Fapikey%3D92140c0e46c54994812403f564787c14%26pid%3D38&referer=");
                    rq.AddParam("apikey", "92140c0e46c54994812403f564787c14");
                    rq.AddParam("u", "thanhps422");
                    rq.AddParam("p", "123123123");
                    rq.AddParam("imgcode", txtCaptcha.Text);
                    rq.AddParam("xlogin", "");
                    rq.AddParam("websso", "1");
                    rq.AddParam("token", token);

                    html = rq.Post("https://sso3.zing.vn/aliasatten").ToString();
                    if (html.Contains("logout"))
                    {
                        MessageBox.Show("ok");
                    }
                }
                else
                    MessageBox.Show("eo captcha");

                //}



                //doc.LoadHtml(html);
                //string username = doc.DocumentNode.SelectSingleNode("//div[@class='infotext']").InnerText;
                //txtOutput.Text = username;
                //html = rq.Get("https://id.zing.vn/v2/infosetting/personal").ToString();
                //doc.LoadHtml(html);
                //string personal_tokenCaptcha = doc.DocumentNode.SelectSingleNode("//input[@id='personal_tokenCaptcha']").Attributes["value"].Value;
                //Random rn = new Random();
                //string jsrandom = rn.Next(400000, 600000).ToString("D6");
                //string demohtml = rq.Get($"https://id.zing.vn/ajax/gentoken?callback=zmCore.js{jsrandom}").ToString();
                //string tokenExpire = Regex.Match(demohtml, "msg\":\"([^\"]+)").Groups[1].Value;

                //rq.AddUrlParam("action", "personal.update");
                //rq.AddUrlParam("fullname", "Moi update 2 ");
                //rq.AddUrlParam("gender", "0");
                //rq.AddUrlParam("add", "dia chi ao 2");
                //rq.AddUrlParam("dob", "3");
                //rq.AddUrlParam("mob", "4");
                //rq.AddUrlParam("yob", "2000");
                //rq.AddUrlParam("city", "53");
                //rq.AddUrlParam("occupation", "4");
                //rq.AddUrlParam("maritalstatus", "0");
                //rq.AddUrlParam("tokenCaptcha", personal_tokenCaptcha);
                //rq.AddUrlParam("tokenExpire", tokenExpire);
                //rq.AddUrlParam("callback", $"zmCore.js{jsrandom}");
                //html = rq.Get("https://id.zing.vn/v2/infosetting/personal/update").ToString();
                //if (html.Contains("Cập nhật thành công"))
                //    MessageBox.Show("Update Success!");
                //else
                //    MessageBox.Show("Error!");
                //Dictionary<int, string> city = new Dictionary<int, string>();
                //city.Add(31, "An Giang");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(Login);
            th.Start();
        }
        public void Delay(int time = 30)
        {
            for (int i = 0; i < time; i++)
            {
                if (flag)
                {
                    flag = false;
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flag = true;
        }
    }
}
