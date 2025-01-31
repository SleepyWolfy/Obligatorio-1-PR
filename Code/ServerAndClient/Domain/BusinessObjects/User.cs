﻿using System;
using System.Collections.Generic;

namespace Domain.BusinessObjects
{
    [Serializable]
    public class User
    {
        public const int DEFAULT_USER_ID = -1;
        public string Username { get; set; }
        public bool LoggedIn { get; set; }
        public int ID {get; set;}
        
        public List<Game> ownedGames { get; set; }

        public User(string username, int id)
        {
            this.Username = username;
            this.LoggedIn = false;
            this.ID = id;
            ownedGames = new List<Game>();
        }

        public override bool Equals(object obj)
        {
            User user = (User)obj;

            return this.Username == user.Username || this.ID == user.ID;
        }
    }
}
