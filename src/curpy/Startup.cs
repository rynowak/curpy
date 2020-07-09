using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
namespace curpy
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
            services.AddControllers();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.Use(next => async context =>
            {
                if (context.Request.Headers.ContainsKey(HeaderNames.Authorization))
                {
                    logger.LogInformation("Got authorization header.");

                    var header = AuthenticationHeaderValue.Parse(context.Request.Headers[HeaderNames.Authorization]);
                    logger.LogInformation("Authorize header is {Scheme} {Payload}", header.Scheme, header.Parameter);

                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(header.Parameter);
                    var json = JsonSerializer.Serialize(token, new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                    });
                    logger.LogInformation("Parsed JWT: {JWT}", json);
                }
                else
                {
                    logger.LogInformation("Request is anonymous.");
                }

                foreach (var header in context.Request.Headers)
                {
                    logger.LogInformation("Header: {Key}: {Value}", header.Key, header.Value);
                }

                context.Request.EnableBuffering();
                using (var reader = new StreamReader(context.Request.Body, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    var text = await reader.ReadToEndAsync();
                    logger.LogInformation("Body: {Body}", text);

                    context.Request.Body.Seek(0L, SeekOrigin.Begin);
                }

                await next(context); 
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthz");
                endpoints.MapControllers();
            });
        }
    }
}
