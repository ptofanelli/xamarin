using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarathon.model;
using XamarinMarathon.services;

namespace XamarinMarathon.view
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestWebServiceClient : ContentPage
    {

        readonly IList<Book> books = new ObservableCollection<Book>();
        readonly BookManager manager = new BookManager();

        public RestWebServiceClient()
        {
            BindingContext = books;
            InitializeComponent();
        }

        async void OnRefresh(object sender, EventArgs e)
        {
            loading.IsRunning = true;
            loading.IsVisible = true;

            var bookCollection = await manager.GetAll();

            foreach (Book book in bookCollection)
            {
                if (books.All(b => b.ISBN != book.ISBN))
                    books.Add(book);
            }

            loading.IsRunning = false;
            loading.IsVisible = false;
        }

        async void OnAddNewBook(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditBookPage(manager, books));
        }

        async void OnEditBook(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditBookPage(manager, books, (Book)e.Item));
        }

        async void OnDeleteBook(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Book book = item.CommandParameter as Book;
            if (book != null)
            {
                if (await this.DisplayAlert("Delete Book?",
                    "Are you sure you want to delete the book '"
                        + book.Title + "'?", "Yes", "Cancel") == true)
                {
                    await manager.Delete(book.ISBN);
                    books.Remove(book);
                }
            }
        }
    }
}
