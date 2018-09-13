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

        private bool _IsEditMode;
        public bool IsEditMode
        {
            get { return _IsEditMode; }
            set { _IsEditMode = value; OnPropertyChanged("IsEditMode"); }
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

                IsEditMode = true;
                DeletePostCommand = new Command(DeletePostCommandExecute, (obj) => { return IsEditMode; });
                OnPropertyChanged("DeletePostCommand");
            }
        }

        public NewPostPage ()
		{
            post = new Post();
            PostImage = ImageSource.FromResource("Icon.png");
            SavePostCommand = new Command(SavePostCommandExecute);
            DeletePostCommand = new Command(DeletePostCommandExecute, (obj) => { return IsEditMode; });

            BindingContext = this;
            InitializeComponent();
        }

        private async void DeletePostCommandExecute(object obj)
        {
            IsBusy = true;
            App app = (App.Current as App);
            await app.PostManager.Delete(post.Id);
            IsBusy = false;
            DisplayAlert("Remover", "Post removido com sucesso.", "OK");
            Navigation.PopAsync();
        }

        private async void SavePostCommandExecute(object obj)
        {
            IsBusy = true;
            App app = (App.Current as App);

            if (!string.IsNullOrWhiteSpace(post?.Id))
            {
                post.Text = PostText;
                await app.PostManager.Update(post);
            }
            else
            {
                await app.PostManager.Add(PostText, imageBase64);
            }

            IsBusy = false;
            DisplayAlert("Salvar", "Post salvo com sucesso.", "OK");
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