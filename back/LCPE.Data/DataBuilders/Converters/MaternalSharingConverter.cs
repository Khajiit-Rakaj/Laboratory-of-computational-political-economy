using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using LCPE.Interfaces.DataModels.DataTypes;
using System.Linq;

namespace LCPE.Data.DataBuilders.Converters
{
    internal class MaternalSharingConverter : TypeConverter<MaternalSharing[]>
    {
        public override MaternalSharing[] ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new MaternalSharing[0];
            }

            var datas = text.Split(';');
            MaternalSharing[] res = new MaternalSharing[datas.Length];
            for ( int i = 0; i < datas.Length; i++)
            {
                var split = datas[i].Split(',');
                res[i] = new MaternalSharing()
                {
                    Date = DateTime.ParseExact(split[0], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Info = split[1],
                    Num = int.Parse(split[2])
                };
            }

            return res;
        }

        public override string ConvertToString(MaternalSharing[] value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string res = "";
            foreach ( MaternalSharing val in value )
            {
                res += val.Date.ToString() + "," + val.Info + "," + val.Num;
            }

            return res;
        }
    }
}
