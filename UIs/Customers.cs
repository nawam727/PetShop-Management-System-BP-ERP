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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }

        //Connect to the database
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");

        private void DisplayCustomers()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void Clear()
        {
            CustNameTbl.Text = "";
            CustAddTbl.Text = "";
            CustPhoneTbl.Text = "";
        }

        int Key = 0;


        //save button 
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTbl.Text == "" || CustAddTbl.Text == "" || CustPhoneTbl.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl values(@CN,@CA,@CP)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTbl.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTbl.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added!");
                    Con.Close();
                    DisplayCustomers();
                    Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
