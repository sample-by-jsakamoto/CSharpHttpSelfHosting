using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Owin;

// 下記のとおり Microsoft.AspNet.WebApi.OwinSelfHost NuGet パッケージをインストールしておくこと。
//
// PM> Install-Package Microsoft.AspNet.WebApi.OwinSelfHost


[assembly: OwinStartup(typeof(HttpOwinServerApp.OwinServerProgram))]

namespace HttpOwinServerApp
{
    // 下記 PowerShell コードでも動作を確認できる。
    // 
    // @{Name="J.Sakamoto"} | ConvertTo-Json | Invoke-RestMethod -Uri "http://localhost:8080/api/greeting" -Method Post -ContentType "application/json"

    class OwinServerProgram
    {
        static void Main(string[] args)
        {
            using (var webApp = WebApp.Start("http://+:8080/"))
            {
                Console.WriteLine("Enter any key to abort.");
                Console.ReadKey(intercept: true);
            }
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }
    }
}
