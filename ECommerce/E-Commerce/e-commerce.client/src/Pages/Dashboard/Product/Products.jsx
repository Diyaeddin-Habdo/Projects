import { useEffect, useState } from 'react';
import { baseURL, PRODUCTS } from '../../../API/Api.js';
import axios from 'axios';
import Cookie from 'cookie-universal';
import { Link } from "react-router-dom";
import TableShow from "../../../Components/Dashboard/TableShow.jsx";
import { Axios } from "../../../API/axios.js";


function Products() {
    const cookie = Cookie();

    const [products, SetProducts] = useState([]);
    const [page, setPage] = useState(1);
    const limit = 5;


    useEffect(() => {
        axios.get(`${baseURL}/${PRODUCTS}`, {
            headers: {
                Authorization: "Bearer " + cookie.get('e-commerce'),
            }
        })
            .then((data) => SetProducts(data.data))
            .catch((err) => console.log(err));
    }, []);

    // handle delete
    async function handleDelete(id) {
        try {
            const res = await Axios.delete(`${PRODUCTS}/${id}`);
            SetProducts((prev) => prev.filter((item) => item.id !== id));
        }
        catch (err) {
            console.log(err);
        }
    }

    const header = [
        {
            key: "images",
            name: "Images",
        },
        {
            key: "title",
            name: "Title",
        },
        {
            key: "description",
            name: "Description",
        },
        {
            key: "price",
            name : "Price",
        },
        {
            key: "rating",
            name : "Rating",
        }
    ];


    return (
        <div className="bg-white w-100 p-2">
            <div className="d-flex align-items-center justify-content-between">
                <h1>Products List</h1>
                <Link className="btn btn-primary" to="/dashboard/product/add">
                    Add Product
                </Link>
            </div>

            {/*<TableShow header={header} data={products} delete={handleDelete} />*/}
            <TableShow limit={limit} page={page} header={header} data={products} delete={handleDelete} setPage={setPage} isProducts={true} />
        </div>
    );
}
export default Products;