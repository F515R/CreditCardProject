using CreditCardsApi.Models.DTOs;
using CreditCardsApi.Models.Entities;
using System.Text.RegularExpressions;

namespace CreditCardsApi.Commons
{
    public class Assembler
    {
        static public bool isValid(InsertCardDTO dto)
        {

            Regex numberRx = new Regex(@"^[0-9]{16}$");
            Regex titularRx = new Regex(@"^[a-zA-ZäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ\u00f1\u00d1 ]{3,23}$");
            Regex expirationDateRx = new Regex(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$");
            Regex securityCodeRx = new Regex(@"^\d{3}$");

            return numberRx.IsMatch(dto.number.Replace(" ",String.Empty)) && titularRx.IsMatch(dto.titular) && expirationDateRx.IsMatch(dto.expirationDate) && securityCodeRx.IsMatch(dto.securityCode);

        }

        static public CreditCard? toNewCreditCard(InsertCardDTO dto)
        {
            if (isValid(dto))
            {
                CreditCard newCreditCard = new(
                dto.number,
                dto.titular,
                dto.expirationDate,
                dto.securityCode
                );
                return newCreditCard;
            }
            else
            {
                return null;
            }

        }
    }
}
