using Knab.CodingAssessment.Exchange.Endpoint.WebApi;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

var apiConfiguration = app.Services.GetService<ApiConfiguration>();

startup.Configure(app, app.Environment, apiConfiguration);

app.Run();
