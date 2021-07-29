using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QoolloEDU.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QoolloEDU.Database.models;
using QoolloEDU.Database.Repositories.AuthRepository;
using QoolloEDU.Database.Repositories.DeveloperRepository;
using QoolloEDU.Database.Repositories.EventPageRepository;
using QoolloEDU.Database.Repositories.EventRepository;
using QoolloEDU.Database.Repositories.GeneratorRepository;
using QoolloEDU.Database.Repositories.LinkRepository;
using QoolloEDU.Database.Repositories.MediaContentRepository;
using QoolloEDU.Database.Repositories.NewsRepository;
using QoolloEDU.Database.Repositories.OrganizerPageRepository;
using QoolloEDU.Database.Repositories.OrganizerRepository;
using QoolloEDU.Database.Repositories.ProfilePageRepository;
using QoolloEDU.Database.Repositories.ProjectPageRepository;
using QoolloEDU.Database.Repositories.ProjectRepository;
using QoolloEDU.Database.Repositories.RatingRepository;
using QoolloEDU.Database.Repositories.RequestRepository;
using QoolloEDU.Database.Repositories.SearchRepository;
using QoolloEDU.Database.Repositories.TagRepository;
using QoolloEDU.Dto;
using QoolloEDU.WebService.Services;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace QoolloEDU.WebService
{
    public class Startup
    {
        private readonly string _corsPolicy = "AskBmstuFm";
        
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("QoolloEDU");
            services.AddEntityFrameworkNpgsql().AddDbContext<QoolloEduDbContext>(options => 
                options.UseNpgsql(connectionString, x => x.MigrationsAssembly("QoolloEDU.WebService"))
            );
            
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IDeveloperRepository, DeveloperRepository>();
            services.AddTransient<IEventPageRepository, EventPageRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IGeneratorRepository, GeneratorRepository>();
            services.AddTransient<ILinkRepository, LinkRepository>();
            services.AddTransient<IMediaContentRepository, MediaContentRepository>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<IOrganizerPageRepository, OrganizerPageRepository>();
            services.AddTransient<IOrganizerRepository, OrganizerRepository>();
            services.AddTransient<IProfilePageRepository, ProfilePageRepository>();
            services.AddTransient<IProjectPageRepository, ProjectPageRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<ISearchRepository, SearchRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<AuthService>();
            services.AddTransient<ProjectService>();
            services.AddTransient<EventService>();
            
            services.AddControllers();
            services.AddHealthChecks();
            services.AddCors(o => o.AddPolicy(_corsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddMvc().AddJsonOptions(opt => opt.JsonSerializerOptions.MaxDepth = Int32.MaxValue);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
 
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
 
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //UpdateDatabase(app);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(_corsPolicy); 
            app.UseStaticFiles();
            app.UseRouting(); 
            
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHealthChecks("/health");
                //endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
        
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<QoolloEduDbContext>();
            context?.Database.Migrate();
        }
    }

}