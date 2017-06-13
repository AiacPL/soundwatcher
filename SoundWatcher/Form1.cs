using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundWatcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SetTextTest(String text)
        {
            label1.Text = text;
        }

        public void SetTextBox1(String text)
        {
            textBox1.Text = text;
        }
        public void SetTextBox2(String text)
        {
            textBox2.Text = text;
        }
        public void SetTextBox3(String text)
        {
            textBox3.Text = text;
        }
        public void SetTextBox4(String text)
        {
            textBox4.Text = text;
        }
        public void SetDelay(decimal val)
        {
            numericUpDown1.Value = val;
        }
        public void SetCloseDelay(decimal val)
        {
            numericUpDown2.Value = val;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                SetTextBox1(Properties.Settings.Default["textBoxUrl1"].ToString());
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
            }
            try
            {
                SetTextBox2(Properties.Settings.Default["textBoxUrl2"].ToString());
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
            }
            try
            {
                SetTextBox3(Properties.Settings.Default["textBoxUrl3"].ToString());
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
            }
            try
            {
                SetTextBox4(Properties.Settings.Default["textBoxUrl4"].ToString());
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
            }
            try
            {
                SetDelay(Properties.Settings.Default.delay);
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
            }
            try
            {
                SetCloseDelay(Properties.Settings.Default.closeDelay);
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default["textBoxUrl1"] = textBox1.Text;
            Properties.Settings.Default["textBoxUrl2"] = textBox2.Text;
            Properties.Settings.Default["textBoxUrl3"] = textBox3.Text;
            Properties.Settings.Default["textBoxUrl4"] = textBox4.Text;
            Properties.Settings.Default["delay"] = numericUpDown1.Value;
            Properties.Settings.Default["closeDelay"] = numericUpDown2.Value;
            Properties.Settings.Default.Save();
        }

    }
}
