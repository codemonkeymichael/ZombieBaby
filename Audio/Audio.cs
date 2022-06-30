using System.Timers;
using LibVLCSharp.Shared;

namespace ZombieBaby.Audio;
public static class Audio
{
    //private static System.Timers.Timer _timerLite;
    //private static int _currentLiteCue = 0;
    //private static System.Timers.Timer _timerCurtin;
    //private static int _currentCurtinCue = 0;
    //private static System.Timers.Timer _timerAnim;
    //private static int _currentAnimCue = 0;


    public static MediaPlayer player;
    public static bool songPlaying = false;


    /// <summary>
    /// Do this well before you need to play. This gives the player some time to buffer the audio.
    /// </summary>
    /// <param name="s"></param>
    public static void Cue()
    {
        var songPath = AppDomain.CurrentDomain.BaseDirectory + "Audio/Bass.wav";

        //VLC Player Init
        Core.Initialize();
        var libVLC = new LibVLC();
        var media = new Media(libVLC, songPath, FromType.FromPath);
        player = new MediaPlayer(media);
        //player.Playing += OnPlaybackStart;
        //player.EndReached += OnPlaybackFinished;
        //player.TimeChanged += Player_TimeChanged; //Keep cues in sync with the audio
        //Reset for a new song
        //_currentLiteCue = 0;
        //_currentCurtinCue = 0;

    }

    public static void Play()
    {
        player.Play();
        //To control the postion of the audio for song programming
        //player.LengthChanged += Player_LengthChanged;
    }

    //private static void Player_LengthChanged(object? sender, MediaPlayerLengthChangedEventArgs e)
    //{
    //    //Skip ahead in the song to this time so we can edit cues without having to listen to the whole song
    //    float startTime = 3000 + (2 * 60000); // ms min
    //    float len = player.Length;
    //    float pos = startTime / len;
    //    player.Position = pos; //The pos should be between 1.0 and 0.0
    //    Console.WriteLine("startTime = " + startTime + "  length of song = " + len + "   new song pos = " + pos);
    //}

    //private static void Player_TimeChanged(object? sender, MediaPlayerTimeChangedEventArgs e)
    //{
    //    //Console.WriteLine("Player Time " + e.Time);
    //    //This corrects the timer interval as the audio plays, it dosn't need this to play a cue.
    //    //Lights
    //    if (_currentLiteCue < _song.CuesLite.Count)
    //    {
    //        int cueTime = _song.CuesLite[_currentLiteCue].CueTime + (_song.CuesLite[_currentLiteCue].CueTimeMin * 60000);
    //        double newInterval = cueTime - e.Time; //Compair the cue time with where the song is

    //        //Fast Forward Light Cues
    //        if (newInterval < -10000) //10 seconds This should never be called unless we are fast forwarding a clip to edit cues
    //        {   //Find the cue by the audio time cause we past the current cue and set the _currentLiteCue
    //            Console.ForegroundColor = ConsoleColor.Red;
    //            Console.WriteLine("The Time for this cue has past. newInterval = " + newInterval + "  player time = " + e.Time + "  cueTime = " + cueTime + " Cue Name = " + _song.CuesLite[_currentLiteCue].CueName);
    //            foreach (var cue in _song.CuesLite)
    //            {
    //                int ct = cue.CueTime + (cue.CueTimeMin * 60000);
    //                if (ct > e.Time)
    //                {
    //                    Console.WriteLine("Got it! Cue Time=" + ct + " Audio Time " + e.Time);
    //                    _timerLite.Interval = ct - e.Time;
    //                    break;
    //                }
    //                else
    //                {
    //                    //Console.WriteLine("Nope Cue Time=" + ct + " Audio Time " + e.Time);
    //                    _currentLiteCue++;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            //This will correct the cue timing to match the audio
    //            if (newInterval > 2) _timerLite.Interval = newInterval;
    //        }
    //    }

    //    //Curtin
    //    if (_currentCurtinCue < _song.CuesCurtin.Count)
    //    {
    //        int cueTime = _song.CuesCurtin[_currentCurtinCue].CueTime + (_song.CuesCurtin[_currentCurtinCue].CueTimeMin * 60000);
    //        double newInterval = cueTime - e.Time; //Compair the cue time with where the song is             
    //        if (newInterval > 2) _timerCurtin.Interval = newInterval;
    //    }

    //    //Animation
    //    Console.WriteLine("_currentAnimCue " + _currentAnimCue);
    //    Console.WriteLine("_song.CuesAnim.Count " + _song.CuesAnim.Count);
    //    if (_currentAnimCue < _song.CuesAnim.Count)
    //    {
    //        int cueTime = _song.CuesAnim[_currentAnimCue].CueTime + (_song.CuesAnim[_currentAnimCue].CueTimeMin * 60000);
    //        double newInterval = cueTime - e.Time; //Compair the cue time with where the song is             
    //        if (newInterval > 2) _timerAnim.Interval = newInterval;
    //    }
    //}

    //public static void StopSong()
    //{
    //    player.Stop();
    //    _timerLite.Stop();
    //    _timerLite.Dispose();
    //    _timerCurtin.Stop();
    //    _timerCurtin.Dispose();
    //    _timerAnim.Stop();
    //    _timerAnim.Dispose();
    //}

    //private static void OnPlaybackStart(object? sender, EventArgs e)
    //{
    //    //Lights
    //    _timerLite = new System.Timers.Timer(_song.CuesLite[_currentLiteCue].CueTime);
    //    _timerLite.Elapsed += OnTimedLiteEvent;
    //    _timerLite.AutoReset = false;
    //    _timerLite.Enabled = true;
    //    _timerLite.Start();
    //    //Curtin 
    //    _timerCurtin = new System.Timers.Timer(_song.CuesCurtin[_currentCurtinCue].CueTime);
    //    _timerCurtin.Elapsed += OnTimedCurtinEvent;
    //    _timerCurtin.AutoReset = false;
    //    _timerCurtin.Enabled = true;
    //    _timerCurtin.Start();
    //    //Animation
    //    _timerAnim = new System.Timers.Timer(_song.CuesCurtin[_currentAnimCue].CueTime);
    //    _timerAnim.Elapsed += OnTimedAnimationeEvent;
    //    _timerAnim.AutoReset = false;
    //    _timerAnim.Enabled = true;
    //    _timerAnim.Start();

    //}
    //private static void OnTimedAnimationeEvent(Object source, ElapsedEventArgs e)
    //{
    //    int cueTime = _song.CuesAnim[_currentAnimCue].CueTime + (_song.CuesAnim[_currentAnimCue].CueTimeMin * 60000);
    //    Console.ForegroundColor = ConsoleColor.Green;
    //    Console.WriteLine("Cue " + (_currentAnimCue + 1) +
    //        " of " + _song.CuesAnim.Count +
    //        "  Time " + cueTime +
    //        "  CueName Animation-" + _song.CuesAnim[_currentAnimCue].CueName);

    //    int num = _currentAnimCue; //I don't know why I have to do this???? It works.
    //    Thread t = new Thread(() => _song.CuesAnim[num].CueAction.Invoke());
    //    t.Name = _song.CuesAnim[_currentAnimCue].CueName;
    //    t.Start();

    //    _currentAnimCue++;
    //    if (_currentAnimCue < _song.CuesAnim.Count)
    //    {
    //        //Set up the next cue
    //        int nextCueTime = _song.CuesAnim[_currentAnimCue].CueTime + (_song.CuesAnim[_currentAnimCue].CueTimeMin * 60000);
    //        int newInterval = nextCueTime - cueTime;
    //        _timerAnim.Interval = newInterval;
    //        //Console.WriteLine("Set up the next animation cue " + _song.CuesAnim[_currentAnimCue].CueName + ". newInterval=" + newInterval);
    //    }
    //}

    //private static void OnTimedLiteEvent(Object source, ElapsedEventArgs e)
    //{
    //    int cueTime = _song.CuesLite[_currentLiteCue].CueTime + (_song.CuesLite[_currentLiteCue].CueTimeMin * 60000);
    //    Console.ForegroundColor = ConsoleColor.Yellow;
    //    Console.WriteLine("Cue " + (_currentLiteCue + 1) +
    //        " of " + _song.CuesLite.Count +
    //        "  Time " + cueTime +
    //        "  CueName Light-" + _song.CuesLite[_currentLiteCue].CueName);

    //    int num = _currentLiteCue; //I don't know why I have to do this???? It works.
    //    Thread t = new Thread(() => _song.CuesLite[num].CueAction.Invoke());
    //    t.Name = _song.CuesLite[_currentLiteCue].CueName;
    //    t.Start();

    //    _currentLiteCue++;
    //    if (_currentLiteCue < _song.CuesLite.Count)
    //    {
    //        //Set up the next cue
    //        int nextCueTime = _song.CuesLite[_currentLiteCue].CueTime + (_song.CuesLite[_currentLiteCue].CueTimeMin * 60000);
    //        int newInterval = nextCueTime - cueTime;
    //        _timerLite.Interval = newInterval;
    //        //Console.WriteLine("Set up the next cue " + _song.CuesLite[_currentLiteCue].CueName + ". newInterval=" + newInterval);
    //    }
    //}

    //private static void OnTimedCurtinEvent(Object source, ElapsedEventArgs e)
    //{
    //    int cueTime = _song.CuesCurtin[_currentCurtinCue].CueTime + (_song.CuesCurtin[_currentCurtinCue].CueTimeMin * 60000);
    //    Console.ForegroundColor = ConsoleColor.Cyan;
    //    Console.WriteLine("Cue " + _currentCurtinCue +
    //        " of " + _song.CuesCurtin.Count +
    //        "  Time " + cueTime +
    //        "  CueName Curtin-" + _song.CuesCurtin[_currentCurtinCue].CueName);

    //    int num = _currentCurtinCue; //I don't know why I have to do this???? It works.
    //    Thread t = new Thread(() => _song.CuesCurtin[num].CueAction.Invoke());
    //    t.Name = _song.CuesCurtin[_currentCurtinCue].CueName;
    //    t.Start();

    //    _currentCurtinCue++;
    //    if (_currentCurtinCue < _song.CuesCurtin.Count)
    //    {
    //        int nextCueTime = _song.CuesCurtin[_currentCurtinCue].CueTime + (_song.CuesCurtin[_currentCurtinCue].CueTimeMin * 60000);
    //        int newInterval = nextCueTime - cueTime;
    //        _timerLite.Interval = newInterval;
    //    }
    //}

    //private static void OnPlaybackFinished(object? sender, EventArgs e)
    //{
    //    Console.WriteLine("Playback Finished");
    //    _timerLite.Stop();
    //    _timerLite.Dispose();
    //    _timerCurtin.Stop();
    //    _timerCurtin.Dispose();
    //    _timerAnim.Stop();
    //    _timerAnim.Dispose();
    //}
}

