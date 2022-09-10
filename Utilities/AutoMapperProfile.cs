using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Audio;

namespace ZombieBaby.Utilities
{
    public static class ModelMapper{
        public static IRuntimeMapper Mapper;
        public static MapperConfiguration Configuration { get; }
        static ModelMapper()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Awake, TrackObject>();
                cfg.CreateMap<Dreaming, TrackObject>();
                cfg.CreateMap<SittingUp, TrackObject>();
                cfg.CreateMap<SleepingIn, TrackObject>();
                cfg.CreateMap<SleepingOut, TrackObject>();
                cfg.CreateMap<Screaming, TrackObject>();
                cfg.CreateMap<CueList, TrackObject>();

            });

            Mapper = new Mapper(Configuration);
        }

        public static T Map<T>(object obj)
        {
            return Mapper.Map<T>(obj);
        }

    }
}
