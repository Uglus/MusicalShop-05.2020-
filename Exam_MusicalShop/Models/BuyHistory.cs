using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Exam_MusicalShop.Models
{
    public class BuyHistory
    {
        public int Id { get; set; }

        public int DiskId { get; set; }
        public Disk Disk { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public DateTime BuyDateTime { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
