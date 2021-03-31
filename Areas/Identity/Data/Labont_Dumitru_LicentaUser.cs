using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Labont_Dumitru_Licenta.Models;
using Microsoft.AspNetCore.Identity;

namespace Labont_Dumitru_Licenta.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Labont_Dumitru_LicentaUser class
    public class Labont_Dumitru_LicentaUser : IdentityUser
    {

        [Display(Name = "Nume")]
        public string FirstName { get; set; }

        [Display(Name = "Prenume")]
        public string LastName { get; set; }

        [InverseProperty("User")]
        public UserLocation UserLocation { get; set; }
        //[InverseProperty("ID")]
        //public int UserLocationId { get; set; }

        [InverseProperty("CarOwner")]
        public List<Car>? Cars { get; set; }

        [InverseProperty("CarOwner")]
        public List<Contract>? ContractsAsOwern { get; set; }

        [InverseProperty("CarReciver")]
        public List<Contract>? ContractsAsReciver { get; set; }


        [InverseProperty("Sender")]
        public List<Request>? RequestsSender { get; set; }

        [InverseProperty("Reciver")]
        public List<Request>? RequestsReciver { get; set; }



    }
}
