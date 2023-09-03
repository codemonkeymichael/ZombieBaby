﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists
{
    public static class Carriage
    {

        //private readonly Movement.Carriage movementCarriage = new Movement.Carriage();

        //This won't work with reflection that I'm using in the Animation Player json.
        //private readonly Movement.Carriage movementCarriage;

        //public Carriage(Movement.Carriage _mc)
        //{
        //    movementCarriage = _mc;
        //}

        public static void Rock()
        {
            Console.WriteLine("Carriage Rock");
            int count = 3;

            for (var i = 0; i < count; i++)
            {
                Movement.Carriage.Down();
                Thread.Sleep(400);
                Movement.Carriage.Up();
                Thread.Sleep(400);
            }
            Movement.Carriage.Release();
        }
    }
}
