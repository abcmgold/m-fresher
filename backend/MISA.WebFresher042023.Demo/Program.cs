using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.HandleException;
using System.Text.Json;
using MISA.WebFresher042023.Demo.Core.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Repository;
using MISA.WebFresher042023.Demo.Middleware;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using MISA.WebFresher042023.Demo.Infrastructure.UnitOfWork;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values;

            var result = JsonSerializer.Serialize(errors);
            return new BadRequestObjectResult(new BaseException()
            {
                ErrorCode = StatusCodes.Status400BadRequest,
                UserMessage = "",
                DevMessage = "",
                TraceId = "",
                MoreInfo = "",
                Errors = errors
            }.ToString() ?? "");
        };
    })
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();   

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IPropertyTypeService, PropertyTypeService>();
builder.Services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();

builder.Services.AddScoped<ITransferAssetService, TransferAssetService>();
builder.Services.AddScoped<ITransferAssetRepository, TransferAssetRepository>();

builder.Services.AddScoped<ITransferAssetDetailService, TransferAssetDetailService>();
builder.Services.AddScoped<ITransferAssetDetailRepository, TransferAssetDetailRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddCors(p => p.AddPolicy("corspolicy", configurePolicy: build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
