using Friendly.Blazor;
using Friendly.Blazor.Inside.Protocol;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Threading;

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
        public void Test1()
        {
            _driver!.Url = "https://localhost:7128/counter";

            while(_driver.Title != "Counter") Thread.Sleep(100);


            var app = new BlazorAppFriend(_driver);

            string test = app.Type("BlazorApp.Sample1").Test();
            int test2 = app.Type("BlazorApp.Sample1").Test2();
            int test3 = app.Type("BlazorApp.Sample1").Test3;
            app.Type("BlazorApp.Sample1").Test3 = 10;
            var sample2 = app.Type("BlazorApp.Sample1").Sample2;
            sample2.Value = 100;

            //カウンター画面取得
            var counter = app.FindComponentByType("BlazorApp.Pages.Counter");

            //操作
            counter.currentCount = 1000;
            counter.StateHasChanged();
        }
    }
}