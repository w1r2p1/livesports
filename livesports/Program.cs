using System;

namespace livesports
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://flashscore.pl/";


            Console.WriteLine("1 - Pilka nozna\n2 - Tenis\n3 - Hokej\n4 - Koszykówka\n5 - Siatkówka\n6 - Baseball");
            Console.Write("Podaj numer lub nazwe dyscypliny: ");
            string sportsname = Console.ReadLine();
            switch (sportsname)
            {
                case "1":
                    sportsname = "Piłka nożna";
                    break;
                case "2":
                    sportsname = "Tenis";
                    break;
                case "3":
                    sportsname = "Hokej";
                    break;
                case "4":
                    sportsname = "Koszykówka";
                    break;
                case "5":
                    sportsname = "Siatkówka";
                    break;
                case "6":
                    sportsname = "Baseball";
                    break;
                default:
                    break;
            }

            Console.Write("Podaj nazwe druzyny: ");
            string teamname = Console.ReadLine();

            if (teamname == "quit")
            {
                Base.CloseEverything();
            }
            else
            {
                Driver.Goto(url);
                Base.CloseCookies();

                Homepage.SwitchSports(sportsname);
                Base.Getter(teamname);
            }

        }
    }
}
