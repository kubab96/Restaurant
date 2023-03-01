<template>
  <base-card>
  <main class="form-login">
      <form @submit.prevent="loginUser">
        <h1>Log in</h1>
        <div class="form-control" :class="{ invalid: !formData.email.isValid }">
          <label for="email">Email</label>
          <input
            type="email"
            id="email"
            v-model.trim="formData.email.value"
            @blur="clearValidity('email')"
          />
          <p v-if="!formData.email.isValid">Email must not be empty.</p>
        </div>
        <div
          class="form-control"
          :class="{ invalid: !formData.password.isValid }"
        >
          <label for="password">Password</label>
          <input
            type="password"
            id="password"
            v-model.trim="formData.password.value"
            @blur="clearValidity('password')"
          />
          <p v-if="!formData.email.isValid">
            Password must not be empty.
          </p>
          <p v-if="error">{{error.response.data}}</p>
        </div>
        <base-button>Login</base-button>
        <base-button mode="outline" link @click="this.$router.replace('/')">Cancel</base-button>
      </form>
    </main>
    </base-card>
  </template>

<script>

export default {
  data() {
    return {
      formData: {
        email: {
          value: "",
          isValid: true
        },
        password: {
          value: "",
          isValid: true
        },
      },
      error: null,
      formisValid: true,
    };
  },
  methods: {
    clearValidity(input){
      this.formData[input].isValid = true;
    },
    validateForm() {
      this.formisValid = true;
      if (this.formData.email.value === "") {
        this.formData.email.isValid = false;
        this.formisValid = false;
      }
      if (this.formData.password.value === "") {
        this.formData.password.isValid = false;
        this.formisValid = false;
      }
    },
    errors(){
      return this.error = this.$store.getters["users/errors"];
    },
    async loginUser(){
      this.validateForm();
      if(!this.formisValid){
        return;
      }
      try{
        await this.$store.dispatch('users/loginUser', this.formData);
        this.$router.replace('/');
      }
        catch(e){
          this.error = e;
        }
        
    }
  }
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

input {
  display: block;
  width: 100%;
  border: 1px solid #ccc;
  font: inherit;
}

input:focus {
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

.invalid input {
  border: 1px solid red;
}
</style>

