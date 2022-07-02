namespace ZombieBaby.Utilities;

public class DMXChannels
{
    public List<DMXChannel> Channels { get; set; }
    public int Duration { get; set; }
}

public class DMXChannel
{
    public int ChannelId { get; set; }
    public int ChannelValue { get; set; }
    public int TargetValue { get; set; }
    public int DMXStepsPerFrame { get; set; }
    public int FramesPerDMXStep { get; set; }
    public bool DMXStepUP { get; set; }

}
