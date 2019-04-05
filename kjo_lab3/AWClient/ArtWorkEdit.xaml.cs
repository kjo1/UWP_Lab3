using AWClient.Models;
using AWClient.DAL;
using AWClient.Utilities;
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
using System.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AWClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArtWorkEdit : Page
    {
        public ArtWorkEdit()
        {
            this.InitializeComponent();
            fillDropDown();
        }

        private async void fillDropDown()
        {

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
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            view = (ArtWork)e.Parameter;
            this.DataContext = view;

            if (view.ID == 0) //Adding
            {
                //Disable the delete button if adding
                btnDelete.IsEnabled = false;
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                view.ArtType = null;
                ArtWorkRepository er = new ArtWorkRepository();
                if (view.ID == 0)
                {
                    await er.AddArtWork(view);
                }
                else
                {
                    await er.UpdateArtWork(view);
                }
                Frame.GoBack();
            }
            catch (AggregateException ex)
            {
                string errMsg = "";
                foreach (var exception in ex.InnerExceptions)
                {
                    errMsg += Environment.NewLine + exception.Message;
                }
                Jeeves.ShowMessage("One or more exceptions has occurred:", errMsg);
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                //sb.AppendLine(string.Format(" HTTP Status Code: {0}", apiEx.StatusCode.ToString()));
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                Jeeves.ShowMessage("Problem Saving the Patient:", sb.ToString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation.");
                }
            }

        }
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string strTitle = "Confirm Delete";
            string strMsg = "Are you certain that you want to delete " + view.Name + "?";
            ContentDialogResult result = await Jeeves.ConfirmDialog(strTitle, strMsg);
            if (result == ContentDialogResult.Secondary)
            {
                try
                {
                    view.ArtType = null;
                    ArtWorkRepository er = new ArtWorkRepository();
                    await er.DeleteArtWork(view);
                    Frame.GoBack();
                }
                catch (AggregateException ex)
                {
                    string errMsg = "";
                    foreach (var exception in ex.InnerExceptions)
                    {
                        errMsg += Environment.NewLine + exception.Message;
                    }
                    Jeeves.ShowMessage("One or more exceptions has occurred:", errMsg);
                }
                catch (ApiException apiEx)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Errors:");
                    foreach (var error in apiEx.Errors)
                    {
                        sb.AppendLine("-" + error);
                    }
                    Jeeves.ShowMessage("Problem Deleting the Patient:", sb.ToString());
                }
                catch (Exception)
                {
                    Jeeves.ShowMessage("Error", "Error Deleting Patient");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        ArtWork view;

    }
}
