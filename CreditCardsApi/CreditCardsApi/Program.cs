using CreditCardsApi.Commons;
using CreditCardsApi.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

// Add services to the container.

builder.Services.AddSingleton<ICardRepository, CardRepositoryImpl>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    config =>
    config.WithOrigins("http://localhost:3000")
          .WithMethods("*")
          .WithHeaders("*")
);

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
