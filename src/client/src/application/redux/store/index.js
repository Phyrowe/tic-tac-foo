import {applyMiddleware, compose, createStore, combineReducers} from "redux";
import {reducers} from '../reducers';

const middlewares = [];

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export const store = createStore(
    combineReducers({...reducers}),
    composeEnhancers(applyMiddleware(...middlewares))
);