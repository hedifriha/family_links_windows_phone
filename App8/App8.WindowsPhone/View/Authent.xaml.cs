using App8.Common;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
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
    public sealed partial class Authent : Page
    {
        ParseObject currentPerson;
        private List<Person> listU = new List<Person>();
        private bool cnx;
        public Authent()
        {
            this.InitializeComponent();
            cnx = false;
        }

        private async void CnxSimple_Click(object sender, RoutedEventArgs e)
        {

            string usernameText = username.Text;
            string message = "";
            string passwordText = password.Password;

            cnx = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (!cnx)
            {
                message = "Check your connection to network";
                var alert = new MessageDialog(message);
                await alert.ShowAsync();
            }
            else if (usernameText == "") message = "You must log in. Login Required";
            else
            {
                try
                {
                    await ParseUser.LogInAsync(usernameText, passwordText);
                    // Login was successful.  

                    ParseQuery<ParseObject> query = ParseObject.GetQuery("Person").WhereEqualTo("User", ParseUser.CurrentUser);
                    currentPerson = await query.FirstAsync();
                    sharedInformation.SahredParsePerson = currentPerson;
                    Person p = new Person()
                    {
                        personId = currentPerson.ObjectId,
                        Name = currentPerson.Get<String>("FirstName"),
                        LastName = currentPerson.Get<String>("LastName"),
                        Dob = currentPerson.Get<String>("DOB"),
                        Gender = currentPerson.Get<String>("Gender"),
                        Job = currentPerson.Get<String>("Job"),
                        Country = currentPerson.Get<String>("Country"),
                        Father = currentPerson.Get<ParseObject>("FatherP"),
                        Mother = currentPerson.Get<ParseObject>("MotherP"),
                        Spouse = currentPerson.Get<ParseObject>("SpouseP"),
                        FatherId = currentPerson.Get<ParseObject>("FatherP").ObjectId,
                        MotherId = currentPerson.Get<ParseObject>("MotherP").ObjectId,
                        SpouseId = currentPerson.Get<ParseObject>("SpouseP").ObjectId,
                    };
                    sharedInformation.SahredConnectedPerson = p;
                    Frame.Navigate(typeof(lifeStory));
                    message = "You are now logged in";//ParseUser.CurrentUser.Get<String>("LastName"); //
                }
                catch (ParseException ex)
                {
                    message = "Wrong Username/password";// "Wrong Username/Password";
                }
            }
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            cnx = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (!cnx)
            {
                var alert = new MessageDialog("Check your connection to network");
                await alert.ShowAsync();
            }
            else
                Frame.Navigate(typeof(Register));
        }
    }
}
   