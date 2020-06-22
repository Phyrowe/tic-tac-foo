import {applyMiddleware, compose, createStore, combineReducers} from "redux";
import {reducers} from '../reducers';
import {signalr} from '../middlewares/signalr';
import {redraw} from '../middlewares/redraw';

const middlewares = [
    redraw,
    signalr
];

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export const store = createStore(
    combineReducers({...reducers}),
    composeEnhancers(applyMiddleware(...middlewares))
);