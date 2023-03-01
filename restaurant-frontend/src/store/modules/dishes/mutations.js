export default{
    SET_DISHES(state, dishes){
        state.dishes = dishes;
    },
    SET_DISH(state, dish){
        state.dish = dish;
    },
    SET_ERRORS(state, errors){
        state.errors = errors;
    },
    SET_NOT_FOUND(state){
        state.notFoundDish = true;
    }
};