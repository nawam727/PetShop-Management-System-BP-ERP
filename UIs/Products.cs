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
            DisplayProducts();
        }

        //Connect to the database
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\DELL\OneDrive - NSBM\Documents\PetShopDb.mdf"";Integrated Security=True;Connect Timeout=30");

        //To display entered products
        private void DisplayProducts()
        {
            Con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void Clear()
        {
            PrNameTbl.Text = "";
            PrCatTbl.SelectedIndex = 0;
            PrQuantityTbl.Text = "";
            PrPriceTbl.Text = "";
        }

        int Key = 0;
        //Save button
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
        //Edit button
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTbl.Text == "" || PrCatTbl.SelectedIndex == -1 || PrQuantityTbl.Text == "" || PrPriceTbl.Text == "")
            {
                MessageBox.Show("Select an Product!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ProductTbl set PrName=@PN,PrCat=@PC,PrQty=@PQ,PrPrice=@PP where ProID=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PN", PrNameTbl.Text);
                    cmd.Parameters.AddWithValue("@PC", PrCatTbl.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", PrQuantityTbl.Text);
                    cmd.Parameters.AddWithValue("@PP", PrPriceTbl.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated!");
                    Con.Close();
                    DisplayProducts();
                    Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        //Delete button
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
                    SqlCommand cmd = new SqlCommand("delete from ProductTbl where ProID = @PKey", Con);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted!");
                    Con.Close();
                    DisplayProducts();
                    Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        //Displaying product details
        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTbl.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            PrCatTbl.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            PrQuantityTbl.Text = ProductDGV.SelectedRows[0].Cells[3].Value.ToString();
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

        private void label1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
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

        private void label6_Click(object sender, EventArgs e)
        {
            Loginb obj = new Loginb();
            obj.Show();
            this.Hide();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            label7.Text = "Emp " + Loginb.empName;
        }
    }
}
