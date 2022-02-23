using Selenium.Friendly.Blazor;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System;

namespace SeleniumTest
{
    public class Tests
    {
        ChromeDriver? driver;

        [SetUp]
        public void Setup() => driver = new ChromeDriver();

        [TearDown]
        public void TearDown()=>driver?.Dispose();

        [Test]
        public void Counter()
        {
            driver!.Url = "https://localhost:7128/counter";

            //wait for loading.
            while (driver.Title != "Counter") Thread.Sleep(100);

            var app = new BlazorAppFriend(driver);

            //get Component
            var counter = app.FindComponentByType("BlazorApp.Pages.Counter");

            //operation
            counter.currentCount = 1000;
            counter.StateHasChanged();
        }

        [Test]
        public void FetchData()
        {
            driver!.Url = "https://localhost:7128/fetchdata";

            //wait for loading.
            while (driver.Title != "Weather forecast") Thread.Sleep(100);

            var app = new BlazorAppFriend(driver);

            //get Component
            var fetchData = app.FindComponentByType("BlazorApp.Pages.FetchData");

            //create new data.
            var forecasts = app.Type("BlazorApp.Pages.FetchData+WeatherForecast[]")(1);
            forecasts[0] = app.Type("BlazorApp.Pages.FetchData+WeatherForecast")();
            forecasts[0].Date = DateTime.Now;
            forecasts[0].TemperatureC = 3;
            forecasts[0].Summary = "Friendly!";

            //set
            fetchData.forecasts = forecasts;
            fetchData.StateHasChanged();
        }
    }
}