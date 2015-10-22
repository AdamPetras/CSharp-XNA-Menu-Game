﻿using System;
using GrandTheftAuto.MenuFolder;

namespace GrandTheftAuto
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new GameClass())
                game.Run();
        }
    }
#endif
}
