<template>
    <section>
        <base-card>
        <h2>Add new dish</h2>
        <add-dish-form v-bind:id="id"></add-dish-form>
    </base-card>
    </section>
</template>

<script>
import AddDishForm from '../../components/dishes/AddDishForm.vue';

export default {
    props: ['id'],
    components:{
        AddDishForm
    },
}
</script>

