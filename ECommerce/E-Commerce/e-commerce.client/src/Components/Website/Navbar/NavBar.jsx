import { Form } from "react-bootstrap";
import Container from "react-bootstrap/Container";
import { Link } from "react-router-dom";
import Logo from "../../../assets/images/Logo.png";
import Cart from "../../../assets/images/cart.png";
import Profile from "../../../assets/images/profile.png";
import { baseURL, CATEGORIES } from '../../../API/Api.js';
import { useEffect, useState } from 'react';
import { Axios } from "../../../API/axios";
import "./NavBar.css";
import MySkeleton from "../Skeleton/Skeleton.jsx";
export default function NavBar() {

    const [loading, SetLoading] = useState(true);
    const [categories, SetCategories] = useState([]);

    useEffect(() => {
        Axios.get(`${CATEGORIES}`)
            .then((data) => SetCategories(data.data.slice(-8)))
            .catch((err) => console.log(err)).finally(() => SetLoading(false));
    }, []);


    const categoriesShow = categories.map((category, index) => (
        <Link key={index} to={`/category/${category.id}`} className="m-0 category-title text-black" > {category.name.length > 15 ? category.name.slice(1, 15) + "..." : category.name}</Link>
    ));

    return (
        <nav className="py-3">
            <Container>
                <div className="d-flex align-items-center justify-content-between flex-wrap">
                    <Link className="col-3" to="/">
                        <img
                            width="200px"
                            src={Logo}
                            alt="Logo"
                        />
                    </Link>

                    <div className="col-12 col-md-6 order-md-2 order-3 mt-md-0 mt-3 position-relative">
                        <Form.Control 
                            type="search"
                            className="form-control custom-search py-3 rounded-0"
                            placeholder="search product"
                        />

                        <h3 className="btn btn-primary position-absolute top-0 end-0 h-100 line-height m-0 px-4 rounded-0 
                                        d-flex align-items-center justify-content-center"
                        >
                            Search
                        </h3>
                    </div>
                        <div className="col-3 d-flex align-items-center justify-content-end gap-4 order-md-3 order-1">
                            <Link to="/cart">
                                <img
                                    width="40px"
                                    src={Cart}
                                    alt="cart"
                                />
                            </Link>

                            <Link to="/profile">
                                <img
                                    width="35px"
                                    src={Profile}
                                    alt="cart"
                                />
                            </Link>

                        </div>
                </div>


                <div className="mt-3">
                    <div className="d-flex align-items-center justify-content-start gap-5">
                        {loading ? (
                            <>
                                <MySkeleton width="80px" height="30px" length="8" />
                            </>
                        ) : <>
                                {categoriesShow}
                                < Link className="text-black category-title" to="/categories">
                                    Show All
                                </Link>
                            </>
                           
                        }
                        
                        
                    </div>
                </div>

            </Container>
        </nav>
    
    );
}