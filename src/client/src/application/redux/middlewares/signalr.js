export const signalRMiddleware = (store) => (next) => async (action) => {
    const { dispatch, getState } = store;
    return next(action);
}