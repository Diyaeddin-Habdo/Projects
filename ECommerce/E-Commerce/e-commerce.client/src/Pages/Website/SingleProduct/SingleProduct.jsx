import ImageGallery from "react-image-gallery";
import { Container } from "react-bootstrap";
import { Axios } from "../../../../src/API/axios.js";
import { PRODUCTS } from '../../../../src/API/Api.js';
import { useState, useEffect, useRef } from "react";
import { useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar as solid } from "@fortawesome/free-solid-svg-icons";
import { faStar as regularStar } from "@fortawesome/free-regular-svg-icons";
import cart from "../../../assets/images/cart.png";
import MySkeleton from "../../../Components/Website/Skeleton/Skeleton.jsx";
export default function SingleProduct() {

    const [Loading, setLoading] = useState(true);
    const [product, setProduct] = useState({});
    const [images, setImages] = useState([]);

    const roundStars = Math.round(product.rating);
    const stars = Math.min(roundStars, 5);
    const showGoldStars = Array.from({ length: stars }).map((_, index) => (
        <FontAwesomeIcon key={index} icon={solid} color="gold" />
    ));
    const showEmptyStars = Array.from({ length: 5 - stars }).map((_, index) => (
        <FontAwesomeIcon key={index} icon={regularStar} />
    ));


    // Get ID
    const { id } = useParams();

    // Get product by Id
    useEffect(() => {
        setLoading(true);

        Axios.get(`${PRODUCTS}/${id}`)
            .then((data) => {setImages(data.data.images.map((img) => {
                return { original: img, thumbnail: img }
            }));
                setProduct(data.data);
            }).finally(() => setLoading(false));
    }, [])
    

    return (
        <Container className="mt-5">
            <div className="d-flex align-items-start flex-wrap row-gap-5">

                {Loading ? (
                    <>
                        <div className="col-lg-4 col-md-6 col-12">
                            <MySkeleton height="250px" length="1" classes="col-12" />
                            <div className="col-12 d-flex mt-1">
                                <MySkeleton height="100px" length="1" classes="col-4" />
                                <MySkeleton height="100px" length="1" classes="col-4" />
                                <MySkeleton height="100px" length="1" classes="col-4" />
                            </div>
                        </div> 

                        <div className="col-lg-8 col-md-6 col-12">
                            <MySkeleton height="20px" length="1" classes="col-8" />
                            <MySkeleton height="210px" length="1" classes="col-lg-8 col-12 mt-2" />
                            <hr className="col-lg-8 col-12" />
                            <div className="d-flex align-items-center justify-content-between col-lg-8 col-12">
                                <MySkeleton height="20px" length="1" classes="col-4 mt-2" />
                                <MySkeleton height="20px" length="1" classes="col-4 mt-2" />
                            </div> 
                        </div>
                    </>
                ) :
                <>
                <div className="col-lg-4 col-md-6 col-12">
                    <ImageGallery items={images} />
                </div>

                <div className="col-lg-8 col-md-6 col-12">
                    <div className="ms-5">
                        <div className="border-bottom">
                            <h1>{product.title}</h1>
                            <p style={{ color: "gray" }}>{product.description}</p>
                            <h3 className="fw-normal">{product.about}</h3>
                        </div>                 
                        {/*from product*/}
                        <div className="d-flex align-items-center justify-content-between mt-2">
                            <div >
                                {showGoldStars}
                                {showEmptyStars}
                                <div className="d-flex align-items-center gap-3">
                                    <h5 className="m-0 text-primary">{product.price}$</h5>
                                    <h6 className="m-0" style={{ color: "gray", textDecoration: "line-through" }}>
                                        {product.discount}$
                                    </h6>
                                </div>
                            </div>

                            <div className="border p-2 rounded">
                                <img
                                    src={cart}
                                    alt="cart"
                                    width="20px"
                                />
                            </div>
                        </div>
                    </div>
                        </div>
                    </>
                }
            </div>
      </Container>
  );
}
