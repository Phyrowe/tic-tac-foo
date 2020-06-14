import {appState} from '../state/appState';

export const AppActions = state => ({
    setHub: hub => state.hub = hub
});

export const appActions = AppActions(appState);