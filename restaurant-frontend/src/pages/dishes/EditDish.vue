<template>
    <base-card>
  <form @submit.prevent="editRestaurant">
    <div class="form-control" :class="{ invalid: !formData.name.isValid }">
      <label for="name">Dish name</label>
      <input
        type="text"
        id="name"
        v-model.trim="formData.name.value"
        @blur="clearValidity('name')"
      />
      <p v-if="!formData.name.isValid">Dish name must not be empty.</p>
    </div>
    <div class="form-control" :class="{ invalid: !formData.price.isValid }">
      <label for="price">Price</label>
      <input
        type="decimal"
        min="0"
        id="price"
        v-model.number="formData.price.value"
        @blur="clearValidity('price')"
      />
      <p v-if="!formData.price.isValid">Price must greater than 0.</p>
      <p v-if="error">{{ this.error }}</p>
    </div>
    <base-button>Edit dish</base-button>
    <base-button mode="outline" link @click="this.$router.go(-1)">Cancel</base-button>
  </form>
</base-card>
</template>

<script>
export default {
  props: ["restaurantId", "dishId"],
  data() {
    return {
      formData: {
        name: {
          value: "",
          isValid: true,
        },
        price: {
          value: "",
          isValid: true,
        },
        restaurantId: this.restaurantId,
        dishId: this.dishId,
        token: "",
      },
      error: null,
      formisValid: true,
    };
  },
  methods: {
    loadDish(){
            this.$store.dispatch("dishes/getDish", this.formData);
            const dishData =  this.$store.getters["dishes/dish"];
            this.formData.name.value = dishData.name;
            this.formData.price.value = dishData.price;
        },
    clearValidity(input) {
      this.formData[input].isValid = true;
    },
    validateForm() {
      this.formisValid = true;
      if (this.formData.name.value === "") {
        this.formData.name.isValid = false;
        this.formisValid = false;
      }
      if (!this.formData.price.value || this.formData.price.value < 0) {
        this.formData.price.isValid = false;
        this.formisValid = false;
      }
    },
    async editRestaurant() {
      this.validateForm();
      if (!this.formisValid) {
        return;
      }
      this.formData.token = this.$store.getters["users/token"];
      try {
        await this.$store.dispatch("dishes/editDish", this.formData);
        this.$router.replace(`../`);
      } catch (e) {
        this.error = e;
      }
    },
  },
  created() {
    this.loadDish();
  },
};
</script>

<style scoped>
  .form-control {
    margin: 0.5rem 0;
  }
  
  label {
    font-weight: bold;
    display: block;
    margin-bottom: 0.5rem;
  }
  
  input[type="checkbox"] + label {
    font-weight: normal;
    display: inline;
    margin: 0 0 0 0.5rem;
  }
  
  input,
  textarea {
    display: block;
    width: 100%;
    border: 1px solid #ccc;
    font: inherit;
  }
  
  input:focus,
  textarea:focus {
    background-color: #f0e6fd;
    outline: none;
    border-color: #3d008d;
  }
  
  input[type="checkbox"] {
    display: inline;
    width: auto;
    border: none;
  }
  
  input[type="checkbox"]:focus {
    outline: #3d008d solid 1px;
  }
  
  h3 {
    margin: 0.5rem 0;
    font-size: 1rem;
  }
  
  .invalid label {
    color: red;
  }
  
  .invalid input,
  .invalid textarea {
    border: 1px solid red;
  }
  </style>
  