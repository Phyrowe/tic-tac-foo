import m from 'mithril';
import {connect} from '../../application/redux/store/connect';

const mapStateToAttr = state => ({
    players: state.players.available
})

export const PlayersAvailable = ({
    view: ({ attrs: {players} }) => (
        <div>
            Available
        </div>
    )
})

export default connect(mapStateToAttr, null)(PlayersAvailable);