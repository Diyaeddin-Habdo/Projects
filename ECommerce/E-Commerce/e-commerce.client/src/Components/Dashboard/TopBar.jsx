import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { NavLink } from 'react-router-dom';
import { Menu } from '../../Context/MenuContext.jsx';
import { useContext } from 'react';
import Cookie from 'cookie-universal';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownButton from 'react-bootstrap/DropdownButton';

export default function TopBar() {

    const menu = useContext(Menu);
    const setOpen = menu.SetIsOpen;

    function handleLogout() {
        cookie.remove("ID");
        cookie.remove("Name");
        cookie.remove("e-commerce");
        cookie.remove("role");

        window.location.pathname = "/login";
    }

    // Handle cookies
    const cookie = Cookie();

    return (
        <div className="top-bar">
            <div className="d-flex align-items-center justify-content-between h-100">
                <div className="d-flex align-items-center gap-5">
                    <h3>E-Commerce</h3>
                    <FontAwesomeIcon onClick={() => setOpen(prev => !prev)} cursor={"pointer"} icon={faBars} />
                </div>

                <div>
                    <DropdownButton id="dropdown-basic-button" title={cookie.get("Name")}>
                        <Dropdown.Item onClick={handleLogout}>Logout</Dropdown.Item>
                    </DropdownButton>
                </div>
            </div>
      </div>
  )
};