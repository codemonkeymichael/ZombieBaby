﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists;

public static class Head
{
    public static void LookAround(int count)
    {
        for (int i = 0; i < count; i++)
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

    public static void Awake1(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Movement.Head.Left();
            Thread.Sleep(1000);
            Movement.Head.Center();
            Thread.Sleep(1000);
            Movement.Head.Right();
            Thread.Sleep(50);
            Movement.Head.Center();
            Thread.Sleep(1000);
            Movement.Head.Release();
            Thread.Sleep(1000);
        }
    }

    public static void NightmareLoop(int duration)
    {
        var sw = new Stopwatch();
        sw.Start();
        while (true)
        {
            Movement.Head.Right();
            Thread.Sleep(600);
            if (sw.ElapsedMilliseconds > duration) break;
            Movement.Head.LeftHalf();
            Thread.Sleep(250);
            if (sw.ElapsedMilliseconds > duration) break;
            Movement.Head.Center();
            Thread.Sleep(400);
            if (sw.ElapsedMilliseconds > duration) break;
            Movement.Head.Right();
            Thread.Sleep(800);
            if (sw.ElapsedMilliseconds > duration) break;
            Movement.Head.Left();
            Thread.Sleep(1400);
            Movement.Head.Right();
        }
        sw.Stop();
    }

    public static void Nightmare()
    {
        Movement.Head.Right();
        Thread.Sleep(600);
        Movement.Head.LeftHalf();
        Thread.Sleep(250);
        Movement.Head.Center();
        Thread.Sleep(400);
        Movement.Head.Right();
        Thread.Sleep(800);
        Movement.Head.Left();
        Thread.Sleep(1400);
        Movement.Head.Right();
    }
}
