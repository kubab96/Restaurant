import axios from 'axios';

export default{
    async getRestaurants(context, page){
        await axios.get(`https://localhost:7109/api/restaurant?searchPhrase=${page.SearchPhrase}&pageSize=${page.PageSize}&pageNumber=${page.PageNumber}&sortBy=${page.SortBy}&sortDirection=${page.SortDirection}`)
        .then(response => {
            context.commit('SET_RESTAURANTS', response.data);
        })
    },  
    async getRestaurant(context, restaurantId){
        await axios.get(`https://localhost:7109/api/restaurant/${restaurantId}`)
        .then(response => {
            context.commit('SET_RESTAURANT', response.data);
        })
    },
    async deleteRestaurant(_, formData){
        await axios.delete(`https://localhost:7109/api/restaurant/${formData.restaurantId}`, { headers: {
            "Authorization": `Bearer ${formData.token}`,
        }})
        .then(response => {
            console.log(response);
        })
    },
    async addRestaurant(_, formData){
        const restaurantData = {
            name: formData.name.value,
            description: formData.description.value,
            category: formData.category.value,
            hasDelivery: formData.hasDelivery.value,
            contactEmail: formData.contactEmail.value,
            contactNumber: formData.contactNumber.value,
            city: formData.city.value,
            street: formData.street.value,
            postalCode: formData.postalCode.value,
          };
        await axios.post(`https://localhost:7109/api/restaurant`, restaurantData, { headers: {
            "Authorization": `Bearer ${formData.token}`,
        }})
    },

    async editRestaurant(_, formData){
        const restaurantData = {
            id: formData.id.value,
            name: formData.name.value,
            description: formData.description.value,
            category: formData.category.value,
            hasDelivery: formData.hasDelivery.value,
            contactEmail: formData.contactEmail.value,
            contactNumber: formData.contactNumber.value,
            city: formData.city.value,
            street: formData.street.value,
            postalCode: formData.postalCode.value,
          };
        await axios.put(`https://localhost:7109/api/restaurant/${restaurantData.id}`, restaurantData, { headers: {
            "Authorization": `Bearer ${formData.token}`,
        }})
        .then(response => {
            console.log(response);
        })
    },
};