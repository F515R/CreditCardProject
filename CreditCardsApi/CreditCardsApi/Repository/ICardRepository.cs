using CreditCardsApi.Models.DTOs;
using CreditCardsApi.Models.Entities;

namespace CreditCardsApi.Commons
{
    public interface ICardRepository
    {
       Result<IEnumerable<CreditCard>?> getCreditCards();
       Result<CreditCard?> getCreditCard(string cardId);
       Result<string?> addCreditCard(CreditCard card);
       Result<bool> updateCreditCard(string id, InsertCardDTO card);
       Result<bool> deleteCard(string employeeId);
       bool saveChanges();
    }
}
