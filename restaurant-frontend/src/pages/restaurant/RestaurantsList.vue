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

        

        <div class="sort-items">
          <div class="search">
            <input type="text" v-model="search" placeholder="Restaurant name" />
            <button class="pagination-button" @click="loadRestaurants">Search</button>
          </div>
          <div class="sort">
            <select v-model="sortBy" @change="this.loadRestaurants">
              <option
                v-for="option in options"
                :key="option.value"
                :value="option.text"
                
              >
                {{ option.text }}
              </option>
            </select>
            
            <button
              :class="ascIsActive ? 'sort-active' : 'sort-btn'"
              type="button"
              @click="this.setAsc"
            >
              &uarr;
            </button>
            <button
              :class="descIsActive ? 'sort-active' : 'sort-btn'"
              type="button"
              @click="this.setDesc"
            >
              &darr;
            </button>
          </div>
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


        <div class="pagination-items">
          <div class="pagination">
              <button class="pagination-button" type="button" @click="first">
                First
              </button>

              <button class="pagination-button" type="button" @click="previous">
                &lt;
              </button>

              <button class="pagination-button" type="button" @click="next">
                &gt;
              </button>


              <button class="pagination-button" type="button" @click="last">
                Last
              </button>

          </div>

          <div class="size">

              <button
                class="pagination-button"
                type="button"
                @click="setPageSize(5)"
              >
                5
              </button>

              <button
                class="pagination-button"
                type="button"
                @click="setPageSize(10)"
              >
                10
              </button>

              <button
                class="pagination-button"
                type="button"
                @click="setPageSize(15)"
              >
                15
              </button>

          </div>
        </div>

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
      search: "",
      sortBy: "Name",
      ascIsActive: true,
      descIsActive: false,
      sortDirection: "ASC",
      options: [
        { text: "Name", value: 1 },
        { text: "Category", value: 2 },
        { text: "City", value: 3 },
      ],
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
    setAsc() {
      this.sortDirection = "ASC";
      this.descIsActive = false;
      this.ascIsActive = true;
      this.loadRestaurants();
    },
    setDesc() {
      this.sortDirection = "DESC";
      this.descIsActive = true;
      this.ascIsActive = false;
      this.loadRestaurants();
    },
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
        SearchPhrase: this.search,
        SortBy: this.sortBy,
        SortDirection: this.sortDirection,
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
  margin-left: 0.2rem;
  display: inline-block;
}

a:hover,
a:active,
button:hover,
button:active {
  background-color: #270041;
  border-color: #270041;
}

.sort-active{
  text-decoration: none;
  padding: 0.35rem 0.8rem;
  margin-left: -0.2rem;
  font: inherit;
  border-color: #270041;
  cursor: pointer;
  display: inline-block;
}

.sort-btn{
  text-decoration: none;
  padding: 0.35rem 0.8rem;
  margin-left: -0.2rem;
  font: inherit;
  border-color: #ffffff;
  cursor: pointer;
  display: inline-block;
}

.sort-active:hover,
.sort-btn:hover {
  background-color: #ffffff;
  border-color: #270041;
}

.pagination-items {
  display: flex;
  justify-content: center;
  border-top: 1px solid #eee;
  margin-top: 1em;
  padding-top: 1em;
  margin-bottom: 0.5em;
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
.sort-items {
  display: flex;
  justify-content: center;
  border-top: 1px solid #eee;
  margin-top: 1em;
  padding-top: 1em;
  margin-bottom: 0.5em;
}
.search {
  gap: 5px;
  list-style: none;
  margin: 0;
  padding: 0;
  display: flex;
}
.sort {
  gap: 10px;
  text-align: center;
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

select { width: 7.5em }

.controls {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
</style>
