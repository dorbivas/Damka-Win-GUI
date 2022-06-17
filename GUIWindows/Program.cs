namespace GUIWindows
{
    using System;

    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            GameManager game = new GameManager();
            game.Run();
        }
    }
}
