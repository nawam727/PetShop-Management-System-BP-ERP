using PetShop_Management_System.UIs;
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

        int Key = 0;

        //To add the customers to the db
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

        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTbl.Text = EmployeesDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpAddTbl.Text = EmployeesDGV.SelectedRows[0].Cells[2].Value.ToString();
            DOBTbl.Text = EmployeesDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPhoneTbl.Text = EmployeesDGV.SelectedRows[0].Cells[4].Value.ToString();
            EmpPassTbl.Text = EmployeesDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (EmpNameTbl.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(EmployeesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        //To edit customer informations in DB
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTbl.Text == "" || EmpAddTbl.Text == "" || EmpPhoneTbl.Text == "" || EmpPassTbl.Text == "")
            {
                MessageBox.Show("Select an Employee!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update EmployeeTbl set EmpName=@EN,EmpAdd=@EA,EmpDOB=@ED,EmpPhone=@EP,EmpPass=@EPa where EmpNum=@EKey", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTbl.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTbl.Text);
                    cmd.Parameters.AddWithValue("@ED", DOBTbl.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTbl.Text);
                    cmd.Parameters.AddWithValue("@EPa", EmpPassTbl.Text);
                    cmd.Parameters.AddWithValue("@EKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated!");
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

        //To delete employees details from the table
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select an Employee!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EmpNum = @EmpKey", Con);
                    cmd.Parameters.AddWithValue("@EmpKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted!");
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

        private void Employees_Load(object sender, EventArgs e)
        {
            label7.Text = "Emp " + Loginb.empName;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Loginb obj = new Loginb();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Bills obj = new Bills();
            obj.Show();
            this.Hide();
        }

        
    }
}
