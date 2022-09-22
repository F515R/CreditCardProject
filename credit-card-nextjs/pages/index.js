import { useReducer } from 'react'
import CardForm from '../components/CardForm';
import { CardReducer, initialCardState } from '../hooks/CardReducer';
import SavedCardsDrawer from '../components/SavedCardsDrawer';
import useCardList from '../hooks/useCardList';
import FloatingCard from '../components/FloatingCard';

export default function Home() {

  const [card,dispatch] = useReducer(CardReducer,initialCardState)
  const [,cards,, reload] = useCardList()

  return (
    <div className={"container"}>
      <FloatingCard card={card}/>
      <CardForm card={card} dispatcher={dispatch} reload={reload}></CardForm>
      <SavedCardsDrawer cards={cards}/>
    </div>
  )
}
