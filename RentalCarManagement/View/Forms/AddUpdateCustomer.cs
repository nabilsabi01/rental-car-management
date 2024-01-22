using RentalCarManagement.Models;
using RentalCarManagement.DataBase;
using RentalCarManagement.View.UserControls;
using System;
using System.Windows.Forms;
using System.Linq;

namespace RentalCarManagement.View.Forms
{
    public partial class AddUpdateCustomer : Form
    {
        private readonly UserControl userCustomer;
        public Guid idCus;

        public AddUpdateCustomer(UserControl _userCustomer)
        {
            InitializeComponent();
            this.userCustomer = _userCustomer;
        }

        string Vider()
        {
            if (txtFirstName.Text == string.Empty || txtLastName.Text == string.Empty || txtIDCard.Text == string.Empty || txtDrivingLicenceId.Text == string.Empty || txtAdress.Text == string.Empty || txtEmail.Text == string.Empty || txtPhoneNombre.Text == string.Empty || (rbHomme.Checked == false && rbFemme.Checked == false))
            {
                return "remplir les champ";
            }
            return null;
        }

        private void BtnEnregestrer_Click(object sender, EventArgs e)
        {
            if (Vider() != null)
            {
                MessageBox.Show(Vider(), "Obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (lblTitre.Text == "Ajouter Client")
                {
                    Customers customer = new Customers();
                    if (customer.Add_Customer(txtFirstName.Text, txtLastName.Text, txtIDCard.Text, txtDrivingLicenceId.Text, txtPhoneNombre.Text, txtEmail.Text, txtAdress.Text, rbHomme.Checked))
                    {
                        MessageBox.Show("Client Ajouter avec succes", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        (userCustomer as ListCustomers).ActualiserDatagrid();
                    }
                    else
                    {
                        MessageBox.Show("Nom  est deja existant ", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Customers customer = new Customers();
                    DialogResult R = MessageBox.Show("vous-voulez vraiment Modifier le Client", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (R == DialogResult.Yes)
                    {
                        customer.Update_Customer(idCus, txtFirstName.Text, txtLastName.Text, txtIDCard.Text, txtDrivingLicenceId.Text, txtPhoneNombre.Text, txtEmail.Text, txtAdress.Text, rbHomme.Checked);
                        (userCustomer as ListCustomers).ActualiserDatagrid();
                        MessageBox.Show("Le Client Modifier avec succes", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Modification est annulé", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void AddUpdateCustomer_Load(object sender, EventArgs e)
        {
            if (lblTitre.Text == "Modifier Client")
            {
                using (DBRentalCarEntity db = new DBRentalCarEntity())
                {
                    var cus = db.customers.Where(x => x.id == idCus).SingleOrDefault();
                    txtFirstName.Text = cus.first_name;
                    txtLastName.Text = cus.last_name;
                    txtIDCard.Text = cus.id_card;
                    txtDrivingLicenceId.Text = cus.driving_licence_id;
                    txtPhoneNombre.Text = cus.phone;
                    txtEmail.Text = cus.email;
                    txtAdress.Text = cus.address;
                    bool a = cus.gender == true ? rbHomme.Checked = true : rbFemme.Checked = true;
                }
            }
        }

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            if (lblTitre.Text == "Modifier Client")
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
