using System;
using System.Windows.Forms;
using RentalCarManagement.DataBase;
using RentalCarManagement.View.Forms;
using RentalCarManagement.Models;
using System.Linq;

namespace RentalCarManagement.View.UserControls
{
    public partial class ListCustomers : UserControl
    {
        private static ListCustomers userCustomer;

        public ListCustomers()
        {
            InitializeComponent();
        }

        public static ListCustomers Instance
        {
            get
            {
                if (userCustomer == null)
                {
                    userCustomer = new ListCustomers();
                }
                return userCustomer;
            }
        }

        public void ActualiserDatagrid()
        {
            dvgListCustomer.Rows.Clear();
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                foreach (var x in db.customers)
                {
                    dvgListCustomer.Rows.Add(x.id, x.first_name+" "+x.last_name, x.id_card, x.driving_licence_id, x.phone, x.email, x.address, (x.gender == true ? "Homme" : "Femme"));
                }
            } 
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dvgListCustomer.SelectedRows.Count > 0)
            {
                AddUpdateCustomer frm = new AddUpdateCustomer(this);
                for (int i = 0; i < dvgListCustomer.Rows.Count; i++)
                {
                    if (dvgListCustomer.CurrentRow != null)
                    {
                        frm.idCus = Guid.Parse(dvgListCustomer.CurrentRow.Cells[0].Value.ToString());
                    }
                }
                frm.lblTitre.Text = "Modifier Client";
                frm.ShowDialog();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dvgListCustomer.SelectedRows.Count > 0)
            {
                Customers cus = new Customers();
                DialogResult R = MessageBox.Show("Vous-Voulez vraiment Supprimer", "Suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    for (int i = 0; i < dvgListCustomer.Rows.Count; i++)
                    {
                        if (dvgListCustomer.CurrentRow != null)
                        {
                            cus.Delete_Customer(Guid.Parse(dvgListCustomer.CurrentRow.Cells[0].Value.ToString()));
                        }
                    }
                    ActualiserDatagrid();
                    MessageBox.Show("Supression avec succées", "Suppresion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Supression est annulé ", "Suppresion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddUpdateCustomer frm = new AddUpdateCustomer(this);
            frm.ShowDialog();
        }
        private void ListCustomers_Load(object sender, EventArgs e)
        {
            ActualiserDatagrid();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var list = db.customers.Where(v => v.first_name.StartsWith(txtSearch.Text) || v.last_name.StartsWith(txtSearch.Text) || v.id_card.StartsWith(txtSearch.Text) || v.driving_licence_id.StartsWith(txtSearch.Text)).ToList();
                dvgListCustomer.Rows.Clear();
                foreach (var x in list)
                {
                    dvgListCustomer.Rows.Add(x.id, x.first_name + " " + x.last_name, x.id_card, x.driving_licence_id, x.phone, x.email, x.address, (x.gender == true ? "Homme" : "Femme"));
                }
            }
        }
    }
}
