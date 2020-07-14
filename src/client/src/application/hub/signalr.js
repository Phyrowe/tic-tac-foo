import {HubConnectionBuilder, HubConnectionState, LogLevel} from '@microsoft/signalr';

export const build = (url, logLevel = LogLevel.Debug) => {
    return new HubConnectionBuilder()
        .withUrl(url)
        .withAutomaticReconnect(reconnectPolicy())
        .configureLogging(logLevel)
        .build();
}

export const start = async (hub) => {
    try {
        if(hub.state === HubConnectionState.Connected)
            return hub;
        await hub.start();
        return hub;
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