using System;
using System.Collections.Generic;
using System.Linq;
using RentalCarManagement.DataBase;

namespace RentalCarManagement.Models
{
    class Cars
    {
        // declaration
        private cars cr;
        // function add car
        public bool Add_Car(string reg_number, string model, string brand, double mileage, string fuel_type, bool available, bool rent)
        {
            cr = new cars
            {
                id = Guid.NewGuid(),
                reg_number = reg_number,
                model = model,
                brand = brand,
                mileage = mileage,
                fuel_type = fuel_type,
                available = available,
                rent = rent,
            };
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                if (db.cars.SingleOrDefault(x => x.reg_number == reg_number && x.model == model) == null)
                {
                    db.cars.Add(cr);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            } 
        }
        // update car
        public void Update_Car(Guid id, string reg_number, string model, string brand, double mileage, string fuel_type, bool available, bool rent)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                cr = db.cars.SingleOrDefault(x => x.id == id);
                if (cr != null)
                {
                    cr.reg_number = reg_number;
                    cr.model = model;
                    cr.brand = brand;
                    cr.mileage = mileage;
                    cr.fuel_type = fuel_type;
                    cr.available = available;
                    cr.rent = rent;
                    db.SaveChanges();
                }
            }
        }
        // delete car 
        public void Delete_Car(Guid id)
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                var cr = db.cars.SingleOrDefault(x => x.id == id);
                if (cr != null)
                {
                    db.cars.Remove(cr);
                    db.SaveChanges();
                }
            }
        }

        // List Carburant
        public enum Carburant
        {
            Gazoil,
            Essence,
            Electrique,
            Hybride
        }
    }
}
