import m from 'mithril';
import {getOr, keys} from 'lodash/fp';
import {connect} from '../../application/redux/store/connect';

const mapStateToAttr = state => ({
    games: state.games.available
})

const GamesAvailableList = (initialVnode) => {

    const renderJoinBtn = () => <button className="btn btn-primary">Join</button>;
    const renderObserveBtn = () => <button className="btn btn-primary">Observe</button>;
    
    const renderActions = (canJoin) => canJoin 
            ? <div>{renderJoinBtn()} {renderObserveBtn()}</div>
            : renderObserveBtn();
            
    return {
        view: ({ attrs: {games, players} }) => (
            <div>
                <h1 className="header-text">Available games</h1>
                <table class="table-primary">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Players</th>
                            <th>Observers</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {keys(games).map(g => 
                            <tr>
                                <td>{getOr(`-`, `name`, games[g])}</td>
                                <td class="text-center">{getOr(`-`, `playerCount`, games[g])} / 2</td>
                                <td class="text-center">0</td>
                                <td>
                                    {renderActions(getOr(`-`, `canJoin`, games[g]))}
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        )
    }
}

export default connect(mapStateToAttr, null)(GamesAvailableList);