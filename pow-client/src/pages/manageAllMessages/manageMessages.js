import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import * as apiService from '../../common/services/apiService';
import { getFromApi_User } from '../../common/services/apiService';
import { prettifyJson } from '../../common/utils/jsonUtils';

function ManageMessages() {
    useSelector(state => state.auth.user);
    const [doughnutData, setDoughnutData] = useState(null);

    async function getAllAdmin() {
        const doughnuts = await apiService.getFromApi_Admin();
        setDoughnutData(doughnuts);
    }

    async function getAllUser() {
        const doughnuts = await getFromApi_User();
        setDoughnutData(doughnuts);
    }

    return (
        <>
            <div className="manageMessages">
                <div>
                    <h1>Manage Messages</h1>

                    <button
                        className="button button-outline"
                        onClick={() => getAllAdmin()}>
                        GetAll_Admin_api
                    </button>
                    <button
                        className="button button-outline"
                        onClick={() => getAllUser()}>
                        GetAll_User_api
                    </button>

                    <pre>
                        <code>
                            {prettifyJson(
                                doughnutData
                                    ? doughnutData
                                    : 'No api connect yet :(',
                            )}
                        </code>
                    </pre>
                </div>
            </div>
        </>
    );
}

export default ManageMessages;
