import m from 'mithril';
import {compose} from 'lodash/fp';
import {connect} from '../../application/redux/store/connect';
import {createGame} from "../../application/redux/actions/games";

const mapDispatchToAttr = dispatch => ({
    createGame: (name, size) => compose(dispatch, createGame)(name, size)
})

const GamesCreate = (initialVnode) => {
    let gameName = ``;

    const setGameName = (name) => {
        gameName = name;
    }

    const createNewGame = (createGame, name, size) => {
        createGame(name, size);
        resetState();
    }

    const resetState = () => {
        document.getElementById(`game-name`).value = ``;
        gameName = ``;
    }

    return {
        view: ({ attrs: {createGame} }) => (
            <div>
                <h1 className="header-text">Create new game</h1>
                <label className="block-type" for="gameName">Choose a game name of minimum 3 characters.</label>
                        <input 
                            id="game-name"
                            name="gameName"
                            type="text"
                            oninput={(e) => setGameName(e.target.value)} />
                        {gameName.length > 2
                            ? <button 
                                className={'btn btn-primary'} 
                                onclick={() => createNewGame(createGame, gameName, 3)}>Create
                            </button>
                            : ``
                        }
            </div>
        )
    }
}

export default connect(null, mapDispatchToAttr)(GamesCreate);