import React from 'react';
import Table from 'react-bootstrap/Table';
import styled from 'styled-components';
import * as apiService from '../../common/services/apiService'

const Btn = styled.button`
  background: #88A87D none repeat scroll 0% 0%;
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
    color: #F0A30A;
    background: #6D8764;
    cursor: pointer;
  }
`;

async function changeRole(value) {
  await apiService.changeUserRole(value)
}

async function updateUser(value) {
  await apiService.updateUser(value)
}

async function deleteUser(value) {
  await apiService.deleteUser(value)
}

export default function UsersTable({theadData, tbodyData}) {
    return (
      <Table striped bordered hover>
          <thead>
             <tr>
              {theadData.map(heading => {
                return <th key={heading}>{heading}</th>
              })}
              <th> actions </th>
            </tr>
          </thead>
          <tbody>
              {tbodyData.map((row, index) => {
                  return <tr key={index}>
                      {theadData.map((key, index) => {
                           return <td key={row[key]}>{row[key]}
                           </td>
                      })}
                          <td >
                           <Btn type="button" onClick={() => changeRole(index)}>change role</Btn>
                         </td>
                         <td >
                           <Btn type="button" onClick={() => updateUser(index)}>update</Btn>
                         </td>
                         <td >
                           <Btn type="button" onClick={() => deleteUser(index)}>delete</Btn>
                         </td>
                </tr>;
              })}
          </tbody>
      </Table>
   );
   }