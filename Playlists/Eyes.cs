using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists;

public class Eyes
{
    Movement.Eyes me = new Movement.Eyes();
    Light.Eyes le = new Light.Eyes();

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
        me.Release();
    }

    public void BlinkOpen()
    {
        Open();
        Closed(); 
        Open();
        Closed();
        Open();
        me.Release();
    }

    public void Open()
    {
        me.Open();
        Thread.Sleep(150);
        le.On();
        Thread.Sleep(100);
    }

    public void Closed()
    {
        me.Closed();
        Thread.Sleep(150);
        le.Off();
        me.Release();
    }
}
