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
            DisplayEmployees();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");

        private void DisplayEmployees()
        {
            Con.Open();
            string Query = "Select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            EmpNameTbl.Text = "";
            EmpAddTbl.Text = "";
            EmpPhoneTbl.Text = "";
            EmpPassTbl.Text = "";
        }
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
                    DisplayEmployees();
                    Clear();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
        }

        private void Employees_Load(object sender, EventArgs e)
        {

        }
    }
}
