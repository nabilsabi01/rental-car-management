using System;
using System.Linq;
using RentalCarManagement.DataBase;

namespace RentalCarManagement.Models
{
    class Customers
    {
        private customers c;
        // add customer
        public bool Add_Customer(string firstName, string lastName, string id_card, string driving_licence_id, string phone, string email, string adress, bool gender)
        {
            c = new customers
            {
                id = Guid.NewGuid(),
                first_name = firstName,
                last_name = lastName,
                id_card = id_card,
                driving_licence_id = driving_licence_id,
                phone = phone,
                email = email,
                address = adress,
                gender = gender,
            };
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                if (db.customers.SingleOrDefault(x => x.first_name == firstName && x.last_name == lastName) == null)
                {
                    db.customers.Add(c);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        // update customer
        public void Update_Customer(Guid id, string firstName, string lastName, string id_card, string driving_licence_id, string phone, string email, string adress, bool gender)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var c = db.customers.SingleOrDefault(x => x.id == id);
                if (c != null)
                {
                    c.first_name = firstName;
                    c.last_name = lastName;
                    c.id_card = id_card;
                    c.driving_licence_id = driving_licence_id;
                    c.phone = phone;
                    c.email = email;
                    c.address = adress;
                    c.gender = gender;
                    db.SaveChanges();
                }
            }
        }
        // delete customer 
        public void Delete_Customer(Guid id)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var c = db.customers.SingleOrDefault(x => x.id == id);
                if (c != null)
                {
                    db.customers.Remove(c);
                    db.SaveChanges();
                }
            }
        }
    }
}
