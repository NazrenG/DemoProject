 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.DataAccess.Concrete;
using TaskFlow.Entities.Data;

var builder = WebApplication.CreateBuilder(args);

 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IUserDal,UserDal>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IAddressDal,AddressDal>();
builder.Services.AddScoped<IAddressService,AddressService>();
builder.Services.AddScoped<IQuizDal,QuizDal>();
builder.Services.AddScoped<IQuizService,QuizService>();
builder.Services.AddScoped<IProjectDal,ProjectDal>();
builder.Services.AddScoped<IProjectService,ProjectService>();
builder.Services.AddScoped<ITaskDal,TaskDal>();
builder.Services.AddScoped<ITaskService,TaskService>();
builder.Services.AddScoped<ITeamDal, TeamDal>();
builder.Services.AddScoped<ITeamService, TeamService>(); 
builder.Services.AddScoped<ITeamMemberDal,TeamMemberDal>();
builder.Services.AddScoped<ITeamMemberService,TeamMemberService>(); 


var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TaskFlowContext>(opt =>
{
    opt.UseSqlServer(conn);
    opt.UseLazyLoadingProxies();
});


var app = builder.Build();

app.UseCors("AllowAllOrigins"); 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
