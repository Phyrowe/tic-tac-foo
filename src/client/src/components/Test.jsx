import m from 'mithril';
import {compose} from 'lodash/fp';
import {connect} from '../application/redux/store/connect';
import {createGame} from "../application/redux/actions/games";

const mapStateToAttr = state => ({
    games: state.games.available
})

const mapDispatchToAttr = dispatch => ({
    createGame: size => compose(dispatch, createGame)(size)
})

const Test = ({
    oncreate: ({ attrs: {games} }) => {
    },
    oninit: ({ attrs: {games} }) => {
    },
    view: ({ attrs: {createGame} }) => (
        <div>
            <button className={'btn btn-blue'} onclick={() => createGame(10)} >Create</button>
            <div className={'grid-board'}>
                <div>1</div>
                <div>2</div>
                <div>3</div>
                <div>4</div>
            </div>
        </div>
    )
})

export default connect(mapStateToAttr, mapDispatchToAttr)(Test);