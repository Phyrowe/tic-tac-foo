import {SET_PLAYERS_AVAILABLE} from '../actions/players';

const initialState = {
    available: []
}

export const players = (state = initialState, action) => {
    switch (action.type) {
        case SET_PLAYERS_AVAILABLE:
            return {
                ...state,
                available: action.available
            };
        default:
            return state;
    }
}