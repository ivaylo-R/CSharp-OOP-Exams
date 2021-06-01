using Minedraft.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;

public class Engine
{
    private DraftManager manager;
    private IWriter writer;
    private IReader reader;

    public Engine()
    {
        reader = new ConsoleReader();
        writer = new ConsoleWriter();
        this.manager = new DraftManager();
    }

    public void Run()
    {
        while (true)
        {
            var input = reader.ReadLine();
            var data = input.Split().ToList();
            var command = data[0];
            List<string> args = null;
            string result = string.Empty;
            switch (command)
            {
                case "Register":

                    if (data[1]=="Provider")
                    {
                        args = new List<string>(data.Skip(2).ToList());
                        writer.WriteLine(manager.RegisterProvider(args));
                        break;
                    }

                    else if (data[1] == "Harvester")
                    {
                        args = new List<string>(data.Skip(2).ToList());
                        writer.WriteLine(manager.RegisterHarvester(args));
                        break;
                    }
                    break;

                case "Day":
                    result = manager.Day();
                    writer.WriteLine(result);
                    break;
                case "Mode":
                    args = new List<string>(data.Skip(1).ToList());
                    writer.WriteLine(manager.Mode(args));
                    break;
                case "Check":
                    args = new List<string>(data.Skip(1).ToList());
                    writer.WriteLine(manager.Check(args));
                    break;
                default:
                    writer.WriteLine(manager.ShutDown());
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
