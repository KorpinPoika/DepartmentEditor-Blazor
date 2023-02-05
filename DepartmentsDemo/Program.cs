using DepartmentsDemo;
using DepartmentsDemo.Dal.Interfaces;
using DepartmentsDemo.Dal.Repositories;
using DepartmentsDemo.Dal.Services;
using DepartmentsDemo.Db.Context;
using DepartmentsDemo.Db.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Config.Init(builder.Environment);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<DepDbContext>(
	options => {
		options.UseSqlServer(Config.AppConfiguration.GetConnectionString("DefaultConnection"));
	}
);   

builder.Services.AddScoped<IEfGenericRepository<Department>, EfGenericRepository<DepDbContext, Department>>();
builder.Services.AddScoped<IEfGenericRepository<Empoyee>, EfGenericRepository<DepDbContext, Empoyee>>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();