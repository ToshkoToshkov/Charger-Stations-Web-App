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
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }
        
        public class User
        {
            public const int FullNameMaxLength = 40;
            public const int FullNameMinLength = 5;

            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }
    }
}
