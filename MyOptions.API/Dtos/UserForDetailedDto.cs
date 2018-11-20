using System;
using System.Collections.Generic;
using MyOptions.API.Models;

namespace MyOptions.API.Dtos
{
    public class UserForDetailedDto
    {
 
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Trade> Trades { get; set; }

    }

}