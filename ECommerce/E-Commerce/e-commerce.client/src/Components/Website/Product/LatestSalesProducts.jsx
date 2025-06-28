import { Axios } from "../../../API/axios.js";
import { LatestSales } from "../../../API/Api.js";
import { useEffect, useState } from 'react';
import Product from "./Product.jsx";
import Container from "react-bootstrap/Container";
import MySkeleton from "../../Website/Skeleton/Skeleton.jsx";
export default function LatestSalesProducts() {

    const [products, setProducts] = useState([]);
    const [loading, SetLoading] = useState(true);


    useEffect(() => {
        Axios.get(`${LatestSales}/${10}`).then(res => setProducts(res.data)).finally(() => SetLoading(false));
    }, []);

    const productsShow = products.map((item, index) => (
        <Product key={index}
            title={item.title}
            description={item.description}
            discount={item.discount}
            price={item.price}
            image={item.images[0]}
            rating={item.rating}
            col="3"
            id={item.id}
        />
    ));
    return (
        <Container>
            <h1>Latest Sale Products</h1>
            <div className="d-flex align-items-strech justify-content-center flex-wrap mt-5 row-gap-2 mb-5">
                {loading ? (
                    <>
                        <MySkeleton length="4" height="300px" classes="col-lg-3 col-md-6 col-12"/>
                    </>
                ) : productsShow
                }
            </div>
        </Container>
  );
}