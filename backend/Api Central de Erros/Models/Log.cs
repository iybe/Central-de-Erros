using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Central_de_Erros.Models
{
    [Table("Log")]
    public class Log
    {
        [Column("logId")]
        [Key]
        public int logId { get; set; }

        [Column("userId")]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }

        [Column("environment")]
        [Required]
        public string environment { get; set; }

        [Column("level")]
        [Required]
        public string level { get; set; }

        [Column("frequency")]
        [Required]
        public int frequency { get; set; }

        [Column("origin")]
        [Required]
        public string origin { get; set; }

        [Column("title")]
        [Required]
        public string title { get; set; }

        [Column("description")]
        [Required]
        public string description { get; set; }

        [Column("createdAt")]
        [Required]
        public DateTime createdAt { get; set; }
    }
}
