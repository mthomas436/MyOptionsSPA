using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOptions.API.Models
{
    public class User
    {
        public int Userid { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string Username { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string Lastname { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string PhotoUrl { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
        public bool Active { get; set; }     
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
 
    }
 
}