# Selenium.Friendly.Blazor

<br>
The status of this library is alpha.
<br>

## Features ...
This is a library for manipulating Blazor's internal objects from the outside using Selenium.


## Getting Started
Install Selenium.Friendly.Blazor from NuGet

    PM> Install-Package Selenium.Friendly.Blazor
https://www.nuget.org/packages/Selenium.Friendly.Blazor/


## Must also be used in the product code

```razor  
<Router AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
        <FocusOnNavigate RouteData="@routeData" Selector="h1" />
    </Found>
    <NotFound>
        <PageTitle>Not found</PageTitle>
        <LayoutView Layout="@typeof(MainLayout)">
            <p role="alert">Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>

@code {
    protected override void OnInitialized()
        // It can be operated from the outside by calling this method.
        => Selenium.Friendly.Blazor.BlazorController.Initialize(this);
}
```

## Test code using Selenium

Refer here for details on how to operate Friendly.


https://github.com/Codeer-Software/Friendly


```razor  
using Selenium.Friendly.Blazor;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System;

namespace SeleniumTest
{
    public class Tests
    {
        ChromeDriver? _driver;

        [SetUp]
        public void Setup() => _driver = new ChromeDriver();

        [TearDown]
        public void TearDown()=>_driver?.Dispose();

        [Test]
        public void Counter()
        {
            _driver!.Url = "https://localhost:7128/counter";

            //wait for loading.
            while (_driver.Title != "Counter") Thread.Sleep(100);

            var app = new BlazorAppFriend(_driver);

            //get Component
            var counter = app.FindComponentByType("BlazorApp.Pages.Counter");

            //operation
            counter.currentCount = 1000;
            counter.StateHasChanged();
        }

        [Test]
        public void FetchData()
        {
            _driver!.Url = "https://localhost:7128/fetchdata";

            //wait for loading.
            while (_driver.Title != "Weather forecast") Thread.Sleep(100);

            var app = new BlazorAppFriend(_driver);

            //get Component
            var fetchData = app.FindComponentByType("BlazorApp.Pages.FetchData");

            //create new data.
            var forecasts = app.Type("BlazorApp.Pages.FetchData+WeatherForecast[]")(1);
            forecasts[0] = app.Type("BlazorApp.Pages.FetchData+WeatherForecast")();
            forecasts[0].Date = DateTime.Now;
            forecasts[0].TemperatureC = 3;
            forecasts[0].Summary = "ABC";

            //set
            fetchData.forecasts = forecasts;
            fetchData.StateHasChanged();
        }
    }
}
```