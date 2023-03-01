<template>
    <section>
        <base-card>
        <h2>Add new restaurant</h2>
        <add-restaurant-form></add-restaurant-form>
    </base-card>
    </section>
</template>

<script>
import AddRestaurantForm from '../../components/restaurants/AddRestaurantForm.vue';

export default {
    components:{
        AddRestaurantForm
    }
}
</script>

