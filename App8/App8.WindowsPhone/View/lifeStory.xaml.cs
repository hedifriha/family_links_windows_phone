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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class lifeStory : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private List<Person> listU = new List<Person>();
        private List<Person> listP = new List<Person>();
        private List<Person> listC = new List<Person>();
        private List<Person> listB = new List<Person>();
        private List<Person> listO = new List<Person>();
        bool cnx;
        //private Person Spouse;

        public lifeStory()
        {
            this.InitializeComponent();

            DrawerLayout.InitializeDrawerLayout();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            cnx = false;
            
                this.getdata();
            
            this.jibImage();
            //DOB.Text = sharedInformation.SahredConnectedPerson.Dob;
            string ln = sharedInformation.SahredConnectedPerson.LastName;
            string fn = sharedInformation.SahredConnectedPerson.Name;

            NameU.Text = (fn + " " + ln); //ParseUser.CurrentUser.Get<String>("FirstName");  
            DOB.Text = sharedInformation.SahredConnectedPerson.Dob;
            Job.Text = sharedInformation.SahredConnectedPerson.Job;
            Country.Text = sharedInformation.SahredConnectedPerson.Country;
            sharedInformation.SharedLastName = ln;
            // var image1 = ParseUser.CurrentUser.ContainsKey("image");
            // byte[] data = System.Text.Encoding.UTF8.GetBytes("Working at Parse is great!");
            //ParseFile file = new ParseFile("image", data);
            // image = await image1.FindAsync();

            //   image =GetPhoto(photoFilePath); ParseUser.CurrentUser.Get<ParseFile>();

        }
        public async void getdata()
        {
            cnx = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (!cnx)
            {
                var alert = new MessageDialog("Check your connection to network");
                await alert.ShowAsync();
            }
            else
            {
                ParseUser User = ParseUser.CurrentUser;
                if (listU != null)
                {
                    listU = new List<Person>();
                }
                if (listC != null)
                {
                    listC = new List<Person>();
                }
                if (listB != null)
                {
                    listB = new List<Person>();
                }
                if (listP != null)
                {
                    listP = new List<Person>();
                }
                if (listO != null)
                {
                    listO = new List<Person>();
                }
                var perss = await ParseObject.GetQuery("Person").FindAsync();//.Include("FatherP")
                                                                             //var query = ParseObject.GetQuery("Comment").WhereEqualTo("post", ParseObject.CreateWithoutData("Post", "1zEcyElZ80"));

                //NameU.Text = pers.Get<ParseObject>("User").ObjectId;

                //ParseObject Fathers = pers.Get<ParseObject>("FatherP");
                //await Fathers.FetchIfNeededAsync();
                // var dialog = new MessageDialog(sharedInformation.SahredParsePerson.Get<String>("FirstName"));
                // await dialog.ShowAsync();
                foreach (var pers in perss)
                {
                   
                        Person p = new Person()
                        {
                            personId = pers.ObjectId,
                            Name = pers.Get<String>("FirstName"),
                            LastName = pers.Get<String>("LastName"),
                            Dob = pers.Get<String>("DOB"),
                            Gender = pers.Get<String>("Gender"),
                            FatherId = pers.Get<ParseObject>("FatherP").ObjectId,
                            MotherId = pers.Get<ParseObject>("MotherP").ObjectId,
                            SpouseId = pers.Get<ParseObject>("SpouseP").ObjectId,
                            Imagel = new BitmapImage(pers.Get<ParseFile>("image").Url),

                        };
                        listU.Add(p);
                    //if (p.personId.Equals("RyQlY5U6UM")) { }else

                        if (p.personId.Equals(sharedInformation.SahredConnectedPerson.FatherId)) { listP.Add(p); sharedInformation.SahredConnectedFather = p; }
                        else if ((p.FatherId.Equals(sharedInformation.SahredConnectedPerson.FatherId)) &&(!p.personId.Equals(sharedInformation.SahredConnectedPerson.personId))) listB.Add(p);
                        else if (p.FatherId.Equals(sharedInformation.SahredConnectedPerson.personId)) listC.Add(p);
                        else if (p.personId.Equals(sharedInformation.SahredConnectedPerson.MotherId)) { listP.Add(p); sharedInformation.SahredConnectedMother = p; }
                        else if (p.personId.Equals(sharedInformation.SahredConnectedPerson.SpouseId)) sharedInformation.sharedSpouse = p;
                        else listO.Add(p);                    
                }
                sharedInformation.sharedListP = listP;
                sharedInformation.sharedListC = listC;
                sharedInformation.sharedListB = listB;
                sharedInformation.sharedListO = listO;
                sharedInformation.sharedListU = listU;
            }
        }

        private void jibImage()
        {
            var applicantResumeFile = sharedInformation.SahredParsePerson.Get<ParseFile>("image");
            img.Source = new BitmapImage(new Uri(applicantResumeFile.Url.ToString()));
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

        private void AddMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (pop.IsOpen)
            {
                pop.Visibility = Visibility.Collapsed;
                pop.IsOpen = false;
            }
            else {
                pop.Visibility = Visibility.Visible;
                pop.IsOpen = true;
            }
            
        }

        /*public void add_parent_but(object sender, RoutedEventArgs e)
        {
            sharedInformation.SahredTypeNewLink = "Parent";
            Frame.Navigate(typeof(AddRelation));

        }*/

         private void addSpouseButton(object sender, RoutedEventArgs e)
        {
            sharedInformation.SahredTypeNewLink = "Spouse";
            Frame.Navigate(typeof(AddRelation));
        }

        private void addChildrenButton(object sender, RoutedEventArgs e)
        {
            sharedInformation.SahredTypeNewLink = "Children";
            Frame.Navigate(typeof(AddRelation));
        }

        private void addSiblingsButton(object sender, RoutedEventArgs e)
        {
            sharedInformation.SahredTypeNewLink = "Sibling";
            Frame.Navigate(typeof(AddRelation));
        }

    }
}