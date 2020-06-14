import m from 'mithril';
import {appRoutes} from '../appRoutes';
import {connectSignalRHub} from '../application/hub/signalr';

export const App = () => ({
    oncreate: async ({attrs: {settings, state, actions}, dom}) => {
        actions.setHub(await connectSignalRHub(settings.hub.url));
        m.route.prefix = settings.routing.prefix;
        m.route(dom, settings.routing.rootPath, appRoutes(state, actions));
    },
    view: ({ children }) => (
        <div class="App">
            {children}
        </div>
    )
});