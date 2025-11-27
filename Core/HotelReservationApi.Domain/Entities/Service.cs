using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class Service : BaseEntity
    {
        public Service()
        {
        }

        public Service(string serviceName, bool ısNeedAFee)
        {
            ServiceName = serviceName;
            IsNeedAFee = ısNeedAFee;
        }


        public string ServiceName { get; set; }
        public bool IsNeedAFee { get; set; }

    }
}
