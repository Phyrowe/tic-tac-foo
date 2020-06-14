import m from 'mithril';

export const Test = ({
    oninit: ({ attrs: { state, actions } }) => {
    },
    view: ({ attrs: { state, actions } }) => (
        <main>
            <h1>{state.title}</h1>
        </main>
    )
})