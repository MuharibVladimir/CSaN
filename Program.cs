using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracert
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write(">>> ");
            string command = Console.ReadLine();
            while (command != "exit")
            {
                string hostName;
                bool showName = false;

                if (command != null && command.IndexOf("tracert") == 0)
                {
                    command = command.Remove(0, 8);
                    if (command.IndexOf("-d", StringComparison.Ordinal) > 0)
                    {
                        command = command.Remove(command.IndexOf("-d", StringComparison.Ordinal) - 1);
                        showName = true;
                    }

                    hostName = command;
                    Trace.Execute(hostName, showName);
                }
                else
                    Console.WriteLine("Были введены некорректные данные.");

                Console.Write(">>> ");
                command = Console.ReadLine();
            }
        }
    }
}
