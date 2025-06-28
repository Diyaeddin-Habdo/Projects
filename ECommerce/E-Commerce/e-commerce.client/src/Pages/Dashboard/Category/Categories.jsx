import { useEffect, useState } from 'react';
import { baseURL, CATEGORIES } from '../../../API/Api.js';
import axios from 'axios';
import Cookie from 'cookie-universal';
import { Link } from "react-router-dom";
import TableShow from "../../../Components/Dashboard/TableShow.jsx";
import { Axios } from "../../../API/axios.js";

function Categories() {
    const cookie = Cookie();

    const [categories, SetCategories] = useState([]);
    const [page, setPage] = useState(1);
    const limit = 3;



    useEffect(() => {
        axios.get(`${baseURL}/${CATEGORIES}`, {
            headers: {
                Authorization: "Bearer " + cookie.get('e-commerce'),
            }
        })
            .then((data) => SetCategories(data.data))
            .catch((err) => console.log(err));
    }, []);

    // handle delete
    async function handleDelete(id) {
        try {
            const res = await Axios.delete(`${CATEGORIES}/${id}`);
            SetCategories((prev) => prev.filter((item) => item.id !== id));
        }
        catch (err) {
            console.log(err);
        }
    }


    const header = [
        {
            key: "name",
            name: "name",
        },
        {
            key: "image",
            name: "image",
        }
    ];

    return (
        <div className="bg-white w-100 p-2">
            <div className="d-flex align-items-center justify-content-between">
                <h1>Categories List</h1>
                <Link className="btn btn-primary" to="/dashboard/category/add">
                    Add Category
                </Link>
            </div>

            <TableShow limit={limit} page={page} header={header} data={categories} delete={handleDelete} setPage={setPage} isProducts={false} />
        </div>
    );
}
export default Categories;