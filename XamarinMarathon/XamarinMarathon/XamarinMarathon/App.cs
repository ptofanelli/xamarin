using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinMarathon.model;
using XamarinMarathon.view;

namespace XamarinMarathon
{
    public class App : Application
    {
        public static PersonRepository PersonRepo { get; private set; }

        public static IFileAccessHelper FileAcessHelper { get; set; }

        public App()
        {
            PersonRepo = new PersonRepository(FileAcessHelper.GetLocalFilePath("people.db3"));

            this.MainPage = new LoginPage();
            //MainPage = new NavigationPage(new XamarinViewsPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
