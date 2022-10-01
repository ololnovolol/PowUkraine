import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import * as apiService from '../../common/services/apiService';
import UsersTable from './table';
import data from './test.json';

function ManageAccounts() {
    useSelector(state => state.auth.user);
    const [usersData, setUsersData] = useState([]);

    useEffect(() => {
        getAllUsers();
    }, []);

    async function getAllUsers() {
        const users = await apiService.getUsers();
        setUsersData(users);
    }

    const getHeadings = () => {
        return Object.keys(data[0]);
    };

    return (
        <>
            <div className="container">
                <h1>Manage Accounts</h1>
                <UsersTable theadData={getHeadings()} tbodyData={usersData} />
            </div>
        </>
    );
}

export default ManageAccounts;
