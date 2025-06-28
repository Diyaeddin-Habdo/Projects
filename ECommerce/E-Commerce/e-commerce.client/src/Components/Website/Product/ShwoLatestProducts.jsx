import { Axios } from "../../../API/axios.js";
import { LatestSales, LatestProducts } from "../../../API/Api.js";
import { useEffect, useState } from 'react';
import Product from "./Product.jsx";
import Container from "react-bootstrap/Container";
import MySkeleton from "../../Website/Skeleton/Skeleton.jsx";
export default function ShwoLatestProducts() {

    const [products, setProducts] = useState([]);
    const [loading, SetLoading] = useState(true);


    useEffect(() => {
        Axios.get(`${LatestProducts}/${6}`).then(res => setProducts(res.data)).finally(() => SetLoading(false));
    }, []);

    const productsShow = products.map((item, index) => (
        <Product key={index}
            title={item.title}
            description={item.description}
            discount={item.discount}
            price={item.price}
            image={item.images[0]}
            rating={item.rating}
            col="6"
            id={item.id}
        />
    ));
    return (
        <div className="col-md-6 col-12">

            <div className="ms-md-3">
                <h1>Latest Products</h1>
                <div className="d-flex align-items-strech justify-content-center flex-wrap mt-5 row-gap-2 mb-5">
                    {loading ? (
                        <>
                            <MySkeleton
                                length="4"
                                height="300px"
                                classes="col-md-6 col-12" />
                        </>
                    ) : productsShow
                    }
                </div>
            </div>
        </div>
    );
}
