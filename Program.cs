using AutoMapper;
using Iot.Device.Pwm;
using LibVLCSharp.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Device.Pwm;
using System.Diagnostics;
using System.Text.Json;
using System.Timers;
using ZombieBaby.Effects;
using ZombieBaby.Light;

using ZombieBaby.Utilities;


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;





namespace ZombieBaby;
class Program
{

	static void Main(string[] args)
	{
		//var serviceProvider = new ServiceCollection()
		//  .AddLogging()
		//  .AddSingleton<PwmController>()
		//  .BuildServiceProvider();


		//If the user stops the program
		Console.CancelKeyPress += new ConsoleCancelEventHandler(CloseHandler);

		Console.Clear();
		Console.WriteLine("Zombie Baby is Running Ver 1.9");

		//Get the IP address of this pi
		var proc = new Process
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = "hostname",  //my linux command i want to execute
				Arguments = "-I",  //the argument to return ip address
				UseShellExecute = false,
				RedirectStandardOutput = true,  //redirect output to my code here
				CreateNoWindow = true //do not show a window
			}
		};
		proc.Start();  //start the process
		while (!proc.StandardOutput.EndOfStream)  //wait until entire stream from output read in
		{
			Console.WriteLine(proc.StandardOutput.ReadLine());  //this contains the ip output                    
		}

		//DMX Light Init
		//DMX.Connect();
		//Light.Ambient.GroundEffect(10, 5000);
		//Light.Ambient.Room();

		////VLC Audio Player Init
		//Core.Initialize();

		//This instantiates the pi gpio pins
		Gpios.SetUpGpios();

		////Thread flicker = new Thread(() => Ambient.Flicker());
		////flicker.Start();

		//CancellationTokenSource source = new CancellationTokenSource();
		//////source.CancelAfter(TimeSpan.FromSeconds(5));
		//Task task = Task.Run(() => Ambient.Flicker(source.Token), source.Token);


	

		////Movement.Carriage mc = new Movement.Carriage();
		////mc.Up();

		var inputLastState = PinValue.Low;
		var activeInput = 0;

		//Body Down
		var pwmBody = PwmChannel.Create(0, 0, 50, 0.078);
		pwmBody.Start();
		////Eyes Closed
		//var pwmEyes = PwmChannel.Create(0, 1, 50, 0.02);
		//pwmEyes.Start();
		//Thread.Sleep(555);
		//pwmEyes.Stop();

		string[] lines = { "How long will this battery last?", "Lets find out." };

		// Set a variable to the Documents path.
		string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		Console.WriteLine(docPath);

		// Write the string array to a new file named "WriteLines.txt".
		using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "BatteryDuration.txt")))
		{
			foreach (string line in lines)
			{
				outputFile.WriteLine(line);
			}
		}

		int minutesCounter = 0;

		while (true)
		{
			Thread.Sleep(1000 * 60);
			minutesCounter++;
			string[] txtLines = { $"{minutesCounter} minutes" };

			// Append new lines of text to the file
			File.AppendAllLines(Path.Combine(docPath, "BatteryDuration.txt"), txtLines);
			Console.WriteLine($"{minutesCounter} minutes");
		}
	}//Main


	private static void CloseHandler(object? sender, ConsoleCancelEventArgs e)
	{
		Console.WriteLine("");
        Light.Eyes.Off();
        Effects.Smoke.Off();
        Light.Blinders.Off();

        Console.WriteLine("Bye for now :)");
	}
}