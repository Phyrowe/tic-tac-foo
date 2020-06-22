import {SET_HUBS_CONNECTION} from '../actions/hubs';

const initialState = {
    connection: null
};

export const hubs = (state = initialState, action) => {
    switch (action.type) {
        case SET_HUBS_CONNECTION:
            return {
                ...state,
                connection: action.connection
            };
        default:
            return state;
    }
}