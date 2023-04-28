using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShop_Management_System.UIs
{
    public partial class AdminHome : Form
    {
        public AdminHome()
        {
            InitializeComponent();
            CountDogs();
            CountCats();
            CountBirds();
        }

        private void CatsLbl_Click(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");

        private void CountDogs()
        {
            string Dog = "Dogs";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from ProductTbl where PrCat= '" + Dog + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DogsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void CountCats()
        {
            string Cat = "Cats";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from ProductTbl where PrCat= '" + Cat + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountBirds()
        {
            string Bird = "Birds";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from ProductTbl where PrCat= '" + Bird + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BirdsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AdminPro obj = new AdminPro();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AdminEmp obj = new AdminEmp();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminCus obj = new AdminCus();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            AdminBill obj = new AdminBill();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Loginb obj = new Loginb();
            obj.Show();
            this.Hide();
        }
    }
}
