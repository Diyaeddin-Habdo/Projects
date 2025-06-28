import { useEffect, useState } from 'react';
import { baseURL, LOGS,tLOGS, isTokenExpired, logout } from '../../../API/Api.js';
import axios from 'axios';
import Cookie from 'cookie-universal';
import { Link } from "react-router-dom";
import { Axios } from "../../../API/axios.js";
import TableShow from "../../../Components/Dashboard/History/TableShow.js";
import LoadingSubmit from "../../../Components/Loading/Loading.js";

function History() {
    const cookie = Cookie();

    const [logs, SetLogs] = useState([]);

    const [page, setPage] = useState(1);
    const limit = 6;

    const [Loading, SetLoading] = useState(false);

    const role = String(cookie.get('role'));
    const url = (role === "3953" || role === "9763") ? LOGS : tLOGS

    console.log(logs)
    // All logs
    useEffect(() => {
        axios.get(`${baseURL}/${url}/${cookie.get('id')}`, {
            headers: {
                Authorization: "Bearer " + cookie.get('token'),
            }
        })
            .then((data) => SetLogs(data.data))
            .catch((err) => console.log(err));
    }, []);

    // Header parameter 
    const header = [
        {
            key:"time",
            name: "Time",
        },
        {
            key:"status",
            name: "Status",
        },
        {
            key:"name",
            name: "Name",
        },
        {
            key:"email",
            name: "Email",
        },
        {
            key: (role === "3953" || role === "9763") ? "sNo" : "phone",
            name: (role === "3953" || role === "9763") ? "Student No" : "Phone"
        },
        {
            key : "imagePath",
            name:"Image",
        }
    ];


    
  return (
      <div className="bg-white w-100 p-2">
          <div className="d-flex align-items-center justify-content-between">
              <h1>Geçmiş</h1>
          </div>
          {Loading && <LoadingSubmit />}
          {/*<TableShow header={header} data={users} delete={handleDelete} currentUser={currentUser} />*/}
          <TableShow  limit={limit} page={page} header={header} data={logs}  setPage={setPage}/>
      </div>
  );
}
export default History;