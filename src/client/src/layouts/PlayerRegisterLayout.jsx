import m from 'mithril';
import {connect} from '../application/redux/store/connect';
import {getOr} from 'lodash/fp';
import {AppLayout} from './AppLayout';
import PlayerRegister from '../components/Players/PlayerRegister';
import PlayersAvailable from '../components/Players/PlayersAvailable';

const mapStateToAttr = state => ({
    playerId: getOr(false, `connectionId`, state.hubs.connection),
    playersAvailable: state.players.available
})


const PlayerRegisterLayout = (initialVnode) => {
    
    // TODO: Should be moved to appRoutes..
    const redirectPlayer = (playerId, playersAvailable) => {
        if(getOr(false, `[${playerId}].name`, playersAvailable)) {
            m.route.set(`/games/lobby`);
        }
    }

    return {
        oncreate: ({ attrs: {playerId, playersAvailable} }) => {
            redirectPlayer(playerId, playersAvailable);
        },
        onupdate: ({ attrs: {playerId, playersAvailable} }) => {
            redirectPlayer(playerId, playersAvailable);
        },
        view: () => (
            <AppLayout>
                <div class='grid-container'>
                    <div class="col-span-3"></div>
                    <div class="col-span-6">
                        <PlayerRegister />
                        <PlayersAvailable />
                    </div>
                    <div class="col-span-3"></div>
                </div>
            </AppLayout>
        )
    }
}

export default connect(mapStateToAttr, null)(PlayerRegisterLayout);