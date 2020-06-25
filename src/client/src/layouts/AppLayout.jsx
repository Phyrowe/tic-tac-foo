import m from 'mithril';
import { AppFooter } from '../components/App/AppFooter';
import { AppHeader } from '../components/App/AppHeader';
import { AppContainer } from '../components/App/AppContainer';

export const AppLayout = ({
    view: ({children}) => (
        <main>
            <AppHeader />
                <AppContainer>
                    {children}
                </AppContainer>
            <AppFooter />
        </main>
    )
})