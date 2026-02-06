namespace E_Commerce.Service.MappingProfiles;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Address, AddressDTO>()
            .ReverseMap();
    }
}
