using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotifiNoteBE.Configurations;
using NotifiNoteBE.Data;
using NotifiNoteBE.Models;
using NotifiNoteBE.Repostries;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JWT"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotifiNoteDB>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("connectionData")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<NotifiNoteDB>();
builder.Services.AddTransient<INoteRepo, NoteRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddAuthentication(a =>
{
	a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(j =>
{
	j.SaveToken = true;
	j.RequireHttpsMetadata = false;
	j.TokenValidationParameters = new TokenValidationParameters() {
		ValidateAudience = true,
		ValidateIssuer = true,
		ValidAudience = builder.Configuration.GetSection("JWT")["ValidAudience"],
		ValidIssuer = builder.Configuration.GetSection("JWT")["ValidIssuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")["Secret"]))

	};
});
builder.Services.AddCors(o =>
{
	o.AddPolicy("allowOrigin", p =>
	{
		p.WithOrigins("http://192.168.1.7:4200").AllowAnyMethod()
			.AllowAnyHeader(); 
	});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("allowOrigin");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
