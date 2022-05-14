using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiffFinder.Models
{
    public class DiffrenceInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DifferenceInformationId { get; set; }
        [Required]
        public string LeftString { get; set; }
        [Required]
        public string RightString { get; set; }
        public string Result { get; set; }

        public virtual ICollection<DiffsOffsets> DiffsOffsets { get; set; }
    }
}
