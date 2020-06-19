import {SET_HUB} from '../action-types/game';

const initialState = {
    hub: null
}

export const game = (state = initialState, action) => {
    switch (action.type) {
        case SET_HUB:
            return {
                ...state,
                hub: action.hub
            };
        default:
            return state;
    }
}