import axios from 'axios';

export default{
    async registerUser(_, formData){
        const userData = {
            email: formData.email.value,
            password: formData.password.value,
            confirmPassword: formData.confirmPassword.value,
            firstName: formData.firstName.value,
            lastName: formData.lastName.value,
            nationality: formData.nationality.value,
            roleId: formData.roleId.value,
          };
        await axios.post(`https://localhost:7109/api/Account/register`, userData)
    },

    async loginUser(context, formData){
        const userData = {
            email: formData.email.value,
            password: formData.password.value,
          };
        await axios.post(`https://localhost:7109/api/Account/login`, userData)
        .then(response => {
            if(response.status === 200){
                context.commit('SET_USER_TOKEN', response.data)
            }
        })
    },

    logOut(context){
        context.commit('LOG_OUT')
    }
};