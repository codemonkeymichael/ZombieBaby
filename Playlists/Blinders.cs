namespace ZombieBaby.Playlists;

public static class Blinders
{
    public static void Flash()
    {
        Movement.Carrage.Rock(3);
        Light.Blinders.OnOff();
    }
}
