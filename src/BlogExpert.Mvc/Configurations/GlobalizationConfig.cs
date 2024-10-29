using Microsoft.AspNetCore.Localization;
using BlogExpert.Dados.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BlogExpert.Mvc.Configurations
{
    public static class GlobalizationConfig
    {
        public static WebApplication UseGlobalizationConfig(this WebApplication app)
        {
            var defaultCulture = new CultureInfo("pt-BR");

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}
