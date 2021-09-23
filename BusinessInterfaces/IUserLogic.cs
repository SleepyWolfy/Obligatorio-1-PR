﻿using Domain;

namespace BusinessInterfaces
{
    public interface IUserLogic
    {
        bool Login(User user);
        void Logout(User user);
    }
}