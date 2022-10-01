export const IDENTITY_CONFIG = {
    authority: (process.env.REACT_APP_AUTH_URL = 'https://localhost:44316/'),
    client_id: (process.env.REACT_APP_IDENTITY_CLIENT_ID = 'pow-web-app'),
    redirect_uri: (process.env.REACT_APP_REDIRECT_URL =
        'http://localhost:3000/signin-oidc'),
    silent_redirect_uri: (process.env.REACT_APP_SILENT_REDIRECT_URL =
        'http://localhost:3000/signout-callback-oidc'),
    post_logout_redirect_uri: (process.env.REACT_APP_LOGOFF_REDIRECT_URL =
        'http://localhost:3000/signout-callback-oidc'),
    audience: process.env.REACT_APP_AUDIENCE,
    response_type: 'code',
    automaticSilentRenew: false,
    loadUserInfo: false,
    scope: (process.env.REACT_APP_SCOPE = 'openid profile PowWebApi'),
};

export const METADATA_OIDC = {
    issuer: process.env.REACT_APP_ISSUER,
    jwks_uri:
        process.env.REACT_APP_AUTH_URL +
        '/.well-known/openid-configuration/jwks',
    authorization_endpoint:
        process.env.REACT_APP_AUTH_URL + '/connect/authorize',
    token_endpoint: process.env.REACT_APP_AUTH_URL + '/connect/token',
    userinfo_endpoint: process.env.REACT_APP_AUTH_URL + '/connect/userinfo',
    end_session_endpoint:
        process.env.REACT_APP_AUTH_URL + '/connect/endsession',
    check_session_iframe:
        process.env.REACT_APP_AUTH_URL + '/connect/checksession',
    revocation_endpoint: process.env.REACT_APP_AUTH_URL + '/connect/revocation',
    introspection_endpoint:
        process.env.REACT_APP_AUTH_URL + '/connect/introspect',
};
