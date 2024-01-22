using RentalCarManagement.Models;
using RentalCarManagement.DataBase;
using RentalCarManagement.View.UserControls;
using System;
using System.Windows.Forms;
using System.Linq;

namespace RentalCarManagement.View.Forms
{
    public partial class AddUpdatePayment : Form
    {
        private readonly UserControl userPayments;
        public Guid idPay;
        public Guid idRes;

        public AddUpdatePayment(UserControl _userPayments)
        {
            InitializeComponent();
            this.userPayments = _userPayments;
        }

        public void FillModePay()
        {
            listPaymentMethode.DataSource = Enum.GetValues(typeof(Payments.PaymentMethods));
        }

        private void BtnEnregestrer_Click(object sender, EventArgs e)
        {
            Payments pay = new Payments();
            if (lblTitre.Text == "Ajouter Paiement")
            {
                pay.Add_Payment(DateTime.Parse(dtp_date_pay.Value.ToString()), listPaymentMethode.Text, float.Parse(txtAmont.Text), txtDescription.Text, idRes);
                MessageBox.Show("Paiement Ajouter avec succes", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DialogResult R = MessageBox.Show("vous-voulez vraiment Modifier le Paiement", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    pay.Update_Payment(idPay, dtp_date_pay.Value, listPaymentMethode.Text, Double.Parse(txtAmont.Text), txtDescription.Text);
                    (userPayments as ListPayments).ActualiserDatagrid();
                    MessageBox.Show("Paiement Modifier avec succes", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Modification est annulé", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void AddUpdatePayment_Load(object sender, EventArgs e)
        {
            FillModePay();
            if (lblTitre.Text == "Modifier Paiement")
            {
                using (DBRentalCarEntity db = new DBRentalCarEntity())
                {
                    var p = db.payments.Where(x => x.id == idPay).SingleOrDefault();
                    dtp_date_pay.Value = p.date;
                    txtAmont.Text = p.amount.ToString();
                    listPaymentMethode.Text = p.payment_method;
                    txtDescription.Text = p.description;
                }
            }
        }

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            if (lblTitre.Text == "Modifier Paiement")
            {
                DialogResult R = MessageBox.Show("vous-voulez vraiment Annuler la Modification", "Annuler", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                DialogResult R = MessageBox.Show("vous-voulez vraiment Annuler l'ajoute", "Annuler", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }
    }
}
