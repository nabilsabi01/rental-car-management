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
    
    public partial class attachments
    {
        public System.Guid id { get; set; }
        public string file_name { get; set; }
        public string type_decument { get; set; }
        public byte[] centenu_file { get; set; }
        public System.Guid reservation_id { get; set; }
    
        public virtual reservations reservations { get; set; }
    }
}
