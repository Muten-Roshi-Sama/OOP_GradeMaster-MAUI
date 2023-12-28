using Microsoft.Extensions.Logging;
using GradeMasterMAUI.Data;

namespace GradeMasterMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var dataInitializer = new DataInitializer();
            DataInitializer.InitializeData();



            //----Default code below-----
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

    }
}
