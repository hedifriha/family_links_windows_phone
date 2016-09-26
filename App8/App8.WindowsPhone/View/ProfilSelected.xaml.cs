using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ProfilSelected : Page
    {
        public ProfilSelected()
        {
            this.InitializeComponent();
            DrawerLayout.InitializeDrawerLayout();
            Person p1 = sharedInformation.sharedSelectedPerson;
            NameU.Text = p1.Name;
            LastNameU.Text = p1.LastName;
            textimg.Source= p1.Imagel;

        } 

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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
    }
}
  
