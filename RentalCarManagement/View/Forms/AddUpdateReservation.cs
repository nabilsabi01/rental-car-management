using RentalCarManagement.DataBase;
using RentalCarManagement.Models;
using RentalCarManagement.View.UserControls;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RentalCarManagement.View.Forms
{
    public partial class AddUpdateReservation : Form
    {
        // declaration
        private readonly UserControl userRes;
        public Guid idRes;

        public AddUpdateReservation(UserControl _userRes)
        {
            InitializeComponent();
            this.userRes = _userRes;
        }

        // function reset
        public string Vider()
        {
            if (txtDailyPrice.Text == string.Empty || txtOtherFees.Text == string.Empty || listCar.SelectedIndex == -1 || listCustomer.SelectedIndex == -1 || date_debut.Value == DateTime.Now && date_fin.Value == DateTime.Now)
            {
                return "remplir les champ";
            }
            return null;
        }

        // Fill Combobox Customer
        public void FillListCustomer()
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                listCustomer.DataSource = db.customers.Select(c => new {
                    ID = c.id,
                    Nom = c.first_name+" "+c.last_name
                }).ToList();
                listCustomer.DisplayMember = "Nom";
                listCustomer.ValueMember = "ID";
            }
        }

        // fill combobox car
        public void FillListCar()
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                listCar.DataSource = db.cars.Select(c => new {
                    ID = c.id,
                    Nom = c.brand+" "+c.reg_number
                }).ToList();
                listCar.DisplayMember = "Nom";
                listCar.ValueMember = "ID";
            }
        }

        // btn enregestrer
        private void BtnEnregestrer_Click(object sender, EventArgs e)
        {
            if (Vider() != null)
            {
                MessageBox.Show(Vider(), "Obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (lblTitre.Text == "Ajouter Reservation")
                {
                    Reservations res = new Reservations();
                    res.Add_Reservation(Guid.NewGuid(), DateTime.Parse(date_debut.Value.ToString("dd/MM/yyyy")), DateTime.Parse(date_fin.Value.ToString("dd/MM/yyyy")), float.Parse(txtDailyPrice.Text), float.Parse(txtOtherFees.Text), txtDescription.Text, Guid.Parse(listCar.SelectedValue.ToString()), Guid.Parse(listCustomer.SelectedValue.ToString()));
                    MessageBox.Show("Reservation Ajouter avec succes", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    (userRes as ListReservations).ActualiserDatagrid();
                }
                else
                {
                    Reservations res = new Reservations();
                    DialogResult R = MessageBox.Show("vous-voulez vraiment Modifier le Reservation", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (R == DialogResult.Yes)
                    {
                        res.Update_Reservation(idRes, DateTime.Parse(date_debut.Value.ToString("dd/MM/yyyy")), DateTime.Parse(date_fin.Value.ToString("dd/MM/yyyy")), float.Parse(txtDailyPrice.Text), float.Parse(txtOtherFees.Text), txtDescription.Text, Guid.Parse(listCar.SelectedValue.ToString()), Guid.Parse(listCustomer.SelectedValue.ToString()));
                        (userRes as ListReservations).ActualiserDatagrid();
                        MessageBox.Show("Reservation Modifier avec succes", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Modification est annulé", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void AddUpdateReservation_Load(object sender, EventArgs e)
        {         
            FillListCustomer();
            FillListCar();
            if (lblTitre.Text == "Modifier Reservation")
            {
                using (DBRentalCarEntity db = new DBRentalCarEntity())
                {
                    var res = db.reservations.Where(x => x.id == idRes).SingleOrDefault();
                    listCustomer.Text = res.customers.first_name +" "+ res.customers.last_name;
                    listCar.Text = res.cars.brand + " " + res.cars.reg_number;
                    date_debut.Value = res.start_date;
                    date_fin.Value = res.end_date;
                    txtDailyPrice.Text = res.daily_price.ToString();
                    txtOtherFees.Text = res.otherFees.ToString();
                    txtDescription.Text = res.description;
                }
            }
            
        }

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            if (lblTitre.Text == "Modifier Reservation")
            {
                DialogResult R = MessageBox.Show("vous-voulez vraiment Annuler la Modification", "Annuler", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                DialogResult R = MessageBox.Show("vous-voulez vraiment Annuler la Ajoute", "Annuler", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            
        }
    }
}
