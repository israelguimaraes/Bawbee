using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bawbee.Mobile.Helpers
{
    public class NavigationHelper
    {
        public static Page CurrentPage => Application.Current.MainPage;

        private static INavigation Navigation => CurrentPage.Navigation;

        public static async Task PushAsync(Page page)
        {
            await Navigation.PushAsync(page);
        }
    }
}
