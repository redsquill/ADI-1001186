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
    public partial class frmLogin : Form
    {
        Database1Entities db = new Database1Entities();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmRegister form = new frmRegister();   
            form.ShowDialog();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var data = db.tblUsers.Where(u => u.Username == txtUsername.Text
                && u.Password == txtPassword.Text).FirstOrDefault();

            if (data != null)
            {
                UserInfo.Id= data.Id;
                UserInfo.Username = data.Username;
                UserInfo.FullName = data.FullName;
                UserInfo.UserType = data.UserType;

                this.Hide();

                frmMainMenu frm = new frmMainMenu();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid User!!", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkViewPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViewPass.Checked == false)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }
    }
}
