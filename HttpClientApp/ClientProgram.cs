using System;
using System.Net.Http;
using System.Threading.Tasks;

// 下記のとおり Microsoft.AspNet.WebApi.Client NuGet パッケージをインストールしておくこと。
//
// PM> Install-Package Microsoft.AspNet.WebApi.Client

namespace HttpClientApp
{
    public class Person
    {
        public string Name { get; set; }
    }

    public class Greeting
    {
        public string Message { get; set; }
    }

    class ClientProgram
    {
        static void Main(string[] args) => MainAsync().Wait();

        static async Task MainAsync()
        {
            var person = new Person { Name = "J.Sakamoto" };
            var webApiUrl = "http://localhost:8080/api/greeting";
            var httpClient = new HttpClient();

            // オブジェクトを引数に HHTTP POST 実行
            var response = await httpClient.PostAsJsonAsync(webApiUrl, person);

            // サーバーからの返信をオブジェクトにマップして取得、結果をコンソールに表示
            var greeting = await response.Content.ReadAsAsync<Greeting>();
            Console.WriteLine(greeting.Message);
        }
    }
}
