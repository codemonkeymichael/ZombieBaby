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
		//DMX.Connect();
		//Light.Ambient.GroundEffect(10, 5000);
		//Light.Ambient.Room();

		//VLC Audio Player Init
		Core.Initialize();

		//This instantiates the pi gpio pins
		Gpios.SetUpGpios();

        //Thread flicker = new Thread(() => Ambient.Flicker());
        //flicker.Start();

        CancellationTokenSource source = new CancellationTokenSource();
        //source.CancelAfter(TimeSpan.FromSeconds(5));
        Task task = Task.Run(() => Ambient.Flicker(source.Token), source.Token);

		AnimationPlayer.InitAnimation();
		Thread.Sleep(2000);

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
							Console.WriteLine("A");
							Playlists.Defcon3.Sleep();
							
						}
						if (activeInput == 23)//B button Inside Defcon Context Cycler
						{
							//Test 
							Console.WriteLine("B");
							//Fan.OnOffQuick();
						}
						if (activeInput == 24)//C button Outside Show sequencer
						{
							
							Console.WriteLine("C Button Push");
						
							//Playlists.Blinders.Flash();
							//Playlists.Show1.SitUp();
							  Playlists.Head h = new Playlists.Head();
							h.LookAround(10);
							// Movement.Head b = new Head();
							//b.Right();
							//Thread.Sleep(2000);
							//b.Left();


						}
						if (activeInput == 25) //D button Outside Show gun shots
						{
							//Test
							Console.WriteLine("D");
							Playlists.Show.GunShot();
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
		//Utilities.DMX.Disconnect();
		Movement.Body b = new Movement.Body();
		b.Release();
		Movement.Carriage c = new Movement.Carriage();
		c.Release();
		Light.Eyes le = new Light.Eyes();
		le.Off();
		Movement.Eyes me = new Movement.Eyes();
		me.Closed();
		Thread.Sleep(1000);
		me.Release();
		Movement.Head mh = new Movement.Head();
		mh.Release();
		Effects.Fan ef = new Effects.Fan();
		ef.Off();
		Light.Blinders.Off();

		Console.WriteLine("Bye for now :)");
	}
}