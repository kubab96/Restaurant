<template>
  <li>
    <div class="controls">
      <h3>{{ name }}</h3>
      <img v-if="hasDelivery" src="../../../src/assets/delivery.png" alt="has delivery" width="50" height="50">
        </div>
    
    <h4>Category: {{ category }}</h4>
    <h5>Address: {{ city }}, {{ street }}</h5>

    <div class="controls">
    <p></p>
    <base-button link :to="restaurantDetailsLink">View details</base-button>
  </div>
  </li>
</template>

<script>
export default {
  props: ["id", "createdById", "name", "hasDelivery", "category", "city", "street"],
  computed: {
    restaurantDetailsLink() {
      return this.$route.path + "/" + this.id;
    },
  },
};
</script>

<style scoped>
li {
  margin: 1rem 0;
  border: 1px solid #424242;
  border-radius: 12px;
  padding: 1rem;
}

h3 {
  font-size: 1.5rem;
}

h3,
h4 {
  margin: 0.5rem 0;
}

div {
  margin: 0.5rem 0;
}

.controls{
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.actions {
  display: flex;
  justify-content: flex-end;
}
</style>
