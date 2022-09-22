using CreditCardsApi.Commons;
using CreditCardsApi.Models.DTOs;
using CreditCardsApi.Models.Entities;
using CreditCardsApi.Models.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;

namespace CreditCardsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardsController : ControllerBase
    {

        private readonly ILogger<CreditCardsController> _logger;
        private readonly ICardRepository cardRepository;
        private UrlHelper urlHelper;
        private List<LinkTo> linkConstructor(string id = "")
        {
            List<LinkTo> linksTo = new List<LinkTo>();
            linksTo.Add(LinkTo.To("Self", "GET","CreditCards",id));
            linksTo.Add(LinkTo.To("All", "GET", "CreditCards"));
            linksTo.Add(LinkTo.To("Delete_Card", "DELETE", "CreditCards", id));
            linksTo.Add(LinkTo.To("Üpdate_Card", "PUT", "CreditCards", id));

            return linksTo;
        }


        public CreditCardsController(ILogger<CreditCardsController> logger, ICardRepository repo)
        {
            _logger = logger;
            cardRepository = repo;  
        }

        [HttpGet]
        public ActionResult<IEnumerable<CreditCardViewModel>> Get()
        {
            Result <IEnumerable<CreditCard?>> cardsListResult = cardRepository.getCreditCards();
            switch (cardsListResult.status)
            {
                case ResultEnum.OK:

                    IEnumerable<CreditCardViewModel> list = cardsListResult.content
                                       .Select(x => new CreditCardViewModel(
                                           expirationDate : x.expirationDate,
                                           number : x.number,
                                           titular : x.titular,
                                           links : linkConstructor(x.id)
                                       )).ToList();
                    return Ok(list);
                case ResultEnum.NOT_OK:
                    return NotFound();
                case ResultEnum.ERROR:
                    return StatusCode(500);
                default:
                    return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<CreditCardViewModel>> Get(string id)
        {
            Result<CreditCard?> cardResult = cardRepository.getCreditCard(id);
            switch (cardResult.status)
            {
                case ResultEnum.OK:

                    CreditCardViewModel card = new CreditCardViewModel(
                                           expirationDate: cardResult.content.expirationDate,
                                           number: cardResult.content.number,
                                           titular: cardResult.content.titular,
                                           links: linkConstructor(id));

                    return Ok(card);
                case ResultEnum.NOT_OK:
                    return NotFound();
                case ResultEnum.ERROR:
                    return StatusCode(500);
                default:
                    return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<string> Post(InsertCardDTO _card)
        {

            CreditCard? newCard = Assembler.toNewCreditCard(_card);
            if (newCard == null) { return BadRequest(); }

            Result<string?> insertResult = cardRepository.addCreditCard(newCard);
  
            switch (insertResult.status)
            {
                case ResultEnum.OK:
                    return Ok(insertResult.content);
                case ResultEnum.ERROR:
                    return StatusCode(500);
                default:
                    return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(string id)
        {
            Result<bool> deleteResult = cardRepository.deleteCard(id);

            switch (deleteResult.status)
            {
                case ResultEnum.OK:
                    return Ok(deleteResult.content);
                case ResultEnum.ERROR:
                    return StatusCode(500);
                default:
                    return BadRequest(deleteResult.content);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<string> Put(string id,InsertCardDTO card)
        {

            var findFirst = cardRepository.getCreditCard(id);

            if(findFirst.status != ResultEnum.OK) {return NotFound();}
            else
            {
                if(card.securityCode != findFirst.content.securityCode){return BadRequest();}
                Result<bool> insertResult = cardRepository.updateCreditCard(id,card);

                switch (insertResult.status)
                {
                    case ResultEnum.OK:
                        return Ok(insertResult.content);
                    case ResultEnum.ERROR:
                        return StatusCode(500);
                    default:
                        return BadRequest(insertResult.content);
                }

            }

            
        }

    }
}