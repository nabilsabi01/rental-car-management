using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using RentalCarManagement.DataBase;
using RentalCarManagement.Models;
using RentalCarManagement.View.Forms;

namespace RentalCarManagement.View.UserControls
{
    public partial class ListReservations : UserControl
    {
        private static ListReservations userRes;

        public ListReservations()
        {
            InitializeComponent();
        }

        public static ListReservations Instance
        {
            get
            {
                if (userRes == null)
                {
                    userRes = new ListReservations();
                }
                return userRes;
            }
        }

        public void ActualiserDatagrid()
        {
            dgvListReservation.Rows.Clear();
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                foreach (var x in db.reservations)
                {
                    float dailyPrix = float.Parse(x.daily_price.ToString());
                    Reservations res = new Reservations();
                    DateTime dateEnd = DateTime.Parse(x.end_date.ToString("dd/MM/yyyy"));
                    DateTime dateStart = DateTime.Parse(x.start_date.ToString("dd/MM/yyyy"));
                   
                    float OtherFees = float.Parse(x.otherFees.ToString());
                    int TotalDay = (dateEnd - dateStart).Days;
                    float Amount = res.TotalAmount(dailyPrix, TotalDay, OtherFees);
                    
                    dgvListReservation.Rows.Add(x.id, x.created_at.ToString("dd/MM/yyyy"), x.customers.first_name+" "+x.customers.last_name, x.cars.brand+" "+x.cars.reg_number, x.start_date.ToString("dd/MM/yyyy"), x.end_date.ToString("dd/MM/yyyy"), x.daily_price, x.otherFees, Amount);
                    
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var list = db.reservations.Where(v => v.customers.first_name.StartsWith(txtSearch.Text));
                dgvListReservation.Rows.Clear();
                foreach (var x in list)
                {
                    float dailyPrix = float.Parse(x.daily_price.ToString());
                    Reservations res = new Reservations();
                    DateTime dateEnd = DateTime.Parse(x.end_date.ToString("dd/MM/yyyy"));
                    DateTime dateStart = DateTime.Parse(x.start_date.ToString("dd/MM/yyyy"));
                    float OtherFees = float.Parse(x.otherFees.ToString());
                    int TotalDay = (dateEnd - dateStart).Days;
                    float Amount = res.TotalAmount(dailyPrix, TotalDay, OtherFees);
                    dgvListReservation.Rows.Add(x.id, x.created_at.ToString("dd/MM/yyyy"), x.customers.first_name + " " + x.customers.last_name, x.cars.brand + " " + x.cars.reg_number, x.start_date.ToString("dd/MM/yyyy"), x.end_date.ToString("dd/MM/yyyy"), x.daily_price, x.otherFees, Amount);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvListReservation.SelectedRows.Count > 0)
            {
                Reservations res = new Reservations();
                DialogResult R = MessageBox.Show("Vous-Voulez vraiment Supprimer", "Suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvListReservation.Rows.Count; i++)
                    {
                        if (dgvListReservation.CurrentRow != null)
                        {
                            res.Delete_Reservation(Guid.Parse(dgvListReservation.CurrentRow.Cells[0].Value.ToString()));
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DateTime dateS = DateTime.Parse(date_debut.Value.ToString("yyyy/MM/dd"));
            DateTime dateE = DateTime.Parse(Date_Fin.Value.ToString("yyyy/MM/dd"));
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var list = db.reservations.Where(x => x.start_date >= dateS && x.end_date <= dateE).ToList();
                dgvListReservation.Rows.Clear();
                foreach (var x in list)
                {
                    float dailyPrix = float.Parse(x.daily_price.ToString());
                    Reservations res = new Reservations();
                    DateTime dateEnd = DateTime.Parse(x.end_date.ToString("dd/MM/yyyy"));
                    DateTime dateStart = DateTime.Parse(x.start_date.ToString("dd/MM/yyyy"));
                    float OtherFees = float.Parse(x.otherFees.ToString());
                    int TotalDay = (dateEnd - dateStart).Days;
                    float Amount = res.TotalAmount(dailyPrix, TotalDay, OtherFees);
                    dgvListReservation.Rows.Add(x.id, x.created_at.ToString("dd/MM/yyyy"), x.customers.first_name + " " + x.customers.last_name, x.cars.brand + " " + x.cars.reg_number, x.start_date.ToString("dd/MM/yyyy"), x.end_date.ToString("dd/MM/yyyy"), x.daily_price, x.otherFees, Amount);
                }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvListReservation.SelectedRows.Count > 0)
            {
                AddUpdateReservation frm = new AddUpdateReservation(this);
                for (int i = 0; i < dgvListReservation.Rows.Count; i++)
                {
                    if (dgvListReservation.CurrentRow != null)
                    {
                         
                            frm.idRes = Guid.Parse(dgvListReservation.CurrentRow.Cells[0].Value.ToString());
                        
                    }
                }
                frm.lblTitre.Text = "Modifier Reservation";
                frm.ShowDialog();
            }
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            if (dgvListReservation.SelectedRows.Count > 0)
            {
                AddUpdatePayment frmAdd = new AddUpdatePayment(this);
                for (int i = 0; i < dgvListReservation.Rows.Count; i++)
                {
                    if (dgvListReservation.CurrentRow != null)
                    {
                        frmAdd.idRes = Guid.Parse(dgvListReservation.CurrentRow.Cells[0].Value.ToString());
                    }
                }
                frmAdd.ShowDialog();
            }
        }

        private void BtnAttachments_Click(object sender, EventArgs e)
        {
            if (dgvListReservation.SelectedRows.Count > 0)
            {
                AddDeleteAttachment frmAdd = new AddDeleteAttachment();
                for (int i = 0; i < dgvListReservation.Rows.Count; i++)
                {
                    if (dgvListReservation.CurrentRow != null)
                    {
                        frmAdd.idRes = Guid.Parse(dgvListReservation.CurrentRow.Cells[0].Value.ToString());
                    }
                }
                frmAdd.ShowDialog();
            }
        }

        private void DgvListReservation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DetailsReservation frm = new DetailsReservation();
            for (int i = 0; i < dgvListReservation.Rows.Count; i++)
            {
                if (dgvListReservation.CurrentRow != null)
                {
                    frm.idRes = Guid.Parse(dgvListReservation.CurrentRow.Cells[0].Value.ToString());
                }
            }
            frm.ShowDialog();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (dgvListReservation.SelectedRows.Count > 0)
            {
                Guid id = Guid.Parse(dgvListReservation.CurrentRow.Cells[0].Value.ToString());
                using (DBRentalCarEntity db = new DBRentalCarEntity())
                {
                    var listPay = db.payments.Where(x => x.reservation_id == id).ToList();
                    if (listPay is null)
                    {
                        MessageBox.Show("Paiment vide !!");
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DBRentalCarManagement;Integrated Security=True");
                        PrintRes frm = new PrintRes();
                        SqlDataAdapter da = new SqlDataAdapter("SELECT customers.first_name, customers.last_name, customers.id_card, customers.driving_licence_id, customers.phone, customers.address, reservations.start_date, reservations.end_date, reservations.daily_price, reservations.otherFees, cars.reg_number, cars.model, cars.brand, payments.amount, payments.payment_method, payments.date FROM reservations INNER JOIN customers ON reservations.customer_id = customers.id AND reservations.customer_id = customers.id INNER JOIN cars ON reservations.car_id = cars.id AND reservations.car_id = cars.id INNER JOIN payments ON reservations.id = payments.reservation_id AND reservations.id = payments.reservation_id AND reservations.id = '" + id + "'", con);
                        da.Fill(frm.DSReservation.DsRes);
                        frm.RptViewRes.RefreshReport();
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddUpdateReservation frm = new AddUpdateReservation(this);
            frm.ShowDialog();
            frm.lblTitre.Text = "Ajouter Reservation";
            frm.date_debut.Value = DateTime.Now;
            frm.date_fin.Value = DateTime.Now;
        }

        private void ListReservations_Load(object sender, EventArgs e)
        {
            ActualiserDatagrid();
        }
    }
}

