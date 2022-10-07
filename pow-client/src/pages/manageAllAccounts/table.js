import React from 'react';
import Table from 'react-bootstrap/Table';
import styled from 'styled-components';
import * as apiService from '../../common/services/apiService';

const Btn = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    weight: 100px;
    justify-content: flex;
    align-items: center;
    position: relative;
    max-width: 100%;
    align-items: center;
    color: #fff;
    min-width: 0px;
    min-height: 0px;
    border-radius: 5px;

    justify-content: space-between;
    align-items: center;
    list-style: none;
    text-decoration: none;
    font-size: 18px;
    &:hover {
        color: #f0a30a;
        background: #6d8764;
        cursor: pointer;
    }
`;

export default function UsersTable({ theadData, tbodyData, state }) {
    async function changeRole(value) {
        await apiService.changeUserRole(value.email);
    }

    async function updateUser(value) {
        await apiService.updateUser(value);
    }

    async function deleteUser(value) {
        await apiService.deleteUser(value.email);
    }

    return (
        <Table striped bordered hover>
            <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                    <th>Phone</th>
                    <th>BirthDay</th>
                    <th>Role</th>
                    <th>Change Role </th>
                    <th>Delete User </th>
                </tr>
            </thead>
            <tbody>
                {tbodyData.map((row, index) => {
                    return (
                        <tr key={index}>
                            {theadData.map((key, index) => {
                                return <td key={row[key]}>{row[key]}</td>;
                            })}
                            <td>
                                <Btn
                                    type="button"
                                    onClick={() => changeRole(row)}>
                                    Change role
                                </Btn>
                            </td>
                            <td>
                                <Btn
                                    type="button"
                                    onClick={() => deleteUser(row)}>
                                    Delete
                                </Btn>
                            </td>
                        </tr>
                    );
                })}
            </tbody>
        </Table>
    );
}
