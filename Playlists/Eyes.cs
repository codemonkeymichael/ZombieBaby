using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists;

public class Eyes
{
    //Movement.Eyes moveEyes = new Movement.Eyes();
    //Light.Eyes lightEyes = new Light.Eyes();

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
        Movement.Eyes.Release();
    }

    public void BlinkOnce()
    {
        Blink(1);
        Movement.Eyes.Release();
    }

    public void BlinkOpen()
    {
        Open();
        Closed();
        Open();
        Closed();
        Open();
        Movement.Eyes.Release();
    }

    public void BlinkClosed()
    {        
        Closed();
        Open();
        Closed();
        Open();
        Closed();
        Movement.Eyes.Release();
    }

    public void Open()
    {
        Movement.Eyes.Open();
        Thread.Sleep(150);
        Movement.Eyes.On();
        Thread.Sleep(100);
    }

    public void Closed()
    {
        Movement.Eyes.Closed();
        Thread.Sleep(150);
        Movement.Eyes.Off();
        Movement.Eyes.Release();
    }
}
