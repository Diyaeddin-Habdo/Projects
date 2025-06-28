import './Bars.css';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";  
import { NavLink } from 'react-router-dom';
import { Menu } from '../../Context/MenuContext.js';
import { WS } from '../../Context/WindowContext';
import { useContext } from 'react';
import Cookie from "cookie-universal";
import { links } from "./Navlink.js";

export default function SideBar() {

    // Get the menu status (Open or close)
    const menu = useContext(Menu);
    const isOpen = menu.IsOpen;

    // Get window size
    const ws = useContext(WS);

    // Get the role of current user
    const cookie = Cookie();
    const role = cookie.get("role");


    return (

        <>
            <div style={{
                position: "fixed",
                top: "70px",
                left: 0,
                width: "100%",
                height: "100vh",
                backgroundColor: "rgba(0,0,0,0.2)",
                display: ws.WindowSize < "768" && isOpen ?  'block': 'none',
            }}></div>

            <div className="side-bar pt-3"
            style={{
                left: ws.WindowSize < "768" ? (isOpen ? 0 : "-100%") : 0,
                width: isOpen ? "240px" : "fit-content",
                position: ws.WindowSize < "768" ? "fixed" : "sticky",
                }}>

                {/*Show the links based on role of current user*/}
                {links.map((link, key) => link.role.includes(role) && 

                (


                    <NavLink
                        key={key}
                        to={link.path}
                        className="d-flex align-items-center gap-2 side-bar-link"
                    >

                        <FontAwesomeIcon
                            style={{ padding: isOpen ? "10px 8px 10px 15px" : "10px 13px" }}
                            icon={link.icon}
                        />
                        <p
                            className="m-0"
                            style={{
                                display: isOpen ? "block" : "none",
                            }}
                        >
                            {link.name}
                        </p>
                    </NavLink>
                )
                )}         
            </div>
        </>
  );
};