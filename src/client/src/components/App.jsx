import m from 'mithril';
import {compose} from 'lodash/fp';
import {connect} from '../application/redux/store/connect';
import {setHub} from "../application/redux/actions/game";
import {withRedraw} from '../lib/withRedraw';
import {appRoutes} from '../appRoutes';

const mapStateToAttr = state => ({
    hub: state.game.hub
})

const mapDispatchToAttr = dispatch => ({
    setHub: hub => compose(withRedraw, dispatch, setHub)(hub)
})

const App = () => ({
    oncreate: async ({ attrs: { hub, setHub, settings }, dom }) => {
        //setHub(await connectSignalRHub(settings.hub.url));
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