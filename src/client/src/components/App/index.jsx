import m from 'mithril';
import {compose} from 'lodash/fp';
import {connect} from '../../application/redux/store/connect';
import {setHubsConnection} from '../../application/redux/actions/hubs';
import {appRoutes} from '../../appRoutes';
import {appSettings} from '../../settings/appSettings';
import {build} from '../../application/hub/signalr';

const mapStateToAttr = state => ({
    connection: state.hubs.connection
})

const mapDispatchToAttr = dispatch => ({
    setHubsConnection: connection => compose(dispatch, setHubsConnection)(connection)
})

const App = () => ({
    oninit: ({ attrs: { setHubsConnection } }) => {
        compose(setHubsConnection, build)(appSettings.hub.url);
    },
    oncreate: ({ attrs: { connection, setHubsConnection, settings }, dom }) => {
        m.route.prefix = settings.routing.prefix;
        m.route(dom, settings.routing.rootPath, appRoutes());
    },
    view: ({ attrs: {hub}, children }) => (
        <div class="App">
            {children}
        </div>
    )
});

export default connect(mapStateToAttr, mapDispatchToAttr)(App);