using AmetistRestarauntHotel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AmetistRestarauntHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string connectionString = "Data Source=.;Initial Catalog=AmetisitRestaurantHotel;Integrated Security=True;";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hotel()
        {
            List<HotelKayit> hotelKayitList = new List<HotelKayit>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP (1000) [Id], [Ad], [Soyad], [TelNo], [Email], [Kisisayisi], [Cinsiyet], [Tarih], [GirisSaati] FROM [AmetisitRestaurantHotel].[dbo].[HotelKayit]";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        HotelKayit hotelKayit = new HotelKayit
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Ad = reader["Ad"].ToString(),
                            Soyad = reader["Soyad"].ToString(),
                            TelNo = reader["TelNo"].ToString(),
                            Email = reader["Email"].ToString(),
                            Kisisayisi = Convert.ToInt32(reader["Kisisayisi"]),
                            Cinsiyet = reader["Cinsiyet"].ToString(),
                            Tarih = Convert.ToDateTime(reader["Tarih"]),
                            GirisSaati = Convert.ToDateTime(reader["GirisSaati"]) 
                        };

                        hotelKayitList.Add(hotelKayit);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                }
            }

            return View(hotelKayitList);
        }

        [HttpPost]
        public IActionResult Kayit(HotelKayit model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO [AmetisitRestaurantHotel].[dbo].[HotelKayit] ([Ad], [Soyad], [TelNo], [Email], [Kisisayisi], [Cinsiyet], [Tarih], [GirisSaati]) VALUES (@Ad, @Soyad, @TelNo, @Email, @Kisisayisi, @Cinsiyet, @Tarih, @GirisSaati)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Ad", model.Ad);
                    command.Parameters.AddWithValue("@Soyad", model.Soyad);
                    command.Parameters.AddWithValue("@TelNo", model.TelNo);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Kisisayisi", model.Kisisayisi);
                    command.Parameters.AddWithValue("@Cinsiyet", model.Cinsiyet);
                    command.Parameters.AddWithValue("@Tarih", model.Tarih);
                    command.Parameters.AddWithValue("@GirisSaati", model.GirisSaati);  

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return RedirectToAction("Hotel");
            }

            return View("Hotel");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
