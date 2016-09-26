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
using Windows.UI;
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
    public sealed partial class Register : Page
    {
        private bool cnx;
        CoreApplicationView view;
        ParseFile parseFile;
        String dobU;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public Register()
        {
            this.InitializeComponent();
            cnx = false;
            dobU = "3/12/2015";
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }
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
            string message = "";
            cnx = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (!cnx)
            {
                message = "Check your connection to network";
                var alert = new MessageDialog(message);
                await alert.ShowAsync();
            }
            else
            {
               
                    if (parseFile == null)
                    {
                        ParseQuery<ParseObject> query = ParseObject.GetQuery("Person");
                        ParseObject unknown = await query.GetAsync("RyQlY5U6UM");
                        parseFile = unknown.Get<ParseFile>("image");
                    }
                if (name.Text == "")
                {
                   name.Background = new SolidColorBrush(Colors.Red);

                    message = "verify your first name";
                }
                else if (lastname.Text == "")
                {
                    lastname.Background = new SolidColorBrush(Colors.Red);

                    message = "verify your lastname";
                }
                else if (username.Text == "")
                {
                    username.Background = new SolidColorBrush(Colors.Red);

                    message = "verify your username";
                }
                else if (email.Text == "")
                {
                    email.Background = new SolidColorBrush(Colors.Red);

                    message = "verify your email";
                }
                else if (!(email.Text.Contains(".")&& email.Text.Contains("@")))
                {
                  //  email.Background = new SolidColorBrush(Colors.Red);
                    email.Foreground = new SolidColorBrush(Colors.Red);
                    message = "verify your email";
                }
                else if ((password.Password == "") && (password.Password != password2.Password))
                {
                    password.Background = new SolidColorBrush(Colors.Red);

                    message = "verify your password";
                }
                else if (job.Text == "")
                {
                    job.Background = new SolidColorBrush(Colors.Red);

                    message = "verify your job";
                }
                else if (country.Text == "")
                {
                    country.Background = new SolidColorBrush(Colors.Red);

                    message = "verify your country";
                }
                else
                    {
                        ParseUser User = new ParseUser()
                        {
                            Username = username.Text,
                            Password = password.Password,
                            Email = email.Text,
                        };
                    try { await User.SignUpAsync(); }
                    catch (ParseException exp)
                    {
                        var dialog = new MessageDialog(exp.Message);
                        await dialog.ShowAsync();
                    }//"User Email/Username"; }

                    sharedInformation.SahredParseUser = User;
                        // other fields can be set just like with ParseObject
                        var Person = new ParseObject("Person");
                        Person["User"] = User;
                        Person["FirstName"] = name.Text;
                        Person["LastName"] = lastname.Text;
                        Person["Email"] = email.Text;
                        Person["DOB"] = dobU;
                        Person["Job"] = job.Text;
                        Person["Country"] = country.Text;
                        Person["FatherP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
                        Person["MotherP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
                        Person["SpouseP"] = ParseObject.CreateWithoutData("Person", "RyQlY5U6UM");
                        Person["image"] = parseFile;
                        

                    if (genderSwitch.IsOn)
                            Person["Gender"] = "Female";
                        else
                            Person["Gender"] = "Male";
                        // User["MotherName"] = mother.Text;
                        // User["FatherName"] = father.Text;
                        await Person.SaveAsync();
                        sharedInformation.SahredParsePerson = Person;
                        Frame.Navigate(typeof(Register2));
                    }
                    if (!message.Equals(""))
                    {
                        var dialog = new MessageDialog(message);
                        await dialog.ShowAsync();
                    }
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

                parseFile = new ParseFile("userWP.jpg", bytes);
                await parseFile.SaveAsync();

            }
        }
    }
}