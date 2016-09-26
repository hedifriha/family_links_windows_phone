using App8.Common;
using System;
using Parse;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;
using App8.View;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Mediatheque : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        List<Person> lstSource;
        public static Person sharedPerson;
        string name;
        string lastname;

        
        public  Mediatheque()
        {
         
            this.InitializeComponent();
            lstSource = new List<Person>();
            getAuthors();
            DrawerLayout.InitializeDrawerLayout();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            this.jibImage1();
            ParseUser User = ParseUser.CurrentUser;
            //lastNameLabel.Text = sharedInformation.SahredParsePerson.Get<String>("LastName");
            //sharedInformation.SharedLastName = lastNameLabel.Text;
        }
        private async void getAuthors()
        {
            var query = ParseObject.GetQuery("Person")
                .WhereNotEqualTo("FirstName","unknown");
            IEnumerable<ParseObject> results = await query.FindAsync();
            int i = 0;
            foreach (ParseObject result in results)
            {
          
                var applicantResumeFile = result.Get<ParseFile>("image");
                string url = await new HttpClient().GetStringAsync(applicantResumeFile.Url);
                 name = result.Get<string>("FirstName");
                 lastname = result.Get<string>("LastName");
                var bitmap = new BitmapImage(result.Get<ParseFile>("image").Url);
                lstSource.Add(new Person() {  Name = name,LastName = lastname  ,Imagel = bitmap });

                i++;
            }

            lst.DataContext = lstSource;


        }
         bool LayoutUpdateFlag = true;//this is for handling layout upadated event 
          private void contactFilterString_TextChanged(object sender, TextChangedEventArgs e)
          {
              //filter items with help of lambda expression 
              lst.ItemsSource = lstSource.Where(w => w.Name.ToUpper().Contains(SearchTextBox.Text.ToUpper()) );
              LayoutUpdateFlag = true;

          }
       

     
        private void jibImage1()
        {
            var applicantResumeFile = sharedInformation.SahredParsePerson.Get<ParseFile>("image");
            //   string resumeText = await new HttpClient().GetStringAsync(applicantResumeFile.Url);
            img1.UriSource = new Uri(applicantResumeFile.Url.ToString());
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            DataTransferManager dtManager = DataTransferManager.GetForCurrentView();
            dtManager.DataRequested += dtManager_DataRequested;
        }

        private async void dtManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            e.Request.Data.Properties.Title = "Share ";
            e.Request.Data.Properties.Description = "Here are new Application in Windows Store.";
            e.Request.Data.SetWebLink(new Uri("http://store.wondows/FamilyLinks"));
        }
        // Click Button to share Web Link
        private void btnShareLink_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }


        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }


        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
       

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
                DrawerLayout.CloseDrawer();
            else
                DrawerLayout.OpenDrawer();
        }

        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
                DrawerLayout.CloseDrawer();
            else
                DrawerLayout.OpenDrawer();
        }




        private void Home_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FamilyList));
        }

        private void Profil_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(lifeStory));
        }

        private void Tree_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Familys));
        }

        private void Search_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mediatheque));
        }

        private void Logout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ParseUser.LogOut();
            // var currentUser = ParseUser.CurrentUser;
            Frame.Navigate(typeof(Authent));
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sharedInformation.sharedSelectedPerson= lstSource[lst.SelectedIndex];
            Frame.Navigate(typeof(ProfilSelected));
        }

        private void AppBarButton_Click1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CalculLove));
        }
    }
}
