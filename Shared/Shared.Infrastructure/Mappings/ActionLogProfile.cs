using AutoMapper;
using OnlineShop.DTOs.Actions;
using OnlineShop.Shared.Core.Entities;

namespace OnlineShop.Shared.Infrastructure.Mappings
{
    public class ActionLogProfile : Profile
    {
        public ActionLogProfile()
        {
            CreateMap<UpdateActionRequest, Action>().ReverseMap();

            CreateMap<CreateActionRequest, Action>().ReverseMap();
        }
    }
}