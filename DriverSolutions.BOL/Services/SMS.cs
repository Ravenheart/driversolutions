using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Services
{
    public class SMS
    {
        private Guid _SubscriptionId;
        private string _BaseAddress;
        private NetworkCredential _Credentials;

        public SMS()
            : this(
            GLOB.Settings.Get<string>(12),//Gateway
            GLOB.Settings.Get<string>(13),//ApiKey
            GLOB.Settings.Get<string>(14),//ApiKey secret
            GLOB.Settings.Get<Guid>(15))//Subscription id
        {
        }

        public SMS(string baseAddress, string apiKey, string apiSecret, Guid subscriptionId)
        {
            _BaseAddress = baseAddress;
            _Credentials = new NetworkCredential(apiKey, apiSecret);
            _SubscriptionId = subscriptionId;
        }

        /// <summary>
        /// Send an SMS
        /// </summary>
        /// <param name="source">Sender name/number</param>
        /// <param name="destination">EE164 formatted phone number</param>
        /// <param name="message">Message text</param>
        /// <returns></returns>
        public bool SendSMS(string source, string destination, string message)
        {
            string url = string.Format("{4}/v1/{3}/sms?source={0}&destination={1}&message={2}",
                Uri.EscapeUriString(source),
                destination,
                Uri.EscapeUriString(message),
                _SubscriptionId.ToString(),
                this._BaseAddress);

            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Credentials = this._Credentials;
            req.Method = "POST";
            req.ContentType = "application/json";

            byte[] data = Encoding.ASCII.GetBytes("{ }");
            req.GetRequestStream().Write(data, 0, data.Length);

            var resp = req.GetResponse() as HttpWebResponse;
            return resp.StatusCode == HttpStatusCode.OK;
        }
    }
}
