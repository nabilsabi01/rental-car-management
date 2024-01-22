using RentalCarManagement.DataBase;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RentalCarManagement.View.Forms
{
    public partial class DetailsReservation : Form
    {
        public Guid idRes;

        public DetailsReservation()
        {
            InitializeComponent();
        }

        public void GetDetials()
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var res = db.reservations.FirstOrDefault(x => x.id == idRes);
                float dailyPrix = (float)res.daily_price;
                DateTime dateEnd = DateTime.Parse(res.end_date.ToString("dd/MM/yyyy"));
                DateTime dateStart = DateTime.Parse(res.start_date.ToString("dd/MM/yyyy"));
                float OtherFees = float.Parse(res.otherFees.ToString());
                int TotalDay = (dateEnd - dateStart).Days;
                float Amount = (TotalDay * dailyPrix) + OtherFees;
                lblFullName.Text = res.customers.first_name + " " + res.customers.last_name;
                lblStartDate.Text = res.start_date.ToString("dd/MM/yyyy");
                lblEndDate.Text = res.end_date.ToString("dd/MM/yyyy");
                lblPriceDay.Text = res.daily_price.ToString();
                lblPhone.Text = "+212 " + res.customers.phone;
                lblEmail.Text = res.customers.email;
                lblAdress.Text = res.customers.address;
                lblAuterFrais.Text = res.otherFees.ToString();
                lblBrandModel.Text = res.cars.brand + " " + res.cars.model;
                lblGroupCustomer.Text += "-" + res.customers.id_card;
                lblCarburant.Text = res.cars.fuel_type;
                lblRegNum.Text = res.cars.reg_number;
                lblKilomitrage.Text = res.cars.mileage.ToString();
                lblTolal.Text = Amount.ToString();
                Double? a = res.payments.Sum(x => x.amount);
                Double? rest = Amount - a;
                lblReste.Text = rest.ToString();
                txtDescription.Text = res.description.ToString();
                var p = db.payments.Where(x => x.reservation_id == idRes).Select(pay => new { pay.date, pay.payment_method, pay.amount }).ToList();
                foreach (var x in p)
                {
                    dgvPayment.Rows.Add(x.date.ToString("dd/MM/yyyy"), x.amount, x.payment_method);
                }
            }
        }

        private void DetailsReservation_Load(object sender, EventArgs e)
        {
            GetDetials();
        }
    }
}
