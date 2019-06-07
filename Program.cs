using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace JsonSerializationTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DeCerialization");
            string requsqURL = @"https://api.unsplash.com/photos/?client_id=a39be0792706d2793a01066693f424df220c49f582483c9d959fb1f6dbe76f38";
            WebRequest request = WebRequest.Create(requsqURL);
            WebResponse responce = request.GetResponse();
            Stream dataStream = responce.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string strResponce = reader.ReadToEnd();
            // Decerializetion
            JArray arr = JArray.Parse(strResponce);
            //List<Imgs> imgs = new List<Imgs>(arr.Children().Select(jt => jt["urls"].ToObject<Imgs>()));
            // Данный тип тоже работает со строкой
            List<string> imgs = new List<string>(arr.Children().Select(jt => jt["urls"]["thumb"].ToObject<string>()));
            foreach (var o in imgs)
            {
                Console.WriteLine(o);
            }


            responce.Close();
            dataStream.Close();
            reader.Close();
        }
    }
    class Imgs
    {
        [JsonProperty("raw")]
        public string url { get; set; }
    }
}
