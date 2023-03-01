import axios from 'axios';

export default{
    async getDishes(context, restaurantId){
        await axios.get(`https://localhost:7109/api/restaurant/${restaurantId}/dish`)
        .then(response => {
            context.commit('SET_DISHES', response.data);
        })
    },  
    
    async getDish(context, ids){
        await axios.get(`https://localhost:7109/api/restaurant/${ids.restaurantId}/dish/${ids.dishId}`)
        .then(response => {
            context.commit('SET_DISH', response.data);
        })
    },

    async addDish(_, formData){
        const dishData = {
            name: formData.name.value,
            price: formData.price.value,
            restaurantId: formData.restaurantId
          };
        await axios.post(`https://localhost:7109/api/restaurant/${formData.restaurantId}/dish`, dishData, { headers: {
            "Authorization": `Bearer ${formData.token}`,
        }})
        .then(response => console.log(response))
        .catch(error => console.log(error))
    },

    async deleteDish(_, formData){
        await axios.delete(`https://localhost:7109/api/restaurant/${formData.restaurantId}/dish/${formData.dishId}`, { headers: {
            "Authorization": `Bearer ${formData.token}`,
        }})
        .then(response => {
            console.log(response);
        })
    },

    async editDish(_, formData){
        const dishData = {
            name: formData.name.value,
            price: formData.price.value,
            restaurantId: formData.restaurantId
          };
        await axios.put(`https://localhost:7109/api/restaurant/${formData.restaurantId}/dish/${formData.dishId}`, dishData, { headers: {
            "Authorization": `Bearer ${formData.token}`,
        }})
    },
};