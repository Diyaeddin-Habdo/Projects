import { Table,Form } from 'react-bootstrap';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckToSlot } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import { useState } from 'react';
import PaginatedItems from '../Pagination/Pagination';


export default function TableShow(props) {

    // Hold searched string
    const [search, SetSearch] = useState("");

    // Hold range of data
    const start = (props.page - 1) * props.limit;
    const end = start + props.limit;
    const final = props.data.slice(start, end);

    // filtering data
    const filterdData = final.filter((item) => item.name.toLowerCase().includes(search.toLowerCase()));

    // handle Search
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
                    {item2.key === "imagePath" ? (
                        <img width="50px" src={item[item2.key]} alt="resim" />
                    ) : (
                        item2.key === "time" ? (
                            new Date(item[item2.key]).toLocaleString(undefined, {
                                weekday: 'long', // Gün ismi
                                year: 'numeric',
                                month: 'long',
                                day: 'numeric',
                                hour: '2-digit',
                                minute: '2-digit',
                                second: '2-digit',
                                hour12: false // 12 saat formatı kullanmak istemiyorsanız 'false' olarak ayarlayın
                            })
                        ) : (
                            item[item2.key]
                        )
                    )}
                </td>
            ))}
           
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
                    </tr>
                  </thead>
                  <tbody>
                      {props.data.length === 0 && (
                          <tr className="text-center">
                              <td colSpan={12} > 
                                No Logs
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



