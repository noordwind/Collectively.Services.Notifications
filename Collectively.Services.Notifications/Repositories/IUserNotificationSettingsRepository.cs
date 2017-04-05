using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Notifications.Domain;

namespace Collectively.Services.Notifications.Repositories
{
    public interface IUserNotificationSettingsRepository
    {
        Task<Maybe<UserNotificationSettings>> GetByIdAsync(Guid userId);
        Task AddAsync(UserNotificationSettings settings);
        Task EditAsync(UserNotificationSettings settings);
    }
}