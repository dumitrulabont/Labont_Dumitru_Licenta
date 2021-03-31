    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Labont_Dumitru_Licenta.Areas.Identity.Data;

namespace Labont_Dumitru_Licenta.Models
{
    public class Car
    {
        public int ID { get; set; }

        //ownerID
        public string Image { get; set; }
        //relatie one-to-one cu CarDetails 
        //public CarDetail CarDetail { get; set; }
        public CarDetail CarDetail { get; set; }
        public int CarDetailID { get; set; }

        [Display(Name = "Este disponibila")]
        public bool IsAvailable { get; set; }

        //FK la tabela User 
        public  Labont_Dumitru_LicentaUser CarOwner { get; set; }
        public string CarOwnerID { get; set; }

        public Contract Contract { get; set; }
        public int? ContractID { get; set; }

        public List<Request> Requests { get; set; }

    }
}
