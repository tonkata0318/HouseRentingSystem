
namespace HouseRentingSystem.Data
{
    public static class DataConstraints
    {
        public const int categoryNameMaxLength = 50;

        public const int HouseTitleMax=50;
        public const int HouseTitleMin = 10;

        public const int HouseAddressMaxLength = 150;
        public const int HouseAddressMinLength = 30;

        public const int HouseDescriptionMaxLength = 500;
        public const int HouseDescriptionMinLength = 50;

        public const int housePricePerMonthMax = 2000;
        public const int housePricePerMonthMin = 0;

        public const int phonenumberMaxLength = 15;
        public const int phoneNumberMinLength = 7;
    }
}
