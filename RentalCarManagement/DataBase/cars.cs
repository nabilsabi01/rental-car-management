//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentalCarManagement.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class cars
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cars()
        {
            this.reservations = new HashSet<reservations>();
            this.reservations1 = new HashSet<reservations>();
        }
    
        public System.Guid id { get; set; }
        public string reg_number { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
        public Nullable<double> mileage { get; set; }
        public string fuel_type { get; set; }
        public bool available { get; set; }
        public bool rent { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservations> reservations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservations> reservations1 { get; set; }
    }
}
