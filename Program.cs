// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.Pwm;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using Iot.Device.ServoMotor;
using Iot.Device.Pwm;

// Some SG90s can do 180 angle range but some other will be oscillating on the edge values
// Max angle which doesn't cause any issues found experimentally was as below.
// The ones which can do 180 will have the minimum pulse width at around 520uS.
const int AngleRange = 173;
const int MinPulseWidthMicroseconds = 600;
const int MaxPulseWidthMicroseconds = 2590;

var busId = 1;
var selectedI2cAddress = 0b000000; // A5 A4 A3 A2 A1 A0
var deviceAddress = Pca9685.I2cAddressBase + selectedI2cAddress;

I2cConnectionSettings settings = new(busId, deviceAddress);
using I2cDevice device = I2cDevice.Create(settings);

using Pca9685 pca9685 = new(device);
Console.Clear();
Console.WriteLine($"PCA9685 is ready on I2C bus {device.ConnectionSettings.BusId} with address {device.ConnectionSettings.DeviceAddress}");
Console.WriteLine($"PWM Frequency: {pca9685.PwmFrequency}Hz");

//Console.WriteLine("Set the PWM freq at 50hz for the SG90 ");
//pca9685.PwmFrequency = 50;

Console.WriteLine("Set the int position");
pca9685.SetDutyCycle(0, 0.030029296875);
Thread.Sleep(1000);

Console.WriteLine("SG90 Servo This is fastest it will single step."); 
for (double d = 0.030029296875; d < 0.12939453125; d += 0.001)
{
    Console.WriteLine("First Loop " + d);
    pca9685.SetDutyCycle(0, d);
    Thread.Sleep(3);
    Console.SetCursorPosition(0, 5);
}
for (double dd = 0.12939453125; dd > 0.030029296875; dd += -0.001)
{
    Console.WriteLine("Second Loop " + dd);
    pca9685.SetDutyCycle(0, dd);
    Thread.Sleep(3);
    Console.SetCursorPosition(0, 6);
}
Thread.Sleep(1000);
Console.WriteLine("To move as fast as it will go just move to a spot without stepping. It's really not much faster that the above.");
pca9685.SetDutyCycle(0, 0.12939453125);
Thread.Sleep(1000);

Console.WriteLine("This is really slow.");
for (double dd = 0.12939453125; dd > 0.030029296875; dd += -0.0001)
{
    Console.WriteLine("Third Loop " + dd);
    pca9685.SetDutyCycle(0, dd);
    Thread.Sleep(5);
    Console.SetCursorPosition(0, 9);
}
for (double d = 0.030029296875; d < 0.12939453125; d += 0.0001)
{
    Console.WriteLine("Fourth Loop " + d);
    pca9685.SetDutyCycle(0, d);
    Thread.Sleep(5);
    Console.SetCursorPosition(0, 10);
}




Console.WriteLine();
PrintHelp();

while (true)
{
    string[]? command = Console.ReadLine()?.ToLower()?.Split(' ');
    if (command?[0] is not { Length: > 0 })
    {
        return;
    }

    switch (command[0][0])
    {
        case 'q':
            pca9685.SetDutyCycleAllChannels(0.0);
            return;
        case 'f':
            {
                var freq = double.Parse(command[1]);
                pca9685.PwmFrequency = freq;
                Console.WriteLine($"PWM Frequency has been set to {pca9685.PwmFrequency}Hz");
                break;
            }

        case 'd':
            {
                switch (command.Length)
                {
                    case 2:
                        {
                            double value = double.Parse(command[1]);
                            pca9685.SetDutyCycleAllChannels(value);
                            Console.WriteLine($"PWM duty cycle has been set to {value}");
                            break;
                        }

                    case 3:
                        {
                            int channel = int.Parse(command[1]);
                            double value = double.Parse(command[2]);
                            pca9685.SetDutyCycle(channel, value);
                            Console.WriteLine($"PWM duty cycle has been set to {value}");
                            break;
                        }
                }

                break;
            }

        case 'h':
            {
                PrintHelp();
                break;
            }

        case 't':
            {
                int channel = int.Parse(command[1]);
                ServoDemo(pca9685, channel);
                PrintHelp();
                break;
            }
    }
}

ServoMotor CreateServo(Pca9685 pca9685, int channel) =>
     new ServoMotor(
        pca9685.CreatePwmChannel(channel),
        AngleRange, MinPulseWidthMicroseconds, MaxPulseWidthMicroseconds);

void PrintServoDemoHelp()
{
    Console.WriteLine("Q                   return to previous menu");
    Console.WriteLine("C                   calibrate");
    Console.WriteLine("{angle}             set angle (0 - 180)");
    Console.WriteLine();
}

void ServoDemo(Pca9685 pca9685, int channel)
{
    using ServoMotor servo = CreateServo(pca9685, channel);
    PrintServoDemoHelp();

    while (true)
    {
        string? command = Console.ReadLine()?.ToLower();
        if (command is null)
        {
            return;
        }

        if (command[0] is 'q')
        {
            return;
        }

        if (command[0] == 'c')
        {
            CalibrateServo(servo);
            PrintServoDemoHelp();
        }
        else
        {
            double value = double.Parse(command);
            servo.WriteAngle(value);
            Console.WriteLine($"Angle set to {value}. PWM duty cycle = {pca9685.GetDutyCycle(channel)}");
        }
    }
}

void PrintHelp()
{
    Console.WriteLine("Command:");
    Console.WriteLine("    F {freq_hz}          set PWM frequency (Hz)");
    Console.WriteLine("    D {value}            set duty cycle (0.0 .. 1.0) for all channels");
    Console.WriteLine("    S {channel} {value}  set duty cycle (0.0 .. 1.0) for specific channel");
    Console.WriteLine("    T {channel}          servo test (defaults to SG90 params)");
    Console.WriteLine("    H                    prints help");
    Console.WriteLine("    Q                    quit");
    Console.WriteLine();
}

void CalibrateServo(ServoMotor servo)
{
    int maximumAngle = 180;
    int minimumPulseWidthMicroseconds = 520;
    int maximumPulseWidthMicroseconds = 2590;

    Console.WriteLine("Searching for minimum pulse width");
    CalibratePulseWidth(servo, ref minimumPulseWidthMicroseconds);
    Console.WriteLine();

    Console.WriteLine("Searching for maximum pulse width");
    CalibratePulseWidth(servo, ref maximumPulseWidthMicroseconds);

    Console.WriteLine("Searching for angle range");
    Console.WriteLine(
        "What is the angle range? (type integer with your angle range or enter to move to MIN/MAX)");

    while (true)
    {
        servo.WritePulseWidth(maximumPulseWidthMicroseconds);
        Console.WriteLine("Servo is now at MAX");
        if (int.TryParse(Console.ReadLine(), out maximumAngle))
        {
            break;
        }

        servo.WritePulseWidth(minimumPulseWidthMicroseconds);
        Console.WriteLine("Servo is now at MIN");

        if (int.TryParse(Console.ReadLine(), out maximumAngle))
        {
            break;
        }
    }

    servo.Calibrate(maximumAngle, minimumPulseWidthMicroseconds, maximumPulseWidthMicroseconds);
    Console.WriteLine($"Angle range: {maximumAngle}");
    Console.WriteLine($"Min PW [uS]: {minimumPulseWidthMicroseconds}");
    Console.WriteLine($"Max PW [uS]: {maximumPulseWidthMicroseconds}");
}

void CalibratePulseWidth(ServoMotor servo, ref int pulseWidthMicroSeconds)
{
    void SetPulseWidth(ref int pulseWidth)
    {
        pulseWidth = Math.Max(pulseWidth, 0);
        servo.WritePulseWidth(pulseWidth);
    }

    Console.WriteLine("Use A/Z (1x); S/X (10x); D/C (100x)");
    Console.WriteLine("Press enter to accept value");

    while (true)
    {
        SetPulseWidth(ref pulseWidthMicroSeconds);
        Console.WriteLine($"Current value: {pulseWidthMicroSeconds}");

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.A:
                pulseWidthMicroSeconds++;
                break;
            case ConsoleKey.Z:
                pulseWidthMicroSeconds--;
                break;
            case ConsoleKey.S:
                pulseWidthMicroSeconds += 10;
                break;
            case ConsoleKey.X:
                pulseWidthMicroSeconds -= 10;
                break;
            case ConsoleKey.D:
                pulseWidthMicroSeconds += 100;
                break;
            case ConsoleKey.C:
                pulseWidthMicroSeconds -= 100;
                break;
            case ConsoleKey.Enter:
                return;
        }
    }
}