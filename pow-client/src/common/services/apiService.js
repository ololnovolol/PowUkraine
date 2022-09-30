import axios from 'axios'

export async function getFromApi_Admin() {
  const response = await axios.get('https://localhost:44312/api/home/getall');
  return response.data;
};

export async function getFromApi_User() {
  const response = await axios.get('https://localhost:44312/api/home/get');
  return response.data;
};

export async function getUsers() {
  const response = await axios.get('https://localhost:44316/api/roles/getUsers');
  return response.data;
};

export async function changeUserRole() {
  const response = await axios.get('https://localhost:44316/api/roles/getUsers');
  return response.data;
};

export async function updateUser() {
  const response = await axios.get('https://localhost:44316/api/roles/getUsers');
  return response.data;
};

export async function deleteUser() {
  const response = await axios.get('localhost:44316/api/roles/getUsers');
  return response.data;
};

export async function sentMessage(values) {
    console.log(values.Title);
    
    axios.put("https://localhost:44312/api/home/message",{
      data: values,
        headers:{
            'Content-Type': 'application/json'
        },
        responseType: "json"
      /*
        Title: values.Title,
        Description: "values.Description",
        Data: values.Data,
        PhoneNumber: "values.PhoneNumber",*/
    })
    .then(function (response) {
        console.log(response);
    })
    .catch(function (error) {
        console.log(error);
    });
    
  }





