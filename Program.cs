using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Text;

namespace KeyboardSpammer
{
    class Program
    {

        static bool isRunning; //False

        [STAThread] //Required for openfiledialog
        static void Main(string[] args)
        {
            Console.WriteLine("Please select a file");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                Console.WriteLine("Please enter the interval (in MILLIseconds) for starting a new line (or a new message in chat apps)");
                int delay = int.Parse(Console.ReadLine());

                isRunning = true;
                start(fileName, delay);
            }
            else
            {
                Console.WriteLine("You did not select a file");
                Thread.Sleep(1000);
                return;
            }
            
        }
        static void start(string fileName, int delay)
        {
            Console.WriteLine("Starting in 3 seconds (you can stop it with ctrl+c in the console)");
            Thread.Sleep(3000);

            int index = 0;
            while (isRunning)
            {
                Thread.Sleep(delay);
                string[] textLines = File.ReadAllLines(fileName, Encoding.UTF8); //Encode to UTF8 cuz why not
                SendKeys.SendWait(textLines.GetValue(index) + "{ENTER}");
                index++;

                //Restart from the beginning of the file
                if (index == textLines.Length)
                {
                    index = 0;
                }
            }
        }
    }
}
