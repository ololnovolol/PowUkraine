import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import * as apiService from '../../common/services/apiService';
import UsersTable from './table';
import data from './test.json';
import * as userManager from '../../common/services/userService';

function ManageAccounts() {
    useSelector(state => state.auth.user);
    const [usersData, setUsersData] = useState([]);
    const [currentUser, setCurrentUser] = useState('');

    useEffect(() => {
        getAllUsers();
        getCurrentUser();
    }, []);

    async function getAllUsers() {
        const users = await apiService.getUsers();
        setUsersData(users);
    }

    function setAllUsers(users) {
        setUsersData(users);
    }

    async function getCurrentUser() {
        let user = await userManager.GetUserId();
        setCurrentUser(user);
    }

    const getHeadings = () => {
        return Object.keys(data[0]);
    };

    return (
        <>
            <div className="container">
                <h1>Manage Accounts</h1>
                <UsersTable
                    theadData={getHeadings()}
                    tbodyData={usersData}
                    setAllUsers={setAllUsers}
                    userId={currentUser}
                />
            </div>
        </>
    );
}

export default ManageAccounts;
