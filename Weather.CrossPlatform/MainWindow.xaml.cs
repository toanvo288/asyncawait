using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Weather.Core.Domain;
using Weather.Core.Services;

namespace Weather.CrossPlatform
{
    public class MainWindow : Window
    {
        public DataGrid WeatherViews => this.FindControl<DataGrid>(nameof(WeatherViews));
        public ProgressBar WeatherProgress => this.FindControl<ProgressBar>(nameof(WeatherProgress));
        public TextBox WeatherIdentifier => this.FindControl<TextBox>(nameof(WeatherIdentifier));
        public Button Search => this.FindControl<Button>(nameof(Search));
        public TextBox Notes => this.FindControl<TextBox>(nameof(Notes));
        public TextBlock WeatherStatus => this.FindControl<TextBlock>(nameof(WeatherStatus));
        public TextBlock DataProvidedBy => this.FindControl<TextBlock>(nameof(DataProvidedBy));
       

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            /// Data provided for free by <a href="https://iextrading.com/developer/" RequestNavigate="Hyperlink_OnRequestNavigate">IEX</Hyperlink>. View <Hyperlink NavigateUri="https://iextrading.com/api-exhibit-a/" RequestNavigate="Hyperlink_OnRequestNavigate">IEX’s Terms of Use.</Hyperlink>
        }

        private static string API_URL = "https://ps-async.fekberg.com/api/stocks";
        private Stopwatch stopwatch = new Stopwatch();

        // private void Search_Click(object sender, RoutedEventArgs e)
        // {
        //     WeatherStatus.Text = "Start Processing... ";
        //     Thread.Sleep(2000);
        //     WeatherStatus.Text = "Finish Processing";
        // }
        
        // private async void Search_Click(object sender, RoutedEventArgs e)
        // {
        //     WeatherStatus.Text = "Start Processing... ";
        //     await Task.Delay(2000);
        //     WeatherStatus.Text = "Finish Processing";
        // }
        
        // Async void
        // private void Search_Click(object sender, RoutedEventArgs e)
        // {
        //     try
        //     {
        //         WeatherStatus.Text = "Start Downloading ... ";
        //         var weatherService = new WeatherService();
        //         weatherService.DownloadWeatherCity(WeatherIdentifier.Text);
        //     }
        //     catch (Exception ex)
        //     {
        //         WeatherStatus.Text = "Error happened: " + ex.Message;
        //     }
        //     finally
        //     {
        //         WeatherStatus.Text = "Finish Downloading";
        //     }
        // }
        
        // private void Search_Click(object sender, RoutedEventArgs e)
        // {
        //     try
        //     {
        //         if (string.IsNullOrEmpty(WeatherIdentifier.Text))
        //         {
        //             return;
        //         }
        //
        //         BeforeLoadingWeatherData();
        //         LoadWeather(WeatherIdentifier.Text);
        //     }
        //     finally
        //     {
        //         AfterLoadingWeatherData();
        //     }
        // }
        
        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(WeatherIdentifier.Text))
                {
                    return;
                }
        
                BeforeLoadingWeatherData();
                await LoadWeatherAsync(WeatherIdentifier.Text);
            }
            finally
            {
                AfterLoadingWeatherData();
            }
        }
        
        private void BeforeLoadingWeatherData()
        {
            stopwatch.Restart();
            WeatherProgress.IsVisible = true;
            WeatherProgress.IsIndeterminate = true;
        }

        private void AfterLoadingWeatherData()
        {
            WeatherStatus.Text = $"Loaded weather for {WeatherIdentifier.Text} city in {stopwatch.ElapsedMilliseconds}ms";
            WeatherProgress.IsVisible = false;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                desktopLifetime.Shutdown();
            }
        }

        private void LoadWeather(string cityName)
        {
            var weatherService = new WeatherService();
            var data = weatherService.GetWeatherOfCity(cityName).ToList();
            // This is the same as ItemsSource in WPF 
            WeatherViews.Items = data;
        }
        
        private async Task LoadWeatherAsync(string cityName)
        {
            var weatherService = new WeatherService();
            var data = await weatherService.GetWeatherOfCityAsync(cityName, default(CancellationToken));
            // This is the same as ItemsSource in WPF 
            WeatherViews.Items = data;
        }
    }
}
