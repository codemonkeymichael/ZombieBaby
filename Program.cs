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
		//Eyes Closed
		var pwmEyes = PwmChannel.Create(0, 1, 50, 0.02);
		pwmEyes.Start();
		Thread.Sleep(555);
		pwmEyes.Stop();

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
							Console.WriteLine("A Button Push Smoke");
							Effects.Smoke.On();
						}
						if (activeInput == 23)//B button Inside Defcon Context Cycler
						{
						   
							Console.WriteLine("B Button Push Sit Up");

							pwmBody.Start();
							pwmBody.DutyCycle = 0.041; //Sit Up

							Effects.Fan.On();
							Light.Eyes.On();
							Light.Blinders.On();
						   
							Thread.Sleep(700);
							Blinders.Off();

                            Thread.Sleep(3000);
                            Effects.Smoke.Off();
                            Thread.Sleep(3000);
                            Effects.Fan.Off();


                        }
						if (activeInput == 24)//C button Outside Show sequencer
						{

							Console.WriteLine($"C Button Push");

						   //Body Down                             
							pwmBody.DutyCycle = 0.078;
							Thread.Sleep(1000);                  
							pwmBody.Stop();

                            Light.Eyes.Off();
                            Effects.Smoke.Off();
                            Light.Blinders.Off();

                        }
						if (activeInput == 25) //D button Outside Show gun shots
						{
							//Test
							Console.WriteLine("D Button Push Gun Shots");

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
		//Movement.Body b = new Movement.Body();
		//b.Release();
		//Movement.Carriage c = new Movement.Carriage();
		//c.Release();
	   // Light.Eyes le = new Light.Eyes();
		//le.Off();
		//Movement.Eyes me = new Movement.Eyes();
		//me.Closed();
		//Thread.Sleep(1000);
		//me.Release();
		//Movement.Head mh = new Movement.Head();
		//mh.Release();
		Effects.Fan ef = new Effects.Fan();
		ef.Off();
		Light.Blinders.Off();

		Console.WriteLine("Bye for now :)");
	}
}