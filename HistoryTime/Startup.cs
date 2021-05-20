using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HistoryTime.Data;
using HistoryTime.Domain;
using Microsoft.OpenApi.Models;



namespace HistoryTime
{
    public class Startup
    {
        private readonly AppConfig _config;
        public Startup(IConfiguration configuration)
        {
            _config = new AppConfig(configuration);
        }
        
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IRepository<Role>>(new RolesRepository(_config.ConnectionString));
            services.AddSingleton<IUsersRepository>(new UsersRepository(_config.ConnectionString));
            services.AddSingleton<IQuizzesRepository>(new QuizzesRepository(_config.ConnectionString));
            services.AddSingleton<IAnswersRepository>(new AnswersRepository(_config.ConnectionString));
            services.AddSingleton<IRepository<Article>>(new ArticlesRepository(_config.ConnectionString));
            services.AddSingleton<IQuestionsRepository>(new QuestionsRepository(_config.ConnectionString));
            services.AddSingleton<IUsersAnswersRepository>(new UsersAnswersRepository(_config.ConnectionString));
            services.AddSingleton<IAnswersTheQuestionsRepository>(
                new AnswersTheQuestionsRepository(_config.ConnectionString));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });    
            app.UseEndpoints(endpoints => { endpoints.MapControllers();});
        }
    }
}