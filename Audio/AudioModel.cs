// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace ZombieBaby.Audio;

public class AudioRoot
{
    public AudioTracks audioTracks { get; set; }
}


public class AudioTracks
{
    public Sleeping sleeping { get; set; }
    public List<Dreaming> dreaming { get; set; }
    public List<Awake> awake { get; set; }
    public List<SittingUp> sittingUp { get; set; }
}

public class Awake
{
    public string path { get; set; }
    public int duration { get; set; }
}

public class Dreaming
{
    public string path { get; set; }
    public int duration { get; set; }
}



public class SittingUp
{
    public string path { get; set; }
    public int duration { get; set; }
}

public class Sleeping
{
    public List<SleepingIn> sleepingIn { get; set; }
    public List<SleepingOut> sleepingOut { get; set; }
}

public class SleepingIn
{
    public string path { get; set; }
    public int duration { get; set; }
}

public class SleepingOut
{
    public string path { get; set; }
    public int duration { get; set; }
}

