using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xamarin.UITest;
using XFApp.BDD.UI_Tests.Screens;

namespace XFApp.BDD.UI_Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile("../../../XFApp/XFApp.Droid/bin/Release/XFApp.Droid.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }

        public static void InitializeScreens(Platform platform)
        {
            if (platform == Platform.Android)
            {                
                FeatureContext.Current.Add(ScreenNames.UserSearchPage, new AndroidUserSearchPage());
                FeatureContext.Current.Add(ScreenNames.RepositoriesPage, new AndroidRepositoriesPage());

            }
        }

        public static void ClearScreens(Platform platform)
        {
            if (platform == Platform.Android)
            {
                FeatureContext.Current.Remove(ScreenNames.UserSearchPage);
                FeatureContext.Current.Remove(ScreenNames.RepositoriesPage);

            }
        }
    }
}
