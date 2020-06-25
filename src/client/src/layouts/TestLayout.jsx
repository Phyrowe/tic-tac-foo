import m from 'mithril';
import {AppLayout} from './AppLayout';
import Test from '../components/Test';

export const TestLayout = ({
    view: () => (
        <AppLayout>
            <Test />
        </AppLayout>
    )
})