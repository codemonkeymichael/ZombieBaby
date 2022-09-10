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

            Thread cuePlayer = new Thread(() => AudioPlayer.CuePlayer(trk.cueList));
            cuePlayer.Start();

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
    }

    private static void CuePlayer(List<CueList> cues)
    {
        Console.WriteLine($"  CuePlayer Cue Count {cues.Count}");

        foreach (var cue in cues)
        {
            var time = cue.time;
            var type = cue.type;
            var method = cue.method;          
            Thread playCue = new Thread(() => PlayCue(time, type, method));
            playCue.Start();
        }
    }

    private static void PlayCue(int time, string type, string method)
    {
        Console.WriteLine($"    PlayCue {method} in {time}");
        try
        {
            Assembly executing = Assembly.GetExecutingAssembly();   
            Type movementType = executing.GetType(type);     
            object typeObject = Activator.CreateInstance(movementType);      
            MethodInfo getMethod = movementType.GetMethod(method);

            //parameters
            //String[] param = new String[2];
            //param[0] = "1";
            //param[1] = "Lisa";

            Thread.Sleep(time);   
            Console.WriteLine($"    Play {method}");
            getMethod.Invoke(typeObject, null);
      
        }
        catch (Exception ex)
        {
            Console.WriteLine("    PlayCue Error :   " + ex);
        }
    }
}