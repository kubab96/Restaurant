<template>
  <section>
    <base-card>
      <div class="controls">
        <base-button
          mode="edit"
          v-if="
            (isLoggedIn && userInfo.UserId == restaurant.createdById) ||
            userInfo.UserRole === 'Admin'
          "
          link
          :to="{
            name: 'edit',
            params: {
              id: id,
              name: restaurant.name,
              description: restaurant.description,
              category: restaurant.category,
              hasDelivery: restaurant.hasDelivery,
              contactEmail: restaurant.contactEmail,
              contactNumber: restaurant.contactNumber,
              city: restaurant.city,
              street: restaurant.street,
              postalCode: restaurant.postalCode,
            },
          }"
          >Edit</base-button
        >
        <base-button
          mode="delete"
          v-if="
            (isLoggedIn && userInfo.UserId == restaurant.createdById) ||
            userInfo.UserRole === 'Admin'
          "
          @click="deleteRestaurant"
          >Delete</base-button
        >
      </div>

      <li>
        <h1>{{ restaurant.name }}</h1>
        <div><b>Category: </b>{{ restaurant.category }}</div>
        <div><b>Description: </b>{{ restaurant.description }}</div>
        <div>
          <b>Delivery: </b><span v-if="!restaurant.hasDelivery">No</span>
          <span v-else>Yes</span>
        </div>

        <div
          v-if="restaurant.contactEmail || restaurant.contactNumber"
          id="contact"
        >
          <p><b>Contact: </b></p>
          <div v-if="restaurant.contactEmail">
            <b>E-mail: </b>{{ restaurant.contactEmail }}
          </div>
          <div v-if="restaurant.contactNumber">
            <b>Telephone: </b>{{ restaurant.contactNumber }}
          </div>
        </div>

        <p><b>Address: </b></p>
        <div><b>City: </b>{{ restaurant.city }}</div>
        <div><b>Street: </b>{{ restaurant.street }}</div>
        <div v-if="restaurant.postalCode">
          <b>Postal code: </b>{{ restaurant.postalCode }}
        </div>
        <div class="menu">
          <base-button
            mode="menu"
            link
            :to="{
              name: 'dishes',
              params: { createdById: restaurant.createdById },
            }"
          ></base-button>
        </div>
      </li>

      <base-button mode="outline" link @click="this.$router.replace('/')"
        >Back</base-button
      >
      <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>

      <p v-if="error">{{ this.error }}</p>
    </base-card>
  </section>
</template>

<script>
import ConfirmDialogue from "@/components/ui/ConfirmDialogue.vue"

export default {
  props: ["id"],
  data() {
    return {
      error: null,
      formData: {
        token: "",
        restaurantId: this.id,
      },
    };
  },
  components: { ConfirmDialogue },
  computed: {
    restaurant() {
      return this.$store.getters["restaurants/restaurant"];
    },
    dishesLink() {
      return this.$route.path + "/dishes";
    },
    isLoggedIn() {
      return this.$store.getters["users/isLogIn"];
    },
    userInfo() {
      return this.$store.getters["users/userInfo"];
    },
  },
  methods: {
    async deleteRestaurant() {
      this.formData.token = this.$store.getters["users/token"];
      const ok = await this.$refs.confirmDialogue.show({
        title: "Delete Restaurant",
        message:
          "Are you sure you want to delete this restaurant? It cannot be undone.",
        okButton: "Confirm Delete",
      });
      if (ok) {
        try {
          await this.$store.dispatch(
            "restaurants/deleteRestaurant",
            this.formData
          );
          this.$router.replace("/restaurants");
        } catch (e) {
          this.error = e.message;
        }
      } else {
        return;
      }
    },
  },

  created() {
    this.$store.dispatch("restaurants/getRestaurant", this.id);
  },
};
</script>

<style scoped>
li {
  margin: 1rem 0;
  border: 1px solid #424242;
  border-radius: 12px;
  padding: 1rem;
  list-style: none;
}

ul {
  list-style: none;
  margin: 0;
  padding: 0;
}

.controls {
  display: flex;
  justify-content: flex-end;
}

.menu {
  display: flex;
  justify-content: flex-end;
}
</style>
