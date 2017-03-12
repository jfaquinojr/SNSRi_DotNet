using System;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using SNSRi.Repository;
using SNSRi.Business;
using System.Net.Http;
using AutoMapper;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Web
{
    public class AutoMapperConfig
    {
        public static void ConfigureMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Device, Device>();
                cfg.CreateMap<Ticket, Ticket>();
                cfg.CreateMap<User, User>();
                cfg.CreateMap<UIRoomDevice, UIRoomDevice>();
                cfg.CreateMap<UIRoom, UIRoom>();
                cfg.CreateMap<Resident, Resident>();
                cfg.CreateMap<HSDevice, HSDevice>();
                cfg.CreateMap<Event, Event>();
            });
        }
    }
}
