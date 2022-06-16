using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUIWindows
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GameManager game = new GameManager();
            game.Run();
        }
    }
}
