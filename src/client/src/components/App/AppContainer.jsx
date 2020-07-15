import m from 'mithril';

export const AppContainer = ({
    view: ({children}) => (
        <div class="container mx-auto">
            {children}
        </div>
    )
})