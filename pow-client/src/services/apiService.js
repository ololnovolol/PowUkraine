import axios from 'axios'

async function getDoughnutsFromApi() {
  const response = await axios.get('https://localhost:44312/api/home/getall');
  return response.data;
}

export {
  getDoughnutsFromApi
}
