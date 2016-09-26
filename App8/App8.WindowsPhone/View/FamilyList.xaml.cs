using App8.Common;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FamilyList : Page
    {
        public static Person sharedPerson;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public FamilyList()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            DrawerLayout.InitializeDrawerLayout();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.jibImage1();
            IEnumerable<Belongs> groups =
                from item in SourceData.GetData()
                group item by item.belongs
                    into Bel
                let belongsItems =
                    from item2 in Bel
                    group item2 by item2.Rela
                    into RelaG
                    select new RelationGroup(RelaG)
                    {
                        RelationG = RelaG.Key
                    }
                select new Belongs(belongsItems)
                {
                    Belong = Bel.Key
                };
            var cvs = (CollectionViewSource)Resources["src"];
            cvs.Source = groups.ToList();
        }

        private void jibImage1()
        {
            var applicantResumeFile = sharedInformation.SahredParsePerson.Get<ParseFile>("image");
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

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

        private async void item_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (lst.SelectedIndex != -1)
            {

                var dialog = new MessageDialog(lst.SelectedIndex+"");
                await dialog.ShowAsync();
                sharedInformation.sharedSelectedPerson = (Person)lst.SelectedItem;
               // this.Frame.Navigate(typeof(ProfilSelected));
            }
            //maListe.RemoveAt(ListeGraphique.SelectedIndex);
            //IsolatedStorageHelper.SaveObject("ListeActeurs", maListe);


        }
    }
}