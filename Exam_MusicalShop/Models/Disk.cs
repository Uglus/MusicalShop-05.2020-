using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Exam_MusicalShop.Models
{
    public class Disk
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int BandId { get; set; }
        public Band Band { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        [Required]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        [Required]
        public int PublishYear { get; set; }

        [Required]
        public decimal SelfPrice { get; set; }


        public virtual List<DiscountDisk> DiscountDisks { get; set; }
        public virtual List<Price> Prices { get; set; }
        public virtual List<Storage> Storages { get; set; }
    }
}
