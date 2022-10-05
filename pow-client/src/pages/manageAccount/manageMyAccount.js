import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import * as userService from '../../common/services/userService';
import styled from 'styled-components';
import * as apiService from '../../common/services/apiService';
import axios from 'axios';

const CenterBlock = styled.div`
    background: #3a5431 none repeat scroll 0% 0%;
    width: 50%;
    background: #fff;
    height: 10px;
    position: absolute;
    align-items: center;
    left: 25%;
    top: 25%;
    min-width: 0px;
    min-height: 0px;
    justify-content: center;
    padding: 25px 25px;
    z-index: 0;
    color: black;
`;

const BgButton = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    border-color: #fff;
    weight: 100%;
    position: relative;
    margin: right 10px;
    color: #f0a30a;
    border-radius: 5px;
    justify-content: space-between;
    font-size: 16px;
    &:hover {
        color: #fff;
        background: #6d8764;
        cursor: pointer;
    }
`;

const Block = styled.div`
    font-size: 1rem;
    position: relative;
    top: 3rem;
    border: 1 rem solid;
`;

const Form = styled.form`
  border-radius: 5px;
  }
`;

function ManageAccount() {
    useSelector(state => state.auth.user);
    const [doughnutData, setDoughnutData] = useState({
        FirstName: '',
        LastName: '',
        Email: '',
        PhoneNumber: '',
        BirthDay: '',
        UserId: '',
    });
    const [userId, setUserId] = useState('');
    GetUser();

    async function GetUser() {
        if (userId === '') {
            const res = await userService.GetUserId();
            console.log(res);
            setUserId(res);
            const response = await apiService.getUser(res);
            console.log(response);

            setDoughnutData(response);
        }
    }

    async function postMsg(e) {
        let user = {
            FirstName: doughnutData.FirstName,
            LastName: doughnutData.FirstName,
            Email: doughnutData.Email,
            PhoneNumber: doughnutData.PhoneNumber,
            BirthDay: doughnutData.BirthDay,
            UserId: doughnutData.UserId,
        };

        axios
            .post('https://localhost:44316/api/roles/updateUser', user, {
                headers: {
                    accept: 'application/json',
                },
            })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    function handle(e) {
        const newData = { ...doughnutData };
        newData[e.target.id] = e.target.value;
        setDoughnutData(newData);
        console.log(newData);
    }

    return (
        <div>
            <CenterBlock>
                <Form onSubmit={postMsg}>
                    <h1>Manage Account</h1>
                    <Block>
                        <label>
                            FirstName
                            <input
                                id="FirstName"
                                name="FirstName"
                                onChange={e => handle(e)}
                                value={doughnutData.FirstName}
                                required
                            />{' '}
                        </label>
                        <label>
                            LastName
                            <input
                                id="LastName"
                                name="LastName"
                                onChange={e => handle(e)}
                                value={doughnutData.LastName}
                                required
                            />
                        </label>
                        <label>
                            Email
                            <input
                                id="Email"
                                name="Email"
                                onChange={e => handle(e)}
                                value={doughnutData.Email}
                                required
                            />
                        </label>
                        <label>
                            PhoneNumber
                            <input
                                id="PhoneNumber"
                                name="PhoneNumber"
                                onChange={e => handle(e)}
                                value={doughnutData.PhoneNumber}
                                required
                            />
                        </label>
                        <label>
                            BirthDay
                            <input
                                id="BirthDay"
                                name="BirthDay"
                                onChange={e => handle(e)}
                                value={doughnutData.BirthDay}
                                required
                            />
                        </label>
                        <Block>
                            <label>Submit</label>
                            <BgButton type="submit">Submit</BgButton>
                        </Block>
                    </Block>
                </Form>
            </CenterBlock>
        </div>
    );
}

export default ManageAccount;
