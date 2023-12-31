namespace EventsAPI
{
    public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<User, AddUserDto>();
                CreateMap<AddUserDto, User>();
                CreateMap<Event, AddEventDto>();
                CreateMap<Invitation, AddInvitationDto>();
            }
        }
}

