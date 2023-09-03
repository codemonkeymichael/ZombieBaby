using AutoMapper;
using Iot.Device.Pwm;
using LibVLCSharp.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Device.Pwm;
using System.Diagnostics;
using System.Text.Json;
using System.Timers;
using ZombieBaby.Animation;
using ZombieBaby.Effects;
using ZombieBaby.Light;
using ZombieBaby.Movement;
using ZombieBaby.Utilities;




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
		DMX.Connect();
		//Light.Ambient.GroundEffect(10, 5000);
		Light.Ambient.Room();

		//VLC Audio Player Init
		Core.Initialize();

		//This instantiates the pi gpio pins
		Gpios.SetUpGpios();

		Thread flicker = new Thread(() => Ambient.Flicker());
		flicker.Start();

		//AnimationPlayer.InitAnimation();
		//Thread.Sleep(2000);

		//Movement.Carriage mc = new Movement.Carriage();
		//mc.Up();

		var inputLastState = PinValue.Low;
		var activeInput = 0;

		while (true)
		{
			foreach (var input in Gpios.InputTriggers)
			{
				if (Gpios.piGPIOController.Read(input) != inputLastState && (activeInput == 0 || activeInput == input))
				{
					Console.WriteLine($"Click {input} {Gpios.piGPIOController.Read(input)}");
					if (PinValue.Low == inputLastState)
					{
						inputLastState = PinValue.High;
						activeInput = input;
						//Thread status = new Thread(() => Status.ElevateDefcon());
						//status.Start();

						if (activeInput == 4) //A button remote Inside Defcon Stepper Upper
						{
							//Test 
							Blinders.OnOff();
						}
						if (activeInput == 23)//B button Inside Defcon Context Cycler
						{
							//Test 
							Fan.OnOffQuick();
						}
						if (activeInput == 24)//C button Outside Show sequencer
						{
							//Test 
							Body.UpDownSlow();
						}
						if (activeInput == 25) //D button Outside Show gun shots
						{
							//Test
							Playlists.Head.LookAround(1);
						}
					}
					else
					{
						inputLastState = PinValue.Low;
						activeInput = 0;
					}
					Thread.Sleep(500);
					break;
				}
			} //foreach
		}//while
	}//Main


	private static void CloseHandler(object? sender, ConsoleCancelEventArgs e)
	{
		Console.WriteLine("");
		Utilities.DMX.Disconnect();	  
        Movement.Body.Release();         
        Movement.Carriage.Release();     
        Light.Eyes.Off();
		Movement.Eyes.Release();
        Movement.Head.Release();
        Effects.Fan.Off();
		Light.Blinders.Off();
		Console.WriteLine("Bye for now :)");
	}
}