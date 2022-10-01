import { signoutRedirect } from '../../common/services/userService';
import { useSelector } from 'react-redux';

function Logout() {
    useSelector(state => state.auth.user);

    function signOut() {
        signoutRedirect();
    }

    return signOut();
}

export default Logout;
