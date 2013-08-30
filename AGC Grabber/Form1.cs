using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace AGC_Grabber
{
    public partial class Form1 : Form
    {
        string version;
        public Form1()
        {
            InitializeComponent();
            GetAGC();
        }

        void GetAGC()
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("http://www.realmofthemadgod.com/version.txt");
                httpRequest.Timeout = 10000;     // 10 secs //10000
                HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());

                version = responseStream.ReadToEnd();

                textBox1.Text = string.Format("http://www.realmofthemadgod.com/AGCLoader{0}.swf", version);
                textBox2.Text = string.Format("http://www.realmofthemadgod.com/AssembleeGameClient{0}.swf", version);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "An error occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Process.Start(textBox2.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(textBox1.Text), Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + string.Format(@"\AGCLoader{0}.swf", version));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(textBox2.Text), Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + string.Format(@"\AssembleeGameClient{0}.swf", version));
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Complete!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
