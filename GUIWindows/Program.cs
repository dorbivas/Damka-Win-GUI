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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormSettings());

            //Application.Run(new FormGame());
            GameManager game = new GameManager();
            game.Run();
        }
    }
}
