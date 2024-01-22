using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalCarManagement.DataBase;
namespace RentalCarManagement.Models
{
    class Reservations
    {
        // declaration
        private reservations res;
        private attachments att;
        // add reservation
        public void Add_Reservation(Guid id, DateTime start_date, DateTime end_date, float daily_price, float otherFees, string description, Guid car_id, Guid customer_id)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                res = new reservations
                {
                    id = id,
                    start_date = DateTime.Parse(start_date.ToString("dd/MM/yyyy")),
                    end_date = DateTime.Parse(end_date.ToString("dd/MM/yyyy")),
                    daily_price = daily_price,
                    otherFees = otherFees,
                    description = description,
                    car_id = car_id,
                    customer_id = customer_id,
                    created_at = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"))
                };
                db.reservations.Add(res);
                db.SaveChanges();
            }
        }
        // update reservation
        public void Update_Reservation(Guid id, DateTime start_date, DateTime end_date, float daily_price, float otherFees, string description, Guid car_id, Guid customer_id)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var res = db.reservations.Single(r => r.id == id);
                res.start_date = DateTime.Parse(start_date.ToString("dd/MM/yyyy"));
                res.end_date = DateTime.Parse(end_date.ToString("dd/MM/yyyy"));
                res.daily_price = daily_price;
                res.otherFees = otherFees;
                res.description = description;
                res.car_id = car_id;
                res.customer_id = customer_id;
                db.SaveChanges();
            }
        }
        // delete reservation 
        public void Delete_Reservation(Guid id)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                res = new reservations();
                res = db.reservations.SingleOrDefault(x => x.id == id);
                if (res != null)
                {
                    db.reservations.Remove(res);
                    db.SaveChanges();
                }
            }
        }
        // delete Attachments 
        public void Delete_Attachment(Guid id)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                att = new attachments();
                att = db.attachments.SingleOrDefault(x => x.id == id);
                if (att != null)
                {
                    db.attachments.Remove(att);
                    db.SaveChanges();
                }
            }
        }
        // total amount
        public float TotalAmount(float Dailyprice, int NbDay, float OtherFees)
        {
            float Amount = (NbDay * Dailyprice) + OtherFees;
            return Amount;
        }
    }
}
