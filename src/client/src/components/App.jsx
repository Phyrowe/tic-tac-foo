import m from 'mithril';
import {appRoutes} from '../appRoutes';
import {connectSignalRHub} from '../application/hub/signalr';
import {compose} from 'lodash/fp';

export const App = () => ({
    oncreate: ({attrs: {settings, state, actions}, dom}) => {
        compose(actions.setHub, connectSignalRHub)(settings.hub.url);
        m.route.prefix = settings.routing.prefix;
        m.route(dom, settings.routing.rootPath, appRoutes(state, actions));
    },
    view: ({ children }) => (
        <div class="App">
            {children}
        </div>
    )
});