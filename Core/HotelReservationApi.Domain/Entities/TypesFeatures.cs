using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class TypesFeatures : BaseEntity
    {
        public TypesFeatures()
        {
        }

        public TypesFeatures(string featureName)
        {
            FeatureName = featureName;
        }


        public string FeatureName { get; set; }

    }
}
