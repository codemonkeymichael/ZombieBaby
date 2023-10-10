using ZombieBaby.Animation;
using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Show1
{
	public static void SitUp()
	{
		Console.WriteLine("Playlists Show1 sit-up");
		AnimationPlayer.Play(AnimationPlayer.AnimationType.Screaming);
		//Thread.Sleep(30000); //Hold here
	}
}
