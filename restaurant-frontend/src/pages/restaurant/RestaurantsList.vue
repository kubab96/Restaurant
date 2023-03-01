<template>
  <div id="list">
    <section>
      <base-card>
      
      
        <div class="controls">
          <h1>Restaurants</h1>
          <base-button
            mode="add"
            v-if="
              (isLoggedIn && userInfo.UserRole === 'Admin') ||
              userInfo.UserRole === 'Manager'
            "
            link
            to="/addRestaurant"
            >Add restaurant</base-button
          >
        </div>
        <div class="pagination-items">
        <ul class="pagination">
            <li>
              <button class="pagination-button" type="button" @click="first">First</button>
            </li>

            <li>
              <button class="pagination-button" type="button" @click="previous">&lt;</button>
            </li>

            <li>
              <button class="pagination-button" type="button" @click="next">></button>
            </li>

            <li>
              <button class="pagination-button" type="button" @click="last">Last</button>
            </li>
          </ul>

          <ul class="size">
            <li>
              <button class="pagination-button" type="button" @click="setPageSize(5)">5</button>
            </li>
            <li>
              <button class="pagination-button" type="button" @click="setPageSize(10)">10</button>
            </li>
            <li>
              <button class="pagination-button" type="button" @click="setPageSize(15)">15</button>
            </li>
          </ul>
          
        </div>


        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <ul v-else-if="!this.isLoading && restaurants.items">
          <restaurant-item
            v-for="restaurant in restaurants.items"
            :key="restaurant.id"
            :id="restaurant.id"
            :name="restaurant.name"
            :hasDelivery="restaurant.hasDelivery"
            :category="restaurant.category"
            :city="restaurant.city"
            :street="restaurant.street"
            :createdById="restaurant.createdById"
          ></restaurant-item>
        </ul>
        <p v-else>No restaurants found.</p>
      </base-card>
    </section>
  </div>
</template>

<script>
import RestaurantItem from "../../components/restaurants/RestaurantItem.vue";
export default {
  data() {
    return {
      size: 5,
      number: 1,
      isLoading: false,
    };
  },
  components: {
    RestaurantItem,
  },
  computed: {
    restaurants() {
      return this.$store.getters["restaurants/restaurants"];
    },
    hasRestaurants() {
      return this.$store.getters["restaurants/hasRestaurants"];
    },
    isLoggedIn() {
      return this.$store.getters["users/isLogIn"];
    },
    userInfo() {
      return this.$store.getters["users/userInfo"];
    },
  },
  methods: {
    setPageSize(pageSize) {
      this.size = pageSize;
      this.number = 1;
      this.loadRestaurants();
    },
    next() {
      this.number = this.number + 1;
      if (this.restaurants.totalPages < this.number) {
        this.number = this.number - 1;
      } else {
        this.loadRestaurants();
      }
    },
    previous() {
      this.number = this.number - 1;
      if (this.number < 1) {
        this.number = this.number + 1;
      } else {
        this.loadRestaurants();
      }
    },
    first() {
      if (this.number != 1) {
        this.number = 1;
        this.loadRestaurants();
      }
    },
    last() {
      if (this.number != this.restaurants.totalPages) {
        this.number = this.restaurants.totalPages;
        this.loadRestaurants();
      }
    },

    async loadRestaurants() {
      this.isLoading = true;
      await this.$store.dispatch("restaurants/getRestaurants", {
        PageSize: this.size,
        PageNumber: this.number,
      });
      this.isLoading = false;
    },
  },
  created() {
    this.loadRestaurants();
  },
};
</script>

<style scoped>
 .pagination-button,
  a {
    text-decoration: none;
    text-align: center;
    padding: 0.35rem 1rem;
    font: inherit;
    background-color: #3a0061;
    border: 1px solid #3a0061;
    color: white;
    cursor: pointer;
    margin-right: 0.2rem;
    display: inline-block;
  }
  
  a:hover,
  a:active,
  button:hover,
  button:active {
    background-color: #270041;
    border-color: #270041;
  }
        .pagination-items {
            display: flex;
            justify-content: center;
            border-top: 1px solid #eee;
            margin-top: 1em;
            padding-top: 1em;
            margin-bottom: 1em;
            padding-bottom: .5em;
        }
        .pagination {
            list-style: none;
            margin: 0;
            padding: 0;
            display: flex;
        }
        .size {
            list-style: none;
            margin-left: auto;
            padding: 0;
            display: flex;
        }


ul {
  list-style: none;
  margin: 0;
  padding: 0;
}

h1 {
  font-size: 2rem;
}

h1 {
  margin: 0.5rem 0;
}

.controls {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
</style>
