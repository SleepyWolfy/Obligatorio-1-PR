﻿using System;
using ClientApplicationInterfaces;
using Common.Protocol.NTOs;
using ConsoleMenusInterfaces;

namespace ConsoleMenus.Client
{
    public class ConsoleModifyGameMenu : ConsoleMenusBase, IConsoleMenu
    {
        public IConsoleMenu NextMenu => _nextMenu;
        public bool RequiresAnswer { get; }
        public void PrintMenu()
        {
            Console.WriteLine("Insert the modified values for the game");
            Console.WriteLine("If a field is not modified, leave it blank");
        }

        public bool Action(string answer)
        {
            GameNetworkTransferObject input = GetModifiedGame();

            string response = IClientHandler.Instance.ModifyGame(input).Result;

            _nextMenu = new ConsoleGameMenu(true);
            Console.WriteLine(response);
            
            return false;
        }
        
        private GameNetworkTransferObject GetModifiedGame()
        {
            GameNetworkTransferObject input = new GameNetworkTransferObject();
            
            Console.WriteLine("Game Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Genre: ");
            string genre = Console.ReadLine();

            Console.WriteLine("Public ESRB rating: ");
            string esrb = Console.ReadLine();

            Console.WriteLine("Enter the game's synopsis: ");
            string synopsis = Console.ReadLine();
            
            Console.WriteLine("Cover Path: ");
            Console.WriteLine("(its necessary to introduce a new cover path to continue)");
            string coverPath = Console.ReadLine();
            while(coverPath == "")
            {
                Console.WriteLine("\nNo cover path introduced, reenter:");
                coverPath = Console.ReadLine();
            }
            
            input.Title = title;
            input.Genre = genre;
            input.ESRB = esrb;
            input.Synopsis = synopsis;
            input.CoverPath = coverPath;

            return input;
        }
    }
}