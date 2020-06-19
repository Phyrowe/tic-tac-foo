import m from 'mithril';
import {connect} from '../application/redux/store/connect';

const mapStateToAttr = state => ({
    hub: state.game.hub
})

export const Test = ({
    oncreate: async ({ attrs: {hub} }) => {
        hub.on("games/available", data => {
            console.log("games", data);
        });;
    },
    view: ({ attrs: {hub} }) => (
        <main>
            <button className={'btn btn-blue'} onclick={hub.invoke("games/create", 10)}>Create</button>
            <div className={'grid-board'}>
                <div>1</div>
                <div>2</div>
                <div>3</div>
                <div>4</div>
            </div>
        </main>
    )
})

export default connect(mapStateToAttr, null)(Test);