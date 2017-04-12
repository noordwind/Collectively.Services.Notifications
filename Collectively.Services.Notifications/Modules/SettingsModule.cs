using AutoMapper;
using Collectively.Services.Notifications.Domain;
using Collectively.Services.Notifications.Dto;
using Collectively.Services.Notifications.Queries;
using Collectively.Services.Notifications.Services;

namespace Collectively.Services.Notifications.Modules
{
    public class SettingsModule : ModuleBase
    {
        public SettingsModule(IMapper mapper,
            IUserNotificationSettingsService settingsService) 
            : base(mapper, "notification/settings", false)
        {
            Get("/{userId}", async args => await Fetch<GetUserNotificationSettings, User>
                (async x => await settingsService.GetSettingsAsync(x.UserId))
                .MapTo<UserNotificationSettingsDto>()
                .HandleAsync());
        }
    }
}