using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerAutoShotdown
{
    public partial class MainWindow : Form
    {
        public const int MILISECONDS = 1000;
        private bool checkerStart = false;
        Timer checker = new Timer();
        Timer mainTimer = new Timer();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            mainTimer.Tick += new EventHandler(CheckSystemTime);
            mainTimer.Interval = 5 * MILISECONDS;
            mainTimer.Start();
        }
        private void TextboxKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void hours_TextChanged(object sender, EventArgs e)
        {
            if (!checkerStart)
            {
                checker.Tick += new EventHandler(CheckTextBoxContent);
                checker.Interval = 10 * MILISECONDS;
                checker.Start();
            }
        }

        private void CheckTextBoxContent(Object myObject, EventArgs myEventArgs)
        {
            checkerStart = true;
            if (hours.Text == "")
            {
                hours.Text = "17";
            }
            if (minutes.Text == "")
            {
                minutes.Text = "25";
            }

            if (int.Parse(hours.Text) > 23)
            {
                hours.Text = "23";
            }
            if (int.Parse(minutes.Text) > 59)
            {
                hours.Text = "59";
            }

            checkerStart = false;
            checker.Stop();
        }

        private void CheckSystemTime(Object myObject, EventArgs myEventArgs)
        {
            DateTime time = DateTime.Now;
            int myHours = int.Parse(hours.Text);
            int myMinutes = int.Parse(minutes.Text);

            if (myHours > 23)
            {
                myHours = 23;
                hours.Text = "23";
            }
            if (myMinutes > 59)
            {
                myMinutes = 59;
                minutes.Text = "59";
            }
            else if (myHours == time.Hour && myMinutes == time.Minute)
            {
                MessageBox.Show("SHUTDOWN!!!!!!!!!!!!!!!!1111oneone");
                //System.Diagnostics.Process.Start("shutdown", "/s /t 0");
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }
    }
}
