import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import * as apiService from '../../common/services/apiService';
import MessageTable from './table';
import data from './test.json';
import * as userManager from '../../common/services/userService';

function ManageMessages() {
    useSelector(state => state.auth.user);
    const [messageData, setMessageData] = useState([]);
    const [currentUser, setCurrentUser] = useState('');

    useEffect(() => {
        getAllMessages();
        getCurrentUser();
    }, []);

    async function getAllMessages() {
        const msg = await apiService.getAllMessages();
        setMessageData(msg);
    }

    function setAllMessages(msg) {
        setMessageData(msg);
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
                <h1>Manage Messages</h1>
                <MessageTable
                    theadData={getHeadings()}
                    tbodyData={messageData}
                    setAllUsers={setAllMessages}
                    userId={currentUser}
                />
            </div>
        </>
    );
}

export default ManageMessages;
