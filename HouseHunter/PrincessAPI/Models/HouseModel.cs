namespace PrincessAPI.Models
{
    public enum HouseType { Condo, Maison }

    public class HouseModel
    {
        public int id { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Hash { get; set; }

        public string MonthSold { get; set; }
        public string YearSold { get; set; }

        public string FinalAmount { get; set; }
        public string AskingAmount { get; set; }

        public HouseType HouseType { get; set; }
    }
}