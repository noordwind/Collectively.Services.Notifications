using AutoMapper;
using Collectively.Messages.Commands.Notifications;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Dto;

namespace Collectively.Services.Notifications.Framework
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateUserNotificationSettings, UserNotificationSettings>();
                cfg.CreateMap<Messages.Commands.Models.NotificationSettings, NotificationSettings>();
                cfg.CreateMap<UserNotificationSettings, UserNotificationSettingsDto>();
            });

            return config.CreateMapper();
        }
    }
}