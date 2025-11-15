import axios from 'axios';

axios.defaults.baseURL = `http://localhost:5026/`;
axios.defaults.headers.common['Accept'] = '*/*';
axios.defaults.headers.post['Content-Type'] = 'application/json';
axios.defaults.headers.get['Cache-Control'] = 'no-cache';
axios.defaults.headers.get['Pragma'] = 'no-cache';
axios.defaults.headers.get['Expires'] = '0';

export default axios;