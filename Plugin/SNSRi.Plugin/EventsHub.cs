using Microsoft.AspNet.SignalR.Client.Hubs;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSeerAPI;

namespace SNSRi.Plugin
{
    public class EventsHub : IEventsHub
    {
        private static IEventsHub _instance;
        private IHubProxy _hub;
        private static string _url;
        private object _lock = new object();

        static EventsHub()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
            Log.Information("Created EventsHub instance at {ExecutionTime} (ctor1)", Environment.TickCount);
        }

        private EventsHub(string url)
        {
            _url = url;
            Console.WriteLine("Inside EventsHub ctor");
        }

        public static IEventsHub CreateInstance(string url)
        {
            Log.Information("Start CreateInstance");

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("url is required", nameof(url));
            }

            if (_url != url || _instance == null)
            {
                _url = url;
                _instance = new EventsHub(_url);
                Log.Information("Instance Created.");
            }
            return _instance;
        }

        public void TransmitMessage(string msg)
        {
            Log.Information("Transmitting message {Message}", msg);
            if (_hub == null)
            {
                Log.Warning("Hub on '{HubUrl}' is not yet initialized", _url);
                return;
            }
            _hub.Invoke<string>("transmitEvent", msg);
            Log.Information("Message transmitted..");
        }

        public void TransmitEvent(EventMessage eventMessage)
        {
            Log.Information("Enter TransmitEvent");
            

            var hubConnection = new HubConnection(_url);
            _hub = hubConnection.CreateHubProxy("snsri");
            hubConnection.Start().ContinueWith(task=>
            {
                Log.Information("Transmitting event with object {@EventMessage}", eventMessage);

                _hub.Invoke<string>("transmitEvent", eventMessage);

                Log.Information("Event {@EventMessage} transmitted..", eventMessage);
            });

            Log.Information("Exit TransmitEvent");
        }

    }
}
