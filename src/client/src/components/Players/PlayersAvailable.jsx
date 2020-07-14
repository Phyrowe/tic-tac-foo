import m from 'mithril';
import {getOr, keys} from 'lodash/fp';
import {connect} from '../../application/redux/store/connect';

const mapStateToAttr = state => ({
    players: state.players.available
})

const PlayersAvailable = (initialVnode) => {
    return {
        view: ({ attrs: {players} }) => (
            <div>
                <h1 className="header-text">Available players</h1>
                <table class="table-primary">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Piece</th>
                            <th>Status</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {keys(players).map(p => 
                            <tr>
                                <td>{getOr(`-`, `name`, players[p])}</td>
                                <td>{getOr(`None`, `piece`, players[p])}</td>
                                <td>{getOr(`-`, `status`, players[p])}</td>
                                <td></td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        )
    }
}

export default connect(mapStateToAttr, null)(PlayersAvailable);