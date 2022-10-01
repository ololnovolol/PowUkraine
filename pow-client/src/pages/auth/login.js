import { signinRedirect } from '../../common/services/userService';
import { useSelector } from 'react-redux';

function Login() {
    useSelector(state => state.auth.user);

    function login() {
        signinRedirect();
    }

    return login();
}

export default Login;
