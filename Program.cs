using System;
using System.Windows.Forms;
using System.Globalization;
using ParticlesScreenSaver;

namespace XNA_Screensaver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (args.Length > 0)
            {
                // Get the 2 character command line argument
                string arg = args[0].ToLower(CultureInfo.InvariantCulture).Trim().Substring(0, 2);

                //check different things depending on what the argument was
                switch (arg)
                {
                    case "/c":
                        ShowOptions();      // Show the options dialog
                        break;

                    case "/p":
                        // Don't do anything for preview
                        break;

                    case "/s":
                        ShowScreenSaver();      // Show screensaver form
                        break;

                    default:
                        MessageBox.Show("Invalid command line argument :" + arg, "Invalid Command Line Argument", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                // If no arguments were passed in, show the screensaver
                ShowScreenSaver();
            }


        }


        /// <summary>
        /// This will pop up the settings form.
        /// 
        /// This is WinForm I've added to the project.
        /// Edit that as you see fit for your screensaver.
        /// </summary>
        static private void ShowOptions()
        {
            SettingsForm settingsForm = new SettingsForm();
            Application.Run(settingsForm);
        }



        /// <summary>
        /// This will show the screensaver.
        /// </summary>
        static private void ShowScreenSaver()
        {

            using (PScreenSaverGame game = new PScreenSaverGame())
            {
                game.Run();
            }

        }

    }

}

