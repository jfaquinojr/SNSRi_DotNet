﻿using Microsoft.AspNet.SignalR.Client.Hubs;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSeerAPI;

namespace SNSRi.Plugin
{
    public class EventsHub : IEventsHub
    {
        private static IHubProxy _hub;
        private string _url; 
        public EventsHub(string url)
        {
            _url = url;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
            Log.Logger.Information("Created EventsHub instance at {ExecutionTime}", Environment.TickCount);
            ConnectoToServer(_url);
        }
        public EventsHub(string url, ILogger logger)
        {
            _url = url;
            Log.Logger = logger;
            Log.Logger.Information("Created EventsHub instance at {ExecutionTime}", Environment.TickCount);
            ConnectoToServer(_url);
        }

        public void TransmitMessage(string msg)
        {
            Log.Information("Transmitting message {Message}", msg);
            if (_hub == null)
            {
                Log.Warning("Hub on '{_url}' is not yet initialized", _url);
                return;
            }
            _hub.Invoke<string>("transmitEvent", msg);
            Log.Information("Message transmitted..");
        }

        public void TransmitEvent(EventMessage eventMessage)
        {
            Log.Information("Transmitting event with object {EventMessage}", eventMessage);
            if (_hub == null)
            {
                Log.Warning("Hub on '{_url}' is not yet initialized", _url);
                return;
            }
            _hub.Invoke<string>("transmitEvent", eventMessage);
            Log.Information("Event transmitted..");
        }

        private void ConnectoToServer(string url)
        {

            if (_hub != null)
            {
                Log.Information("There is already an existing hub {url}", url);
                return;
            }

            Task.Factory.StartNew(() =>
            {
                var attempts = 0;
                var max = 5;
                Log.Information("Attemtping to connect to {url}", url);
                while (_hub == null && attempts < max)
                {
                    attempts += 1;
                    Log.Information("Attempt number {attempts} or {max}", attempts, max);
                    try
                    {

                        var hubConnection = new HubConnection(url);
                        _hub = hubConnection.CreateHubProxy("snsri");
                        hubConnection.Start();
                        Log.Information("Connection to {url} successful.", url);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Unable to Connect to SignalR {Error}", ex);
                    }

                    Thread.Sleep(5000);
                }
            });
        }
    }
}
