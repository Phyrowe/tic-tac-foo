import m from "mithril";
import {appSettings} from "./settings/appSettings";
import App from './components/App';

const root  = document.body;

m.route.prefix = appSettings.routing.prefix
m.render(root, <App settings={appSettings}>Laddar</App>)