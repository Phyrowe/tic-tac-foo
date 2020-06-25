import m from 'mithril';
import {connect} from '../../application/redux/store/connect';

const mapStateToAttr = state => ({
})

export const GamesHistory = ({
    view: () => (
        <div>
            History
        </div>
    )
})

export default connect(mapStateToAttr, null)(GamesHistory);