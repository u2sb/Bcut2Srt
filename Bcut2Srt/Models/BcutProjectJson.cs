using System.Text.Json.Serialization;

namespace Bcut2Srt.Models;

public class BcutProjectJson
{
    public bool MainTrackMute { get; set; }
    public object audio_record { get; set; }
    public string draftCreatedVersion { get; set; }
    public int height { get; set; }
    public bool magnetSwitchStatus { get; set; }
    public Mainwindow mainWindow { get; set; }
    public string modifyTime { get; set; }
    public float needlePos { get; set; }
    public float ratio { get; set; }
    public object rotate { get; set; }
    public Ruler ruler { get; set; }
    public object screen_record { get; set; }
    public int trackCount { get; set; }
    public Track[] tracks { get; set; }
}

public class Mainwindow
{
    public Browserpanelfile[] browserPanelFiles { get; set; }
}

public class Browserpanelfile
{
    public string duration { get; set; }
    public int frameRateDen { get; set; }
    public int frameRateNum { get; set; }
    public int height { get; set; }
    public int itemType { get; set; }
    public string srcPath { get; set; }
    public int width { get; set; }
}

public class Ruler
{
    public object[] MarkPointInfo { get; set; }
}

public class Track
{
    public int BTrackLastSplitPos { get; set; }
    public int BTrackType { get; set; }
    public Clip[] clips { get; set; }
    public bool mute { get; set; }
    public object speed { get; set; }
    public object split { get; set; }
    public int trackIndex { get; set; }
    public bool MiddleTrack { get; set; }
}

public class Clip
{
    [JsonPropertyName("30012")]
    public int _30012 { get; set; }

    [JsonPropertyName("30021")]
    public int _30021 { get; set; }
    public Assetinfo AssetInfo { get; set; }
    public Bspeedinfo BSpeedInfo { get; set; }
    public bool FreezeImage { get; set; }
    public bool IsDBVolume { get; set; }
    public object[] MarkPointInfo { get; set; }
    public Cutinfo cutInfo { get; set; }
    public int duration { get; set; }
    public string fileNamePath { get; set; }
    public int inPoint { get; set; }
    public object[] keyFrameArray { get; set; }
    public long m_id { get; set; }
    public Maskinfo maskInfo { get; set; }
    public int outPoint { get; set; }
    public int trimIn { get; set; }
    public int trimOut { get; set; }
}

public class _10033
{
    public int B { get; set; }
    public int L { get; set; }
    public int R { get; set; }
    public int T { get; set; }
}

public class Assetinfo
{
    public int assetItemType { get; set; }
    public int audioType { get; set; }
    public string content { get; set; }
    public string coverPath { get; set; }
    public string displayName { get; set; }
    public int duration { get; set; }
    public int frameRateDen { get; set; }
    public int frameRateNum { get; set; }
    public int height { get; set; }
    public string itemName { get; set; }
    public int originClipType { get; set; }
    public int originDuration { get; set; }
    public string originSrcFile { get; set; }
    public string realMaterialId { get; set; }
    public string srcPath { get; set; }
    public int type { get; set; }
    public int videoType { get; set; }
    public int width { get; set; }
}

public class Bspeedinfo
{
    public int BSpeedType { get; set; }
    public object[] pointListX { get; set; }
    public object[] pointListY { get; set; }
    public string speedCurveTypeName { get; set; }
    public int speedRate { get; set; }
}

public class Cutinfo
{
    public int bottom { get; set; }
    public int left { get; set; }
    public int right { get; set; }
    public int top { get; set; }
}

public class Maskinfo
{
    public int maskCenterX { get; set; }
    public float maskCenterY { get; set; }
    public int maskFeather { get; set; }
    public int maskReverse { get; set; }
    public int maskRotation { get; set; }
    public int maskRoundAngle { get; set; }
    public float maskScaleX { get; set; }
    public int maskScaleY { get; set; }
    public int maskType { get; set; }
}