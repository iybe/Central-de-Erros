using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Central_de_Erros.DTOs
{
    public class LogInputDTO
    {
        [Required]
        public string environment { get; set; }

        [Required]
        public string level { get; set; }

        [Required]
        public int frequency { get; set; }

        [Required]
        public string origin { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }
    }
}
