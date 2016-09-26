using App8.Common;
using Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class Register2 : Page
    {
        String dobFather = "3/12/2015";
        String dobMother = "3/12/2015";
        bool cnx;
        ParseFile parseFile;
        CoreApplicationView view;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Register2()
        {
            this.InitializeComponent();
            this.getImage();
            cnx = false;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

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

        private async void sign_in_button(object sender, RoutedEventArgs e)
        {
            ParseUser User = sharedInformation.SahredParseUser;
            ParseObject Person = sharedInformation.SahredParsePerson;

            string message = "";
            if (father.Text == "")
            {
                father.Background = new SolidColorBrush(Colors.Red);
                message = "verify your father's name";
            }
            else if (lastname.Text == "" )
            {
                lastname.Background = new SolidColorBrush(Colors.Red);
                message = "verify your mother's lastname ";
            }
            else if(jobF.Text == "")
            {
                jobF.Background = new SolidColorBrush(Colors.Red);
                message = "verify your father's job";
            }
            else if(mother.Text == "")
            {
                mother.Background = new SolidColorBrush(Colors.Red);
                message = "verify your mother's name";
            }
            else if(jobM.Text == "")
            {
                mother.Background = new SolidColorBrush(Colors.Red);
                message = "verify your mother's job";
            }
            else { 
           var PersonF = new ParseObject("Person");
            //PersonF["User"] = Father;
            PersonF["FirstName"] = father.Text;
            PersonF["LastName"] = Person["LastName"];
            PersonF["Gender"] = "Male";
            PersonF["image"] = parseFile;
            PersonF["DOB"] = dobFather;
            PersonF["Job"] = jobF.Text;
            PersonF["Country"] = Person["Country"];
            PersonF["FatherP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
            PersonF["MotherP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
            PersonF["SpouseP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
                await PersonF.SaveAsync();

            var PersonM = new ParseObject("Person");
            PersonM["FirstName"] = mother.Text;
            PersonM["LastName"] = lastname.Text;
            PersonM["Gender"] = "Female";
            PersonM["image"] = parseFile;
            PersonM["DOB"] = dobMother;
            PersonM["Job"] = jobM.Text;
            PersonM["Country"] = Person["Country"];
            PersonM["FatherP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
            PersonM["MotherP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
            PersonM["SpouseP"] = PersonF;
            await PersonM.SaveAsync();   
                        
            PersonF["SpouseP"] = PersonM;
            await PersonF.SaveAsync();

            Person["FatherP"] = PersonF;
            Person["MotherP"] = PersonM;
            await Person.SaveAsync();
            Frame.Navigate(typeof(Authent));
            }
            if (!message.Equals(""))
            {
                var dialog = new MessageDialog(message);
                await dialog.ShowAsync();
            }           
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

                parseFile = new ParseFile("wpimage.jpg", bytes);
                await parseFile.SaveAsync();

            }
        }

        private void dobFChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            dobFather = dobF.Date.ToString();
        }

        private void dobMChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            dobMother = dobM.Date.ToString();
        }
    }
}