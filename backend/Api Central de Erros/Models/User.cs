using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Central_de_Erros.Models
{
    [Table("User")]
    public class User
    {
        [Column("userId")]
        [Key]
        public int userId { get; set; }

        [Column("email")]
        [Required]
        public string email { get; set; }

        [Column("password")]
        [Required]
        public string password { get; set; }

        [Column("createdAt")]
        [Required]
        public DateTime createdAt { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}
