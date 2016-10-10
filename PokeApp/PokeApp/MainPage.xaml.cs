using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace PokeApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Constants.BaseApiUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Constants.AppIdentifier}:{Constants.AppSecret}")));

                var result =
                    await httpClient.PostAsync("/jwt/token", new MultipartFormDataContent());

                TokenLabel.Text = await result.Content.ReadAsStringAsync();
            }
        }
    }
}