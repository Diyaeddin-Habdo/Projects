import { useEffect } from 'react';
import { baseURL, LOGOUT, USERS } from '../../API/Api.js';
import axios from 'axios';
import Cookie from 'cookie-universal';

function Logout() {

    const cookie = Cookie();
    async function handleLogout() {
        //try {
        //    const res = await axios.get(`${baseURL}/${LOGOUT}`, {
        //        headers: {
        //            Authorization: "Bearer " + cookie.get('e-commerce'),
        //            Email: cookie.get('email'),
        //        }
        //    });
        //    console.log(res);
        //}
        //catch (err) {
        //    console.log(err);
        //}
        cookie.remove("e-commerce");
    }



    return (
        <button onClick={handleLogout}>Log out</button>
  );
}

export default Logout;