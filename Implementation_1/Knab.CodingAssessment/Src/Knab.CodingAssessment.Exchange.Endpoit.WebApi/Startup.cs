using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Knab.CodingAssessment.Exchange.ApplicationServices;
using Knab.CodingAssessment.Seedwork.Queries;
using Knab.CodingAssessment.Exchage.DataProvider;
using Knab.CodingAssessment.DataProvider.CoinMarketCap;
using Knab.CodingAssessment.Exchange.Endpoint.WebApi.Extensions;
using Knab.CodingAssessment.Exchange.Endpoint.WebApi;
using Knab.CodingAssessment.Exchange.Endpoint.WebApi.Filters;
using Knab.CodingAssessment.Exchange.Endpoint.WebApi.Middlewares;
using Newtonsoft.Json;

public class Startup
{
    private readonly string _mainCrossOriginName = "MainCrossOrigin";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var apiConfiguration = Configuration.GetSection(typeof(ApiConfiguration).Name).Get<ApiConfiguration>();
        services.AddSingleton(apiConfiguration);

        services.AddOptions<CoinMarketCapOptions>()
            .Bind(Configuration.GetSection(nameof(CoinMarketCapOptions)));

        services.AddMemoryCache();

        services.AddControllers();
        services.AddQueryHandlerServices();
        services.AddDomainServices();

        services.AddScoped<IQueryBus, QueryBus>();
        services.AddTransient<IExchangeDataProvider, CoinMarketCapServiceClient>();
        services.AddHttpClient();

        if (apiConfiguration.IsSwaggerEnabled)
        {
            services.AddSingleton(new SwaggerRequestHeaders
                {
                    { "api-version", apiConfiguration.ApiDefaultVersion },
                    { "accept-language", apiConfiguration.ApiDefaultLanguage }
                });
            services.AddSwagger(apiConfiguration);
        }


        services.AddCors(options =>
        {
            options.AddPolicy(name: _mainCrossOriginName,
                builder =>
                {
                    if (apiConfiguration.CorsAllowAnyOrigin)
                    {
                        builder.AllowAnyOrigin();
                    }
                    else
                    {
                        builder.WithOrigins(apiConfiguration.CorsAllowOrigins);
                    }

                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiConfiguration apiConfiguration)
    {
        if (apiConfiguration.IsSwaggerEnabled)
        {
            app.UseSwaggerUi(apiConfiguration);
        }

        app.UseRouting();

        app.UseCors(_mainCrossOriginName);

        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}


