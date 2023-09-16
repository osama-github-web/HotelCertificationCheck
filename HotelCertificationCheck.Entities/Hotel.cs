using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelCertificationCheck.Entities
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [NotNull]
        public string Name { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public double Postalcode { get; set; }
        public double LatitudeInDecimals { get; set; }  
        public double LongitudeInDecimals { get; set; }

        public Boolean IsCertified {get; set;}

        public DateTime? CertificationDate{ get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string Website { get; set; }

        public int BecauseId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        //RelationShips
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }

        //NotMapped
        [NotMapped]
        public HotelUser? CreatedBy {  get; set; }
        [NotMapped]
        public HotelUser? UpdatedBy {  get; set; }


        public Hotel Update(Hotel hotel)
        {
            if(!string.IsNullOrWhiteSpace(hotel.Name))
                this.Name = hotel.Name;            
            if(!string.IsNullOrWhiteSpace(hotel.Country))
                this.Country = hotel.Country;
            if (!string.IsNullOrWhiteSpace(hotel.State))
                this.State = hotel.State;
            if (!string.IsNullOrWhiteSpace(hotel.Country))
                this.City = hotel.City;
            if (!string.IsNullOrWhiteSpace(hotel.Address))
                this.Address = hotel.Address;

            if (hotel.Postalcode > 0)
                this.Postalcode = hotel.Postalcode;
            if (hotel.LatitudeInDecimals > 0)
                this.LatitudeInDecimals = hotel.LatitudeInDecimals;
            if (hotel.LongitudeInDecimals > 0)
                this.LongitudeInDecimals = hotel.LongitudeInDecimals;
            
            if (hotel.CertificationDate.HasValue)
                this.CertificationDate = hotel.CertificationDate;
            if (hotel.ExpirationDate.HasValue)
                this.ExpirationDate = hotel.ExpirationDate;

            if(hotel.CreatedById != Guid.Empty && hotel.CreatedById.HasValue)
                this.CreatedById = hotel.CreatedById;
            if(hotel.UpdatedById != Guid.Empty && hotel.UpdatedById.HasValue)
                this.UpdatedById = hotel.UpdatedById;

            if(hotel.UpdatedAt.HasValue)
                this.UpdatedAt = hotel.UpdatedAt;

            return this;
        }

    }
}
