using App8.Common;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App8
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddRelation : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        String dobU = "1/1/1997";
        String lastName;
        ParseObject currentperson;
        CoreApplicationView view;
        ParseFile parseFile;
        BitmapImage image;
        bool cnx;

        public AddRelation()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.getImage();
            typeLinkLabel.Text = typeLink;
            currentperson = sharedInformation.SahredParsePerson;
            lastName = currentperson.Get<string>("LastName");
            cnx = false;
            //byte[] data = System.Text.Encoding.UTF8.GetBytes("Working at Parse is great!");
            //parseFile = new ParseFile(@"Assets/Father.jpg");

            if (typeLink != "Spouse") { lastname.Text = lastName; }
            
        }

        public async void getImage()
        {
            cnx = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (!cnx)
            {
                var alert = new MessageDialog("Check your connection to network");
                await alert.ShowAsync();
            }
            else
            {
                ParseQuery<ParseObject> query = ParseObject.GetQuery("Person");
                ParseObject unknown = await query.GetAsync("RyQlY5U6UM");
                parseFile = unknown.Get<ParseFile>("image");
            }

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
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        String typeLink = sharedInformation.SahredTypeNewLink;
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>


        private async void submit_add_relation_button(object sender, RoutedEventArgs e)
        {
            cnx = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (!cnx)
            {
                var alert = new MessageDialog("Check your network connexion");
                await alert.ShowAsync();
            }
            else
            {
                //lastname.Text = sharedInformation.SharedLastName;
                var Person = new ParseObject("Person");
                Person["FirstName"] = name.Text;
                Person["LastName"] = lastname.Text;
                Person["DOB"] = dobU;
                Person["Job"] = Job.Text;
                Person["Country"] = Country.Text;
                Person["SpouseP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
                Person["image"] = parseFile;
                if (genderSwitch.IsOn)
                    Person["Gender"] = "Female";
                else
                    Person["Gender"] = "Male";

                if (typeLink == "Spouse")
                {
                    Person["SpouseP"] = currentperson;
                    await currentperson.SaveAsync();
                }
                else if (typeLink == "Children")
                {
                    if (currentperson.Get<String>("Gender") == "Male")
                    {
                        Person["FatherP"] = currentperson;
                        Person["MotherP"] = ParseObject.CreateWithoutData("Person", sharedInformation.SahredConnectedPerson.SpouseId);
                    } //Person["MotherP"] = currentperson.Get<ParseObject>("SpouseP"); }
                    else
                    {
                        Person["MotherP"] = currentperson;
                        Person["FatherP"] = ParseObject.CreateWithoutData("Person", sharedInformation.SahredConnectedPerson.SpouseId);
                    }
                }
                else if (typeLink == "Sibling")
                {
                    Person["FatherP"] = ParseObject.CreateWithoutData("Person", sharedInformation.SahredConnectedPerson.FatherId);
                    Person["MotherP"] = ParseObject.CreateWithoutData("Person", sharedInformation.SahredConnectedPerson.MotherId);

                }

                await Person.SaveAsync();

                /*ParseQuery<ParseObject> query = ParseObject.GetQuery("Person");
                ParseObject currPer = await query.GetAsync(currentperson.ObjectId);
                await currPer.SaveAsync();*/
                Frame.Navigate(typeof(Familys));
            }
            
        }

        private void dateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            dobU = dob.Date.ToString();//Getting datepicker date value with Date Propert. 
        }

        private void Button_Click(object sender, RoutedEventArgs e) //button xaml
        {
            view = CoreApplication.GetCurrentView();
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types
            filePicker.FileTypeFilter.Clear();
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".jpg");

            filePicker.PickSingleFileAndContinue();
            view.Activated += viewActivated;
        }

        private async void viewActivated(CoreApplicationView sender, IActivatedEventArgs args1)
        {
            FileOpenPickerContinuationEventArgs args = args1 as FileOpenPickerContinuationEventArgs;

            if (args != null)
            {
                if (args.Files.Count == 0) return;
                view.Activated -= viewActivated;
                StorageFile storageFile = args.Files[0];
                var stream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                byte[] bytes = new Byte[stream.Size];
                await stream.AsStream().ReadAsync(bytes, 0, bytes.Length);
                parseFile = new ParseFile("newRelationWP.jpg", bytes);
                await parseFile.SaveAsync();

            }
        }
    }
}
