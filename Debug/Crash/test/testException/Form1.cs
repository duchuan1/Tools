using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testException
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (int i = 0; i < 100; i++)
                {
                    richTextBox1.Invoke(new Action(() => { richTextBox1.Text += (i) + "\r\n"; }));
                    Task.Delay(100).GetAwaiter().GetResult();

                }
            });
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (int i = 100; i < 200; i++)
                {
                    richTextBox1.Invoke(new Action(() => { richTextBox1.Text += (i) + "\r\n"; }));
                    Task.Delay(100).GetAwaiter().GetResult();
                }
            });
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (int i = 200; i < 300; i++)
                {
                    richTextBox1.Invoke(new Action(() => { richTextBox1.Text += (i) + "\r\n"; }));
                    Task.Delay(100).GetAwaiter().GetResult();
                }
            });
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (int i = 300; i < 400; i++)
                {
                    richTextBox1.Invoke(new Action(() => { richTextBox1.Text += (i) + "\r\n"; }));
                    Task.Delay(100).GetAwaiter().GetResult();
                }

                throw new OutOfMemoryException("asdasd");
            });
        }
    }
}
