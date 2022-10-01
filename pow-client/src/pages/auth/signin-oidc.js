import React, { useEffect } from 'react';
import { signinRedirectCallback } from '../../common/services/userService';
import { useHistory } from 'react-router-dom';

function SigninOidc() {
    const history = useHistory();
    useEffect(() => {
        async function signinAsync() {
            await signinRedirectCallback();
            history.push('/');
        }
        signinAsync();
    }, [history]);

    return <div>Redirecting...</div>;
}

export default SigninOidc;
