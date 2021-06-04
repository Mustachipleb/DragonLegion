using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DragonLegion.Data;
using DragonLegion.Models;
using DragonLegion.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DragonLegion
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<Post, PostViewModels.Index>()
                .ForMember(p => p.Author,
                    opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(p => p.AuthorId, 
                    opt => opt.MapFrom(src => src.Author.Id))
                .ForMember(p => p.Content,
                    opt => opt.MapFrom(src => src.Content.Remove(200) + "..."));
            
            CreateMap<Post, PostViewModels.Detail>()
                .ForMember(p => p.Author,
                    opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(p => p.AuthorId,
                    opt => opt.MapFrom(src => src.Author.Id));

            CreateMap<Post, PostViewModels.Edit>();

            CreateMap<IdentityUser, ProfileViewModels.Detail>();

            CreateMap<EventViewModel, Event>()
                .ForMember(e => e.CreationDate, opt => opt.Ignore());
            CreateMap<Event, EventViewModel>();
        }
    }
}
