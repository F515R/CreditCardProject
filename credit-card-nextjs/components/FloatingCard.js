import React from 'react';
import Image from 'next/image'

const FloatingCard = ({card}) => {
    return (
        <div className="card-container">
        <div className="card-container__front ">
          <div className="front__header">
            <h2>monobank</h2>
            <div className='line'></div>
            <label>Universal Bank</label>
            <div className='front__header__signal'>
              <Image
                src={'/../public/signal-transparent.png'}
                width='20' height='30'
              />
            </div>
          </div>
          <div className="front__chip">
            <Image
              src={'/../public/chip.png'}
              width='50'
              height='40'
              objectFit='contain'
            />
          </div>
          <div className="front__numbers">
            <label>world</label>
            <h2>{card?.number?.length>=1? card?.number:"0000 0000 0000 0000"}</h2>
          </div>
          <div className="front__footer">
            <div className='mastercard'>
              <div className="circle--orange"></div>
              <div className="circle--red"></div>
            </div>
            <div className="front__footer--data">
              <div className='front__footer__expire__container'>
                <span className='front__footer__expire__container--title'>VALID<br></br>THRU</span>
                <h4 className='front__footer__expire__container--date'>{card?.expirationDate?.length>=1? card?.expirationDate:"##/##"}</h4>
              </div>
              <h3 className='front__footer--data--name'>{card?.titular?.length>=1? card?.titular:"NO NAME"}</h3>
            </div>
          </div>
        </div>
        <div className="card-container__back ">
          <div className='back__bar'></div>
          <div className='cvv-box'>
            <label>CVV</label>
            <h2>{card?.securityCode?.length>=1? card?.securityCode:"###"}</h2>
          </div>
        </div>
      </div>
    );
};

export default FloatingCard;