export default{
    dishes(state){
        return state.dishes;
    },
    dish(state){
        return state.dish;
    },
    hasDishes(state){
        return state.dishes && state.dishes.length > 0;
    },
    errors(state){
        return state.errors;
    },
};