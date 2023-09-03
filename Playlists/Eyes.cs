using Iot.Device.ExplorerHat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists;

public static class Eyes
{
    /// <summary>
    /// Blink the eyes
    /// </summary>
    /// <param name="count"></param>
    public static void Blink(int count)
    {      
        for (int i = 0; i < count; i++)
        {
            Closed();
            Open();
        }
        Movement.Eyes.Release();
    }

    public static void BlinkOnce()
    {
        Blink(1);
        Movement.Eyes.Release();
    }

    public static void BlinkOpen()
    {
        Open();
        Closed();
        Open();
        Closed();
        Open();
        Movement.Eyes.Release();
    }

    public static void BlinkClosed()
    {        
        Closed();
        Open();
        Closed();
        Open();
        Closed();
        Movement.Eyes.Release();
    }

    public static void Open()
    {
        Movement.Eyes.Open();
        Thread.Sleep(150);
        Light.Eyes.On();
        Thread.Sleep(100);
        Movement.Eyes.Release();
    }

    public static void Closed()
    {
        Movement.Eyes.Closed();
        Thread.Sleep(150);
        Light.Eyes.Off();
        Movement.Eyes.Release();
    }
}
