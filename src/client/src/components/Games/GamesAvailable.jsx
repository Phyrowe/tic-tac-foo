import m from 'mithril';
import {connect} from '../../application/redux/store/connect';

const mapStateToAttr = state => ({
    games: state.games.available
})

export const GamesAvailableList = ({
    view: ({ attrs: {games} }) => (
        <div>
            Available
        </div>
    )
})

export default connect(mapStateToAttr, null)(GamesAvailableList);