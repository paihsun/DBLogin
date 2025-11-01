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

namespace DBApplication01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn=new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=DBSQL;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = txtUserName.Text;
            String password = txtPassword.Text;
            try
            { 
                string sql = "SELECT * FROM Login WHERE userName='"+username+"' AND Password='"+password+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Login successful!");
                    Menu mainMenu = new Menu();
                    mainMenu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUserName.Clear();
                    txtUserName.Focus();
                }
            }
            catch {
                MessageBox.Show("Invalid username or password.");

            }
            finally
            {
                conn.Close();
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtUserName.Clear();
            txtUserName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                // Do nothing, stay in the application
                this.Show();
            }
        }
    }
}
