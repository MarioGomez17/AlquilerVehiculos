using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(SessionOptions =>
    {
        SessionOptions.LoginPath = "/Usuario/IniciarSesion";
        SessionOptions.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        SessionOptions.AccessDeniedPath = "/Inicio/SinPermisos";
        SessionOptions.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.Redirect(SessionOptions.AccessDeniedPath);
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorizationBuilder()
        .AddPolicy("SoloAdministrador", policy =>
                    policy.RequireRole("Administrador"));

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Inicio}/{id?}");

app.Run();

