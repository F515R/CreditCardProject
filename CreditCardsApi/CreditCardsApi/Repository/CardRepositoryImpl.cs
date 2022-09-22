using CreditCardsApi.Commons;
using CreditCardsApi.Models.DTOs;
using CreditCardsApi.Models.Entities;
using System.Globalization;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CreditCardsApi.Repository
{
    
    public class CardRepositoryImpl : ICardRepository
    {
        readonly string _cards_xml_path;
        XDocument existingDoc;
        XElement _cards;

       
        public CardRepositoryImpl(IWebHostEnvironment env)
        {
            string dbDir = Path.Combine(env.ContentRootPath, "Data-xml");
            this._cards_xml_path = Path.Combine(dbDir, "credit-cards.xml");

            if (!Directory.Exists(dbDir))
            {
                Directory.CreateDirectory(dbDir);
            }   

            try
            {
                existingDoc = XDocument.Load(_cards_xml_path);
            }
            catch
            {
                existingDoc = new XDocument(new XElement("credit-cards-collection"));
                existingDoc.Save(_cards_xml_path);
            }
            finally
            {
                _cards = existingDoc.Root;
            }    
        }


        public Result<string?> addCreditCard(CreditCard card)
        {
            Result<string?> result;   
            try
            {
                _cards.Add(new XElement("Card",
                   new XAttribute("id", card.id),
                   new XAttribute("number", card.number),
                   new XAttribute("titular", card.titular),
                   new XAttribute("securityCode", card.securityCode),
                   new XAttribute("expirationDate", card.expirationDate),
                   new XAttribute("creationDate", card.creationDate.ToString()),
                   new XAttribute("lastUpdateDate", card.lastUpdateDate.ToString())
               ));

                result = resultAfterSave(card?.id);

            }
            catch
            {
                result = new Result<string?> { content = null, status = ResultEnum.ERROR };
            }

            return result;
        }

        public Result<bool> deleteCard(string id)
        {
            Result<bool> result;
            try
            {
                _cards.Descendants("Card").Where(node => (string)node.Attribute("id") == id).Remove();
                result = resultAfterSave(true);
            }
            catch
            {
                result = new Result<bool> { content=false,status = ResultEnum.ERROR };
            }

            return result;
            
        }

        public Result<CreditCard?> getCreditCard(string id)
        {
            Result<CreditCard?> result;

            try { 
                CreditCard? card = _cards.Descendants("Card")
                              .Select(node => new CreditCard(node)).Where(x => x.id == id)
                              .FirstOrDefault();
                result = new Result<CreditCard?> { content = card, status = (card == null)? ResultEnum.NOT_OK : ResultEnum.OK };
            }
            catch
            {
                result = new Result<CreditCard?> { content=null,status = ResultEnum.ERROR };
            }
            return result;
        }

        public Result<IEnumerable<CreditCard>?> getCreditCards()
        {
            Result<IEnumerable<CreditCard>?> result;
            try
            {
                IEnumerable<CreditCard> cards = _cards.Descendants("Card")
                                     .Select(node => new CreditCard(node))
                                     .ToList();
                result = new Result<IEnumerable<CreditCard>?> { content = cards, status = (cards == null) ? ResultEnum.NOT_OK : ResultEnum.OK };
            }
            catch
            {
                result = new Result<IEnumerable<CreditCard>?> { content = null, status = ResultEnum.ERROR };
            }

            return result;

        }

        public bool saveChanges()
        {
            try
            {
                _cards.Save(_cards_xml_path);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public Result<bool> updateCreditCard(string id,InsertCardDTO card)
        {

            Result<bool> result;
            var cardToUpdate = _cards.Descendants("Card")
                              .Where(node => (string)node.Attribute("id") == id)
                              .FirstOrDefault();

            if (cardToUpdate == null){
                result = new Result<bool> { content = false, status = ResultEnum.NOT_OK };
                return result;
            }
            try
            {
                cardToUpdate.Attribute("number").Value = card.number;
                cardToUpdate.Attribute("titular").Value = card.titular;
                cardToUpdate.Attribute("expirationDate").Value = card.expirationDate;
                cardToUpdate.Attribute("lastUpdateDate").Value = DateTime.Now.ToString();

                result = resultAfterSave(true);
            }
            catch
            {
                result = new Result<bool> { content = false, status = ResultEnum.ERROR };
            }
            return result;
        }


        protected Result<T> resultAfterSave<T>(T content)
        {
            bool isSaved = saveChanges();
            if (isSaved) { return new Result<T> { content = content, status = ResultEnum.OK }; }
            else { return new Result<T> { status = ResultEnum.ERROR }; }
        }

    }
}
