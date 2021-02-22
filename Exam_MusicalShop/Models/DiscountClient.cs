using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Exam_MusicalShop.Models
{
    public class DiscountClient
    {
        public int Id { get; set; }

        [Required]
        public decimal Money { get; set; }

        [Required]
        public double Percent { get; set; }
    }
}
