using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using BD_;
using BD_.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql($"Host=localhost;Port=5432;Database=ITproject;Username=ruslan;Password=ruslan_password"));
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.EnableSensitiveDataLogging(true));
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IUserRepository, UserR>();
builder.Services.AddTransient<IDoctorRepository, DoctorR>();
builder.Services.AddTransient<ISheduleRepository, SheduleR>();
builder.Services.AddTransient<IReceptionRepository, ReceptionR>();
builder.Services.AddTransient<ReceptionRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<DoctorRepository>();
builder.Services.AddTransient<SheduleRepository>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

