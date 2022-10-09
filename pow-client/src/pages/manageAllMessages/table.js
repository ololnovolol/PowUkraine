import React, { useState } from 'react';
import Table from 'react-bootstrap/Table';
import styled from 'styled-components';
import * as FcIcons from 'react-icons/fc';
import * as apiService from '../../common/services/apiService';
import Modal from './modal';
import FormModal from './formModal';

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

const BtnRed = styled.button`
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
        color: red;
        background: #6d8764;
        cursor: pointer;
    }
`;

export default function MessageTable({ theadData, tbodyData, setAllUsers }) {
    const [show, setShow] = useState(false);
    const [data, setData] = useState([]);

    async function UpdateMsg(value) {
        await apiService.changeUserRole(value.email);
        updateData();
    }

    async function deleteMsg(value) {
        console.log(value);
        await apiService.deleteUser(value.email);
        updateData();
    }

    async function updateData(bool, value) {
        setShow(bool);

        tbodyData.map(e => {
            if (
                e.userId === value.userId &&
                e.title === value.title &&
                e.description === value.description
            ) {
                console.log(e);
                setData(e);
            }
        });
    }

    return (
        <Table striped bordered hover>
            <thead>
                <tr>
                    <th>Event Date</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Has Mark?</th>
                    <th>Edit </th>
                    <th>Delete </th>
                </tr>
            </thead>
            <tbody>
                {tbodyData.map((row, index) => {
                    return (
                        <tr key={index}>
                            {theadData.map((key, index) => {
                                if (key === 'marked') {
                                    return (
                                        <td key={row[key]}>
                                            {row[key] === 0 ? (
                                                '-'
                                            ) : (
                                                <FcIcons.FcCheckmark />
                                            )}
                                        </td>
                                    );
                                } else if (key === 'userId') {
                                } else if (key === 'createdDate') {
                                } else {
                                    return <td key={row[key]}>{row[key]}</td>;
                                }
                            })}
                            <td>
                                <Btn
                                    type="button"
                                    onClick={() => updateData(true, row)}>
                                    Details
                                    <Modal
                                        title="Details"
                                        onClose={() => setShow(false)}
                                        show={show}>
                                        <FormModal array={data} />
                                    </Modal>
                                </Btn>
                            </td>
                            <td>
                                <BtnRed
                                    type="button"
                                    onClick={() => deleteMsg(row)}>
                                    Delete
                                </BtnRed>
                            </td>
                        </tr>
                    );
                })}
            </tbody>
        </Table>
    );
}
