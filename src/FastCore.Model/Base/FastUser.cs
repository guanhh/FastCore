using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.Model
{
    [Table("Base_FastUsers")]
    public class FastUser
    {
        [Key]
        [Required]
        public Guid UserId { get; set; }


        [Required]
        [StringLength(30)]
        public string UserName { get; set; }


        [Required]
        [StringLength(100)]
        public string Password { get; set; }


        [StringLength(15)]
        public string PhoneNumber { get; set; }


        [StringLength(50)]
        public string Email { get; set; }


        [Required]
        public int Status { get; set; }

    }

}
