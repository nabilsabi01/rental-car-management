using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using RentalCarManagement.DataBase;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentalCarManagement.View.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnConnecter_Click(object sender, EventArgs e)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var s = db.users.Where(x => x.user_name == txtLogin.Text && x.password == txtPassword.Text).SingleOrDefault();
                if (s != null)
                {
                    Main m = new Main();
                   
                    m.Show();
                    this.Hide();
                    
                    
                }
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
