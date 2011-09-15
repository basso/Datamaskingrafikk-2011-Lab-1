using System;

namespace Trekant1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Trekant1 game = new Trekant1())
            {
                game.Run();
            }
        }
    }
}

