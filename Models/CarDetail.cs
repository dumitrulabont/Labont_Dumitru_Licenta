using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Labont_Dumitru_Licenta.Models
{
    public class CarDetail
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        [Display(Name = "Pret")]
        
        public int Price { get; set; }

        [Display(Name = "Culoare")]
        public string Color { get; set; }

        [Display(Name = "An")]
        //am considerat anul ca fiind integer 
        public int Year { get; set; }

        public Car Car { get; set; }

        [Display(Name = "Tipul masinii")]
        public CarType CarType { get; set; }
    }
        public enum CarType
        {
            Berlina,
            Coupe,
            Pickup,
            Hatchback,
            Break,
            OffRoad,
            Minibus,
            Monovolum,
            SUV
        }



}
