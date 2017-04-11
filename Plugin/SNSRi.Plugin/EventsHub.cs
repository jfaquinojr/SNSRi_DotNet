using Microsoft.AspNet.SignalR.Client.Hubs;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSeerAPI;
using System.Configuration;
using Microsoft.AspNet.SignalR.Client;
using System.Net;

namespace SNSRi.Plugin
{
    public class EventsHub : IEventsHub
    {
        private static IEventsHub _instance;
        private IHubProxy _hub;
        private HubConnection _hubConnection;
        private static string _url;
        

        static EventsHub()
        {
            var seqUrl = useDefault(ConfigurationManager.AppSettings["SeqUrl"], "http://localhost:5341");
            var logTxt = useDefault(ConfigurationManager.AppSettings["LogFile"], "/logs/log.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logTxt)
                .WriteTo.Seq(seqUrl)
                .CreateLogger();
            Log.Information("Created EventsHub instance at {ExecutionTime} (ctor1)", Environment.TickCount);


            string useDefault(string setting, string defaultValue)
            {
                if (string.IsNullOrEmpty(setting))
                {
                    return defaultValue;
                }
                return setting;
            }
        }

        ~EventsHub()
        {
            Log.Information("Begin EventsHub Destructor");

            if (_hubConnection != null && _hubConnection.State != ConnectionState.Disconnected)
            {
                Log.Debug("Stopping Hub Connection.");
                _hubConnection.Stop();
            }

            Log.Information("End EventsHub Destructor");
        }

        private EventsHub(string url)
        {
            Log.Information("Begin EventsHub Constructor");

            ServicePointManager.DefaultConnectionLimit = 100;
            Log.Debug("Setting DefaultConnectionLimit to " + ServicePointManager.DefaultConnectionLimit);

            Log.Debug($"Creating Hubconnection with URL: '{url}'");
            _url = url;
            _hubConnection = new HubConnection(_url);

            Log.Debug("Creating Hub Proxy");
            _hub = _hubConnection.CreateHubProxy("snsri");

            Log.Information("End EventsHub Constructor");
        }

        public static IEventsHub CreateInstance(string url)
        {
            Log.Information("Begin CreateInstance");

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("url is required", nameof(url));
            }

            if (_url != url || _instance == null)
            {
                _url = url;
                _instance = new EventsHub(_url);
                Log.Debug($"Instance Created '{_url}'.");
            }

            Log.Information("End CreateInstance");
            return _instance;
        }

        public void TransmitMessage(string msg)
        {
            Log.Information("Begin TransmitMessage");

            Log.Information("Transmitting message {Message}", msg);
            if (_hub == null)
            {
                Log.Warning("Hub on '{HubUrl}' is not yet initialized", _url);
                return;
            }

            Log.Debug($"Transmitting message '{msg}'");
            _hub.Invoke<string>("transmitEvent", msg);

            Log.Information("End TransmitMessage");
        }

        public void TransmitEvent(EventMessage eventMessage)
        {
            Log.Information("Begin TransmitEvent");

            _hubConnection.Start().ContinueWith(task=>
            {
                Log.Debug("Transmitting event with object {@EventMessage}", eventMessage);

                _hub.Invoke<string>("transmitEvent", eventMessage);

                Log.Debug("Event {@EventMessage} transmitted..", eventMessage);
            });

            Log.Information("End TransmitEvent");
        }

    }
}
