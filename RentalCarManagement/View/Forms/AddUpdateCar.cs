using System;
using System.Windows.Forms;
using RentalCarManagement.Models;
using RentalCarManagement.DataBase;
using RentalCarManagement.View.UserControls;
using System.Linq;

namespace RentalCarManagement.View.Forms
{
    public partial class AddUpdateCar : Form
    {
        private readonly UserControl userCar;
        public Guid idCar;
        public AddUpdateCar(UserControl _userCar)
        {
            InitializeComponent();
            this.userCar = _userCar;
        }

        public void FillCarburant()
        {
            list_Carburant.DataSource = Enum.GetValues(typeof(Cars.Carburant));
        }

        string Vider()
        {
            if (txtRegNumber.Text == string.Empty || txtkilomitrage.Text == string.Empty || txtModul.Text == string.Empty)
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
                if (lblTitre.Text == "Ajouter Voiture")
                {
                    Cars car = new Cars();
                    if (car.Add_Car(txtRegNumber.Text, txtModul.Text, txtMarque.Text, float.Parse(txtkilomitrage.Text), list_Carburant.Text, rbDisponible.Checked, rbRent.Checked))
                    {
                        MessageBox.Show("voiture Ajouter avec succes", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        (userCar as ListCars).ActualiserDatagrid();
                    }
                    else
                    {
                        MessageBox.Show("car est deja existant ", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Cars customer = new Cars();
                    DialogResult R = MessageBox.Show("vous-voulez vraiment Modifier le voiture", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (R == DialogResult.Yes)
                    {
                        customer.Update_Car(idCar, txtRegNumber.Text, txtModul.Text, txtMarque.Text, float.Parse(txtkilomitrage.Text), list_Carburant.Text, rbDisponible.Checked, rbRent.Checked);
                        (userCar as ListCars).ActualiserDatagrid();
                        MessageBox.Show("Voiture Modifier avec succes", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Modification est annulé", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void AddUpdateCar_Load(object sender, EventArgs e)
        {
            FillCarburant();
            if (lblTitre.Text == "Modifier Voiture")
            {
                using (DBRentalCarEntity db = new DBRentalCarEntity())
                {
                    var cr = db.cars.Where(x => x.id == idCar).SingleOrDefault();
                    txtRegNumber.Text = cr.reg_number;
                    txtModul.Text = cr.model;
                    txtMarque.Text = cr.brand;
                    txtkilomitrage.Text = cr.mileage.ToString();
                    int v = list_Carburant.FindString(cr.fuel_type);
                    list_Carburant.SelectedIndex = v;
                    bool a = cr.available == true ? rbDisponible.Checked = true : rbNonDisponible.Checked = true;
                    bool r = cr.rent == true ? rbRent.Checked = true : rbNonRent.Checked = true;
                }
            }
        }

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            if (lblTitre.Text == "Modifier Voiture")
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
