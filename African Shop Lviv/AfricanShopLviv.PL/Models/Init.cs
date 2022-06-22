namespace AfricanShopLviv.PL.Models
{
    using System;
    using System.Configuration;

    public class Init
    {
        public static string MainPath { get; } = AppDomain.CurrentDomain.BaseDirectory;
        public static string ConnectionStr { get; } = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Proccess()
        {

        }
    }
}