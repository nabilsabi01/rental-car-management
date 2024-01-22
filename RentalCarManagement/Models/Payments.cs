using System;
using System.Linq;
using RentalCarManagement.DataBase;

namespace RentalCarManagement.Models
{
    class Payments
    {
        // add payment
        public void Add_Payment(DateTime date, string payment_method, float amount, string description, Guid reservation_id)
        {
            payments pay = new payments
            {
                id = Guid.NewGuid(),
                date =DateTime.Parse(date.ToString("dd/MM/yyyy")),
                payment_method = payment_method,
                amount = amount,
                description = description,
                reservation_id = reservation_id
            };
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                db.payments.Add(pay);
                db.SaveChanges();
            } 
        }
        // update payment
        public void Update_Payment(Guid id, DateTime date, string payment_method, double amount, string description)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var pay = db.payments.SingleOrDefault(x => x.id == id);
                if (pay != null)
                {
                    pay.date = DateTime.Parse(date.ToString("dd/MM/yyyy"));
                    pay.payment_method = payment_method;
                    pay.amount = amount;
                    pay.description = description;
                    db.SaveChanges();
                }
            } 
        }
        // delete payment 
        public void Delete_Payment(Guid id)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var pay = db.payments.SingleOrDefault(x => x.id == id);
                if (pay != null)
                {
                    db.payments.Remove(pay);
                    db.SaveChanges();
                }
            }
        }
        // List Payment Methods
        public enum PaymentMethods
        {
            Cash,
            Checks,
            DebitCards,
            Credit_cards,
            Mobile_payments
        }
    }
}
