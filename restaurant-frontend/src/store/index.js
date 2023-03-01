import { createStore } from 'vuex';

import restaurantsModule from './modules/restaurants/index.js'
import dishesModule from './modules/dishes/index.js'
import usersModule from './modules/users/index.js'
import createPersistedState from "vuex-persistedstate";

const store = createStore({
    plugins: [createPersistedState()],
    modules:{
        restaurants: restaurantsModule,
        dishes: dishesModule,
        users: usersModule,
    },
});

export default store;