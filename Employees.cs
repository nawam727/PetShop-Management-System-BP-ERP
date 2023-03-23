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

namespace PetShop_Management_System
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\AMANTER\\OneDrive\\Documents\\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTbl.Text == "" || EmpAddTbl.Text == "" || EmpPhoneTbl.Text == "" || EmpPassTbl.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl values(@EN,@EA,@ED,@EP,@EPa)", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTbl.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTbl.Text);
                    cmd.Parameters.AddWithValue("@ED", DOBTbl.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTbl.Text);
                    cmd.Parameters.AddWithValue("@EPa", EmpPassTbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added!");
                    Con.Close();
                    /*DisplayEmployees();
                    Clear();*/

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
