import m from "mithril";
import {appSettings} from "./settings/appSettings";
import {App} from './components/App';
import {appState} from './application/state/appState';
import {appActions} from './application/actions/appActions';

const root  = document.body;

m.route.prefix = appSettings.routing.prefix
m.render(root, <App 
    state={appState} 
    actions={appActions} 
    settings={appSettings}>404 Not found
</App>)
