namespace API.Setup
{
    public static class CorsSetup
    {
        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CustomCorsPolicy", builder =>
            {
                builder
                    .WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Token-Expired");
            }));
        }

        public static void UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors("CustomCorsPolicy");
        }
    }
}
