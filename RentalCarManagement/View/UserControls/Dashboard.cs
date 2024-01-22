using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RentalCarManagement.DataBase;

namespace RentalCarManagement.View.UserControls
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        // count customers,cars,reservations
        private void Count()
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                lblCountCustomers.Text = db.customers.Count().ToString();
                lblCountCars.Text = db.cars.Count().ToString();
                lblCountReservations.Text = db.reservations.Count().ToString();
            }
        }
        // display in datagrid
        public void Datagrid()
        {
            dgvDashboard.Rows.Clear();
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                dgvDashboard.Rows.Clear();
                foreach (var x in db.reservations)
                {
                    float dailyPrix = float.Parse(x.daily_price.ToString());
                    DateTime dateEnd = DateTime.Parse(x.end_date.ToString("dd/MM/yyyy"));
                    DateTime dateStart = DateTime.Parse(x.start_date.ToString("dd/MM/yyyy"));
                    float OtherFees = float.Parse(x.otherFees.ToString());
                    int TotalDay = (dateEnd - dateStart).Days;
                    float Amount = dailyPrix * TotalDay - OtherFees;
                    Double? a = x.payments.Sum(u => u.amount);
                    Double? rest = Amount - a;
                    dgvDashboard.Rows.Add(x.id, x.customers.first_name + " " + x.customers.last_name, x.cars.brand, x.start_date.ToString("dd/MM/yyyy"), x.end_date.ToString("dd/MM/yyyy"), x.daily_price, x.otherFees, Amount, rest);
                }
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Count();
            Datagrid();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var list = db.reservations.Where(v => v.customers.first_name.StartsWith(txtSearch.Text));
                dgvDashboard.Rows.Clear();
                foreach (var x in list)
                {
                    float dailyPrix = float.Parse(x.daily_price.ToString());
                    DateTime dateEnd = DateTime.Parse(x.end_date.ToString());
                    DateTime dateStart = DateTime.Parse(x.start_date.ToString());
                    float OtherFees = float.Parse(x.otherFees.ToString());
                    int TotalDay = (dateEnd - dateStart).Days;
                    float Amount = dailyPrix * TotalDay - OtherFees;
                    Double? a = x.payments.Sum(u => u.amount);
                    Double? rest = Amount - a;
                    dgvDashboard.Rows.Add(x.id, x.customers.first_name + " " + x.customers.last_name, x.cars.brand, x.start_date, x.end_date, x.daily_price, x.otherFees, Amount, rest);
                }
            }
        }
    }
}
