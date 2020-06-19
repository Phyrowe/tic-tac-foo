import m from 'mithril';
import Test from './components/Test';

export const appRoutes = () => ({
    '/': {
        view: () => <Test />
    },
    '/game/:id': {
        view: ({attrs: {id}}) => <h1>Game {id}</h1>
    },
});