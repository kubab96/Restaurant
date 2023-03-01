import VueJwtDecode from 'vue-jwt-decode'

export default{
    SET_USER_TOKEN(state, token){
        let decodedToken = null;
        decodedToken = VueJwtDecode.decode(token)
        state.token = token;
        state.userInfo = decodedToken;
        state.isLogIn = true;
    },
    SET_ERRORS(state, errors){
        state.errors = errors;
    },
    SET_LOGIN(state){
        state.isLogIn = true;
    },
    LOG_OUT(state){
        state.isLogIn = false;
        state.token = null;
        state.userInfo = {};
    }
};