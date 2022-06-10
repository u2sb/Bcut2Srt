using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Bcut2Srt.Models;
using SubtitleManager;

namespace Bcut2Srt;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            var path = args[0];
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            // json格式 工程草稿文件 用于导出字幕
            if (path.ToLower().EndsWith(".json"))
            {
                using var bs = new StreamReader(path);

                var bj = JsonSerializer.Deserialize<BcutProjectJson>(bs.ReadToEnd(), jsonSerializerOptions);

                var number = 0;

                var bja = bj?.tracks.SelectMany(a => a.clips)
                    .Where(b => b._30021 > 0 && !string.IsNullOrWhiteSpace(b.AssetInfo.content))
                    .Select(c => new SrtSubtitleLine
                    {
                        LineNumber = ++number,
                        Start = new TimeSpan(0, 0, 0, 0, c._30021),
                        End = new TimeSpan(0, 0, 0, 0, c._30012 + c._30021),
                        Text = c.AssetInfo.content
                    } as ISubtitleLine).ToList();
                var bjs = new SrtSubtitle
                {
                    Lines = bja
                };

                File.WriteAllText("./导出字幕.srt", bjs.ToString());


                Console.WriteLine("转换完成");
            }

            // 视频格式，用于添加工程
            else if (path.ToLower().EndsWith(".mp4") ||
                     path.ToLower().EndsWith(".mov") ||
                     path.ToLower().EndsWith(".flv"))
            {
                var worksInfoPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Bcut Drafts", "worksInfo.json");


                using (var fs = File.Open(worksInfoPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    var sr = new StreamReader(fs);
                    var json = sr.ReadToEnd();
                    var wis = !string.IsNullOrWhiteSpace(json)
                        ? JsonSerializer.Deserialize<WorksInfos>(json, jsonSerializerOptions)
                        : new WorksInfos();
                    var wi = new WorksInfo
                    {
                        draftId = Guid.NewGuid().ToString().ToUpper(),
                        duration = 1,
                        filePath = path,
                        id = Guid.NewGuid().ToString().ToUpper(),
                        imageRatio = 1,
                        modifyTime = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds,
                        name = Path.GetFileNameWithoutExtension(path),
                        status = 0
                    };

                    wis ??= new WorksInfos();
                    wis.worksInfos.Add(wi);

                    var w = JsonSerializer.SerializeToUtf8Bytes(wis, jsonSerializerOptions);

                    fs.Seek(0, SeekOrigin.Begin);
                    fs.SetLength(0);

                    fs.Write(w, 0, w.Length);

                    fs.Close();

                    Console.WriteLine("写入完成，请重新打开必剪");
                }
            }

            Console.WriteLine("按任意键退出");
        }

        else
        {
            Console.WriteLine("请指定输入文件");
        }


        Console.ReadKey();
    }
}