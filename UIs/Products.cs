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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        //Connect to the database
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTbl.Text == "" || PrCatTbl.SelectedIndex == -1 || PrQuantityTbl.Text == "" || PrPriceTbl.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProductTbl values(@PN,@PC,@PQ,@PP)", Con);
                    cmd.Parameters.AddWithValue("@PN", PrNameTbl.Text);
                    cmd.Parameters.AddWithValue("@PC", PrCatTbl.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", PrQuantityTbl.Text);
                    cmd.Parameters.AddWithValue("@PP", PrPriceTbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Added!");
                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
