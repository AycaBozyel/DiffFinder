using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DiffFinder.Models
{
    public class DiffsOffsets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiffsOffsetsId { get; set; }
        public int DiffrenceInformationId { get; set; }
        public int Diffs { get; set; }
        public int Offset { get; set; }
        [MaxLength(1)]
        public string LeftChar { get; set; }
        [MaxLength(1)]
        public string RightChar { get; set; }
        public virtual DiffrenceInformation DiffrenceInformation { get; set; }
    }
}
