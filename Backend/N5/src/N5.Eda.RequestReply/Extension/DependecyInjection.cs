namespace N5.Eda.RequestReply.Extension;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N5.Eda.Extensions;
using N5.Eda.RequestReply.Interface;
using N5.Eda.RequestReply.Interface.Persistence;
using N5.Eda.RequestReply.Internal;
using N5.Eda.RequestReply.Internal.Persistence.MongoDB;
using N5.Eda.RequestReply.Internal.Persistence.MongoDB.Context;
using N5.Eda.RequestReply.Model;

public static class DependecyInjection
{
    public static IServiceCollection AddRequestReply(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddTransient<IRequestReplyExecute, RequestReplyExecute>();

        services.AddTransient<IReplyContainerRepository, ReplyContainerRepository>();
        services.AddSingleton<RequestReplyContext>();

        return services;
    }

    /// <summary>
    /// Use Request Reply
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IHost UseRequestReply(this IHost app, Action<RequestReplyConfiguration> replyConfiguration = null)
    {
        var requestReplyOptions = new RequestReplyConfiguration(app.Services.GetRequiredService<IBrokerHandlerList>());

        if (replyConfiguration != null)
            replyConfiguration(requestReplyOptions);

        app.UseMessageBroker(options =>
        {
            options = requestReplyOptions;
        });

        return app;
    }
}