using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Gmail { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? DateOfBirth { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
