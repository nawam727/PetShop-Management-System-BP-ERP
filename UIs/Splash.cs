using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bunifu.UI.WinForms.BunifuShapes;

namespace PetShop_Management_System
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Splash_Load(object sender, EventArgs e)
        {

        }
        //Progress bar 
        int startP = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 5;
            if (startP <= MyProgress.Maximum) // Check if startP is within the maximum value of the progress bar
            {
                MyProgress.Value = startP;
                PercentageLbl.Text = startP + "%";
                if (MyProgress.Value == 100)
                {
                    MyProgress.Value = 0;
                    Loginb Obj = new Loginb();
                    Obj.Show();
                    this.Hide();
                    timer1.Stop();
                }
            }
            else
            {
                // If startP exceeds the maximum value of the progress bar, reset it to 0
                startP = 0;
            }
        }

        private void Splash_Load_1(object sender, EventArgs e)
        {

        }
    }
}
