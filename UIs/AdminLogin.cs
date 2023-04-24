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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = UsernameTb.Text;
            string password = PasswordTb.Text;

            try
            {
                String query = "select * from EmployeeTbl where EmpName = '" + UsernameTb.Text + "' and EmpPass = '" + PasswordTb.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    username = UsernameTb.Text;
                    password = PasswordTb.Text;

                    AdminHome Obj = new AdminHome();
                    Obj.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UsernameTb.Clear();
                    PasswordTb.Clear();

                    UsernameTb.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
            finally
            {
                Con.Close();

            }
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Loginb Obj = new Loginb();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
