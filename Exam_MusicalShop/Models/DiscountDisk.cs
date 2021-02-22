using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Exam_MusicalShop.Models
{
    public class DiscountDisk
    {

        public int Id { get; set; }

        [Required]
        public int DiskId { get; set; }
        public Disk Disk { get; set; }

        [Required]
        public double Percent { get; set; }
    }
}
