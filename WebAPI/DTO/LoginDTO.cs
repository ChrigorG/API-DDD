﻿namespace WebAPI.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Age { get; set; }
        public string CellPhone { get; set; } = string.Empty;
    }
}