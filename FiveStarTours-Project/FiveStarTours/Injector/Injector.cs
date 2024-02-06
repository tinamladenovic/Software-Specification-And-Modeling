using FiveStarTours.Interfaces;
using FiveStarTours.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
        { typeof(IAccommodationReservationRepository), new AccommodationReservationsRepository() },
        { typeof(IAccommodationRatingRepository), new AccommodationRatingRepository() },
        { typeof(IAccommodationsRepository), new AccommodationsRepository() },
        { typeof(IAttendanceRepository), new AttendanceRepository() },
        { typeof(ICancelationNotificationRepository), new CancelationNotificationRepository() },
        { typeof(IDrivingsRepository), new DrivingsRepository() },
        { typeof(IDrivingStatisticsRepository), new DrivingStatisticsRepository() },
        { typeof(IDrivingStatisticsRepository2), new DrivingStatisticsRepository2() },
        { typeof(IGiftCardRepository), new GiftCardRepository() },
        { typeof(IGroupRideRepository), new GroupRideRepository() },
        { typeof(IGuestRatingsRepository), new GuestRatingsRepository() },
        { typeof(IKeyPointsRepository), new KeyPointsRepository() },
        { typeof(ILanguagesRepository), new LanguagesRepository() },
        { typeof(ILiveTourRepository), new LiveTourRepository() },
        { typeof(ILocationsRepository), new LocationsRepository() },
        { typeof(IReservationChangeRepository), new ReservationChangeRepository() },
        { typeof(IReservedDrivingsRepository), new ReservedDrivingsRepository() },
        { typeof(ITaximeterRepository), new TaximeterRepository()},
        { typeof(ITourReservationRepository), new TourReservationRepository() },
        { typeof(IToursRepository), new ToursRepository() },
        { typeof(IUserRepository), new UserRepository() },
        { typeof(IVehicleOnAdressRepository), new VehicleOnAdressRepository() },
        { typeof(IVehicleRepository), new VehicleRepository() },
        { typeof(IRenovationRepository), new RenovationRepository() },
        { typeof(ITourRatingRepository), new TourRatingRepository() },
        { typeof(ITourRequestRepository), new TourRequestRepository() },


        // Add more implementations here
    };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
