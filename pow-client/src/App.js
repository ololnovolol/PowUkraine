import React, { useEffect } from 'react';
import './style/app/App.css';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import SigninOidc from './pages/auth/signin-oidc'
import SignoutOidc from './pages/auth/signout-oidc'
import Mark from './pages/marks/mark';
import Login from './pages/auth/login';
import Logout from './pages/logout/logout';
import About from './pages/about/about';
import allMarks from './pages/allMarks/allMarks';
import ManageAccount from './pages/manageAccount/manageMyAccount'; 
import Message from './pages/message/messages';
import manageAccounts from './pages/manageAllAccounts/manageAccounts';
import manageMessages from './pages/manageAllMessages/manageMessages';
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

                <PrivateRoute exact path="/manageMessages" component={manageMessages} />
                <PrivateRoute exact path="/manageAccounts" component={manageAccounts} />
                <PrivateRoute path="/logout" component={Logout} />

                <PrivateRoute exact path="/manageMarks" component={allMarks} />
                <PrivateRoute exact path="/manageAccount" component={ManageAccount} />
               
            </Switch>
          </Router>
        </AuthProvider>   
      </Provider>
  );
}

export default App;
