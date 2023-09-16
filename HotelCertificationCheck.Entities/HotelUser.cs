using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelCertificationCheck.Entities
{
    public class HotelUser : IdentityUser
    {
        //Relationship
        public Guid?  HotelID { get;set; }

        //NotMapped
        [NotMapped]
        public string? Role { get;set; }
        [NotMapped]
        public string? Password { get;set; }
        [NotMapped]
        public string? ConfirmPassword { get;set; }
        [NotMapped]
        public Hotel? Hotel { get;set; }
        [NotMapped]
        public bool IsPersistent { get;set; }
    }
}
