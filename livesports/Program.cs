using System;

namespace livesports
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://flashscore.pl/";
            Console.Write("Podaj nazwe dyscypliny: ");
            string sportsname = Console.ReadLine();


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
