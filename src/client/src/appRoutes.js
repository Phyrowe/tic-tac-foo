import m from 'mithril';
import {GameLayout} from './layouts/GameLayout';
import {GamesLobbyLayout} from './layouts/GamesLobbyLayout';
import {TestLayout} from './layouts/TestLayout';

export const appRoutes = () => ({
    '/': 
        buildRoute(GamesLobbyLayout),
    '/test':
        buildRoute(TestLayout),
    '/game/:id': ({attrs}) => 
        buildRoute(GameLayout, attrs)
});

const buildRoute = (Layout, attrs = {}) => ({
    view: () => <Layout {...attrs} />
});