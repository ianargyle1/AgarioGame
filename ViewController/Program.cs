/// <summary> 
/// Author:    Ian Argyle
/// Partner:   None
/// Date:      4/12/2020
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Ian Argyle - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Ian Argyle, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    GUI launcher for the Agario game.
/// </summary>

using System;
using System.Windows.Forms;

namespace ViewController
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
       {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());
        }
    }
}
