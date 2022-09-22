import React from 'react';

const SavedCardsDrawer = ({cards}) => {
    return (
        <div className="bottom-drawer">
        <div className="bottom-drawer__header">
          <label htmlFor={'bottom-drawer__toggler'}>{`${cards?.length} tarjetas registradas`}</label>
        </div>
        <input type={'checkbox'} className={'bottom-drawer__toggler'} id={'bottom-drawer__toggler'}></input>
        <div className="bottom-drawer__body">
          {cards?.map((card, i) => (
            <div className='card-brief' key={i}>
              <h4>{card.titular}</h4>
              <h3>{card.number}</h3>
              <h4>{card.expirationDate}</h4>
            </div>
          ))}
        </div>
      </div>
    );
};

export default SavedCardsDrawer;