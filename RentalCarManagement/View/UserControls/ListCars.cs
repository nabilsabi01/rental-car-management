using System;
using System.Windows.Forms;
using RentalCarManagement.DataBase;
using RentalCarManagement.View.Forms;
using RentalCarManagement.Models;
using System.Linq;

namespace RentalCarManagement.View.UserControls
{
    public partial class ListCars : UserControl
    {
        private static ListCars userCar;

        public ListCars()
        {
            InitializeComponent();
        }

        public static ListCars Instance
        {
            get
            {
                if (userCar == null)
                {
                    userCar = new ListCars();
                }
                return userCar;
            }
        }

        public void ActualiserDatagrid()
        {
            dgvListCar.Rows.Clear();
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                foreach (var x in db.cars)
                {
                    dgvListCar.Rows.Add(x.id, x.reg_number, x.model, x.brand, x.mileage, x.fuel_type, (x.available == true ? "oui" : "non"), (x.rent == true ? "oui" : "non"));
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
             AddUpdateCar AddCar = new AddUpdateCar(this);
            AddCar.ShowDialog();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvListCar.SelectedRows.Count > 0)
            {
                Cars car = new Cars();
                DialogResult R = MessageBox.Show("Vous-Voulez vraiment Supprimer", "Suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvListCar.Rows.Count; i++)
                    {
                        if (dgvListCar.CurrentRow != null)
                        {
                            car.Delete_Car(Guid.Parse(dgvListCar.CurrentRow.Cells[0].Value.ToString()));
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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvListCar.SelectedRows.Count > 0)
            {
                AddUpdateCar frm = new AddUpdateCar(this);
                for (int i = 0; i < dgvListCar.Rows.Count; i++)
                {
                    if (dgvListCar.CurrentRow != null)
                    {
                        frm.idCar = Guid.Parse(dgvListCar.CurrentRow.Cells[0].Value.ToString());
                    }
                }
                frm.lblTitre.Text = "Modifier Voiture";
                frm.ShowDialog();
            }
        }

        private void ListCars_Load(object sender, EventArgs e)
        {
            ActualiserDatagrid();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var list = db.cars.Where(v => v.reg_number.StartsWith(txtSearch.Text) || v.brand.ToLower().StartsWith(txtSearch.Text.ToLower())).ToList();
                dgvListCar.Rows.Clear();
                foreach (var x in list)
                {
                    dgvListCar.Rows.Add(x.id, x.reg_number, x.model, x.brand, x.mileage, x.fuel_type, (x.available == true ? "oui" : "non"), (x.rent == true ? "oui" : "non"));
                }
            }
        }
    }
}
