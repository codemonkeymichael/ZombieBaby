using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists
{
    public class Carriage
    {

        private readonly Movement.Carriage movementCarriage = new Movement.Carriage();

        //This won't work with reflection that I'm using in the Animation Player json.
        //private readonly Movement.Carriage movementCarriage;

        //public Carriage(Movement.Carriage _mc)
        //{
        //    movementCarriage = _mc;
        //}

        public void Rock()
        {
            Console.WriteLine("Carriage Rock");
            int count = 3;

            for (var i = 0; i < count; i++)
            {
                movementCarriage.Down();
                Thread.Sleep(400);
                movementCarriage.Up();
                Thread.Sleep(400);
            }
            movementCarriage.Release();
        }
    }
}
