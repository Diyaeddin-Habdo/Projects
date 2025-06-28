import { useEffect, useState } from 'react';
import { baseURL, isTokenExpired, logout, sADMINUNAPPROVAL, sTEACHERUNAPPROVAL } from '../../../API/Api.js';
import axios from 'axios';
import Cookie from 'cookie-universal';
import TableShow from "../../../Components/Dashboard/Beklemede/TableShow.js";
import LoadingSubmit from "../../../Components/Loading/Loading.js";

function UnapprovedStudents() {
    const cookie = Cookie();

    const [students, SetStudents] = useState([]);

    const [page, setPage] = useState(1);
    const limit = 7;

    const [Loading, SetLoading] = useState(false);

    const role = cookie.get("role");
    const url = String(role) === "3953" ? sADMINUNAPPROVAL : sTEACHERUNAPPROVAL;
    const departmentId = cookie.get("departmentId");
    // All students
    useEffect(() => {
        if(!isTokenExpired())
        {
            axios.get(`${baseURL}/${url}/${departmentId}`, {
                headers: {
                    Authorization: "Bearer " + cookie.get('token'),
                }
            })
                .then((data) => SetStudents(data.data))
                .catch((err) => console.log(err));
        }
        else
            logout();
    }, []);

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
            key:"phone",
            name: "Phone",
        },
        {
            key:"sNo",
            name: "Student No",
        },
        {
            key:"imagePath",
            name:"Image"
        }
    ];
    

  return (
      <div className="bg-white w-100 p-2">
          <div className="d-flex align-items-center justify-content-between">
              <h1>Students List</h1>
          </div>
          {Loading && <LoadingSubmit />}
          <TableShow limit={limit} page={page} header={header} data={students} setPage={setPage}/>
      </div>
  );
}
export default UnapprovedStudents;