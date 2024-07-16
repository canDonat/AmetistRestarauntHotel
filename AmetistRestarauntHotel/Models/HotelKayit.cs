namespace AmetistRestarauntHotel.Models
{
    public class HotelKayit
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TelNo { get; set; } 
        public string Email { get; set; }
        public int Kisisayisi { get; set; }
        public string Cinsiyet { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime GirisSaati { get; set; }
    }
}
