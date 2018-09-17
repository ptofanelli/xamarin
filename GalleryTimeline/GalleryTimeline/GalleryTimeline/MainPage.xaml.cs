using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GalleryTimeline
{
    public partial class MainPage : ContentPage
    {
        private Command _RefreshCommand;
        public Command RefreshCommand
        {
            get { return _RefreshCommand; }
            set { _RefreshCommand = value; OnPropertyChanged("RefreshCommand"); }
        }

        private Command _EditPostCommand;
        public Command EditPostCommand
        {
            get { return _EditPostCommand; }
            set { _EditPostCommand = value; OnPropertyChanged("EditPostCommand"); }
        }

        private ObservableCollection<Post> _Posts;
        public ObservableCollection<Post> Posts
        {
            get { return _Posts; }
            set { _Posts = value; OnPropertyChanged("Posts"); }
        }

        private App app;

        public MainPage()
        {
            app = App.Current as App;
            RefreshCommand = new Command(RefreshCommandExecute);
            EditPostCommand = new Command(EditPostCommandExecute);

            BindingContext = this;
            InitializeComponent();
            NewPost.Clicked += (sender, e) => { Navigation.PushAsync(new NewPostPage()); };
        }

        private void EditPostCommandExecute(object obj)
        {
            Navigation.PushAsync(new NewPostPage(obj as Post));
        }

        private async void RefreshCommandExecute(object obj)
        {
            IsBusy = true;

            List<Post> posts = new List<Post>();
            posts.AddRange(await app.PostClient.GetAsync());
            foreach (Post post in posts)
            {
                try
                {
                    post.ImageBytes = Convert.FromBase64String(post.ImageBase64);
                }
                catch (Exception)
                {
                    //move on
                }
            }

            Posts = new ObservableCollection<Post>(posts);
            IsBusy = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshCommand.Execute(null);
        }

    }
}
