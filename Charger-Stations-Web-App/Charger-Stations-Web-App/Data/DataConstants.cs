namespace Charger_Stations_Web_App.Data
{
    public class DataConstants
    {
        public class Charger
        {
            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 30;
            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 5000;
            public const double PriceMinValue = 0;
            public const double PriceMaxValue = 500;
        }

        public class Category
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 30;
        }

        public class Dealer
        {
            public const int NameMaxLength = 30;
            public const int PhoneNumberMaxLength = 20;
        }
        
        public class Users
        {
            public const int emailLoginMinLength = 6;
            public const int emailLoginMaxLength = 346;
            public const int passwordLoginMinLength = 5;
            public const int passwordLoginMaxLength = 20;
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 30;
        }
    }
}
