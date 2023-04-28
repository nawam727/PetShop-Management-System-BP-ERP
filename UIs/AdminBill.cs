﻿using System;
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
    public partial class AdminBill : Form
    {
        public AdminBill()
        {
            InitializeComponent();
            DisplayTransaction();
        }

        //Connect to the database
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");

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
        private void TransDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
