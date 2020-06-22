import m from 'mithril';
import * as signalR from '@microsoft/signalr';
import {compose} from 'lodash/fp';
import {start} from '../../hub/signalr';
import {SET_HUBS_CONNECTION} from "../actions/hubs";
import {CREATE_GAME} from "../actions/games";

export const signalr = store => next => async action => {
    const {dispatch, getState} = store;
    if (action.type === SET_HUBS_CONNECTION) {
        await compose(start, addEventListeners)(action.connection);
    }

    const result = next(action);
        // Signalr should not be connected if new connection

    const {connection} = getState().hubs;

    switch (action.type) {
        case CREATE_GAME:
            connection.invoke('games/create', action.size);
            break;
    }

    return result;
}

const addEventListeners = hub => {
    hub.on('games/available', games => {
        console.log(games);
    });
    hub.on('players/available', players => {
        console.log(players);
    });
    return hub;
}