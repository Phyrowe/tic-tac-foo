export const SET_GAMES_AVAILABLE = 'SET_GAMES_AVAILABLE';
export const setGamesAvailable = available => ({
    type: SET_GAMES_AVAILABLE,
    available
});

export const CREATE_GAME = 'CREATE_GAME';
export const createGame = (name, size) => ({
    type: CREATE_GAME,
    name,
    size
});

