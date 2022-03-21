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


## You also need to install the product code

```csproj
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="6.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="6.0.1" PrivateAssets="all" />
    <PackageReference Include="Selenium.Friendly.Blazor" Version="0.4.0" />
  </ItemGroup>

</Project>
```

## Test code using Selenium

Refer here for details on how to operate Friendly.


https://github.com/Codeer-Software/Friendly


```cs
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