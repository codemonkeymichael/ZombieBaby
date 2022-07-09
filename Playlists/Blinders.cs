namespace ZombieBaby.Playlists;

public static class Blinders
{
    public static void Flash()
    {
        Movement.Carriage.Rock(3);
        Light.Blinders.OnOff();
    }
}
