using TechForum.Business.Interfaces;
using TechForum.Business.Notifications;

// using TechForum.Business.Services;
using TechForum.Data.Context;
using TechForum.Data.Repository;

namespace TechForum.Api.Configurations;

public static class InjectionConfig
{
  public static IServiceCollection ResolveDependencies(this IServiceCollection services)
  {
    services.AddScoped<AppDbContext>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ITopicRepository, TopicRepository>();
    services.AddScoped<ICommentRepository, CommentRepository>();

    services.AddScoped<INotifier, Notifier>();

    return services;
  }
}
