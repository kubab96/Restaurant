<template>
  <div id="list">
    <section>
      <base-card>
        <div class="controls">
          <base-button mode="add"
            v-if="
              (isLoggedIn && createdBy.createdById == userInfo.UserId) ||
              userInfo.UserRole === 'Admin'
            "
            link
            :to="{ name: 'addDish', params: { id: this.id } }"
            >Add dish</base-button
          >
        </div>

        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>

        <table v-else-if="!isLoading && !error">
          <thead>
            <tr>
              <th>Dish</th>
              <th>Price</th>
              <th>Details</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="dish in dishes" :key="dish.id">
              <td>{{ dish.name }}</td>
              <td>{{ dish.price }}</td>
              <td>
                <base-button
                  link
                  :to="{
                    name: 'dishDetails',
                    params: { restaurantId: this.id, dishId: dish.id },
                  }"
                  >View details</base-button
                >
              </td>
            </tr>
          </tbody>
        </table>

        <p v-else>{{ error }}</p>
        <base-button mode="outline" link @click="this.$router.go(-1)"
          >Back</base-button
        >
      </base-card>
    </section>
  </div>
</template>

<script>
export default {
  props: ["id"],
  data() {
    return {
      error: null,
      isLoading: false,
    };
  },
  computed: {
    dishes() {
      return this.$store.getters["dishes/dishes"];
    },
    createdBy() {
      return this.$store.getters["restaurants/restaurant"];
    },
    userInfo() {
      return this.$store.getters["users/userInfo"];
    },
    isLoggedIn() {
      return this.$store.getters["users/isLogIn"];
    },
  },
  methods: {
    async getDishes() {
      this.isLoading = true;
      try {
        await this.$store.dispatch("dishes/getDishes", this.id);
      } catch (error) {
        this.error = error.response.data;
      }
      this.isLoading = false;
    },
  },
  created() {
    this.getDishes();
  },
};
</script>

<style scoped>
.controls {
  display: flex;
  justify-content: space-between;
}

table {
  color: #666;

  background: #eaebec;
  padding: 0%;
  margin-top: 15px;
  margin-bottom: 15px;
  width: 100%;
  border: #ccc 1px solid;

  -moz-border-radius: 3px;
  -webkit-border-radius: 3px;
  border-radius: 3px;

  -moz-box-shadow: 0 1px 2px #d1d1d1;
  -webkit-box-shadow: 0 1px 2px #d1d1d1;
  box-shadow: 0 1px 2px #d1d1d1;
}
table th {
  padding: 21px 25px 22px 25px;
  border-top: 1px solid #fafafa;
  border-bottom: 1px solid #e0e0e0;

  background: #ededed;
  background: -moz-linear-gradient(top, #ededed, #ebebeb);
}
table th:first-child {
  text-align: left;
  padding-left: 20px;
}
table tr:first-child th:first-child {
  -moz-border-radius-topleft: 3px;
  -webkit-border-top-left-radius: 3px;
  border-top-left-radius: 3px;
}
table tr:first-child th:last-child {
  -moz-border-radius-topright: 3px;
  -webkit-border-top-right-radius: 3px;
  border-top-right-radius: 3px;
}
table tr {
  text-align: center;
  padding-left: 20px;
}
table td:first-child {
  text-align: left;
  padding-left: 20px;
  border-left: 0;
}
table td {
  padding: 18px;
  border-top: 1px solid #ffffff;
  border-bottom: 1px solid #e0e0e0;
  border-left: 1px solid #e0e0e0;

  background: #fafafa;
  background: -moz-linear-gradient(top, #fbfbfb, #fafafa);
}
table tr.even td {
  background: #f6f6f6;
  background: -moz-linear-gradient(top, #f8f8f8, #f6f6f6);
}
table tr:last-child td {
  border-bottom: 0;
}
table tr:last-child td:first-child {
  -moz-border-radius-bottomleft: 3px;
  -webkit-border-bottom-left-radius: 3px;
  border-bottom-left-radius: 3px;
}
table tr:last-child td:last-child {
  -moz-border-radius-bottomright: 3px;
  -webkit-border-bottom-right-radius: 3px;
  border-bottom-right-radius: 3px;
}
table tr:hover td {
  background: #f2f2f2;
  background: -moz-linear-gradient(top, #f2f2f2, #f0f0f0);
}
</style>
