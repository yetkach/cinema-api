using Cinema.Data.EF;
using Cinema.Data.Interfaces;
using Cinema.Data.Repositories;
using Cinema.Logics.Interfaces;
using Cinema.Logics.Mapping;
using Cinema.Logics.Services;
using Cinema.Web.Mapping;
using Cinema.Web.Middleware;
using Cinema.Web.Models;
using Cinema.Web.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CinemaDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISeatRepository, SeatRepository>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddAutoMapper(typeof(MapperConfigure));
            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<OrderModel>, OrderModelValidator>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
