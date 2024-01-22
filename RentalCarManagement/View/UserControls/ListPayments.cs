using RentalCarManagement.Models;
using RentalCarManagement.View.Forms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RentalCarManagement.DataBase;

namespace RentalCarManagement.View.UserControls
{
    public partial class ListPayments : UserControl
    {
        public ListPayments()
        {
            InitializeComponent();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dvgListPayment.SelectedRows.Count > 0)
            {
                AddUpdatePayment frm = new AddUpdatePayment(this);

                for (int i = 0; i < dvgListPayment.Rows.Count; i++)
                {
                    if (dvgListPayment.CurrentRow != null)
                    {
                        frm.idPay = Guid.Parse(dvgListPayment.CurrentRow.Cells[0].Value.ToString());
                    }
                }
                frm.lblTitre.Text = "Modifier Paiement";
                frm.ShowDialog();
            }
        }

        public void ActualiserDatagrid()
        {
            dvgListPayment.Rows.Clear();
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                foreach (var x in db.payments)
                {
                    dvgListPayment.Rows.Add(x.id, x.date.ToString("dd/MM/yyyy"), x.reservations.customers.first_name + " " + x.reservations.customers.last_name, x.reservations.cars.brand + " " + x.reservations.cars.reg_number, x.amount, x.payment_method);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dvgListPayment.SelectedRows.Count > 0)
            {
                Payments pay = new Payments();
                DialogResult R = MessageBox.Show("Vous-Voulez vraiment Supprimer", "Suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    for (int i = 0; i < dvgListPayment.Rows.Count; i++)
                    {
                        if (dvgListPayment.CurrentRow != null)
                        {
                            pay.Delete_Payment(Guid.Parse(dvgListPayment.CurrentRow.Cells[0].Value.ToString()));
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

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var list = db.payments.Where(v => v.reservations.customers.first_name.StartsWith(txtSearch.Text));
                dvgListPayment.Rows.Clear();
                foreach (var x in list)
                {
                    dvgListPayment.Rows.Add(x.id, x.date.ToString("dd/MM/yyyy"), x.reservations.customers.first_name + " " + x.reservations.customers.last_name, x.reservations.cars.brand + " " + x.reservations.cars.reg_number, x.amount, x.payment_method);
                }
            }
        }

        private void ListPayments_Load(object sender, EventArgs e)
        {
            ActualiserDatagrid();
        }
    }
}
