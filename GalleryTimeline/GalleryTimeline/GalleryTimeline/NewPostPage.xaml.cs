using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryTimeline
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPostPage : ContentPage
	{
        private ImageSource _PostImage;
        public ImageSource PostImage
        {
            get { return _PostImage; }
            set { _PostImage = value; OnPropertyChanged("PostImage"); }
        }

        private string _PostText;
        public string PostText
        {
            get { return _PostText; }
            set { _PostText = value; OnPropertyChanged("PostText"); }
        }

        private Command _SavePostCommand;
        public Command SavePostCommand
        {
            get { return _SavePostCommand; }
            set { _SavePostCommand = value; OnPropertyChanged("SavePostCommand"); }
        }

        private Command _DeletePostCommand;
        public Command DeletePostCommand
        {
            get { return _DeletePostCommand; }
            set { _DeletePostCommand = value; OnPropertyChanged("DeletePostCommand"); }
        }

        private string imageBase64;

        private Post post;

        private byte[] bytes;

        public NewPostPage(Post post) : this()
        {
            if(post != null)
            {
                this.post = post;

                PostImage = post.Image;
                PostText = post.Text;

                DeletePostCommand = new Command(DeletePostCommandExecute, (obj) => { return true; });
            }
        }

        public NewPostPage ()
		{
            post = new Post();
            PostImage = ImageSource.FromResource("Icon.png");
            SavePostCommand = new Command(SavePostCommandExecute);
            DeletePostCommand = new Command(DeletePostCommandExecute, (obj) => { return false; });

            BindingContext = this;
            InitializeComponent();
        }

        private async void DeletePostCommandExecute(object obj)
        {
            IsBusy = true;
            App app = (App.Current as App);
            await app.PostClient.DeleteAsync(post.Id);
            IsBusy = false;
            DisplayAlert("Remove", "Post removed successfuly.", "OK");
            Navigation.PopAsync();
        }

        private async void SavePostCommandExecute(object obj)
        {
            IsBusy = true;
            App app = (App.Current as App);

            post.Text = PostText;

            if (!string.IsNullOrWhiteSpace(post?.Id))
            {
                await app.PostClient.UpdateAsync(post.Id, post);
            }
            else
            {
                await app.PostClient.AddAsync(post);
            }

            IsBusy = false;
            DisplayAlert("Save", "Post saved successfuly.", "OK");
            Navigation.PopAsync();
        }

        private async void TakePhotoAsync(object sender, EventArgs e)
        {
            MediaFile imageFile = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "Gallery Timeline"
            });

            if (imageFile != null)
            {
                LoadImage(imageFile);
                imageFile.Dispose();
            }
        }

        private async void PickPhotoAsync(object sender, EventArgs e)
        {
            MediaFile imageFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();

            if (imageFile != null)
            {
                LoadImage(imageFile);
                imageFile.Dispose();
            }
        }

        private void LoadImage(MediaFile imageFile)
        {
            Stream stream = imageFile.GetStream();
            bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);

            post.ImageBase64 = imageBase64 = Convert.ToBase64String(bytes);
            PostImage = ImageSource.FromStream(() => {
                Stream byteStream = new MemoryStream(bytes);
                return byteStream;
            });
        }
    }
}