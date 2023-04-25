using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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
            AddColumnsToDataGridView();
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

        //Printing the bill
        int prodid, prodqty, prodprice, total, pos = 60;

        private void TransDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Loginb obj = new Loginb();
            obj.Show();
            this.Hide();
        }

        private void Bills_Load(object sender, EventArgs e)
        {
            EmpNameLbl.Text = "Emp " + Loginb.empName;
        }

        string prodname;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Pet Hotel Pet Care Center", new Font("FAKE RECEIPT", 12, FontStyle.Bold), Brushes.Black, new Point(80));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL", new Font("FAKE RECEIPT", 10, FontStyle.Bold), Brushes.Black, new Point(26, 20));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                total = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString("" + prodid, new Font("FAKE RECEIPT", 8, FontStyle.Bold), Brushes.Black, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("FAKE RECEIPT", 8, FontStyle.Bold), Brushes.Black, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("FAKE RECEIPT", 8, FontStyle.Bold), Brushes.Black, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("FAKE RECEIPT", 8, FontStyle.Bold), Brushes.Black, new Point(170, pos));
                e.Graphics.DrawString("" + total, new Font("FAKE RECEIPT", 8, FontStyle.Bold), Brushes.Black, new Point(235, pos));
                pos += 20;
            }
            e.Graphics.DrawString("Grand Total: Rs " + GrdTotal, new Font("FAKE RECEIPT", 12, FontStyle.Bold), Brushes.Black, new Point(50, pos + 50));
            e.Graphics.DrawString("***********PET SHOP***********", new Font("FAKE RECEIPT", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 350, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }



        //Print the bill when print button is clicked
        private void InsertBill()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BillTbl values(@BD,@CI,@CN,@EN,@Am)", Con);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@CI", CustIDCB.Text);
                cmd.Parameters.AddWithValue("@CN", CustNameTbl.Text);
                cmd.Parameters.AddWithValue("@EN", EmpNameLbl.Text);
                cmd.Parameters.AddWithValue("@Am", GrdTotal);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Saved!");
                Con.Close();
                DisplayTransaction();
                //Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        //Display updated product
        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTbl.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            //cu.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            Stock = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[3].Value.ToString());
            PrPriceTbl.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTbl.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
