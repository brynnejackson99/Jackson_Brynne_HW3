using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Jackson_Brynne_HW3.Models;
using Jackson_Brynne_HW3.DAL;


//TODO: Make this namespace match your project - be sure to remove the []
namespace Jackson_Brynne_HW3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //NOTE: This adds the MVC engine and Razor code
            services.AddControllersWithViews();

            //TODO: (For HW3 and beyond) Add a connection string here once you have created it on Azure
            String connectionString = "Server=tcp:fa21jacksonbrynnehw3.database.windows.net,1433;Initial Catalog=fa21jacksonbrynnehw3;Persist Security Info=False;User ID=MISAdmin;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            //TODO: (For HW3 and beyond) Uncomment this line once you have your connection string
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        }

        public void Configure(IApplicationBuilder app)
        {
            //These lines allow you to see more detailed error messages
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();      

            //This line allows you to use static pages like style sheets and images
            app.UseStaticFiles();

            //This marks the position in the middleware pipeline where a routing decision
            //is made for a URL.
            app.UseRouting();

            //This allows the data annotations for currency to work on Macs
            app.Use(async (context, next) =>
            {
                CultureInfo.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

                await next.Invoke();
            });

         

            
            //This method maps the controllers and their actions to a patter for
            //requests that's known as the default route. This route identifies
            //the Home controller as the default controller and the Index() action
            //method as the default action. The default route also identifies a 
            //third segment of the URL that's a parameter named id.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }   
}