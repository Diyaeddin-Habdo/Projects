import { Table,Form } from 'react-bootstrap';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPenToSquare } from "@fortawesome/free-solid-svg-icons";
import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import PaginatedItems from "../../Components/Dashboard/Pagination/Pagination.js";
import { useState } from 'react';

export default function TableShow(props) {

    // current user for users table only
    const currentUser = props.currentUser || {
        name:"",
    };

    // Hold serached string
    const [search, SetSearch] = useState("");

    // Hold range of data
    const start = (props.page - 1) * props.limit;
    const end = start + props.limit;
    const final = props.data.slice(start, end);

    // filtering data
    const filterdData = final.filter((item) => item.name.toLowerCase().includes(search.toLowerCase()));

    // handleSearch
    function handleSearch(e) {
        SetSearch(e.target.value);
    }

    // Header show 
    const headerShow = props.header.map((item, index) => <th key={index}>{item.name}</th>);

    // Body show
    const dataShow = filterdData.map((item, key) => (
        <tr key={key}>
            <td>{key + 1}</td>

            {props.header.map((item2, key2) => (
                <td key={key2}>
                    {item2.key === "imagePath" ? (<img width="50px" src={item[item2.key]} alt="resim" />)
                        :  item[item2.key]}
                  
                    {currentUser && item[item2.key] === currentUser.name && " (You)"}
                </td>
            ))}

            <td>
                <div className="d-flex align-items-center gap-2">
                    <Link to={`${item.id}`}> <FontAwesomeIcon fontSize={"19px"} icon={faPenToSquare} /> </Link>

                    {
                        currentUser.name !== item.name && 
                        <FontAwesomeIcon onClick={() => props.delete(item.id)} cursor={"pointer"} color={"red"} fontSize={"19px"} icon={faTrash} />
                    }

                </div>
            </td>
        </tr>
    ));

    

  // Return data
    return (

        <>

            <div className="col-3">
                <Form.Control
                    className="my-2"
                    type="search"
                    aria-label="input example"
                    placeholder="search"
                    onChange={handleSearch}
                />
            </div>

              <Table striped bordered hover>
                  <thead>

              
                      <tr>
                          <th>Id</th>
                          {headerShow}
                          <th>Action</th>
                          </tr>
              

                  </thead>
                  <tbody>
                      {props.data.length === 0 && (
                          <tr className="text-center">
                              <td colSpan={12} > 
                                Loading...
                              </td>
                          </tr>
                      )}
                      {dataShow}
                  </tbody>
              </Table>


            <PaginatedItems setPage={props.setPage} itemsPerPage={props.limit} data={props.data} />
        </>
  );
}