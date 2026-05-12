using System.Net;
using System.Text.Json;


namespace LibraryManagement.API.Middleware
{
    // نموذج استجابة الخطأ الموحد الذي سيتم إرجاعه للمستخدم
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        // لتجنب الكشف عن تفاصيل الخادم في بيئة الإنتاج، نكتفي برمز وحالة عامة
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    // الطبقة الوسيطة الفعلية لمعالجة الأخطاء
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly bool _isDevelopment;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _isDevelopment = env.IsDevelopment();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // إذا لم يكن هناك خطأ، استمر في معالجة الطلب (الخطوة التالية في Pipeline)
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // 1. تسجيل الخطأ كاملاً في سجلات الخادم
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

                // 2. إعداد رمز الحالة (500 Internal Server Error)
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // 3. إنشاء كائن الاستجابة بناءً على بيئة التشغيل
                var response = new ErrorDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    // في بيئة التطوير، نعرض التفاصيل للمطور
                    // في بيئة الإنتاج، نعرض رسالة عامة فقط
                    Message = _isDevelopment ? ex.Message : "An unexpected error occurred. Please try again later."
                };

                // 4. كتابة الاستجابة في شكل JSON
                await httpContext.Response.WriteAsync(response.ToString());
            }
        }
    }

    // كلاس لتوسيع IApplicationBuilder لجعل إضافة Middleware سهلة
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}