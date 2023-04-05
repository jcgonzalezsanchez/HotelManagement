using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Interfaces.Services;
using HotelManagement.Repository.Repositories;
using HotelManagement.Service.Services;

namespace HotelManagement.Api.Configuration
{
    public static class ServicesConfiguration
    {

        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IAgencyService, AgencyService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IEmergencyContactService, EmergencyContactService>();


            //Repositories
            services.AddScoped<IAgencyRepository, AgencyRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IEmergencyContactRepository, EmergencyContactRepository>();


            return services;
        }

    }
}
