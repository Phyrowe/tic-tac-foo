import {compose} from 'lodash/fp';
import {start} from '../../hub/signalr';
import {SET_HUBS_CONNECTION} from "../actions/hubs";
import {CREATE_GAME, setGamesAvailable} from "../actions/games";
import {setPlayersAvailable, SET_PLAYERS_NAME} from '../actions/players';

export const signalr = store => next => async action => {
    const {dispatch, getState} = store;
    if (action.type === SET_HUBS_CONNECTION) {
        await compose(start, addEventListeners)(dispatch, action.connection);
    }
    const result = next(action);
    // Signalr should now be connected if new connection
    const {connection} = getState().hubs;
    invokeHubMethod(connection, action);

    return result;
}

const invokeHubMethod = (connection, action) => {
    switch (action.type) {
        case CREATE_GAME:
            connection.invoke('games/create', action.name, action.size);
            break;
        case SET_PLAYERS_NAME:
            connection.invoke('players/update/name', action.name);
            break;
    }
}

const addEventListeners = (dispatch, hub) => {
    hub.on('games/available', games => {
        compose(dispatch, setGamesAvailable)(games);
    });
    hub.on('players/available', players => {
        compose(dispatch, setPlayersAvailable)(players);
    });
    return hub;
}