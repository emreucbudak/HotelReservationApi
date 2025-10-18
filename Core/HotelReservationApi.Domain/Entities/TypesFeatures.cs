using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
