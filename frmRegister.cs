using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auth_App
{
    public partial class frmRegister : Form
    {
        Database1Entities db = new Database1Entities();
        public frmRegister()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            var data = db.tblUsers.OrderByDescending(x=>x.Username).ToList();//only for descending, (Use '.orderby' if ascending)
            dataGridView1.DataSource = data;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            txtFullname.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmpassword.Clear();
        }
        private void frmRegister_Load(object sender, EventArgs e)
        {
                LoadData();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            string password = txtPassword.Text;
            bool validPassword = password.Length >= 8
                && password.Any(char.IsLower)
                && password.Any(char.IsUpper);
            if (!validPassword && password != "")
            {
                sb.AppendLine("Invalid Password");
            }

            if (txtFullname.Text.Trim().Length == 0)
            {
                sb.AppendLine("Please enter user's Full Name");
            }
            if (txtUsername.Text.Trim().Length == 0)
            {
                sb.AppendLine("Please enter Username");
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                sb.AppendLine("Please enter password");
            }
            if (txtConfirmpassword.Text.Trim().Length == 0)
            {
                sb.AppendLine("please re-enter the password");
            }

            if (txtPassword.Text.Trim() != txtConfirmpassword.Text.Trim())
            {
                sb.AppendLine("Sorry, the passwords dont match");
            }

            if (sb.ToString() != String.Empty)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            var dupliData = db.tblUsers.Where(u => u.Username == txtUsername.Text).FirstOrDefault();
            if (dupliData != null)
            {
                MessageBox.Show("This Username is already taken.. ",
                    "Duplicate Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }
            tblUser user = new tblUser();

            user.FullName = txtFullname.Text;
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Text;
            user.UserType = "Customer";

            db.tblUsers.Add(user);
            db.SaveChanges();

            LoadData();
            ClearForm();
            MessageBox.Show("Data Saves Successfully...."
            , "Welcome",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        }

        private void chkViewPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViewPass.Checked == false)
            {
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmpassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
                txtConfirmpassword.UseSystemPasswordChar = false;
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmLogin formlogin = new frmLogin();
            formlogin.ShowDialog();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
