import * as signalR from '@microsoft/signalr';
import {compose} from 'lodash/fp';

const build = (url, logLevel) => {
    return new signalR.HubConnectionBuilder()
        .withUrl(url)
        .withAutomaticReconnect(reconnectPolicy())
        .configureLogging(logLevel)
        .build();
}

const start = async (hub) => {
    try {
        if(hub.state === signalR.HubConnectionState.Connected) 
            return hub;
        await hub.start();
    } catch (e) {
        console.error(e);
        setTimeout(async () => await start(hub), 6000);
    }
}

const reconnectPolicy = () => {
        return { 
            nextRetryDelayInMilliseconds: (retryContext) => {
                if (retryContext.elapsedMilliseconds < 60000) {
                    // If we've been reconnecting for less than 60 seconds so far,
                    // wait between 0 and 10 seconds before the next reconnect attempt.
                    return Math.random() * 10000;
                } else {
                    // If we've been reconnecting for more than 60 seconds so far, stop reconnecting.
                    console.error("Signalr hub connection failed.");
                    return null;
                }
            }
        }
}

export const connectSignalRHub = async (url, logLevel = signalR.LogLevel.Debug) => {
    try {
        console.log(url)
        compose((await start), build)(url, logLevel);
    } catch (e) {
        console.log(e);
    }
}