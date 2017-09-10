using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Autofac;
using Collectively.Common.Exceptionless;
using Collectively.Common.Extensions;
using Collectively.Common.Mongo;
using Collectively.Common.NancyFx;
using Collectively.Common.RabbitMq;
using Collectively.Common.Security;
using Collectively.Common.ServiceClients;
using Collectively.Common.Services;
using Collectively.Messages.Commands;
using Collectively.Messages.Events;
using Collectively.Services.Notifications.Framework.IoC;
using Collectively.Services.Notifications.Repositories;
using Collectively.Services.Notifications.Services;
using Collectively.Services.Notifications.Settings;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Newtonsoft.Json;
using Serilog;
using RawRabbit.Configuration;

namespace Collectively.Services.Notifications.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly ILogger Logger = Log.Logger;
        private static IExceptionHandler _exceptionHandler;
        private readonly IConfiguration _configuration;
        private static readonly string DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private static readonly string InvalidDecimalSeparator = DecimalSeparator == "." ? "," : ".";
        public static ILifetimeScope LifetimeScope { get; private set; }

        public Bootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

#if DEBUG
        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
#endif

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);

            container.Update(builder =>
            {
                builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>().SingleInstance();
                builder.RegisterInstance(_configuration.GetSettings<ExceptionlessSettings>()).SingleInstance();
                builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>()).SingleInstance();
                builder.RegisterType<ExceptionlessExceptionHandler>().As<IExceptionHandler>().SingleInstance();
                builder.RegisterType<Handler>().As<IHandler>();
                builder.RegisterInstance(AutoMapperConfig.InitializeMapper());
                builder.RegisterInstance(_configuration.GetSettings<MongoDbSettings>()).SingleInstance();
                builder.RegisterModule<MongoDbModule>();
                builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
                builder.RegisterType<DatabaseSeeder>().As<IDatabaseSeeder>();
                builder.RegisterModule<ServiceClientModule>();
                builder.RegisterModule<ServiceClientsModule>();

                builder.RegisterType<UserNotificationSettingsRepository>().As<IUserNotificationSettingsRepository>();
                builder.RegisterType<RemarkSubscribersRepository>().As<IRemarkSubscribersRepository>();
                builder.RegisterType<LocalizedResourceRepository>().As<ILocalizedResourceRepository>();

                builder.RegisterType<NotificationService>().As<INotificationService>();
                builder.RegisterType<RemarkSubscribersService>().As<IRemarkSubscribersService>();
                builder.RegisterType<UserNotificationSettingsService>().As<IUserNotificationSettingsService>();
                builder.RegisterType<EmailMessageService>().As<IEmailMessageService>();

                var assembly = typeof(Startup).GetTypeInfo().Assembly;
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>)).InstancePerLifetimeScope();
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
                SecurityContainer.Register(builder, _configuration);
                RabbitMqContainer.Register(builder, _configuration.GetSettings<RawRabbitConfiguration>());
            });
            LifetimeScope = container;
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.SetupTokenAuthentication(container.Resolve<IJwtTokenHandler>());
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                _exceptionHandler.Handle(ex, ctx.ToExceptionData(),
                    "Request details", "Collectively", "Service", "Remarks");

                return ctx.Response;
            });
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            var databaseInitializer = container.Resolve<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync();
            var databaseSeeder = container.Resolve<IDatabaseSeeder>();
            databaseSeeder.SeedAsync();
            pipelines.BeforeRequest += (ctx) =>
            {
                FixNumberFormat(ctx);

                return null;
            };
            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,PUT,GET,OPTIONS,DELETE");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers",
                    "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };
            _exceptionHandler = container.Resolve<IExceptionHandler>();
            Logger.Information("Collectively.Services.Notifications API has started.");
        }

        private void FixNumberFormat(NancyContext ctx)
        {
            if (ctx.Request.Query == null)
                return;

            var fixedNumbers = new Dictionary<string, double>();
            foreach (var key in ctx.Request.Query)
            {
                var value = ctx.Request.Query[key].ToString();
                if (!value.Contains(InvalidDecimalSeparator))
                    continue;

                var number = 0;
                if (int.TryParse(value.Split(InvalidDecimalSeparator[0])[0], out number))
                    fixedNumbers[key] = double.Parse(value.Replace(InvalidDecimalSeparator, DecimalSeparator));
            }
            foreach (var fixedNumber in fixedNumbers)
            {
                ctx.Request.Query[fixedNumber.Key] = fixedNumber.Value;
            }
        }
    }
}
