export const SET_PLAYERS_AVAILABLE = 'SET_PLAYERS_AVAILABLE';
export const setPlayersAvailable = available => ({
    type: SET_PLAYERS_AVAILABLE,
    available
});

export const SET_PLAYERS_NAME = 'SET_PLAYERS_NAME';
export const setPlayersName = name => ({
    type: SET_PLAYERS_NAME,
    name
});