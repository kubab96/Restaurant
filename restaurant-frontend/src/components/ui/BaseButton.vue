<template>
    <button v-if="!link" :class="mode">
      <slot></slot>
    </button>
    <router-link v-else :to="to" :class="mode">
      <slot></slot>
    </router-link>
  </template>
  
  <script>
  export default {
    props: {
      mode: {
        type: String,
        required: false,
        default: null
      },
      link: {
        type: Boolean,
        required: false,
        default: false
      },
      to: {
        type: String,
        required: false,
        default: '/'
      }
    }
  }
  </script>
  
  <style scoped>
  button,
  a {
    text-decoration: none;
    text-align: center;
    padding: 0.75rem 1.5rem;
    font: inherit;
    background-color: #3a0061;
    border: 1px solid #3a0061;
    color: white;
    cursor: pointer;
    border-radius: 30px;
    margin-right: 0.5rem;
    display: inline-block;
  }
  
  a:hover,
  a:active,
  button:hover,
  button:active {
    background-color: #270041;
    border-color: #270041;
  }
  
  .flat {
    background-color: transparent;
    color: #3a0061;
    border: none;
  }

  .delete {
    background-color: #610000;
    color: #ffffff;
    border: none;
  }

  .menu{
    background-image: url('../../../src/assets/menu.png');
    text-decoration: none;
    text-align: center;
    padding: 0.75rem 1.5rem;
    font: inherit;
    background-size: 100%;
    width: 150px;
    height: 75px;
    color: white;
    cursor: pointer;
    border-radius: 30px;
    margin-right: 0.5rem;
    display: inline-block;
  }

  .edit {
    background-color: #002761;
    color: #ffffff;
    border: none;
  }

  .add {
    background-color: #036100;
    color: #ffffff;
    border: none;
  }

  .add:active,
  .add:hover {
    background-color: #023600;
  }

  .delete:active,
  .delete:hover {
    background-color: #440000;
  }

  .edit:active,
  .edit:hover {
    background-color: #011c44;
  }

  .menu:active,
  .menu:hover {
    background-image: url('../../../src/assets/menu_hover.png');
  }
  
  .outline {
    background-color: transparent;
    border-color: #270041;
    color: #270041;
  }
  
  .flat:hover,
  .flat:active,
  .outline:hover,
  .outline:active {
    background-color: #edd2ff;
  }
  </style>