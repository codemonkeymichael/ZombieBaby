using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists;

public class Head
{
    Movement.Head mh = new Movement.Head();

    public void LookAround(int count)
    {
        for (int i = 0; i < count; i++)
        {
            mh.Left();
            Thread.Sleep(1000);
            mh.Center();
            Thread.Sleep(1000);
            mh.Right();
            Thread.Sleep(1000);
            mh.Center();
            Thread.Sleep(1000);
            mh.Release();
            Thread.Sleep(1000);
        }
    }

    public void Awake1(int count)
    {
        for (int i = 0; i < count; i++)
        {
            mh.Left();
            Thread.Sleep(1000);
            mh.Center();
            Thread.Sleep(1000);
            mh.Right();
            Thread.Sleep(50);
            mh.Center();
            Thread.Sleep(1000);
            mh.Release();
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// This won't work with the json until you and a parameter to pass in
    /// </summary>
    /// <param name="duration"></param>
    public void NightmareLoop(int duration)
    {
        var sw = new Stopwatch();
        sw.Start();
        while (true)
        {
            mh.Right();
            Thread.Sleep(600);
            if (sw.ElapsedMilliseconds > duration) break;
            mh.LeftHalf();
            Thread.Sleep(250);
            if (sw.ElapsedMilliseconds > duration) break;
            mh.Center();
            Thread.Sleep(400);
            if (sw.ElapsedMilliseconds > duration) break;
            mh.Right();
            Thread.Sleep(800);
            if (sw.ElapsedMilliseconds > duration) break;
            mh.Left();
            Thread.Sleep(1400);
            mh.Right();
        }
        sw.Stop();
    }

    public void Nightmare()
    {
        mh.Right();
        Thread.Sleep(600);
        mh.LeftHalf();
        Thread.Sleep(250);
        mh.Center();
        Thread.Sleep(400);
        mh.Right();
        Thread.Sleep(800);
        mh.Left();
        Thread.Sleep(1400);
        mh.Right();
    }
}
