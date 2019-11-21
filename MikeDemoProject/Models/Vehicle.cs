namespace MikeDemoProject.Models
{
    /// <summary>
    /// Base Model for all vehicle classes
    /// </summary>
    public class Vehicle
    {
        public int vehicleId { get; set; }
        public string colour { get; set; }
        public int horsePower { get; set; }
        public double price { get; set; }
        public string name { get; set; }
        public int year { get; set; }
    }
}
