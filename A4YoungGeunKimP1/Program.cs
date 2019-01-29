/*
 Program ID: Assignment4

 Purpose: Theater reserveation and cancelation system

 Revision History
    written Young Geun. Kim & Martin. Hwang Jan 5, 2017 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace A4YoungGeunKimP1
{
    class Program
    {
        //Initialize Seats
        public static string[,] seatArray = new string[4, 4];

        //Helper Method(Number Check)
        static bool IsNumeric(string text)
        {
            int test;
            return int.TryParse(text, out test);
        }


        //Display Seat Method
        static void Display()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (seatArray[i, j] == null)
                    {
                        Console.Write("*" + (i + 1) + "-" + (j + 1) + " ");
                    }
                    else
                    {
                        Console.Write("*" + seatArray[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        //Get proper seat number and check the value
        static string GetSeat(string askSentence)
        {
            string seat;
            bool keepGoing;
            keepGoing = true;

            do
            {
                Console.Clear();
                Display();
                Console.Write(askSentence);
                seat = Console.ReadLine();

                if (CheckSeat(seat))
                {
                    if (1 <= GetRow(seat) && GetRow(seat) <= 4 &&
                        1 <= GetColumnn(seat) && GetColumnn(seat) <= 4)
                    {
                        keepGoing = false;

                        return seat;
                    }
                    else
                    {
                        Console.Write("\nPlease Input Proper Seat Number. " +
                            "We have only 4 row and Columnn");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.Write("\nPlease Input Proper Value " +
                        "with (a-b) form i.e) 1-4");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (keepGoing);

            return seat;
        }

        static bool CheckSeat(string seat)
        {
            return seat.Length == 3 &&
                IsNumeric(seat.Substring(0, 1)) &&
                seat.IndexOf("-") == 1 &&
                IsNumeric(seat.Substring(2, 1));

        }

        //Get Row and Column form seat number
        static int GetRow(string seat)
        {
            int row;

            row = int.Parse(seat.Substring(2, 1));

            return row;
        }
        static int GetColumnn(string seat)
        {
            int Columnn;

            Columnn = int.Parse(seat.Substring(0, 1));

            return Columnn;
        }

        //Get proper user name and check the value
        static string GetName(string askName)
        {
            string name;
            bool keepGoing;
            keepGoing = true;

            do
            {
                Console.Clear();
                Display();
                Console.Write(askName);
                name = Console.ReadLine();

                if (CheckName(name))
                {
                    keepGoing = false;

                    return name;
                }
                else
                {
                    Console.Write("\nPlease Input Proper Format. " +
                        "First name inital dot last name i.e) J.smith");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (keepGoing);

            return name;
        }
        static bool CheckName(string text)
        {


            var test = new Regex("^[a-zA-Z]*$");

            try
            {
                if (test.IsMatch(text.Substring(0, 1)) &&
                                test.IsMatch(text.Substring(2)) &&
                                text.Substring(1, 1) == ".")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }

        }


        //Cancel the reseration by SeatNumber or Name
        static void CancelBySeatNumber(string seatNumber)
        {
            int row;
            int column;

            row = GetRow(seatNumber);
            column = GetColumnn(seatNumber);
            if (seatArray[column - 1, row - 1] == null)
            {
                Console.Write("\nThe seat is not reserved yet. Please choose " +
                    "another seat.");
                Console.ReadLine();
            }
            else
            {
                seatArray[column - 1, row - 1] = null;
                Console.Write("\nSeatNumber " + (column) + "-" + (row) +
                    " has been canceled");
                Console.ReadLine();

            }

        }
        static void CancelByName(string name)
        {
            int row;
            int column;
            string cancelSeat;
            cancelSeat = null;
            bool keepGoing = true;
            if (SameNameCheck(name) == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (seatArray[i, j] == name)
                        {
                            seatArray[i, j] = null;
                            Console.Write("\nSeatNumber " + (i + 1) + "-" +
                                (j + 1) + " has been canceled");
                            Console.ReadLine();
                        }
                    }
                }
            }
            else if (SameNameCheck(name) == 0)
            {
                Console.Write("There is no certain name. Please Check again");
                Console.ReadLine();
            }
            else
            {
                do
                {
                    cancelSeat = GetSeat("\nmore than two same names " +
                        "are reserved in the seat.\nWhat seat number do " +
                        "you want to cancel\n\nPlease input the seat " +
                        "number: ");
                    column = GetColumnn(cancelSeat);
                    row = GetRow(cancelSeat);

                    if (seatArray[column - 1, row - 1] == name)
                    {
                        CancelBySeatNumber(cancelSeat);
                        return;
                    }

                    Console.Write("Please choose the seat that reserved" +
                        " by your name");
                    Console.ReadLine();
                    Console.Clear();
                } while (keepGoing);
            }
        }
        //Check there is more than one same name
        static int SameNameCheck(string name)
        {
            int sameNameCounter = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (seatArray[i, j] == name)
                    {
                        sameNameCounter++;
                    }
                }
            }
            return sameNameCounter;
        }
        //Reservation System
        static void AddSeat(string name)
        {
            bool keepGoing;
            string seat;
            int row;
            int column;

            keepGoing = true;

            do
            {
                seat = GetSeat("\nWhat seat do you want to reserve" +
                    "(input a-b form): ");
                row = GetRow(seat);
                column = GetColumnn(seat);
                if (seatArray[column - 1, row - 1] == null)
                {
                    seatArray[column - 1, row - 1] = name;
                    keepGoing = false;
                }
                else
                {
                    Console.Write("\nThe seat is alreay reserved\n" +
                        "Please Choose another seat");
                    Console.ReadLine();
                }
            } while (keepGoing);
        }
        //Cancel Resercation System
        static void CancelSeat()
        {
            string seatName;
            bool keepGoing;
            char option;

            keepGoing = true;
            option = '0';

            do
            {
                Console.Clear();
                Display();
                Console.WriteLine("\n1. Cancel by Name");
                Console.WriteLine("2. Cancel by Seat Number");
                Console.Write("\nPlease choose the option that way to cancel" +
                    " your reservation: ");
                try { option = char.Parse(Console.ReadLine()); }
                catch (FormatException)
                {

                }


                switch (option)
                {
                    case '1':
                        seatName = GetName("\nWhat was the name that you" +
                            " reserved i.e)J.smith): ");
                        CancelByName(seatName);
                        keepGoing = false;
                        break;
                    case '2':
                        seatName = GetSeat("\nWhat seat do you want to cancel" +
                            "(input a-b form): ");
                        CancelBySeatNumber(seatName);
                        keepGoing = false;
                        break;
                    default:
                        Console.WriteLine("\nPlease choose between one or two");
                        Console.ReadLine();
                        break;
                }
            } while (keepGoing);
        }
        //Check Seat is Full
        static bool CheckFullSeat()
        {
            int seatCounter;
            seatCounter = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (seatArray[i, j] != null)
                    {
                        seatCounter++;
                    }
                }
            }
            return (seatCounter == 16);
        }


        static void Main(string[] args)
        {
            bool keepGoing;
            string name;

            do
            {
                Console.Clear();
                keepGoing = true;
                Display();
                Char option;

                option = '0';
                if (!CheckFullSeat())
                {
                    Console.WriteLine("\n1. Reservation");
                    Console.WriteLine("2. Remove reservation");
                    Console.WriteLine("X. Exit");
                    Console.Write("\nPlease Choose the option: ");
                }
                else
                {
                    Console.WriteLine("\n**********************");
                    Console.WriteLine("*******SOLD OUT*******");
                    Console.WriteLine("**********************\n");
                    Console.WriteLine("2. Remove reservation");
                    Console.WriteLine("X. Exit");
                    Console.Write("\nPlease Choose the option: ");
                }


                try { option = char.Parse(Console.ReadLine()); }
                catch (FormatException)
                {

                }

                switch (option)
                {
                    case '1':
                        if (!CheckFullSeat())
                        {
                            name = GetName("\nWhat is the name you are goning" +
                                " to reserve? i.e)J.smith): ");
                            AddSeat(name);
                        }
                        else
                        {
                            Console.WriteLine("\nNo seats available");
                            Console.ReadLine();
                        }

                        break;
                    case '2':
                        CancelSeat();
                        break;
                    case 'X':
                    case 'x':
                        keepGoing = false;
                        break;
                    default:
                        Console.Write("\nPlease Choose from the option");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            } while (keepGoing);
        }
    }
}
