namespace BlogExpert.Mvc.Configurations
{
    public static class AppConfig
    {
        public static WebApplication AddAppConfig(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }

            app.UseGlobalizationConfig();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Posts}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.UseDbMigrationHelper();

            return app;
        }
    }
}
