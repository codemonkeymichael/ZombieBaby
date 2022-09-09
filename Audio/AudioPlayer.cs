﻿using System.Reflection;
using System.Text.Json;
using System.Timers;
using AutoMapper;
using Iot.Device.Media;
using LibVLCSharp.Shared;
using ZombieBaby.Utilities;

namespace ZombieBaby.Audio;

public static class AudioPlayer
{

    public enum AudioType
    {
        SleepingIn,
        SleepingOut,
        Dreaming,
        Awake,
        SittingUp,
        Screaming
    }

    private static int SleepingInCurrent { get; set; } = 0;
    private static int SleepingOutCurrent { get; set; } = 0;
    private static int DreamingCurrent { get; set; } = 0;
    private static int AwakeCurrent { get; set; } = 0;
    private static int SittingUpCurrent { get; set; } = 0;
    private static int ScreamingCurrent { get; set; } = 0;
    private static Root? Tracks { get; set; }


    private static LibVLC _vlc = new LibVLC();
    //private static Media _media;
    //private static MediaPlayer _mediaPlayer;

    //public static int CurrentTrackDuration { get; set; }



    public static async void InitAudio()
    {
        //Read the json tracks
        string fileName = $"{AppDomain.CurrentDomain.BaseDirectory}/Audio/audioTracks.json";
        using FileStream openStream = File.OpenRead(fileName);           
        Tracks = await JsonSerializer.DeserializeAsync<Root>(openStream);
      
        //Console.WriteLine("InitAudio Test Read 1 " + Tracks.audioTracks.sleeping.sleepingIn.Count);
        //Console.WriteLine("InitAudio Test Read 2 " + Tracks.audioTracks.dreaming.Count);
        //Console.WriteLine("InitAudio Test Read 3 " + Tracks.audioTracks.dreaming[0].path);
        Console.WriteLine("InitAudio Test Read 4 " + Tracks.audioTracks.dreaming[0].cueList.Count);

    }

    /// <summary>
    /// Do this well before you need to play. This gives the player some time to buffer the audio.
    /// </summary>
    public static void Play(AudioType at)
    {
        TrackObject trk = new TrackObject();

        switch (at.ToString())
        {
            case "SleepingIn":
                SleepingInCurrent++;
                if (SleepingInCurrent >= Tracks.audioTracks.sleeping.sleepingIn.Count) SleepingInCurrent = 0;             
                trk = ModelMapper.Map<TrackObject>(Tracks.audioTracks.sleeping.sleepingIn[SleepingInCurrent]);

                break;
            case "SleepingOut":
                SleepingOutCurrent++;
                if (SleepingOutCurrent >= Tracks.audioTracks.sleeping.sleepingOut.Count) SleepingOutCurrent = 0;
                trk = ModelMapper.Map<TrackObject>(Tracks.audioTracks.sleeping.sleepingOut[SleepingOutCurrent]);
                break;
            case "Dreaming":
                DreamingCurrent++;
                if (DreamingCurrent >= Tracks.audioTracks.dreaming.Count) DreamingCurrent = 0;             
                trk = ModelMapper.Map<TrackObject>(Tracks.audioTracks.dreaming[DreamingCurrent]);   
                break;
            case "Awake":
                AwakeCurrent++;
                if (AwakeCurrent >= Tracks.audioTracks.awake.Count) AwakeCurrent = 0;            
                trk = ModelMapper.Map<TrackObject>(Tracks.audioTracks.awake[AwakeCurrent]);            
                break;
            case "SittingUp":
                SittingUpCurrent++;
                if (SittingUpCurrent >= Tracks.audioTracks.sittingUp.Count) SittingUpCurrent = 0;            
                trk = ModelMapper.Map<TrackObject>(Tracks.audioTracks.sittingUp[SittingUpCurrent]);               
                break;
            case "Screaming":
                ScreamingCurrent++;
                if (ScreamingCurrent >= Tracks.audioTracks.screaming.Count) ScreamingCurrent = 0;           
                trk = ModelMapper.Map<TrackObject>(Tracks.audioTracks.screaming[ScreamingCurrent]);                
                break;
        }


        Console.WriteLine("Audio Play " + trk.path);

        //VLC Player Init
        try
        {
            Media _media = new Media(_vlc, trk.path, FromType.FromPath);
            MediaPlayer _mediaPlayer = new MediaPlayer(_media);
            _mediaPlayer.Volume = trk.volume;

            //Why doesn't this work? The Media Player doesn't get disposed cause the event is on another thread ... I think
            //_mediaPlayer.EndReached += new EventHandler<EventArgs>((sender, e) => OnPlaybackFinished(sender, e, _media, _mediaPlayer));

            CuePlayer(trk.cueList);

            Thread.Sleep(trk.audioStartDelay);
            _mediaPlayer.Play();
            Thread.Sleep(trk.duration);

            _media.Dispose();
            _mediaPlayer.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine("AudioPlayer   -- " + ex);
        }

 

    }



    private static void OnPlaybackFinished(object? sender, EventArgs e, Media _media, MediaPlayer _mediaPlayer)
    {
        Console.WriteLine("OnPlaybackFinished");
        _mediaPlayer.Dispose();
        _media.Dispose();
        //_mediaPlayer.Stop();
        //_mediaPlayer.Media.ParseStop();
        ////_media.ParseStop();
        ////_media = null;
        ////_media = null;
        //_media.Dispose();
        ////_mediaPlayer = null;
        //_mediaPlayer.Dispose();
        //GC.Collect();
        //Console.WriteLine(_mediaPlayer.Media.Duration);
    }

    private static void CuePlayer(List<CueList> cues) {
        Console.WriteLine($"CuePlayer type={cues[0].type} method={cues[0].method}");

        try
        {
            Assembly executing = Assembly.GetExecutingAssembly();
            Console.WriteLine("   Assembly.GetExecutingAssembly()");
            Type movementType = executing.GetType(cues[0].type);
            Console.WriteLine("   executing.GetType(cues[0].type)" + cues[0].type);
            object typeObject = Activator.CreateInstance(movementType);
            Console.WriteLine("   Activator.CreateInstance");
            //Store the info of the method in an object of class MethodInfo
            MethodInfo getMethod = movementType.GetMethod(cues[0].method);
            Console.WriteLine("   movementType.GetMethod");

            // To store the parameters required
            // by Method GetDetails
            //String[] param = new String[2];
            //param[0] = "1";
            //param[1] = "Lisa";

            // To display the result of the method
            getMethod.Invoke(typeObject, null);
            Console.WriteLine("   getMethod.Invoke");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cue Player Error  : " + ex);
        }



            //Type thisType = AudioPlayer.GetType();
            //MethodInfo theMethod =  GetMethod(TheCommandString);
            //theMethod.Invoke(this, userParameters);


            //var type = Type.GetType("ZombieBaby.Movement.Body");
            //// Lookup the method
            //var myMethod = type.GetMethod("UpSlow");
            //myMethod.Invoke(null, null);

            //_timer = new System.Timers.Timer(Track.audioTracks.);
            //_timerLite.Elapsed += OnTimedLiteEvent;
            //_timerLite.AutoReset = false;
            //_timerLite.Enabled = true;
            //_timerLite.Start();
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

