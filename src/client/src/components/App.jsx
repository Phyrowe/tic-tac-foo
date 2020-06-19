import m from 'mithril';
import {appRoutes} from '../appRoutes';
import {connectSignalRHub} from '../application/hub/signalr';
import {connect} from '../application/redux/store/connect';
import {compose} from 'lodash/fp';
import {setHub} from '../application/redux/actions/game';

const mapStateToAttr = state => ({
    hub: state.game.hub
})

const mapDispatchToAttr = dispatch => ({
    setHub: hub => dispatch(setHub(hub))
})

const App = () => ({
    oncreate: async ({ attrs: { hub, setHub, settings }, dom }) => {
        setHub(await connectSignalRHub(settings.hub.url))
        m.route.prefix = settings.routing.prefix;
        m.route(dom, settings.routing.rootPath, appRoutes());
    },
    view: ({ children }) => (
        <div class="App">
            {children}
        </div>
    )
});

export default connect(mapStateToAttr, mapDispatchToAttr)(App);