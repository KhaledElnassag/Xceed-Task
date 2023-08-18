using AutoMapper;
using DemoDAL.Models;
using DmoPL.Models;
using System;
using System.Linq;

namespace DmoPL.Helper
{
	public class MapProfile:Profile
	{
		public MapProfile()
		{
			CreateMap<EmployeeViewModel, Empployee>().
				ForMember(D => D.LanguageLevel, O => O.MapFrom(S => string.Join(',', S.LanguageLevel)));
			CreateMap<Empployee,EmployeeViewModel>().
				ForMember(D => D.LanguageLevel, O => O.MapFrom(S => S.LanguageLevel.Split(',',StringSplitOptions.None)));
		}

	}
}
