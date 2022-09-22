using System.Xml.Linq;

namespace CreditCardsApi.Models.Entities
{
    public class CreditCard
    {
        public string id { get; }
        public string number { get; set; }
        public string titular { get; set; }
        public string expirationDate { get; set; }
        public string securityCode { get; set; }
        public DateTime creationDate { get; }
        public DateTime lastUpdateDate { get; set; }

        public CreditCard(string number, string titular, string expirationDate, string securityCode)
        {
            this.id = Guid.NewGuid().ToString();
            this.lastUpdateDate = DateTime.Now;
            this.creationDate = DateTime.Now;
            this.number = number;
            this.titular = titular;
            this.expirationDate = expirationDate;
            this.securityCode = securityCode;
        }

        public CreditCard(XElement node)
        {
            this.id = (string)node.Attribute("id");
            this.titular = (string)node.Attribute("titular");
            this.number = (string)node.Attribute("number");
            this.expirationDate = (string)node.Attribute("expirationDate");
            this.securityCode = (string)node.Attribute("securityCode");
            this.creationDate = (DateTime)node.Attribute("creationDate");
            this.lastUpdateDate = (DateTime)node.Attribute("lastUpdateDate");   
        }


    }
}
