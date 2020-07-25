using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDealer.Data.Entities;
using TwitterDealer.Dtos;

namespace TwitterDealer.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<User, UserForListDto>()
				.ForMember(d => d.PhotoUrl, o =>
				{
					o.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
				});
			CreateMap<User, UserForDetailedDto>()
				.ForMember(d => d.PhotoUrl, o =>
				{
					o.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
				});
			CreateMap<Photo, PhotosForDetailedDto>();
		}
	}
}
