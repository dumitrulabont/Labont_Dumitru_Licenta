using Labont_Dumitru_Licenta.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labont_Dumitru_Licenta.Models
{
    public class Contract
    {
        public int ID { get; set; }

        public Labont_Dumitru_LicentaUser CarOwner { get; set; }
        public Labont_Dumitru_LicentaUser CarReciver { get; set; }
        [Display(Name = "Data de incepere")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Data de terminare")]
        public DateTime FinishDate { get; set; }

        public Car Car { get; set; }

        public Request Request { get; set; }
        public int RequestID { get; set; }

    }
}
