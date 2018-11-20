using System;

namespace MyOptions.API.Dtos
{
    public class UserForListDto
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public string PhotoUrl { get; set; }
        
    }
}