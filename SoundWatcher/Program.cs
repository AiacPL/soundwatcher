using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using CSCore.CoreAudioAPI;

namespace SoundWatcher
{
    public class Requests
    {
        /*
        private int temp;
        private string forecast;
		
        public Requests()
        {

        }
		*/
        public static bool SendON()
        {
            try
            {
                //form a webrequest for the html of the weather page

                // todo: Get values of text inputs for "ON" signal URL, and send the requests
                WebRequest wreqa = WebRequest.Create(Properties.Settings.Default["textBoxUrl1"].ToString());
                WebResponse responsea = wreqa.GetResponse();
                responsea.Close();
                WebRequest wreqb = WebRequest.Create(Properties.Settings.Default["textBoxUrl2"].ToString());
                WebResponse responseb = wreqb.GetResponse();
                responseb.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return false;
        }

        public static bool SendOFF()
        {
            try
            {
                //form a webrequest for the html of the weather page

                // todo: Get values of text inputs for "ON" signal URL, and send the requests
                WebRequest wreqc = WebRequest.Create(Properties.Settings.Default["textBoxUrl3"].ToString());
                WebResponse responsec = wreqc.GetResponse();
                responsec.Close();
                WebRequest wreqd = WebRequest.Create(Properties.Settings.Default["textBoxUrl4"].ToString());
                WebResponse responsed = wreqd.GetResponse();
                responsed.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return false;
        }

    }

    public class AudioCheck
    {
        public static bool lastCheck = false;
        public static int notPlayingSleep = 0;

        public static MMDevice GetDefaultRenderDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            }
        }

        public static bool IsAudioPlaying(MMDevice device)
        {
            using (var meter = AudioMeterInformation.FromDevice(device))
            {
                return meter.PeakValue > 0;
            }
        }
    }

    public class Interval
    {
        private static System.Timers.Timer aTimer = new System.Timers.Timer();

        public Interval()
        {
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = (int)Properties.Settings.Default.delay;
            aTimer.Enabled = true;
        }

        public static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            bool playingcheck = AudioCheck.IsAudioPlaying(AudioCheck.GetDefaultRenderDevice());
            if (playingcheck != AudioCheck.lastCheck)
            {
                AudioCheck.lastCheck = playingcheck;
                //MessageBox.Show(playingcheck.ToString() + aTimer.Interval.ToString());
                if (playingcheck)
                {
                    // Turn ON
                    //Requests request = new Requests();
                    //request.SendON();
                    Requests.SendON();
                    aTimer.Stop();
                    aTimer.Interval = (int)Properties.Settings.Default.closeDelay;
                    aTimer.Start();
                }
                else
                {
                    // Turn OFF
                    //Requests request = new Requests();
                    Requests.SendOFF();
                    aTimer.Stop();
                    aTimer.Interval = (int)Properties.Settings.Default.delay;
                    aTimer.Start();
                }
            }
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Interval interval = new Interval();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyCustomApplicationContext());
        }
    }


    public class MyCustomApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        /*
        public Icon DrawIcon(string str)
        {

            Brush br;

            Bitmap bit = new Bitmap(16, 16);
            Graphics g = Graphics.FromImage(bit);

            br = new SolidBrush(SystemColors.WindowText);

            g.DrawString(str, new Font("Arial", 8), br, 0, 0);
            IntPtr Hicon = bit.GetHicon();
            Icon ico = Icon.FromHandle(Hicon);

            return ico;
        }
		*/

        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Turn ON", TurnOn), new MenuItem("Turn OFF", TurnOff),
                new MenuItem("Settings", ShowSettings), new MenuItem("Exit", Exit)
            }),
                Visible = true
            };
            trayIcon.DoubleClick += new System.EventHandler(ShowSettings);
        }

        void TurnOn(object sender, EventArgs e)
        {
            /*
        Requests request = new Requests();
        request.SendON();
        */
            Requests.SendON();
        }

        void TurnOff(object sender, EventArgs e)
        {
            /*
			Requests request = new Requests();
			request.SendOFF();
			*/
            Requests.SendOFF();
        }

        void ShowSettings(object sender, EventArgs e)
        {

            // todo: do not 

            Form fc = Application.OpenForms["Form1"];
            if (fc != null)
            {
                //fc.Close();
                fc.Focus();
            }
            else
            {
                Form1 myForm = new Form1();
                myForm.Show();
            }
            /*
			Form1 myForm = new Form1();
			if (!myForm.Visible) {
				//myForm.SetTextTest(Convert.ToString(AudioCheck.IsAudioPlaying(AudioCheck.GetDefaultRenderDevice())));
				myForm.Show();
			} else {
				myForm.Focus();
			}
			*/
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }
    }
}
