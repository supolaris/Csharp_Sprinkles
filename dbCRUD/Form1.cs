using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace dbCRUD
{
    public partial class Form1 : Form
    {
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }


        SqlConnection conn = new SqlConnection("Data Source=SUPOLARIS\\SQLEXPRESS;Initial Catalog=studentDb;Integrated Security=True");
        private void insertButton_Click(object sender, EventArgs e)
        { 
            conn.Open();

            string myReg = regText.Text;
            cmd = new SqlCommand("SELECT * FROM dbCrud WHERE reg=@REG", conn);
            cmd.Parameters.Add("REG", regText.Text);
            cmd.ExecuteNonQuery();
            if (regText.Text.Equals(myReg))
            {
                MessageBox.Show("User with registration number: " + myReg +  " Already exists");

            }
            else
            {
                cmd = new SqlCommand("INSERT INTO dbCrud (id, reg, name, class) VALUES (@ID, @REG, @NAME, @CLASS)", conn);
                cmd.Parameters.Add("ID", idText.Text);
                cmd.Parameters.Add("REG", regText.Text);
                cmd.Parameters.Add("NAME", nameText.Text);
                cmd.Parameters.Add("CLASS", classText.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Stored");
            }
            conn.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            regText.Text = null;
            nameText.Text = null;
            classText.Text = null;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("DELETE dbCrud WHERE Id=@ID", conn);
            cmd.Parameters.Add("ID", idText.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("User Deleted");

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("UPDATE dbCrud SET reg=@REG, name=@NAME, class=@CLASS WHERE Id=@ID", conn);
            cmd.Parameters.Add("ID", idText.Text);
            cmd.Parameters.Add("REG", regText.Text);
            cmd.Parameters.Add("NAME", nameText.Text);
            cmd.Parameters.Add("CLASS", classText.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Data Updated");
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM dbCrud WHERE Id=@ID", conn);
            cmd.Parameters.Add("ID", idSearchText.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter myAdapter = new SqlDataAdapter(cmd);
            DataTable myDataTable = new DataTable();
            myAdapter.Fill(myDataTable);

            dataGridView1.DataSource = myDataTable;
            conn.Close();
        }
    }
}
