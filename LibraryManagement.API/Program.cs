using LibraryManagement.API.Middleware;
using LibraryManagement.BLL;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.BLL.Mapper;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة الخدمات الأساسية للمتحكمات
builder.Services.AddControllers();

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

// 4. إعداد AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(MappingProfile).Assembly);
});

// 5. إعداد Swagger (توليد المستندات)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management API V1",
        Version = "v1",
        Description = "Library Management - API"
    });
});

var app = builder.Build();

// 6. Middleware لمعالجة الاستثناءات (Exception Handling)
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
app.UseAuthorization();

// 8. تعيين المسارات للمتحكمات
app.MapControllers();

app.Run();