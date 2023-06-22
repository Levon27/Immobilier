namespace Immobilier.Interfaces.Requests
{
    public class CreatePropertyRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ulong IdOwner { get; set; }
    }
}
