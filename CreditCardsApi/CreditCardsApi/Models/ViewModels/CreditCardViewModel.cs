using CreditCardsApi.Commons;

namespace CreditCardsApi.Models.ViewModels
{
    public class CreditCardViewModel:BaseResponseViewModel
    {
        public string number { get; set; }
        public string titular { get; set; }
        public string expirationDate { get; set; }

        public CreditCardViewModel(string number, string titular, string expirationDate, List<LinkTo> links)
        {
            string sliced = number.Substring(2, number.Length - 5);
            this.number = number.Replace(sliced, "** **** **** ");
            this.titular = titular;
            this.expirationDate = expirationDate;
            this.links = links;
        }
    }
}
