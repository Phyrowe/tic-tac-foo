import m from 'mithril';
import {store} from '../store';

// Simple redux connect HOC for mithril :-)
export const connect = (mapStateToAttr, mapActionsToAttr) => {
    return Component => ({
        view: ({attrs, children}) => (
            <Component 
                {...mapAttrs(attrs, mapStateToAttr, mapActionsToAttr)}>
                {children}
            </Component>
        )
    })
}

const mapAttrs = (attrs, mapStateToAttr, mapActionsToAttr) => {
    const state = mapFunction(mapStateToAttr, store.getState());
    const actions = mapFunction(mapActionsToAttr, store.dispatch);
    return {...attrs, ...state, ...actions};
}

const mapFunction = (f, arg) => typeof f === 'function' ? f(arg) : null;