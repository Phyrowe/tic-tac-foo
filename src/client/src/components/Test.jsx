import m from 'mithril';

export const Test = ({
    oninit: ({ attrs: { state, actions } }) => {
        state.hub.on("games/available", data => {
            console.log("games", data);
        });
    },
    view: ({ attrs: { state, actions } }) => (
        <main>
            <button onclick={() => state.hub.invoke("games/create", 10)}>Create</button>
        </main>
    )
})