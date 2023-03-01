<template>
    <li>
      <h3>{{ name }}</h3>
      <h3>{{ price }}</h3>
      <base-button link :to="{ name: 'dishDetails', params: {restaurantId: this.restaurantId, dishId: this.id } }">View details</base-button>
    </li>
  </template>
  
  <script>
  export default {
    props: ["id", "name", "price", "restaurantId"],
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
  
  .actions {
    display: flex;
    justify-content: flex-end;
  }
  </style>
  