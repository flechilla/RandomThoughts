using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Humanizer;
using RandomThoughts.Domain;
using RandomThoughts.Models.ThoughtViewModels;

namespace RandomThoughts.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            this.CreateMap<Thought, ThoughtIndexViewModel>()
                .AfterMap((src, dest) => dest.CreateAtHumanized = src.CreatedAt.Humanize());

            this.CreateMap<ThoughtCreateViewModel, Thought>();

            this.CreateMap<ThoughtCreateViewModel, Thought>();

        }
    }
}
