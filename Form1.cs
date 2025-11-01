using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//引入SQL Server資料庫相關命名空間

namespace DBApplication01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //建立SQL Server資料庫連接字串
        SqlConnection conn =new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=DBSQL;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //加入程式的註解：取得使用者輸入的帳號和密碼
            String username = txtUserName.Text;
            String password = txtPassword.Text;
            try
            {
                //打開資料庫連接
                conn.Open();
                //查詢使用者輸入的帳號和密碼是否存在於資料庫中
                string sql = "SELECT * FROM Login WHERE userName='"+username+"' AND Password='"+password+"'";
                //使用SqlDataAdapter來執行SQL查詢並填充DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();//建立一個DataTable來存放查詢結果
                adapter.Fill(dt);//將查詢結果填充到DataTable中
                if (dt.Rows.Count > 0)//如果查詢結果有資料，表示登入成功
                {
                    MessageBox.Show("Login successful!");//顯示登入成功訊息
                    Menu mainMenu = new Menu();//建立主選單視窗的實例
                    mainMenu.Show();//顯示主選單視窗
                    this.Hide();//隱藏登入視窗
                }
                else //如果查詢結果沒有資料，表示登入失敗
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear(); //清除密碼欄位
                    txtUserName.Clear(); //清除使用者名稱欄位
                    txtUserName.Focus(); //將焦點設置到使用者名稱欄位
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

        private void btnClear_Click(object sender, EventArgs e) //清除按鈕事件處理程式
        {
            txtPassword.Clear();
            txtUserName.Clear();
            txtUserName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e) //退出按鈕事件處理程式
        {
            //顯示確認退出的對話框
            DialogResult result;
            result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //根據使用者的選擇進行相應的操作
            if (result == DialogResult.Yes)
            {
                Application.Exit(); //退出應用程式
            }
            else
            {
                // Do nothing, stay in the application
                this.Show();//保持當前視窗顯示
            }
        }
    }
}
