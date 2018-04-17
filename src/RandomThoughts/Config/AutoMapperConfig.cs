using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Humanizer;
using RandomThoughts.Domain;
using RandomThoughts.Models.ThoughtHoleViewModels;
using RandomThoughts.Models.ThoughtViewModels;

namespace RandomThoughts.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            this.CreateMap<Thought, ThoughtIndexViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.CreateAtHumanized = src.CreatedAt.Humanize();
                    dest.ModifiedAtHumanized = src.ModifiedAt.Humanize();
                });

            this.CreateMap<ThoughtCreateViewModel, Thought>();

            this.CreateMap<ThoughtEditViewModel, Thought>();

            this.CreateMap<ThoughtHole, ThoughtHoleIndexViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.CreateAtHumanized = src.CreatedAt.Humanize();
                    dest.ModifiedAtHumanized = src.ModifiedAt.Humanize();
                });

            this.CreateMap<ThoughtHoleCreateViewModel, ThoughtHole>();

        }
    }
}
