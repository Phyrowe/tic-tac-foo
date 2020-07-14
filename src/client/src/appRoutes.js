import m from 'mithril';
import {GameLayout} from './layouts/GameLayout';
import GamesLobbyLayout from './layouts/GamesLobbyLayout';
import PlayerRegisterLayout from './layouts/PlayerRegisterLayout';

export const appRoutes = () => ({
    '/': 
        buildRoute(PlayerRegisterLayout),
    '/games/lobby': ({attrs}) => 
        buildRoute(GamesLobbyLayout, attrs),
    '/game/:id': ({attrs}) => 
        buildRoute(GameLayout, attrs)
});

const buildRoute = (Layout, attrs = {}) => ({
    view: () => <Layout {...attrs} />
});