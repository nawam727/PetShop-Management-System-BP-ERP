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
        }

        //Connect to the database
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");

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

        }

        int Key = 0, Stock = 0;
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
                //UpdateStock();
                //Reset();
            }
        }
    }
}
