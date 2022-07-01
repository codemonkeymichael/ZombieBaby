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

    public static void Open()
    {
        Movement.Eyes.Open();
        Thread.Sleep(150);
        Light.Eyes.On();
        Thread.Sleep(100);        
    }

    public static void Closed()
    {
        Movement.Eyes.Closed();
        Light.Eyes.Off();
        Thread.Sleep(250);   
    }
}
