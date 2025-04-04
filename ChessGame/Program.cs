﻿using System.Globalization;
using System.Text;
using ChessGame.Entities.BoardLayer;
using ChessGame.Entities.ChessLayer;
using ChessGame.Entities.Enums;
using ChessGame.Exceptions;
using ChessGame.Services;

namespace ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                Console.OutputEncoding = Encoding.UTF8;

                ChessMatch newGame = new();

                do
                {
                    Console.WriteLine("\n\nDo you want to play again?");
                    Console.WriteLine("\nPress 1 for Yes or 2 for No.");
                    char response = char.Parse(Console.ReadLine()!);
                    if (response == '2')
                    {
                        break;
                    }
                    else if (response != '1')
                    {
                        Console.WriteLine("\nInvalid option. Please try again.");
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nStarting a new game...");
                        newGame = new ChessMatch();
                    }
                } while (true);
            }
            catch (BoardExceptions e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nAn unexpected error occurred: " + e.Message);
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit...");
            }


        }
    }
}
