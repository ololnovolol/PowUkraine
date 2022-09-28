import React, { useState } from 'react'
import { useSelector } from 'react-redux'
import * as apiService from '../../common/services/apiService'
import { prettifyJson } from '../../common/utils/jsonUtils'

function ManageAccounts() {
  useSelector(state => state.auth.user)
  const [doughnutData, setDoughnutData] = useState(null)
  const [usersData, setUsersData] = useState(null)

  async function getAllAdmin() {
    const doughnuts = await apiService.getFromApi_Admin()
    setDoughnutData(doughnuts)
  }

  async function getAllUsers() {
    const users = await apiService.getUsers()
    setUsersData(users)
  }

  return (
    <>
    <div className='manageAccounts'>
      <div>
        <h1>Manage Accounts</h1>

        <button className="button button-outline" onClick={() => getAllAdmin()}>GetAll_Admin_api</button>
        <button className="button button-outline" onClick={() => getAllUsers()}>GetAll_User_Identity</button>

        <div>
          <code>
            {prettifyJson(doughnutData ? doughnutData : 'No api connect yet :(')}
          </code>
        </div>

        <div>
          <code>
            {prettifyJson(usersData ? usersData : 'No api users yet :(')}
          </code>
        </div>

      </div>
    </div>
  </>
  )
}

export default ManageAccounts
