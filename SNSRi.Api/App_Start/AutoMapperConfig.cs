using System;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using SNSRi.Repository;
using SNSRi.Business;
using System.Net.Http;
using AutoMapper;
using SNSRi.Entities;

namespace SNSRi.Web
{
    public class AutoMapperConfig
    {
        public static void ConfigureMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Device, Device>();
            });
        }
    }
}
