import { useEffect, useState } from 'react';
import { baseURL, isTokenExpired, logout, sADMINAPPROVAL, sTEACHERAPPROVAL } from '../../../API/Api.js';
import axios from 'axios';
import Cookie from 'cookie-universal';
import TableShow from "../../../Components/Dashboard/Beklemede/TableShow.js";
import LoadingSubmit from "../../../Components/Loading/Loading.js";

function ApprovedStudents() {

    // Get cookies
    const cookie = Cookie();

    // Get students data
    const [students, SetStudents] = useState([]);

    // Handling pagination
    const [page, setPage] = useState(1);
    const limit = 7;

    // handle loading
    const [Loading, SetLoading] = useState(false);

    // Get current user role and route him based on it
    const role = cookie.get("role");
    const url = String(role) === "3953" ? sADMINAPPROVAL : sTEACHERAPPROVAL;
    const departmentId = cookie.get("departmentId");
    // Get All approved students
    useEffect(() => {

        // Check if token expired first
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

    // Header parameters of TableShow component
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
export default ApprovedStudents;