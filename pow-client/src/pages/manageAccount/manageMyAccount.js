import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import * as userService from '../../common/services/userService';
import styled from 'styled-components';
import * as apiService from '../../common/services/apiService';
import '../../style/forms/form.css';

const CenterBlock = styled.div`
    width: 50%;
    height: auto;
    background: #fff;
    position: absolute;
    align-items: center;
    margin-top: 5rem;
    margin-bottom: 5rem;
    left: 25%;
    padding: 25px 25px;
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
`;

const BgButton = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    weight: 100px;
    border-color: #fff;
    color: #f0a30a;
    border-radius: 5px;
    font-size: 16px;
    margin-top: 1rem;
    margin-left: 40%;
    &:hover {
        color: #fff;
        background: #6d8764;
        cursor: pointer;
    }
`;

const Block = styled.div`
    font-size: 1rem;
    position: relative;
    top: 2rem;
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
            LastName: doughnutData.LastName,
            Email: doughnutData.Email,
            PhoneNumber: doughnutData.PhoneNumber,
            BirthDay: doughnutData.BirthDay,
            UserId: doughnutData.UserId,
        };

        apiService.updateUser(user);
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
                    <div className="hh">
                        <h1>Manage Account</h1>
                    </div>
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
                                maxLength={14}
                                onChange={e => handle(e)}
                                value={doughnutData.PhoneNumber}
                                type="tel"
                                placeholder="+380-99-77-77-777"
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
                                type={'datetime-local'}
                                required
                            />
                        </label>

                        <BgButton type="submit">OK</BgButton>
                    </Block>
                </Form>
            </CenterBlock>
        </div>
    );
}

export default ManageAccount;
