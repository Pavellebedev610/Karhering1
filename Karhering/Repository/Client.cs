namespace Karhering.Repository

{
    public class Client
    {
        public static object ClientInfo { get; internal set; }
        public object id_polz { get; internal set; }
        public string FIO { get; set;  }
        public string mail { get; set; }
        public string password { get; set; }
        public string number_prav { get; set; }
        public string telefon { get; set; }

        public string rating { get; set; }

        public string bonus { get; set; }
    }
}
