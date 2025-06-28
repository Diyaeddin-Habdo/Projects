import { useEffect, useState } from 'react';
import { baseURL, USERS } from '../../../API/Api.js';
import axios from 'axios';
import Cookie from 'cookie-universal';
import { Link } from "react-router-dom";
import { Axios } from "../../../API/axios.js";
import TableShow from "../../../Components/Dashboard/TableShow.jsx";
function Users() {
    const cookie = Cookie();

    const [users, SetUsers] = useState([]);
    const [userDelete, SetUserDelete] = useState(false);
    const [currentUser, SetCurrentUser] = useState("");

    const [page, setPage] = useState(1);
    const limit = 2;


    // Current user
    useEffect(() => {
        Axios.get(`/${USERS}/${cookie.get('ID')}`).then((res) => SetCurrentUser(res.data));
    }, []);


    // All users
    useEffect(() => {
        axios.get(`${baseURL}/${USERS}`, {
            headers: {
                Authorization: "Bearer " + cookie.get('e-commerce'),
            }
        })
            .then((data) => SetUsers(data.data))
            .catch((err) => console.log(err));
    }, [userDelete]);

    // Header parameter
    const header = [
        {
            key:"name",
            name: "Name",
        },
        {
            key:"email",
            name: "Email",
        },
        {
            key:"roles",
            name: "Role",
        }
    ];
    
    // handle delete
    async function handleDelete(id) {
        try {
            const res = await Axios.delete(`${USERS}/${id}`);
            SetUsers((prev) => prev.filter((item) => item.id !== id));

        }
        catch (err) {
            console.log(err);
        }
    }



  return (
      <div className="bg-white w-100 p-2">
          <div className="d-flex align-items-center justify-content-between">
              <h1>Users List</h1>
              <Link className="btn btn-primary" to = "/dashboard/user/add">
                Add User
              </Link>
          </div>

          {/*<TableShow header={header} data={users} delete={handleDelete} currentUser={currentUser} />*/}
          <TableShow currentUser={currentUser} limit={limit} page={page} header={header} data={users} delete={handleDelete} setPage={setPage} isProducts={false} />
      </div>
  );
}
export default Users;