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
            Thread.Sleep(250);
            Open();
            Thread.Sleep(250);    
        }
    }

    public static void Open()
    {
        Movement.Eyes.Open();
        Light.Eyes.On();
    }

    public static void Closed()
    {
        Movement.Eyes.Closed();
        Light.Eyes.Off();
    }
}
