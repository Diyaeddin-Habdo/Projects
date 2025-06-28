import NavBar from "../../Components/Website/Navbar/NavBar.jsx";
import { Outlet } from "react-router-dom";

export default function Website() {

    return (
        <>
            <NavBar />
            <Outlet/>
        </>
    );
}