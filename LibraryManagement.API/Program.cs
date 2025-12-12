
using LibraryManagement.API.Middleware;
using LibraryManagement.BLL;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.BLL.Mapper;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryDbContext>
    (options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
//builder.Services.AddAutoMapper();
builder.Services.AddAutoMapper(cfg =>
{
    // الآن، نستخدم دالة ضبط التجميعة (AddMaps) داخل Action
    cfg.AddMaps(typeof(MappingProfile).Assembly);
});
// هذا السطر يضيف الخدمات المطلوبة لتوليد التوثيق (JSON file)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management API V1", // العنوان الذي سيظهر في الواجهة
        Version = "v1",                     // رقم الإصدار
        Description = "API for managing books, users, and borrowing operations."
    });
});


var app = builder.Build();

ExceptionMiddlewareExtensions.UseExceptionMiddleware(app);

// Configure the HTTP request pipeline.

// 2. تفعيل واجهة Swagger UI
// يتم تفعيل الواجهة التفاعلية (الصفحة التي نختبر بها) فقط في بيئة التطوير.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // لإنشاء ملف توثيق JSON
    app.UseSwaggerUI(); // لعرض الواجهة التفاعلية
}

app.UseHttpsRedirection(); // يفضل استخدامه دائماً لفرض HTTPS
app.UseAuthorization();

app.MapControllers();

app.Run();