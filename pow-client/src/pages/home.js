import React, { useState } from 'react'
import { signoutRedirect } from '../services/userService'
import { useSelector } from 'react-redux'
import * as apiService from '../services/apiService'
import { prettifyJson } from '../utils/jsonUtils'

function Home() {
  const user = useSelector(state => state.auth.user)
  const [doughnutData, setDoughnutData] = useState(null)
  function signOut() {
    signoutRedirect()
  }

  async function getDoughnuts() {
    const doughnuts = await apiService.getDoughnutsFromApi()
    setDoughnutData(doughnuts)
  }

  return (
    <div>
      <h1>Home</h1>
      <p>Hello, {user.profile.given_name}.</p>

      <p>💡 <strong>Tip: </strong><em> ............. </em></p>

      <button className="button button-outline" onClick={() => getDoughnuts()}>GetAll_api</button>
      <button className="button button-clear" onClick={() => signOut()}>Sign Out</button>

      <pre>
        <code>
          {prettifyJson(doughnutData ? doughnutData : 'No doughnuts yet :(')}
        </code>
      </pre>

    </div>
  )
}

export default Home
