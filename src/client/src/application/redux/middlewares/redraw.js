import m from 'mithril';

// Monkey patch redux dispatch/actions to force mithril to redraw :-)
export const redraw = store => next => action => {
    const result = next(action);
    m.redraw();
    console.log("Rewdraw")
    return result;
}