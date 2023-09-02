namespace ZombieBaby.Animation;

public class AnimationTracks
{
    public Sleeping sleeping { get; set; }
    public  List<Dreaming> dreaming { get; set; }
    public  List<Awake> awake { get; set; }
    public  List<SittingUp> sittingUp { get; set; }
    public  List<Screaming> screaming { get; set; }
}

public class Awake
{
    public static string audioPath { get; set; }
    public static int duration { get; set; }
    public static int audioStartDelay { get; set; }
    public static int volume { get; set; }
    public static List<CueList> cueList { get; set; }
}

public class CueList
{
    public static int time { get; set; }
    public static string type { get; set; }
    public static string method { get; set; }
}

public class Dreaming
{
    public static string audioPath { get; set; }
    public static int duration { get; set; }
    public static int audioStartDelay { get; set; }
    public static int volume { get; set; }
    public static List<CueList> cueList { get; set; }
}

public class Root
{
    public static AnimationTracks animationTracks { get; set; }
}

public class Screaming
{
    public static string audioPath { get; set; }
    public static int duration { get; set; }
    public static int audioStartDelay { get; set; }
    public static int volume { get; set; }
    public static List<CueList> cueList { get; set; }
}

public class SittingUp
{
    public static string audioPath { get; set; }
    public static int duration { get; set; }
    public static int audioStartDelay { get; set; }
    public static int volume { get; set; }
    public List<CueList> cueList { get; set; }
}

public class Sleeping
{
    public static List<SleepingIn> sleepingIn { get; set; }
    public static List<SleepingOut> sleepingOut { get; set; }
}

public class SleepingIn
{
    public static string audioPath { get; set; }
    public static int duration { get; set; }
    public static int audioStartDelay { get; set; }
    public static int volume { get; set; }
    public static List<CueList> cueList { get; set; }
}

public class SleepingOut
{
    public static string audioPath { get; set; }
    public static int duration { get; set; }
    public static int audioStartDelay { get; set; }
    public static int volume { get; set; }
    public static List<CueList> cueList { get; set; }
}




public class TrackObject
{
    public static string audioPath { get; set; }
    public static int duration { get; set; }
    public static int audioStartDelay { get; set; }
    public static int volume { get; set; }
    public static List<CueList> cueList { get; set; }
}




