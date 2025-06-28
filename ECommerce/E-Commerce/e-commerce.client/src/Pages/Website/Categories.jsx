import { useEffect, useState } from 'react';
import { Axios } from "../../API/axios.js";
import { baseURL, CATEGORIES } from '../../API/Api.js';
import Container from "react-bootstrap/Container";
//import Skeleton from "react-loading-skeleton";
import MySkeleton from "../../Components/Website/Skeleton/Skeleton.jsx";
export default function WebsiteCategories() {

    const [loading, SetLoading] = useState(true);
    const [categories, SetCategories] = useState([]);
    useEffect(() => {
        Axios.get(`${CATEGORIES}`)
            .then((data) => SetCategories(data.data.slice(-8)))
            .catch((err) => console.log(err)).finally(() => SetLoading(false));
    }, []);

    const categoriesShow = categories.map((category, index) => (
        <div className="col-lg-2 col-md-6 col-12 bg-transparent border-0">
            <div className="m-1 bg-white border d-flex align-items-center justify-content-start gap-3 rounded py-2 h-100">
                <img className="ms-3" width="50px" src={category.image} alt="image" />
                <p className="m-0">
                    {category.name.length > 12 ? category.name.slice(1, 12) + "..." : category.name}
                </p>        
            </div>
        </div>
    ));


    return (
        <>
            <div className="bg-secondary py-5">
                <Container>
                    <div className="d-flex align-items-stretch justify-content-center flex-wrap raw-gap-2">
                        {loading ? (
                            <MySkeleton length="8" height="70px" classes="col-lg-2 col-md-6 col-12"/>
                        ) : (
                            categoriesShow
                        )}
                    </div>
                </Container>
            </div>
        </>
    );

}