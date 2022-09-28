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
  const response = await axios.get('https://localhost:44316/api/roles/getusers');
  return response.data;
};



