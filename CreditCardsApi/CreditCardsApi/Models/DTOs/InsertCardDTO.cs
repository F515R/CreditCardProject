using CreditCardsApi.Models.Entities;

namespace CreditCardsApi.Models.DTOs
{
    public class InsertCardDTO
    {
        public string number { get; set; }
        public string titular { get; set; }
        public string expirationDate { get; set; }
        public string securityCode { get; set; }

    }
}
