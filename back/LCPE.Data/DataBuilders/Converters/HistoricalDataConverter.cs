using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using LCPE.Interfaces.DataModels.DataTypes;

namespace LCPE.Data.DataBuilders.Converters
{
    internal class HistoricalDataConverter : TypeConverter<HistoricalData[]>
    {
        public override HistoricalData[] ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new HistoricalData[0];
            }

            var datas = text.Split(';');
            HistoricalData[] res = new HistoricalData[datas.Length];

            for (int i = 0; i < datas.Length; i++)
            {
                var split = datas[i].Split(',');
                res[i] = new HistoricalData()
                {
                    Date = DateTime.ParseExact(split[0], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Info = split[1]
                };
            }

            return res;
        }

        public override string ConvertToString(HistoricalData[] value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string res = "";
            foreach ( HistoricalData data in value)
            {
                res += data.Date.ToString() + "," + data.Info + ";";
            }

            return res;
        }
    }
}
