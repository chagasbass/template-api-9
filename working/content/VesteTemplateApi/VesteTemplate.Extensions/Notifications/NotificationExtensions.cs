using Microsoft.Extensions.DependencyInjection;
using VesteTemplate.Shared.Notifications;

namespace VesteTemplate.Extensions.Notifications
{
    public static class NotificationExtensions
    {
        public static IServiceCollection AddNotificationControl(this IServiceCollection services)
        {
            services.AddSingleton<INotificationServices, NotificationServices>();
            return services;
        }
    }
}
