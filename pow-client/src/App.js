import React, { useEffect } from 'react';
import './App.css';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import SigninOidc from './pages/auth/signin-oidc'
import SignoutOidc from './pages/auth/signout-oidc'
import Lobby from './pages/lobby';
import Mark from './pages/mark';
import Login from './pages/login';
import About from './pages/about';
import Message from './pages/messages';
import Admin from './pages/admin';
import { Provider } from 'react-redux';
import store from './store';
import userManager, { loadUserFromStorage } from './services/userService'
import AuthProvider from './providers/authProvider'
import PrivateRoute from './utils/protectedRoute'
import NavState from './context/navState';
import MainMenu from './components/mainMenu'
import Navbar from './components/navbar/navbar';

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
          <Navbar /> 
            <Switch>
           
                <Route path="/mark" exact component={Mark} />
                <Route path="/message" component={Message} />
                <Route path="/login" component={Login} />
                <Route path="/signout-oidc" component={SignoutOidc} />
                <Route path="/signin-oidc" component={SigninOidc} />
                <Route path="/about" component={About} />

                <PrivateRoute exact path="/userLobby" component={Lobby} />
                <PrivateRoute exact path="/admin" component={Admin} />
               
            </Switch>
          </Router>
        </AuthProvider>   
      </Provider>
    </NavState>
  );
}

export default App;
