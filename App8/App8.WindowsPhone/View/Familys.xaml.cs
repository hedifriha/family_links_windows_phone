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
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using App8.Model;
using Windows.UI.Popups;



// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class Familys : Page
    {
        Person father;
        Person mother;
        Person spouse;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public int i = 10;
        public Familys()
        {
            this.InitializeComponent();
            DrawerLayout.InitializeDrawerLayout();
            this.jibImage();
            father = sharedInformation.SahredConnectedFather;
            mother = sharedInformation.SahredConnectedMother;
            spouse = sharedInformation.sharedSpouse; ;
            this.jibImageFather();
            this.jibImageMother();
            this.jibImageSpouse();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            IEnumerable<Belongs2> groups =
               from item in SourceData2.GetData()
               group item by item.belongs
                   into Bel
               let belongsItems =
                   from item2 in Bel
                   group item2 by item2.Rela
                   into RelaG
                   select new RelationGroup2(RelaG)
                   {
                       RelationG = RelaG.Key
                   }
               select new Belongs2(belongsItems)
               {
                   Belong = Bel.Key
               };
            var cvs = (CollectionViewSource)Resources["src2"];
            cvs.Source = groups.ToList();




            IEnumerable<Belongs2> groups1 =
                  from item in SourceData2.GetData2()
                  group item by item.belongs
                      into Bel
                  let belongsItems =
                      from item2 in Bel
                      group item2 by item2.Rela
                      into RelaG
                      select new RelationGroup2(RelaG)
                      {
                          RelationG = RelaG.Key
                      }
                  select new Belongs2(belongsItems)
                  {
                      Belong = Bel.Key
                  };
            var cvs1 = (CollectionViewSource)Resources["src3"];
            cvs1.Source = groups1.ToList();

            IEnumerable<Belongs2> groupsP =
                  from item in SourceData2.GetDataP()
                  group item by item.belongs
                      into Bel
                  let belongsItems =
                      from item2 in Bel
                      group item2 by item2.Rela
                      into RelaG
                      select new RelationGroup2(RelaG)
                      {
                          RelationG = RelaG.Key
                      }
                  select new Belongs2(belongsItems)
                  {
                      Belong = Bel.Key
                  };
            var cvs2 = (CollectionViewSource)Resources["src5"];
            cvs2.Source = groupsP.ToList();

            IEnumerable<Belongs2> groupsS =
                 from item in SourceData2.GetDataS()
                 group item by item.belongs
                     into Bel
                 let belongsItems =
                     from item2 in Bel
                     group item2 by item2.Rela
                     into RelaG
                     select new RelationGroup2(RelaG)
                     {
                         RelationG = RelaG.Key
                     }
                 select new Belongs2(belongsItems)
                 {
                     Belong = Bel.Key
                 };
            var cvs4 = (CollectionViewSource)Resources["src4"];
            cvs4.Source = groups.ToList();
        }
        private async void jibImage()
        {
            try
            {
                var applicantResumeFile = sharedInformation.SahredParsePerson.Get<ParseFile>("image");
                img.Source = new BitmapImage(new Uri(applicantResumeFile.Url.ToString()));
                img1.UriSource = new Uri(applicantResumeFile.Url.ToString());
            }
            catch (Exception exp)
            {
                var dialog = new MessageDialog("Mei" + exp.Message);
                await dialog.ShowAsync();
            }
        }
        private async void jibImageFather()
        {
            try
            {
                if (!father.personId.Equals("RyQlY5U6UM"))
                {
                    var applicantResumeFile = father.Imagel;
                    imgf.Source = applicantResumeFile;
                    namef.Text = father.Name;
                }
            }
            catch (Exception exp)
            {
                var dialog = new MessageDialog("fi"+exp.Message);
                await dialog.ShowAsync();
            }
        }
        private async void jibImageMother()
        {
            try
            {
                if (!mother.personId.Equals("RyQlY5U6UM"))
                {
                    var applicantResumeFile = mother.Imagel;
                    imgm.Source = applicantResumeFile;
                    namem.Text = mother.Name;
                }
            }
            catch (Exception exp)
            {
                var dialog = new MessageDialog("mi"+exp.Message);
                await dialog.ShowAsync();
            }
        }

        private async void jibImageSpouse()
        {
            try
            {
                if (!spouse.personId.Equals("RyQlY5U6UM")) {
                    var applicantResumeFile = spouse.Imagel;
                    imgS.Source = applicantResumeFile;
                    NameS.Text = spouse.Name;
                }


            }
            catch (Exception exp)
            {
               
                var dialog = new MessageDialog("si"+exp.Message);
                await dialog.ShowAsync();
            }
        }


        private void goParent(object sender, TappedRoutedEventArgs e)
        {
            /*   i++;
               string f = personShown.SelectedText.ToString();*/

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

        #region NavigationHelper registration

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
            DataTransferManager dtManager = DataTransferManager.GetForCurrentView();
            dtManager.DataRequested += dtManager_DataRequested;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

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



        private async void dtManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            e.Request.Data.Properties.Title = "Family Links";
            e.Request.Data.Properties.Description = "Here are new application for Windows Phone.";
            e.Request.Data.SetWebLink(new Uri("http://code.msdn.com/FamilyLinks"));
        }
        // Click Button to share Web Link
        private void btnShareLink_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();

        }

    }
}
