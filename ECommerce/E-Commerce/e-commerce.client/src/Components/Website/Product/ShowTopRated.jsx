import { Axios } from "../../../API/axios.js";
import { useEffect, useState } from 'react';
import { Top_N_Rated } from "../../../API/Api.js";
import TopRated from "./TopRated.jsx";
import MySkeleton from "../../Website/Skeleton/Skeleton.jsx";
export default function ShowTopRated() {

    const [products, setProducts] = useState([]);
    const [loading, SetLoading] = useState(true);

    useEffect(() => {
        Axios.get(`${Top_N_Rated}/${5}`).then(res => setProducts(res.data)).finally(() => SetLoading(false));
    }, []);

    const productsShow = products.map((item, index) => (
        <TopRated key={index}
            title={item.title}
            description={item.description}
            image={item.images[0]}
            sale
            price={item.price}
            discount={item.discount}
            rating={item.rating}
            id={item.id}
        />
    ));

    return (
        <div className="col-md-6 col-12" style={{ border: "2px solid #0d6efd" }}>
            <h1 className="text-center m-0 p-3 bg-primary text-white">Top Rated</h1>
            <div className="p-5">
                {loading ? (
                    <>
                        <MySkeleton
                            length="1"
                            height="800px"
                            classes="col-12" />
                    </>
                ) : productsShow
                }
            </div>
        </div>
  );
}