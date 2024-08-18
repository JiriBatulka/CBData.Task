using CBData.Task.Core.Main;
using CBData.Task.Core.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<OrdersRepo>();
builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddScoped<ApiRepository>();
builder.Services.AddSingleton<OrdersSender>();
builder.Services.AddLogging();
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.AllowTrailingCommas = true;
    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.PropertyNameCaseInsensitive = true;
    options.Converters.Add(new JsonStringEnumConverter());
});
WebApplication app = builder.Build();

//we need singleton instance of this class to write stuff in the console
app.Services.GetRequiredService<OrdersSender>();

//we need logger and main class instances here but the request handling is scoped so we need to keep that in mind
ILogger<Program> logger;
ApiRepository apiRepository;

using (IServiceScope scope = app.Services.CreateScope())
{
    logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    apiRepository = scope.ServiceProvider.GetRequiredService<ApiRepository>();
}

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    //Exception handling middleware for unkwnown exceptions. Client exceptions are handled by Result class as communication via exceptions is slow
    catch (Exception ex)
    {
        logger.LogError(ex, "Exception message: {ExceptionMessage}", ex.Message);
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(new ResultDTO()
        {
            ResultCode = ResultCode.Failure,
            ResultObject = "Request was correct but there was an internal server error. Try again later or contact someone"
        });
    }
});


app.MapPut("/orders", async (HttpRequest request) =>
{
    Result<List<OrderDTO>, ResultException> orders = await Utils.DeserializeJsonFromClientAsync<OrderDTO>(request.Body, request.HttpContext.RequestAborted);
    if (orders.IsOk)
    {
        Result<bool, ResultException> result = apiRepository.Orders.AddOrders(orders.Value!);
        if (result.IsOk)
        {
            return new ResultDTO()
            {
                ResultCode = ResultCode.Success
            };
        }
        else
        {
            request.HttpContext.Response.StatusCode = (int)result.Error!.HttpStatusCode;
            return new ResultDTO()
            {
                ResultCode = ResultCode.Failure,
                ResultObject = result.Error.Message
            };
        }
    }
    else
    {
        request.HttpContext.Response.StatusCode = (int)orders.Error!.HttpStatusCode;
        return new ResultDTO()
        {
            ResultCode = ResultCode.Failure,
            ResultObject = orders.Error.Message
        };
    }
});

app.Run();
