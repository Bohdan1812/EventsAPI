namespace Application.Events.Dto
{
    public record AddressInfo(
        string House,
        string Street,
        string City, 
        string State,
        string Country);
}
