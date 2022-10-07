import { UserManager } from 'oidc-client';
import {
    storeUserError,
    storeUser,
} from '../../pages/auth/actions/authActions';
import { IDENTITY_CONFIG } from '../utils/Constants/authConstants';
import jwtDecode from 'jwt-decode';

const userManager = new UserManager(IDENTITY_CONFIG);

export async function GetUser() {
    return await userManager.getUser();
}

export async function GetUserId() {
    let user = await userManager.getUser();
    let access_token = user.access_token;
    let decode = '';
    let userId = '';

    if (access_token) {
        try {
            decode = jwtDecode(access_token);
        } catch (error) {
            console.log('👾 invalid token format', error);
        }

        userId = '' + decode.sub;
        console.log(userId);

        return userId;
    }
    return userId;
}

export async function GetUserRole() {
    let user = await userManager.getUser();
    let access_token = user?.access_token;
    let decode = '';
    let userRole = '';

    if (access_token) {
        try {
            decode = jwtDecode(access_token);
        } catch (error) {
            console.log('👾 invalid token format', error);
            userRole = 'All';
            return userRole;
        }
        userRole = '' + decode.role;
        if (userRole === null && userRole === undefined) {
            userRole = 'All';
        }
        console.log(userRole);

        return userRole;
    } else {
        userRole = 'All';
        return userRole;
    }
}

export async function loadUserFromStorage(store) {
    try {
        let user = await userManager.getUser();
        if (!user) {
            return store.dispatch(storeUserError());
        }
        store.dispatch(storeUser(user));
    } catch (e) {
        console.error(`User not found: ${e}`);
        store.dispatch(storeUserError());
    }
}

export function signinRedirect() {
    return userManager.signinRedirect();
}

export function signinRedirectCallback() {
    return userManager.signinRedirectCallback();
}

export async function signoutRedirect() {
    let user = await userManager.getUser();

    userManager.clearStaleState();
    userManager.removeUser();

    let id = { id_token_hint: user.id_token };
    return userManager.signoutRedirect(id);
}

export function signoutRedirectCallback() {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirectCallback();
}

export default userManager;
