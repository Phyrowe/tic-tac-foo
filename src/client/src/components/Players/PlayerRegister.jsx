import m from 'mithril';
import {compose, getOr, keys} from 'lodash/fp';
import {connect} from '../../application/redux/store/connect';
import {setPlayersName} from '../../application/redux/actions/players';

const mapStateToAttr = state => ({
    playerId: getOr(false, `connectionId`, state.hubs.connection),
    playersAvailable: state.players.available
})

const mapDispatchToAttr = dispatch => ({
    setPlayersName: (name) => compose(dispatch, setPlayersName)(name)
})


const PlayerRegister = (initialVnode) => {
    let playerName = ``;

    const setPlayerName = (name) => {
        playerName = name;
    }

    return {
        view: ({ attrs: {setPlayersName} }) => (
            <div>
                <h1 className="header-text">Register</h1>
                <label className="block-type" for="playerName">Choose a player name of minimum 3 characters.</label>
                <input 
                    name="playerName"
                    type="text"
                    oninput={(e) => setPlayerName(e.target.value)} />
                {playerName.length > 2
                    ? <button 
                        className={'btn btn-primary'} 
                        onclick={() => setPlayersName(playerName)}>Register</button>
                    : ``
                }
            </div>
        )
    }
}
export default connect(mapStateToAttr, mapDispatchToAttr)(PlayerRegister);