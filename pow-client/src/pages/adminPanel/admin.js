import React, { useState } from 'react'
import { signoutRedirect } from '../../common/services/userService'
import { useSelector } from 'react-redux'
import * as apiService from '../../common/services/apiService'
import { getFromApi_User } from '../../common/services/apiService'
import { prettifyJson } from '../../common/utils/jsonUtils'

function Admin() {
  const user = useSelector(state => state.auth.user)
  const [doughnutData, setDoughnutData] = useState(null)

  function signOut() {
    signoutRedirect()
  }

  async function getAllAdmin() {
    const doughnuts = await apiService.getFromApi_Admin()
    setDoughnutData(doughnuts)
  }

  async function getAllUser() {
    const doughnuts = await getFromApi_User()
    setDoughnutData(doughnuts)
  }

  return (
    <>
    <div className='lobby'>
      <div>
        <h1>Admin</h1>
        <p>Hello, {user.profile.given_name}.</p>

        <button className="button button-outline" onClick={() => getAllAdmin()}>GetAll_Admin_api</button>
        <button className="button button-outline" onClick={() => getAllUser()}>GetAll_User_api</button>
        <button className="button button-danger" onClick={() => signOut()}>Sign Out</button>

        <pre>
          <code>
            {prettifyJson(doughnutData ? doughnutData : 'No api connect yet :(')}
          </code>
        </pre>

      </div>
    </div>
  </>
  )
}

export default Admin
