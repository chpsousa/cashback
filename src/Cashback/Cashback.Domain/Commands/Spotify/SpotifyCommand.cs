using Cashback.Domain.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Cashback.Domain.Commands.Spotify
{
    public class SpotifyCommand : ISpotifyCommand
    {
        public string AccessToken { get; set; }

        public void Authorize()
        {
            var url = "https://accounts.spotify.com/api/token";
            var clientId = "";
            var clientSecret = "";

            //request to get the access token
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, clientSecret)));

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Basic " + data);

            byte[] reqBytes = Encoding.ASCII.GetBytes("grant_type=client_credentials");
            webRequest.ContentLength = reqBytes.Length;

            Stream strm = webRequest.GetRequestStream();
            strm.Write(reqBytes, 0, reqBytes.Length);
            strm.Close();

            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            String json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json and parse for accesstoken
                    json = rdr.ReadToEnd();
                    rdr.Close();
                    SpotifyToken token = JsonConvert.DeserializeObject<SpotifyToken>(json);
                    this.AccessToken = token.access_token;
                }
            }
        }
    }
}
