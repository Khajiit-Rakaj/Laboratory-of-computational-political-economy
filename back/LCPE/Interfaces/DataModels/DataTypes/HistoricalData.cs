namespace LCPE.Interfaces.DataModels.DataTypes
{
    public class HistoricalData
    {
        public string Info { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return Date.ToString() + "," + Info;
        }
    }
}
