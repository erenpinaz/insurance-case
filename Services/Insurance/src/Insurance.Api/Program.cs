var builder = WebApplication.CreateBuilder(args);

#region Configure Serilog

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger);

#endregion

// Register services
{
    builder.Services.AddControllers();

    #region Swagger

    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

    #endregion

    builder.Services.AddApplicationServices(builder.Configuration);

    builder.Services.AddInfrastructureServices(builder.Configuration);
}

var app = builder.Build();

// Configure request pipeline
{
    if (app.Environment.IsDevelopment())
    {
        #region Swagger

        app.UseSwagger();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Insurance API v1"));

        #endregion
    }

    app.UseExceptionHandler("/error");

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();
}

app.Run();