<template>
    <section>
        <base-card>
        <div class="controls">
          <base-button mode="edit" v-if="isLoggedIn && userInfo.UserId == restaurant.createdById || userInfo.UserRole === 'Admin'" link :to="{ name: 'editDish', params: {restaurantId: this.restaurantId, dishId: this.dishId } }">Edit dish</base-button>
        <base-button mode="delete" v-if="isLoggedIn && userInfo.UserId == restaurant.createdById || userInfo.UserRole === 'Admin'" @click="this.deleteDish">Delete</base-button>
        </div>
        <h1>{{ dish.name }}</h1>
        <h3>Price: {{ dish.price }}</h3>
        <p v-if="dish.description"><b>Description: </b>{{ dish.description }}</p>
        
        <p v-if="error">{{ this.error }}</p>
        <base-button mode="outline" link @click="this.$router.go(-1)">Back</base-button>
        <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    </base-card>
    <router-view></router-view>
    
    </section>
</template>

<script>
import ConfirmDialogue from "@/components/ui/ConfirmDialogue.vue"

export default {
  props: ['restaurantId', 'dishId'],
  data(){
    return{
      error: null,
      formData: {
        token: '',
        restaurantId: this.restaurantId,
        dishId: this.dishId,
      }
    }
  },
  components: { ConfirmDialogue },
  computed: {
    dish() {
      return this.$store.getters["dishes/dish"];
    },
    restaurant() {
      return this.$store.getters["restaurants/restaurant"];
    },
    isLoggedIn() {
      return this.$store.getters["users/isLogIn"];
    },
    userInfo() {
      return this.$store.getters["users/userInfo"];
    },
  },
  methods:{
    async deleteDish() {
      this.formData.token = this.$store.getters["users/token"];
      const ok = await this.$refs.confirmDialogue.show({
        title: "Delete Dish",
        message:
          "Are you sure you want to delete this dish? It cannot be undone.",
        okButton: "Confirm Delete",
      });
      if (ok) {
        try{
          await this.$store.dispatch("dishes/deleteDish", this.formData);
          this.$router.replace("./");
        }
        catch(e){
          this.error = e;
        }
      } else {
        return;
      }
    },
  },
  created() {
    this.$store.dispatch('dishes/getDish', {
        restaurantId: this.restaurantId,
        dishId: this.dishId,
      });
  },
};
</script>

<style scoped>
ul {
  list-style: none;
  margin: 0;
  padding: 0;
}

.controls {
  display: flex;
  justify-content: flex-end;
}
</style>