

namespace ZombieBaby.Playlists
{
    public static class Test
    {
        public static void All()
        {
            
            Thread status1 = new Thread(() => Light.Status.SleepingStatus1());
            status1.Start();        
            Light.Blinders.OnOff();
        }
    }
}
