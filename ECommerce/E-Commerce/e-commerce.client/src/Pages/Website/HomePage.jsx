import Container from "react-bootstrap/Container";
import "./Home.css";
import { Link } from "react-router-dom";
import Landing from "../../Components/Website/Landing/Landing";
import LatestSalesProducts from "../../Components/Website/Product/LatestSalesProducts.jsx";
import ShowTopRated from "../../Components/Website/Product/ShowTopRated.jsx";
import ShwoLatestProducts from "../../Components/Website/Product/ShwoLatestProducts";
import BeforeTopRated from "./BeforeTopRated.jsx";

export default function HomePage() {

    return (
        <div>
            <Landing />
            <LatestSalesProducts />
            <BeforeTopRated />
            <Container>
                <div className="d-flex align-items-start flex-wrap mt-5">
                    
                    <ShowTopRated />
                    <ShwoLatestProducts/>
                </div>
            </Container>
        </div>
    );
}