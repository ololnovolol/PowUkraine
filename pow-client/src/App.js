import React, { useEffect } from 'react';
import './style/app/App.css';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import SigninOidc from './pages/auth/signin-oidc'
import SignoutOidc from './pages/auth/signout-oidc'
import Lobby from './pages/userLobby/lobby';
import Mark from './pages/marks/mark';
import Login from './pages/auth/login';
import About from './pages/about/about';
import Message from './pages/message/messages';
import Admin from './pages/adminPanel/admin';
import { Provider } from 'react-redux';
import store from './store/store';
import userManager, { loadUserFromStorage } from './common/services/userService'
import AuthProvider from './pages/auth/authProvider'
import PrivateRoute from './routes/protectedRoute'
import Navbar from './pages/navbar/navbar';


function App() {

  useEffect(() => {
    // fetch current user from cookies
    loadUserFromStorage(store)
  }, [])

  return (
      <Provider store={store}>            
        <AuthProvider userManager={userManager} store={store}>
          <Router>          
            <Navbar />          
            <Switch>      
                <Route path="/" exact component={Mark} />
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
  );
}

export default App;
