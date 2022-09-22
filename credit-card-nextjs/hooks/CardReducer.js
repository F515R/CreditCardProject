const initialCardState = {number:"",titular:"",expirationDate:"",securityCode:""}

const ReducerActions = {
    CHANGE_TITULAR: "CHANGE_TITULAR",
    CHANGE_EXPDATE: "CHANGE_EXPDATE",
    CHANGE_SECCODE: "CHANGE_SECCODE",
    CHANGE_CNUMBER: "CHANGE_CNUMBER",
    RESET: "RESET"
}

function cardReducer(state, action) {

    switch (action.type) {
        case ReducerActions.CHANGE_CNUMBER:
            return { ...state, number: action.payload }
        case ReducerActions.CHANGE_TITULAR:
            return { ...state, titular: action.payload }
        case ReducerActions.CHANGE_EXPDATE:
            return { ...state, expirationDate: action.payload }
        case ReducerActions.CHANGE_SECCODE:
            return { ...state, securityCode: action.payload }
        case ReducerActions.RESET:
            return initialCardState
    }
}

export {ReducerActions,cardReducer as CardReducer,initialCardState}