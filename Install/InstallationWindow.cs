using System;
using System.Windows.Forms;
using System.Drawing;

namespace Install
{
    public class InstallationWindow : Form
    {
        public InstallationWindow()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            Text = "First application";
            ClientSize = new Size(800, 450);
            CenterToScreen();
        }

        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
        }
    }
}