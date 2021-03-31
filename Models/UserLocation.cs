using Labont_Dumitru_Licenta.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labont_Dumitru_Licenta.Models
{
    public class UserLocation
    {
        public int ID { get; set; }
        [Display(Name = "Judet")]
        public string County { get; set; }
        [Display(Name = "Oras")]
        public string City { get; set; }
        [Display(Name = "Strada")]
        public string Street { get; set; }
        [Display(Name = "Numarul strazii")]
        public int StreetNumber { get; set; }

        public Labont_Dumitru_LicentaUser User { get; set; }

        public string Labont_Dumitru_LicentaUserID { get; set; }

    }
}
