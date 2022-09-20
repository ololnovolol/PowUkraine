import React, { useEffect } from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import SigninOidc from './pages/auth/signin-oidc'
import SignoutOidc from './pages/auth/signout-oidc'
import Home from './pages/home'
import Login from './pages/login'
import { Provider } from 'react-redux';
import store from './store';
import userManager, { loadUserFromStorage } from './services/userService'
import AuthProvider from './providers/authProvider'
import PrivateRoute from './utils/protectedRoute'
import NavState from './context/navState';
import MainMenu from './components/mainMenu'

function App() {

  useEffect(() => {
    // fetch current user from cookies
    loadUserFromStorage(store)
  }, [])

  return (
    <NavState>  
      <Provider store={store}>            
        <AuthProvider userManager={userManager} store={store}>
          <Router>
            <Switch>
           
                <Route path="/login" component={Login} />
                <Route path="/signout-oidc" component={SignoutOidc} />
                <Route path="/signin-oidc" component={SigninOidc} />
                <PrivateRoute exact path="/" component={Home} />

                 <MainMenu />     
            </Switch>
          </Router>
        </AuthProvider>   
      </Provider>
    </NavState>
  );
}

export default App;
