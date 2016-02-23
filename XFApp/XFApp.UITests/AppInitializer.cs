using Xamarin.UITest;

namespace XFApp.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    //.ApiKey("818b3bd138ed33a39209ae36565ef62c")
                    .ApkFile("../../../XFApp/XFApp.Droid/bin/Release/XFApp.Droid.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}