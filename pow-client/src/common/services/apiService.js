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
    console.log(response.data);
    return response.data;
}

export async function getUser(id) {
    let request = { Data: id };
    let user = {
        FirstName: '',
        LastName: '',
        Email: '',
        PhoneNumber: '',
        BirthDay: DataTransfer.Data,
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
            console.log(user);
            return user;
        })
        .catch(function (error) {
            console.log(error);
        });

    return response;
}

export async function changeUserRole(userEmail) {
    let request = { Data: userEmail };
    console.log(userEmail + '');
    const response = await axios
        .post('https://localhost:44316/api/roles/ChangeRole', request, {
            headers: {
                accept: 'application/json',
            },
        })
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        });

    return response;
}

export async function updateUser(doughnutData) {
    let user = {
        FirstName: doughnutData.FirstName,
        LastName: doughnutData.LastName,
        Email: doughnutData.Email,
        PhoneNumber: doughnutData.PhoneNumber,
        BirthDay: doughnutData.BirthDay,
        UserId: doughnutData.UserId,
    };

    const response = await axios
        .post('https://localhost:44316/api/roles/updateUser', user, {
            headers: {
                accept: 'application/json',
            },
        })
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        });

    return response.data;
}

export async function deleteUser(userEmail) {
    let request = { Data: userEmail };

    console.log(userEmail + '');

    const response = await axios
        .post('https://localhost:44316/api/roles/DeleteUser', request, {
            headers: {
                accept: 'application/json',
            },
        })
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        });
    return response;
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
