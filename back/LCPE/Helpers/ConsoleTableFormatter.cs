using System.Text;
using LCPE.Extensions;

namespace LCPE.Helpers;

public static class ConsoleTableFormatter
{
    public static ICollection<string> FormatTableToConsoleOutput(List<List<string>> data,
        int tabLength = 8)
    {
        if (!data.Any() || data.TrueForAll(x => !x.Any()))
        {
            return List.Create<string>();
        }

        var sizes = data[0].Select((x, i) => data.Select(s => s.Count > i ? s[i].Length : 0).Max()).ToList();
        var fixedSizes = sizes.Select(x => Math.Ceiling(x / (double)tabLength) * tabLength).ToArray();

        var result = data.Select(r =>
            string.Concat(r.Select((x, i) =>
                $"{x}{RepeatString("\t", (int)Math.Ceiling((fixedSizes[i] - x.Length) / tabLength))}"))).ToList();

        return result;
    }

    private static string RepeatString(string text, int times)
    {
        var stringBuilder = new StringBuilder(string.Empty);

        for (var i = 0; i < times; i++)
        {
            stringBuilder.Append(text);
        }

        return stringBuilder.ToString();
    }
}