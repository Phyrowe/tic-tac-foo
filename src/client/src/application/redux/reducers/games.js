import {SET_GAMES_AVAILABLE} from '../actions/games';

const initialState = {
    available: []
}

export const games = (state = initialState, action) => {
    switch (action.type) {
        case SET_GAMES_AVAILABLE:
            return {
                ...state,
                available: action.available
            };
        default:
            return state;
    }
}