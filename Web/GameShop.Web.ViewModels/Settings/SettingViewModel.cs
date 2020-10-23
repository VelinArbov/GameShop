namespace GameShop.Web.ViewModels.Settings
{
    using GameShop.Data.Models;
    using GameShop.Services.Mapping;

    using AutoMapper;

    public class SettingViewModel 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string NameAndValue { get; set; }

   
    }
}
