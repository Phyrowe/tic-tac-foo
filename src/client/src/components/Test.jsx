import m from 'mithril';
import {compose} from 'lodash/fp';
import {connect} from '../application/redux/store/connect';
import {setHub} from "../application/redux/actions/game";
import {withRedraw} from '../lib/withRedraw';

const mapStateToAttr = state => ({
    hub: state.game.hub
})

const mapDispatchToAttr = dispatch => ({
    setHub: hub => compose(withRedraw, dispatch, setHub)(hub)
})

export const Test = ({
    oncreate: ({ attrs: {hub, setHub} }) => {
    },
    oninit: ({ attrs: {hub, setHub} }) => {
    },
    view: ({ attrs: {hub} }) => (
        <main>
            <button className={'btn btn-blue'} >Create</button>
            <div className={'grid-board'}>
                <div>1</div>
                <div>2</div>
                <div>3</div>
                <div>4</div>
            </div>
        </main>
    )
})

export default connect(mapStateToAttr, mapDispatchToAttr)(Test);