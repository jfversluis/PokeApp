using Newtonsoft.Json;
using PokeApp.Helpers;
using PokeApp.Models;
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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Constants.AppIdentifier}:{Constants.AppSecret}")));

                var result = await httpClient.PostAsync("/jwt/token", new MultipartFormDataContent());
                var resultString = await result.Content.ReadAsStringAsync();

                var resultJwt = JsonConvert.DeserializeObject<JwtToken>(resultString);

                Settings.TokenType = resultJwt.TokenType;
                Settings.Token = resultJwt.Token;

                HttpStatusLabel.Text = result.StatusCode.ToString();
                ResultLabel.Text = "I've got tha powah!";
            }
        }

        private async void Button1_OnClicked(object sender, EventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Constants.BaseApiUrl);

                if (!string.IsNullOrWhiteSpace(Settings.TokenType) && !string.IsNullOrWhiteSpace(Settings.Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Settings.TokenType, Settings.Token);

                var result = await httpClient.GetAsync("/");
                var resultString = await result.Content.ReadAsStringAsync();

                HttpStatusLabel.Text = $"{result.StatusCode}";
                ResultLabel.Text = resultString;
            }
        }

        private void Button2_OnClicked(object sender, EventArgs e)
        {
            Settings.TokenType = string.Empty;
            Settings.Token = string.Empty;

            HttpStatusLabel.Text = string.Empty;
            ResultLabel.Text = "Poof, it's gone";
        }
    }
}