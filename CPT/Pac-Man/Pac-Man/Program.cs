using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pac_Man
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Gameplay());

            Gameplay GameplayForm = new Gameplay();
            GameplayForm.Show();

            //Form1 MainMenuForm = new Form1();
            //MainMenuForm.Show();

            Application.Run();
        }
    }
}
