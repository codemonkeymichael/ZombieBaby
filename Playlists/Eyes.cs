using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists;

public class Eyes
{
    Movement.Eyes moveEyes = new Movement.Eyes();
    Light.Eyes lightEyes = new Light.Eyes();

    public Eyes()
    {

    }

    /// <summary>
    /// Blink the eyes
    /// </summary>
    /// <param name="count"></param>
    public void Blink(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Closed();
            Open();
        }
        moveEyes.Release();
    }

    public void BlinkOnce()
    {
        Blink(1);
        moveEyes.Release();
    }

    public void BlinkOpen()
    {
        Open();
        Closed();
        Open();
        Closed();
        Open();
        moveEyes.Release();
    }

    public void BlinkClosed()
    {        
        Closed();
        Open();
        Closed();
        Open();
        Closed();
        moveEyes.Release();
    }

    public void Open()
    {
        moveEyes.Open();
        Thread.Sleep(150);
        lightEyes.On();
        Thread.Sleep(100);
    }

    public void Closed()
    {
        moveEyes.Closed();
        Thread.Sleep(150);
        lightEyes.Off();
        moveEyes.Release();
    }
}
