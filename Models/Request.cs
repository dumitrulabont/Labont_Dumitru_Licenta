using Labont_Dumitru_Licenta.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labont_Dumitru_Licenta.Models
{
    public class Request
    {
        [Key]
        public int ID { get; set; }

        public Labont_Dumitru_LicentaUser Sender { get; set; }
        public string SenderID { get; set; }
        public Labont_Dumitru_LicentaUser Reciver { get; set; }
        public string ReciverID { get; set; }

        [Display(Name ="Raspuns")]
        public bool? RequestState { get; set; }

        [Display(Name = "Data de incepere")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Data de terminare")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }

        [Display(Name = "Data cereri")]
        public DateTime RequestDate { get; set; }
        public Contract? Contract { get; set; }

        public int? CarId { get; set; }
        public Car Car { get; set; }

    }
    
   
}

