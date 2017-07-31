using System;
using System.Net;
using Newtonsoft.Json;

namespace HttpServerApp
{
    public class Person
    {
        public string Name { get; set; }
    }

    // 下記 PowerShell コードでも動作を確認できる。
    // 
    // @{Name="J.Sakamoto"} | ConvertTo-Json | Invoke-RestMethod -Uri "http://localhost:8080/" -Method Post

    class ServerProgram
    {
        static void Main(string[] args)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://+:8080/");
            listener.Start();

            for (;;)
            {
                // クライアントからの要求待ち (ブロッキング)
                var context = listener.GetContext();

                // 要求から JSON 文字列を取り出して、オブジェクトに逆シリアル化
                var encoding = context.Request.ContentEncoding;
                var buff = new byte[context.Request.ContentLength64];
                context.Request.InputStream.Read(buff, 0, buff.Length);
                var person = JsonConvert.DeserializeObject<Person>(encoding.GetString(buff));

                // 応答用の JSON 文字列を、匿名型オブジェクトから生成
                var responseJson = JsonConvert.SerializeObject(new { Message = $"Hello, {person.Name}." });
                var contents = encoding.GetBytes(responseJson);

                // クライアントに返信
                var response = context.Response;
                response.ContentType = "application/json";
                response.OutputStream.Write(contents, 0, contents.Length);
                response.Close();
            }
        }
    }
}
