using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Exam_MusicalShop.Models
{
    public class Storage
    {
        public int Id { get; set; }

        public int DiskId { get; set; }
        public Disk Disk { get; set; }

        public int Amount { get; set; }
    }

}
