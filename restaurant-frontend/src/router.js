import { createRouter, createWebHistory } from "vue-router";

import RestaurantList from "./pages/restaurant/RestaurantsList.vue";
import RestaurantDetails from "./pages/restaurant/RestaurantDetails.vue";
import EditRestaurant from "./pages/restaurant/EditRestaurant.vue";
import AddRestaurant from "./pages/restaurant/AddRestaurant.vue";
import DishesList from "./pages/dishes/DishesList.vue";
import DishDetails from "./pages/dishes/DishDetails.vue";
import EditDish from "./pages/dishes/EditDish.vue";
import AddDish from "./pages/dishes/AddDish.vue";
import UserRegister from "./pages/user/UserRegister.vue";
import UserLogin from "./pages/user/UserLogin.vue";
import NotFound from "./pages/NotFound.vue";

const routes = [
  { path: "/", redirect: "/restaurants" },
  
  { path: "/restaurants", component: RestaurantList},

  { path: '/restaurants/:id', component: RestaurantDetails, props: true, },

  { path: '/restaurants/edit/:id', name: 'edit', component: EditRestaurant, props: true, },

  { path: '/restaurants/:id/dishes', name: 'dishes', component: DishesList, props: true, }, 

  { path: '/restaurants/:id/dishes/addDish', name: 'addDish', component: AddDish, props: true, }, 

  { path: '/restaurants/:restaurantId/dishes/:dishId', name: 'dishDetails', component: DishDetails, props: true, }, 

  { path: '/restaurants/:restaurantId/dishes/edit/:dishId', name: 'editDish', component: EditDish, props: true, }, 

  { path: "/addRestaurant", component: AddRestaurant},
  
  { path: "/register", component: UserRegister },

  { path: "/login", component: UserLogin },

  { path: "/:notFound(.*)", component: NotFound },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
