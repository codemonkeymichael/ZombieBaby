using System.Reflection;
using System.Text.Json;
using System.Timers;
using AutoMapper;
using Iot.Device.Media;
using LibVLCSharp.Shared;
using ZombieBaby.Utilities;

namespace ZombieBaby.Animation;

public static class AnimationPlayer
{

	public enum AnimationType
	{
		SleepingIn,
		SleepingOut,
		Dreaming,
		Awake,
		SittingUp,
		Screaming,
		GunShot

	}

	private static int SleepingInCurrent { get; set; } = 0;
	private static int SleepingOutCurrent { get; set; } = 0;
	private static int DreamingCurrent { get; set; } = 0;
	private static int AwakeCurrent { get; set; } = 0;
	private static int SittingUpCurrent { get; set; } = 0;
	private static int ScreamingCurrent { get; set; } = 0;
	private static int GunShotCurrent { get; set; } = 0;
	private static Root? Tracks { get; set; }


	private static LibVLC _vlc = new LibVLC();


	//This should go in its own class
	public static async void InitAnimation()
	{
		//Read the json tracks
		string fileName = $"{AppDomain.CurrentDomain.BaseDirectory}/Animation/animationTracks.json";
		using FileStream openStream = File.OpenRead(fileName);
		Tracks = await JsonSerializer.DeserializeAsync<Root>(openStream);
		Console.WriteLine("InitAnimation Test Read " + Tracks.showTracks.gunShots.Count);

	}

	public static void Play(AnimationType at)
	{
		TrackObject trk = new TrackObject();

		switch (at.ToString())
		{
			case "SleepingIn":
				SleepingInCurrent++;
				Console.WriteLine($"SleepingIn {SleepingInCurrent}");
				if (SleepingInCurrent >= Tracks.animationTracks.sleeping.sleepingIn.Count) SleepingInCurrent = 0;
				trk = ModelMapper.Map<TrackObject>(Tracks.animationTracks.sleeping.sleepingIn[SleepingInCurrent]);
				break;
			case "SleepingOut":
				SleepingOutCurrent++;
				Console.WriteLine($"SleepingOut {SleepingOutCurrent}");
				if (SleepingOutCurrent >= Tracks.animationTracks.sleeping.sleepingOut.Count) SleepingOutCurrent = 0;
				trk = ModelMapper.Map<TrackObject>(Tracks.animationTracks.sleeping.sleepingOut[SleepingOutCurrent]);
				break;
			case "Dreaming":
				DreamingCurrent++;
				Console.WriteLine($"DreamingCurrent {DreamingCurrent}");
				if (DreamingCurrent >= Tracks.animationTracks.dreaming.Count) DreamingCurrent = 0;
				trk = ModelMapper.Map<TrackObject>(Tracks.animationTracks.dreaming[DreamingCurrent]);
				break;
			case "Awake":
				AwakeCurrent++;
				Console.WriteLine($"AwakeCurrent {AwakeCurrent}");
				if (AwakeCurrent >= Tracks.animationTracks.awake.Count) AwakeCurrent = 0;
				trk = ModelMapper.Map<TrackObject>(Tracks.animationTracks.awake[AwakeCurrent]);
				break;
			case "SittingUp":
				SittingUpCurrent++;
				Console.WriteLine($"SittingUp {SittingUpCurrent}");
				if (SittingUpCurrent >= Tracks.animationTracks.sittingUp.Count) SittingUpCurrent = 0;
				trk = ModelMapper.Map<TrackObject>(Tracks.animationTracks.sittingUp[SittingUpCurrent]);
				break;
			case "Screaming":
				ScreamingCurrent++;
				Console.WriteLine($"Screaming {ScreamingCurrent}");
				if (ScreamingCurrent >= Tracks.animationTracks.screaming.Count) ScreamingCurrent = 0;
				trk = ModelMapper.Map<TrackObject>(Tracks.animationTracks.screaming[ScreamingCurrent]);
				break;
			case "GunShot":
				GunShotCurrent++;
				Console.WriteLine($"GunShot {GunShotCurrent}");
				trk = ModelMapper.Map<TrackObject>(Tracks.showTracks.gunShots[GunShotCurrent]);
				break;
		}


		trk.audioPath = AppDomain.CurrentDomain.BaseDirectory + trk.audioPath;
		Console.WriteLine("trk.audioPath " + trk.audioPath);
		//Console.WriteLine("trk.audioPath " + trk.duration);
		//Console.WriteLine("trk.audioStartDelay " + trk.audioStartDelay);
		//Console.WriteLine("trk.volume " + trk.volume);

		//VLC Player Init
		try
		{
			Media _media = new Media(_vlc, trk.audioPath, FromType.FromPath);
			MediaPlayer _mediaPlayer = new MediaPlayer(_media);
			_mediaPlayer.Volume = trk.volume;

			//Why doesn't this work? The Media Player doesn't get disposed cause the event is on another thread ... I think
			//_mediaPlayer.EndReached += new EventHandler<EventArgs>((sender, e) => OnPlaybackFinished(sender, e, _media, _mediaPlayer));

			Thread cuePlayer = new Thread(() => AnimationPlayer.CuePlayer(trk.cueList));
			cuePlayer.Start();

			Thread.Sleep(trk.audioStartDelay);
			_mediaPlayer.Play();
			Thread.Sleep(trk.duration);

			_media.Dispose();
			_mediaPlayer.Dispose();
		}
		catch (Exception ex)
		{
			Console.WriteLine("AnimationPlayer -- " + ex);
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
		//Console.WriteLine($"  CuePlayer Cue Count {cues.Count}");

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
		Console.WriteLine($"    PlayCue Type {type}   Method {method}   Time {time}");
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
			Console.WriteLine("   PlayCue Error :   " + ex);
		}
	}
}