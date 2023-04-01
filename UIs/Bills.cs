using System;
using System.Collections;
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
    public partial class Bills : Form
    {
        public Bills()
        {
            InitializeComponent();
            GetCustomers();
            DisplayProduct();
            DisplayTransaction();
        }

        //Connect to the database
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");

        //Get customers from the DB
        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CustID from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustID", typeof(int));
            dt.Load(Rdr);
            CustIDCB.ValueMember = "CustID";
            CustIDCB.DataSource = dt;
            Con.Close();
        }

        //Display product details from the DB
        private void DisplayProduct()
        {
            Con.Open();
            string Query = "select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        //Display Transaction
        private void DisplayTransaction()
        {
            Con.Open();
            string Query = "select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        //Get customers name from BD to TextBox
        private void GetCustName()
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            string Query = "select * from CustomerTbl where CustID = '" + CustIDCB.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTbl.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }

        //Updating the stock 
        private void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(PrQuantityTbl.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTbl set PrQty=@PQ where ProID=@PKey", Con);
                cmd.Parameters.AddWithValue("@PQ", NewQty);

                cmd.Parameters.AddWithValue("@PKey", Key);

                cmd.ExecuteNonQuery();
                Con.Close();
                DisplayProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddColumnsToDataGridView()
        {
            // Add columns to the DataGridView control
            BillDGV.Columns.Add("Column1", "ID");
            BillDGV.Columns.Add("Column2", "Product Name");
            BillDGV.Columns.Add("Column3", "Quantity");
            BillDGV.Columns.Add("Column4", "Price");
            BillDGV.Columns.Add("Column5", "Total");
        }

        int n = 0, GrdTotal = 0;

        //Connect Home tab
        private void label1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        //Connect Product tab
        private void label2_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        //Connect Employees tab
        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        //Connect Customer tab
        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void CustIDCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustName();
        }

        //Reset
        private void Reset()
        {
            PrNameTbl.Text = "";
            PrQuantityTbl.Text = "";
            PrPriceTbl.Text = "";
            Stock = 0;
            Key = 0;
        }

        int Key = 0, Stock = 0;


        //Reset button
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (PrQuantityTbl.Text == "" || Convert.ToInt32(PrQuantityTbl.Text) > Stock)
            {
                MessageBox.Show("No Enough In House");
            }
            else if (PrQuantityTbl.Text == "" || Key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                int total = Convert.ToInt32(PrQuantityTbl.Text) * Convert.ToInt32(PrPriceTbl.Text);

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                /*newRow.Cells.Add(new DataGridViewTextBoxCell());
                newRow.Cells.Add(new DataGridViewTextBoxCell());
                newRow.Cells.Add(new DataGridViewTextBoxCell());
                newRow.Cells.Add(new DataGridViewTextBoxCell());
                newRow.Cells.Add(new DataGridViewTextBoxCell());*/
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = PrNameTbl.Text;
                newRow.Cells[2].Value = PrQuantityTbl.Text;
                newRow.Cells[3].Value = PrPriceTbl.Text;
                newRow.Cells[4].Value = total;
                GrdTotal += total;
                BillDGV.Rows.Add(newRow);
                n++;
                TotalLbl.Text = "RS " + GrdTotal;
                UpdateStock();
                Reset();
            }
        }
    }
}
