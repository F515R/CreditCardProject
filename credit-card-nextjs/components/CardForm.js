import React, { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { callApi, methods } from '../hooks/apiMethods';
import { ReducerActions } from '../hooks/CardReducer';

const normalizeCardNumber = (value) => {
  let newVal = value.replace(/\s/g, "").match(/[0-9]{1,4}/g)?.join(" ").substr(0, 19) || ""
  console.log(newVal)
  return newVal
}
const normalizeExpirationDate = (value) => {

  let date = new Date()
  let month = date.getMonth() + 1
  let year = date.getFullYear() - 2000
  let newVal = value.replace(/\s/g, "").match(/[0-9]{1,2}/g)?.join("/").substr(0, 5) || ""

  if (newVal.length == 2) {
    if (parseInt(newVal) > 12) {
      return ''
    }
  }
  if (newVal.length == 5) {

    let [strMonth, strYear] = newVal.match(/[0-9]{2}/g)

    if (parseInt(strYear) == year) {
      if (parseInt(strMonth) <= month) {
        return ''
      }
    } if (parseInt(strYear) > (year + 5) || parseInt(strYear) < year) {
      strYear = ''
    }
    return strMonth + '/' + strYear
  }

  return newVal
}
const flipCard = (reverse = true) => {
  try {
    var front = document.querySelector('.card-container__front')
    var back = document.querySelector('.card-container__back')
    if (reverse) {
      front.style.transform = 'perspective(1000px) rotateY(-180deg)';
      back.style.transform = 'perspective(1000px) rotateY(0deg)';
    } else {
      front.style.transform = 'perspective(1000px) rotateY(0deg)';
      back.style.transform = 'perspective(1000px) rotateY(180deg)';
    }
  } catch {

  }
}

const CardForm = ({ card, dispatcher, reload }) => {

  const { register, handleSubmit, formState: { errors } } = useForm();
  const [flip, setFlip] = useState(false)

  const onSubmit = data => {

    var cardFront = document.querySelector('.card-container__front')
    cardFront.style.transform = "translate(0px,-500px)"

    callApi('CreditCards', methods.POST, data).then(result => {
      reload();
      if (result.errors) { alert("Ha ocurrido un error al agregar la tarjeta por favor intentalo de nuevo") }
      else { alert("Se ha agregado la tarjeta de manera satisfactoria") }
      dispatcher({ type: ReducerActions.RESET })
    }).then(x => {
      cardFront.style.transform = "translate(0px,0px)"
    })
  }

  useEffect(() => {
    flipCard(flip)
  }, [flip])

  return (
    <div className="card">
      <form onSubmit={handleSubmit(onSubmit)} className="grid" noValidate>
        <div className={"form-control"}>
          <label className="form-control__label">Número de tarjeta</label>
          <input className='form-control__input'
            name='number'
            inputMode='numeric'
            autoComplete={'cc-number'}
            value={card?.number}
            {...register('number', {
              required: true,
              pattern: /^[0-9- ]{19}$/,
              onChange: (event) => {
                const { value } = event.target
                dispatcher({ payload: normalizeCardNumber(value), type: ReducerActions.CHANGE_CNUMBER })
              },
            })
            }
          ></input>
          {errors.number && <span className='form-control__error'>Ingrese un numero de tarjeta valido</span>}
        </div>
        <div className={"form-control"}>
          <label className="form-control__label">Fecha de Vencimiento</label>
          <input className='form-control__input'
            name='expirationDate'
            inputMode='numeric'
            value={card?.expirationDate}
            {...register('expirationDate', {
              required: true,
              pattern: /^(0[1-9]|1[0-2])\/?([0-9]{2})$/,
              onChange: (event) => {
                const { value } = event.target
                dispatcher({ payload: normalizeExpirationDate(value), type: ReducerActions.CHANGE_EXPDATE })
              },
            })
            }
          ></input>
          {errors.expirationDate && <span className='form-control__error'>Ingrese una fecha de vencimiento valida</span>}
        </div>
        <div className={"form-control"}>
          <label className="form-control__label">Nombre del Titular </label>
          <input className='form-control__input' name='titular'
            value={card?.titular}
            {...register('titular', {
              required: true,
              pattern: /^[a-zA-ZäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ\u00f1\u00d1 ]{3,23}$/,
              onChange: (event) => {
                dispatcher({ payload: event.target.value.substring(0, 23), type: ReducerActions.CHANGE_TITULAR })
              },
            })
            }
          ></input>
          {errors.titular && <span className='form-control__error'>Ingrese un código de seguridad valido</span>}
        </div>
        <div className={"form-control"}>
          <label className="form-control__label">CVV</label>
          <input className='form-control__input' name='securityCode'
            onFocus={() => { setFlip(true) }}
            value={card?.securityCode}
            {...register('securityCode', {
              required: true,
              pattern: /^[0-9]{3}$/,
              onChange: (event) => {
                dispatcher({ payload: event.target.value.substring(0, 3), type: ReducerActions.CHANGE_SECCODE })
              },
              onBlur: () => setFlip(false)
            })
            }
          ></input>
          {errors.securityCode && <span className='form-control__error'>Ingrese un código de seguridad valido</span>}
        </div>
        <div className='form-control buttons-container'>
          <button type='submit' className='buttons-container__button--submit'>Agregar Tarjeta</button>
          <button className='buttons-container__button--cancel' onClick={(e) => { e.preventDefault(); dispatcher({ type: ReducerActions.RESET }); }}>Cancelar</button>
        </div>
      </form>
    </div>
  );
};

export default CardForm;