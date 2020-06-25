import m from 'mithril';
import {AppLayout} from './AppLayout';
import GamesAvailable from '../components/Games/GamesAvailable';
import GamesHistory from '../components/Games/GamesHistory';

export const GamesLobbyLayout = ({
    view: () => (
        <AppLayout>
            <GamesAvailable />
            <GamesHistory />
        </AppLayout>
    )
})