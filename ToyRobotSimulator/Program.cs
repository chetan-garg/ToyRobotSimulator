using Contracts.Enums;
using Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using ToyRobotSimlator;
using ToyRobotSimlator.ToyRobot;

namespace ToyRobotSimulator
{
    internal class Program
    {
        static ServiceProvider Services;
        static void Main()
        {
            ResolveDependencies();

            StringBuilder commandFormat = new StringBuilder();
            commandFormat.AppendLine($"This is a robot simulator for a 6X6 board.\r\n" +
                $"Following commands can be given to the robot:\r\n\r\n" +
                $"PLACE - Format \"PLACE X,Y,DIRECTION\" where X and Y are board coordinates " +
                $"and DIRECTION is the face of the robot.\r\n Please note there is no space allowed between X,Y,DIRECTION");
            commandFormat.AppendLine($"REPORT - Tells the current position of the robot.");
            commandFormat.AppendLine($"MOVE - Moves the robot one place in the direction it is facing.");
            commandFormat.AppendLine($"LEFT - Rotates the robot LEFT by 90 degress without moving.");
            commandFormat.AppendLine($"RIGHT - Rotates the robot RIGHT by 90 degress without moving.");
            commandFormat.AppendLine($"EXIT - Exit the application.\r\n\r\n");
            commandFormat.AppendLine($"PLACE has to be the first command. Any command before PLACE will be ignored.\r\n Please enter command below.");
            Console.WriteLine(commandFormat);

            var simulator = Services.GetService<RobotOrchestrator>();
            
            if (simulator != null)
            {
                bool exitApp = false;
                while (!exitApp)
                {
                    string input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        try
                        {
                            string[] commandInput = input.Split(' ');
                            RobotCommandType command = simulator.CommandParser.ParseCommand(commandInput[0]);
                            switch (command)
                            {
                                case RobotCommandType.Place:
                                case RobotCommandType.Move:
                                case RobotCommandType.Left:
                                case RobotCommandType.Right:
                                case RobotCommandType.Report:
                                    Console.WriteLine(simulator.Process(input));
                                    Console.WriteLine("Please enter next command..");
                                    break;
                                case RobotCommandType.Exit:
                                    exitApp = true;
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a command to proceed.");
                    }
                }
            }
            else
            {
                Console.Write("Cannot initialise application. Exiting..");
            }         

        }

        /// <summary>
        /// This method will resolve all the dependencies for any interfaces required by our classes.
        /// </summary>
        private static void ResolveDependencies()
        {
            Services = new ServiceCollection().
                AddSingleton<IBoard>(new Board(6, 6)).
                AddSingleton<ICommandParser, CommandParser>().
                AddSingleton<IRobot, Robot>().AddSingleton<RobotOrchestrator>().BuildServiceProvider();
        }
    }
}
