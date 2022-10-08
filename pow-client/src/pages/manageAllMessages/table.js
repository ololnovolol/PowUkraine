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

export default function MessageTable({ theadData, tbodyData, setAllUsers }) {
    async function UpdateMsg(value) {
        await apiService.changeUserRole(value.email);
        updateData();
    }

    async function deleteMsg(value) {
        await apiService.deleteUser(value.email);
        updateData();
    }

    async function updateData() {
        const messages = await apiService.getAllMessages();
        setAllUsers(messages);
    }

    return (
        <Table striped bordered hover>
            <thead>
                <tr>
                    <th>Event Date</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Created Date</th>
                    <th>Has Mark?</th>
                    <th>User Id</th>
                    <th>Update Mark </th>
                    <th>Delete Mark </th>
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
                                    onClick={() => UpdateMsg(row)}>
                                    Update
                                </Btn>
                            </td>
                            <td>
                                <Btn
                                    type="button"
                                    onClick={() => deleteMsg(row)}>
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
