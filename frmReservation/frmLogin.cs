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

namespace frmReservation
{
    public partial class frmLogin : Form
    {
        // DB objects
        private SqlCommand command;
        private SqlDataReader reader;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //User does not enter username and/or password
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please enter Username and Password.", "Information Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (var con = new SqlConnection(DBObjects.conString))
                {
                    con.Open();

                    //declare stored procedure command
                    command = new SqlCommand("pass_Login", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", txtUserName.Text);
                    command.Parameters.AddWithValue("@password", txtPassword.Text);

                    int loginResult = Convert.ToInt32(command.ExecuteScalar()); //Save result 1 or 0

                    if (loginResult == 1)
                    {
                        MessageBox.Show("Login Successful");
                        this.Hide();
                        frmReservation frm = new frmReservation();
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login failed. Please check Username and Password.", "Login Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
