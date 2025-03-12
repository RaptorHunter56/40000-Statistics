using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _40000_Statistics
{
    internal class Program
    {
        #region Fullscreen
        // Import the necessary Windows API functions
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong); // Change to uint

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        const int SW_MAXIMIZE = 3;
        const int GWL_STYLE = -16;
        const uint WS_POPUP = 0x80000000; // Change to uint
        const uint WS_VISIBLE = 0x10000000; // Change to uint
        #endregion
        static void Main(string[] args)
        {
            #region Fullscreen
            // Get the console window handle
            IntPtr consoleWindow = GetConsoleWindow();

            // Set the console window to fullscreen
            ShowWindow(consoleWindow, SW_MAXIMIZE);

            // Get the current style as uint
            uint currentStyle = (uint)GetWindowLong(consoleWindow, GWL_STYLE);

            // Set the new style
            SetWindowLong(consoleWindow, GWL_STYLE, currentStyle | WS_POPUP | WS_VISIBLE); // No need to cast

            #endregion

            bool running = true;

            Console.Clear();
            Console.WriteLine("Select an option:");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
            while (true)
            {
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                if (input == "0") break;

                var tt = (AstraMilitarum.Units[0] as UnitBase)[9].GetAttackOptionGroupBase(9).GetAttackGroups();

                int index = int.TryParse(input, out index) ? index - 1 : -1;
            }
        }
    }
}
