namespace Bcut2Srt.Models;

internal class WorksInfos
{
    public WorksInfos()
    {
        worksInfos = new List<WorksInfo>();
    }

    public List<WorksInfo> worksInfos { get; set; }
}

public class WorksInfo
{
    public string draftId { get; set; }
    public int duration { get; set; }
    public string? filePath { get; set; }
    public string id { get; set; }
    public float imageRatio { get; set; }
    public long modifyTime { get; set; }
    public string name { get; set; }
    public int status { get; set; }
}