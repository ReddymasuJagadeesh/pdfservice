using DinkToPdf;
using DinkToPdf.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Register DinkToPdf
builder.Services.AddSingleton<IConverter>(
    new SynchronizedConverter(new PdfTools())
);



///swager 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NafaGoldTry API v1");
        c.RoutePrefix = "swagger"; // URL = /swagger
    });
}


app.MapControllers();

app.Run();
