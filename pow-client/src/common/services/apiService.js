import axios from 'axios';

export async function getFromApi_Admin() {
    const response = await axios.get('https://localhost:44312/api/home/getall');
    return response.data;
}

export async function getFromApi_User() {
    const response = await axios.get('https://localhost:44312/api/home/get');
    return response.data;
}

export async function getUsers() {
    const response = await axios.get(
        'https://localhost:44316/api/roles/getUsers',
    );
    return response.data;
}

export async function getUser(id) {
    let request = { UserId: id };
    let user = {
        FirstName: '',
        LastName: '',
        Email: '',
        PhoneNumber: '',
        BirthDay: '',
        UserId: '',
    };
    const response = axios
        .post('https://localhost:44316/api/roles/getUser', request, {
            headers: {
                accept: 'application/json',
            },
        })
        .then(function (response) {
            user = {
                FirstName: response.data.firstName,
                LastName: response.data.lastName,
                Email: response.data.email,
                PhoneNumber: response.data.phoneNumber,
                BirthDay: response.data.birthDay,
                UserId: id,
            };

            return user;
        })
        .catch(function (error) {
            console.log(error);
        });

    return response;
}

export async function changeUserRole() {
    const response = await axios.get(
        'https://localhost:44316/api/roles/getUsers',
    );
    return response.data;
}

export async function updateUser() {
    const response = await axios.get(
        'https://localhost:44316/api/roles/getUsers',
    );
    return response.data;
}

export async function deleteUser() {
    const response = await axios.get('localhost:44316/api/roles/getUsers');
    return response.data;
}

export async function sentMessage(values) {
    console.log(values.Title);

    axios
        .put('https://localhost:44312/api/home/message', {
            data: values,
            headers: {
                'Content-Type': 'application/json',
            },
            responseType: 'json',
        })
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        });
}
