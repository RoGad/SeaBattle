namespace SeaBattle3
{
    public enum Status { 
        neisvestno, // 0
        mimo, // 1
        ranil, // 2
        ubil, // 3
        pobedil // 4
    }

    public struct Tochka {

        public int x;
        public int y;

        public Tochka (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}