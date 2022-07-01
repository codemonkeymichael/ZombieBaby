using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists;

public static class Head
{
    public static void LookAround(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Movement.Head.Left(); 
            Thread.Sleep(1000);
            Movement.Head.Center();
            Thread.Sleep(1000);
            Movement.Head.Right();
            Thread.Sleep(1000);
            Movement.Head.Center();
            Thread.Sleep(1000);
            Movement.Head.Release();
            Thread.Sleep(1000);
        }
    }
}
