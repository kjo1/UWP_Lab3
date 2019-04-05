using AWClient.DAL;
using AWClient.Models;
using AWClient.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AWClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            fillDropDown();
        }

        private async void fillDropDown()
        {
            //Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            ArtTypeRepository r = new ArtTypeRepository();
            try
            {
                List<ArtType> artTypes = await r.GetArtType();
                //Add the All Option
                artTypes.Add(new ArtType { ID = 0, Type = " - All Art Types" });
                //Save them for the Patient Details 
                App thisApp = Application.Current as App;
                thisApp.allTypes = artTypes;
                //Bind to the ComboBox
                ArtTypeCombo.ItemsSource = artTypes.OrderBy(at => at.Type);
                btnAdd.IsEnabled = true; //Since we have doctors, you can try to add new.
                showArtWorks(null);
            }

            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                Jeeves.ShowMessage("Could not complete operation:", sb.ToString());
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation.");
                }
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private async void showArtWorks(int? TypeID)
        {
            //Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            ArtWorkRepository r = new ArtWorkRepository();
            try
            {
                List<ArtWork> artWorks;
                if (TypeID.GetValueOrDefault() > 0)
                {
                    artWorks = await r.GetArtWorksByArtType(TypeID.GetValueOrDefault());
                }
                else
                {
                    artWorks = await r.GetArtWorks();
                }
                artWorkList.ItemsSource = artWorks.OrderByDescending(aw => aw.ID);

            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                Jeeves.ShowMessage("Could not complete operation:", sb.ToString());
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation.");
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }


        private void artWorkList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ArtWork artWork = (ArtWork)e.ClickedItem;
            Frame.Navigate(typeof(ArtWorkEdit), artWork);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            fillDropDown();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ArtWork artWork = new ArtWork();
            Frame.Navigate(typeof(ArtWorkEdit), artWork);
        }

        private void ArtTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int? typeID = ((ArtType)ArtTypeCombo.SelectedItem)?.ID;
            showArtWorks(typeID);
        }
    }
}
