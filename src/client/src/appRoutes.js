import m from 'mithril';
import {Test} from './components/Test';

export const appRoutes = (state, actions) => ({
    '/': {
        view: () => <Test state={state} actions={actions} />
    },
    '/game/:id': {
        view: ({attrs: {id}}) => <h1>Game {id}</h1>
    },
});