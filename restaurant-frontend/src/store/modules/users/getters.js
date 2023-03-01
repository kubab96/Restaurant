export default{
    token(state){
        return state.token;
    },
    errors(state){
        return state.errors;
    },
    isLogIn(state){
        return state.isLogIn;
    },
    userId(state){
        return state.userInfo.UserId;
    },
    userInfo(state){
        return state.userInfo;
    },
    
};