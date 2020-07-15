import m from 'mithril';
import {connect} from '../application/redux/store/connect';
import {getOr} from 'lodash/fp';
import {AppLayout} from './AppLayout';
import GamesAvailable from '../components/Games/GamesAvailable';
import GamesHistory from '../components/Games/GamesHistory';
import GamesCreate from '../components/Games/GamesCreate';
import PlayersAvailable from '../components/Players/PlayersAvailable';

const mapStateToAttr = state => ({
    playerId: getOr(false, `connectionId`, state.hubs.connection),
    playersAvailable: state.players.available
})

const GamesLobbyLayout = (initialVnode) => {

    // TODO: Should be moved to appRoutes..
    const redirectPlayer = (playerId, playersAvailable) => {
        if(!getOr(false, `[${playerId}].name`, playersAvailable)) {
            m.route.set(`/`);
        }
    }

    return {
        oninit: ({ attrs: {playerId, playersAvailable} }) => {
            redirectPlayer(playerId, playersAvailable);
        },
        view: () => (
            <AppLayout>
                <div class='grid grid-cols-12'>
                    <div class="col-span-3"></div>
                    <div class="col-span-6">
                        <GamesCreate />
                        <GamesAvailable />
                        <PlayersAvailable />
                        <GamesHistory />
                    </div>
                    <div class="col-span-3"></div>
                </div>
            </AppLayout>
        )
    }
}

export default connect(mapStateToAttr, null)(GamesLobbyLayout);