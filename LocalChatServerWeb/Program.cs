 using LocalChatServerWeb.Data;
 using LocalChatServerWeb.Models;
 using LocalChatServerWeb.Repositories;

 var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
 builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
     .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
     (
         mongoDbSettings.ConnectionString, mongoDbSettings.Name
     );
 builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddSingleton<SessionRepository>();
builder.Services.AddSingleton<MessageRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
