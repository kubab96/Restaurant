<template>
  <base-card>
    <main class="form-register">
      <form @submit.prevent="registerUser">
        <h1>Register user</h1>
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
          <p v-if="!formData.password.isValid">
            Password must not be empty and must match with confirm password.
          </p>
        </div>
        <div
          class="form-control"
          :class="{ invalid: !formData.confirmPassword.isValid }"
        >
          <label for="confirmPassword">Confirm password</label>
          <input
            type="password"
            id="confirmPassword"
            v-model.trim="formData.confirmPassword.value"
            @blur="clearValidity('confirmPassword')"
          />
          <p v-if="!formData.email.isValid">
            Confirm password must not be empty and must match with password.
          </p>
        </div>
        <div class="form-control">
          <label for="firstName">First name</label>
          <input
            type="text"
            id="firstName"
            v-model.trim="formData.firstName.value"
          />
        </div>
        <div class="form-control">
          <label for="nationality">Nationality</label>
          <input
            type="text"
            id="nationality"
            v-model.trim="formData.nationality.value"
          />
        </div>
        <div class="form-control">
          <label for="lastName">Last name</label>
          <input
            type="text"
            id="lastName"
            v-model.trim="formData.lastName.value"
          />
        </div>
        <div class="form-control">
          <label for="role">Role</label>
          <select v-model="formData.roleId.value">
            <option
              v-for="option in options"
              :key="option.value"
              :value="option.value"
            >
              {{ option.text }}
            </option>
          </select>
        </div>
        <p v-if="error">{{error.response.data.errors}}</p>
        <base-button>Register</base-button>
        <base-button mode="outline" link @click="this.$router.replace('/')">Cancel</base-button>
      </form>
    </main>
  </base-card>
</template>

<script>
export default {
  data() {
    return {
      options: [
        { text: "User", value: 1 },
        { text: "Menager", value: 2 },
        { text: "Admin", value: 3 },
      ],
      formData: {
        email: {
          value: "",
          isValid: true,
        },
        password: {
          value: "",
          isValid: true,
        },
        confirmPassword: {
          value: "",
          isValid: true,
        },
        firstName: {
          value: "",
          isValid: true,
        },
        lastName: {
          value: "",
          isValid: true,
        },
        nationality: {
          value: "",
          isValid: true,
        },
        roleId: {
          value: 1,
          isValid: true,
        },
      },
      formisValid: true,
      error: null,
    };
  },
  methods: {
    clearValidity(input) {
        this.formData[input].isValid = true;
    },
    validateForm() {
      this.formisValid = true;
      if (this.formData.email.value === "") {
        this.formData.email.isValid = false;
        this.formisValid = false;
      }
      if (
        this.formData.password.value === "" ||
        this.formData.password.value !== this.formData.confirmPassword.value
      ) {
        this.formData.password.isValid = false;
        this.formisValid = false;
      }
      if (
        this.formData.confirmPassword.value === "" ||
        this.formData.confirmPassword.value !== this.formData.password.value
      ) {
        this.formData.confirmPassword.isValid = false;
        this.formisValid = false;
      }
    },
    async registerUser() {
      this.validateForm();
      if (!this.formisValid) {
        return;
      }
      try{
        await this.$store.dispatch("users/registerUser", this.formData);
        this.$router.replace('../login');
      }
      catch(e){
        this.error = e;
        console.log(e);
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
