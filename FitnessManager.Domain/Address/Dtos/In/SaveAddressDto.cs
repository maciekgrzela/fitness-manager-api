namespace FitnessManager.Domain.Address.Dtos.In
{
    public class SaveAddressDto
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}