import { UserManager } from 'oidc-client';
import {
    storeUserError,
    storeUser,
} from '../../pages/auth/actions/authActions';
import { IDENTITY_CONFIG } from '../utils/Constants/authConstants';

const userManager = new UserManager(IDENTITY_CONFIG);

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
