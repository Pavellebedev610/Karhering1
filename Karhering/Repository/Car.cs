using System.Data.Entity;
namespace Karhering.Repository

{
    public class Car
    {
        public int id_car { get; set; }
        public string marka_auto { get; set; }
        public string model_auto { get; set; }
        public string number { get; set; }
        public string god_vipuska { get; set; }
        public string probeg { get; set; }
        public string toplivo { get; set; }
        public double cordinat_x { get; set; }
        public double cordinat_y { get; set; }
        public required byte[] PhotoCar { get; set; }
            
    }
}
