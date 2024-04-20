using A2_SinhToDau_QuanLyBenhVien.Data;
using A2_SinhToDau_QuanLyBenhVien.Models;
using A2_SinhToDau_QuanLyBenhVien.Repositories;
using DotNetEd.CoreAdmin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IServiceRepository, EFServiceRepository>();
// Thêm ISpecialistRepository và EFSpecialistRepository
builder.Services.AddScoped<ISpecialistRepository, EFSpecialistRepository>();
builder.Services.AddScoped<INewsRepository, EFNewsRepository>();
builder.Services.AddScoped<IAppointmentRepository, EFAppointmentRepository>();
builder.Services.AddScoped<IDoctorRepository, EFDoctorRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IDegreeRepository, EFDegreeRepository>();
builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})
.AddDefaultUI()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
// .AddDefaultUI()
// .AddEntityFrameworkStores<ApplicationDbContext>()
// .AddDefaultTokenProviders();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
//add package coreAdmin services for role Admin who required to access the panel
builder.Services.AddCoreAdmin("Admin");
//Prevent certain types of entities being shown or available in the admin panel 
builder.Services.AddCoreAdmin(new CoreAdminOptions() { IgnoreEntityTypes = new List<Type>()
{
    typeof(Appointment),
    typeof(Degree),
    typeof(New),
    typeof(Service),
    typeof(IdentityRoleClaim<String>),
    typeof(IdentityUserToken<String>),
    typeof(IdentityUser),
    typeof(IdentityUserClaim<String>),
    typeof(IdentityUserLogin<String>),
    typeof(IdentityUserRole<String>),
}
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

//enabled static files middleware and Endpoints MapDefaultControllerRoute() for coreAdmin package
app.UseStaticFiles();
//app.MapDefaultControllerRoute();
app.UseRouting();
app.UseAuthorization();
//app.UseCoreAdminCustomUrl("Admin");
//app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Manager}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
//custom URL to access the panel at /admin
app.UseCoreAdminCustomTitle("Admin Dashboard");
app.Run();
