using MinimalApi;
using MinimalApi.Interfaces;
using MinimalApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Microsoft.Extensions.Http
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient< IControls, RepControlDapper> ();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  Chamada ao método ApiMapping que criamos como extension da classe WebApplication
app.ApiMapping();

app.Run();