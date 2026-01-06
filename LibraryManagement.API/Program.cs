using LibraryManagement.API.Middleware;
using LibraryManagement.BLL;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.BLL.Mapper;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة الخدمات الأساسية للمتحكمات
builder.Services.AddControllers();
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters =

        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["jwt:Issuer"],
            ValidAudience = builder.Configuration["jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])
               )
        };
  });

builder.Services.AddAuthorization();

// 2. إعداد قاعدة البيانات (SQL Server)
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. حقن التبعيات (Dependency Injection) - تأكد من مطابقة الأسماء في مشروعك
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IBorrowingService, BorrowingService>();
builder.Services.AddScoped<IRoleRepository,RoleRepository>();
builder.Services.AddScoped<IRoleService,RoleService>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISecurityService,SecurityService>();

// 4. Setup AutoMapper


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(MappingProfile).Assembly);
});

// 5. Setup Swagger (Docs generate)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management API V1",
        Version = "v1",
        Description = "Library Management - API"
    });
    // إضافة تعريف الأمان (Security Definition)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' , Example: 'Bearer eye123...'"
    });

    // إضافة متطلبات الأمان (Security Requirement) لجميع العمليات
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 6. Middleware  (Exception Handling)
app.UseExceptionMiddleware();

// 7. إعدادات Pipeline لبيئة التطوير
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // يولد ملف swagger.json
    app.UseSwaggerUI(c =>
    {
        // 🚨 الربط الصريح الذي يحل مشكلة الـ Parser Error
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management API V1");
        c.RoutePrefix = "swagger"; // يجعل الواجهة تظهر عند الرابط الأساسي /swagger
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// 8. تعيين المسارات للمتحكمات
app.MapControllers();

app.Run();