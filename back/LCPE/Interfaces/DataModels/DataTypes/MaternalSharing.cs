namespace LCPE.Interfaces.DataModels.DataTypes
{
    public class MaternalSharing
    {
        public string Info { get; set; }

        public int Num {  get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return Date.ToString() + "," + Info + "," + Num;
        }
    }
}
