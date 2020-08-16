/////////////////////////////////////////
///         R4TSquad-DTool           ///
///     Created By: Mango#3580      ///
//////////////////////////////////////

using System;
using System.Collections.Specialized;
using System.Net;

namespace R4TSquad_DTool
{
    public class DiscordWeb : IDisposable
    {
        public string WebHook { get; set; }
        private readonly WebClient WC;
        private static NameValueCollection DV = new NameValueCollection();

        public DiscordWeb()
        {
            WC = new WebClient();
        }

        public void SendMessage(string msgSend)
        {
            DV.Add("content", msgSend);
            WC.UploadValues(WebHook, DV);
        }

        public void Dispose() {}
    }
}
