import m from 'mithril';
import {connect} from '../../application/redux/store/connect';

const mapStateToAttr = state => ({
})

export const GamesHistory = ({
    view: () => (
        <div>
            <h1 className="header-text">History &amp; statistics</h1>
        </div>
    )
})

export default connect(mapStateToAttr, null)(GamesHistory);