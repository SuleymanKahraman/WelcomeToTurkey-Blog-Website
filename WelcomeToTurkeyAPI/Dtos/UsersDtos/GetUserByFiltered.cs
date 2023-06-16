﻿using WelcomeToTurkeyAPI.Data.Enums;

namespace WelcomeToTurkeyAPI.Dtos.UsersDtos
{
    public class GetUserByFiltered
    {
        public string FilterChars { get; set; }
        public UserTypes UserType { get; set; }

    }
}
