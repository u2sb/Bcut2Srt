using System.Text.Json;
using Bcut2Srt.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 1)
        {
            using var bs = new StreamReader(args[0]);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var bj = JsonSerializer.Deserialize<BcutProjectJson>(bs.ReadToEnd(), options);
            var bja = bj.tracks.SelectMany(a => a.clips)
                .Where(b => b._30021 > 0 && !string.IsNullOrWhiteSpace(b.AssetInfo.content))
                .Select(c => ToSrtSingle(c._30021, c._30012, c.AssetInfo.content)).ToArray();
            var bjs = ToSrt(bja);

            File.WriteAllText("./导出字幕.srt", bjs);


            Console.WriteLine("转换完成");
        }

        else
        {
            Console.WriteLine("请指定输入文件");
        }

        Console.ReadKey();
    }

    private static string ToSrtSingle(int start, int l, string content)
    {
        var startTimeStr = new DateTime(0).AddMilliseconds(start).ToString("HH:mm:ss,fff");
        var endTimeStr = new DateTime(0).AddMilliseconds(start).AddMilliseconds(l).ToString("HH:mm:ss,fff");

        return $"{startTimeStr}-->{endTimeStr}\r\n{content}\r\n";
    }

    private static string ToSrt(string[] srtSingle)
    {
        var a = "";
        for (var i = 0; i < srtSingle.Length; i++)
        {
            a += i + 1;
            a += "\r\n";
            a += srtSingle[i];
            a += "\r\n";
        }

        return a;
    }
}