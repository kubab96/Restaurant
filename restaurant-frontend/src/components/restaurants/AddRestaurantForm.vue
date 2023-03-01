<template>
  <form @submit.prevent="addRestaurant">
    <div class="form-control" :class="{ invalid: !formData.name.isValid }">
      <label for="name">Restaurant name</label>
      <input
        type="text"
        id="name"
        v-model.trim="formData.name.value"
        @blur="clearValidity('name')"
      />
      <p v-if="!formData.name.isValid">Restaurant name must not be empty.</p>
    </div>
    <div class="form-control">
      <label for="description">Description</label>
      <textarea
        id="description"
        rows="5"
        v-model.trim="formData.description.value"
      ></textarea>
    </div>
    <div class="form-control" :class="{ invalid: !formData.category.isValid }">
      <label for="category">Category</label>
      <input
        type="text"
        id="category"
        v-model.trim="formData.category.value"
        @blur="clearValidity('category')"
      />
      <p v-if="!formData.category.isValid">Category must not be empty.</p>
    </div>
    <div class="form-control">
      <label for="hasDelivery">Has delivery?</label>
      <input
        type="checkbox"
        id="hasDelivery"
        v-model="formData.hasDelivery.value"
      />
    </div>
    <div class="form-control">
      <label for="contactEmail">Contact email</label>
      <input
        type="text"
        id="contactEmail"
        v-model.trim="formData.contactEmail.value"
      />
    </div>
    <div class="form-control">
      <label for="contactNumber">Contact number</label>
      <input
        type="text"
        id="contactNumber"
        v-model.trim="formData.contactNumber.value"
      />
    </div>
    <div class="form-control" :class="{ invalid: !formData.city.isValid }">
      <label for="city">City</label>
      <input
        type="text"
        id="city"
        v-model.trim="formData.city.value"
        @blur="clearValidity('city')"
      />
      <p v-if="!formData.city.isValid">City must not be empty.</p>
    </div>
    <div class="form-control" :class="{ invalid: !formData.street.isValid }">
      <label for="street">Street</label>
      <input
        type="text"
        id="street"
        v-model.trim="formData.street.value"
        @blur="clearValidity('street')"
      />
      <p v-if="!formData.street.isValid">Street must not be empty.</p>
    </div>
    <div class="form-control">
      <label for="postalCode">Postal code</label>
      <input
        type="text"
        id="postalCode"
        v-model.trim="formData.postalCode.value"
      />
    </div>
    <p v-if="!formisValid">Please fix the above errors and submit again.</p>
    <base-button>Add restaurant</base-button>
    <base-button mode="outline" link @click="this.$router.go(-1)">Cancel</base-button>
    <p v-if="error">{{ this.error }}</p>
  </form>
</template>

<script>
export default {
  data() {
    return {
      formData: {
        name: {
          value: "",
          isValid: true,
        },
        description: {
          value: "",
          isValid: true,
        },
        category: {
          value: "",
          isValid: true,
        },
        hasDelivery: {
          value: true,
          isValid: true,
        },
        contactEmail: {
          value: "",
          isValid: true,
        },
        contactNumber: {
          value: "",
          isValid: true,
        },
        city: {
          value: "",
          isValid: true,
        },
        street: {
          value: "",
          isValid: true,
        },
        postalCode: {
          value: "",
          isValid: true,
        },
        token: "",
      },
      error: null,
      formisValid: true,
    };
  },
  methods: {
    clearValidity(input) {
      this.formData[input].isValid = true;
    },
    validateForm() {
      this.formisValid = true;
      if (this.formData.name.value === "") {
        this.formData.name.isValid = false;
        this.formisValid = false;
      }
      if (this.formData.category.value === "") {
        this.formData.category.isValid = false;
        this.formisValid = false;
      }
      if (this.formData.city.value === "") {
        this.formData.city.isValid = false;
        this.formisValid = false;
      }
      if (this.formData.street.value === "") {
        this.formData.street.isValid = false;
        this.formisValid = false;
      }
    },
    async addRestaurant() {
      this.validateForm();

      if (!this.formisValid) {
        return;
      }
      this.formData.token = this.$store.getters["users/token"];
      try{
        await this.$store.dispatch("restaurants/addRestaurant", this.formData);
        this.$router.replace(`../restaurants`);
      }
      catch(e){
        this.error = e;
      }
    },
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
