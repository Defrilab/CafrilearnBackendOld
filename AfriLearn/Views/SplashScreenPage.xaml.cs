﻿using AfriLearnMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using Akavache;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenPage : ContentPage
    {
        Image SplashscreenImage;
        public SplashScreenPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var container = new AbsoluteLayout();
            SplashscreenImage = new Image()
            {
                Source = "HandsTogetherRotated.jpg"
            };
            AbsoluteLayout.SetLayoutFlags(SplashscreenImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SplashscreenImage,
                new Rectangle(1, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            container.Children.Add(SplashscreenImage);
            this.Content = container;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await SplashscreenImage.ScaleTo(1, 1500);
            await SplashscreenImage.ScaleTo(0.8, 2500, Easing.Linear);
            await SplashscreenImage.ScaleTo(150, 2200, Easing.Linear);

            try
            {
                //Retrived once and used in the entire app
                var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
                await BlobCache.InMemory.InsertObject<AppUser>("appUser", appUser);
                if (appUser.IsSignedIn)
                {
                    App.Current.MainPage = new NavigationPage(new HomePage());
                }
                else
                {
                    App.Current.MainPage = new NavigationPage(new SignInPage());
                }

            }
            catch (System.Exception)
            {
                App.Current.MainPage = new NavigationPage(new IntroPageOne());
            }
        }
    }}