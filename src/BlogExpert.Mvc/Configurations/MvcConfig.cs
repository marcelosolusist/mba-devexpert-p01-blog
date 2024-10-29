using BlogExpert.Dados.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogExpert.Mvc.Configurations
{
    public static class MvcConfig
    {
        public static WebApplicationBuilder AddMvcConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BlogExpertDbContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                MvcOptionsConfig.ConfigurarMensagensDeModelBinding(options.ModelBindingMessageProvider);
            });

            return builder;
        }
    }
}
