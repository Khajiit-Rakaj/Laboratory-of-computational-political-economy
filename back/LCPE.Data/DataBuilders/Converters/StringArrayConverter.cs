using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace LCPE.Data.DataBuilders.Converters
{
    internal class StringArrayConverter : TypeConverter<string[]>
    {
        public override string[] ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new string[0];
            }

            return text.Split(new char[] { ' ' });
        }

        public override string ConvertToString(string[] value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null || value.Length == 0)
            {
                return string.Empty;
            }

            return string.Join(' ', value);
        }
    }
}
