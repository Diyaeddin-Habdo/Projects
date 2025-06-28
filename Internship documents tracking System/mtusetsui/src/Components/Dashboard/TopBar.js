/*import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { Menu } from '../../Context/MenuContext.js';
import { useContext } from 'react';
import Cookie from 'cookie-universal';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownButton from 'react-bootstrap/DropdownButton';
import { useNavigate } from 'react-router-dom';


export default function TopBar() {

    // Get the menu status (open or close)
    const menu = useContext(Menu);
    const setOpen = menu.SetIsOpen;

    // handle the logout operation
    function handleLogout() {
        cookie.remove("id");
        cookie.remove("token");
        cookie.remove("role");

        window.location.pathname = "/";
    }

    // Get cookies
    const cookie = Cookie();
    
    const navigate = useNavigate();
    return (
        <div className="top-bar">
            <div className="d-flex align-items-center justify-content-between h-100">
                <div className="d-flex align-items-center gap-5">
                    <h3>MTÜ SETS</h3>
                    <FontAwesomeIcon onClick={() => setOpen(prev => !prev)} cursor={"pointer"} icon={faBars} />
                </div>

                <div>
                    <DropdownButton id="dropdown-basic-button" title="Hesap Ayarları" >
                        <Dropdown.Item onClick={() => navigate('/dashboard/profile')} >Profile</Dropdown.Item>
                        <Dropdown.Item onClick={handleLogout}>Logout</Dropdown.Item>
                    </DropdownButton>
                </div>
            </div>
      </div>
  )
};*/


import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { Menu } from '../../Context/MenuContext.js';
import { useContext, useEffect, useState } from 'react';
import Cookie from 'cookie-universal';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownButton from 'react-bootstrap/DropdownButton';
import { useNavigate } from 'react-router-dom';
import {baseURL,sIMAGE,isTokenExpired,logout, tIMAGE} from "../../API/Api.js";
import { Axios } from "../../API/axios.js";
export default function TopBar() {
    // State to hold the profile image path
    const [profileImage, setProfileImage] = useState('');
    
    // Get the menu status (open or close)
    const menu = useContext(Menu);
    const setOpen = menu.SetIsOpen;

    // Get cookies
    const cookie = Cookie();
    const userId = cookie.get("id"); // Assuming the user ID is stored in cookies
    const role = String(cookie.get("role"));
    const url = role === "1753" ? sIMAGE : tIMAGE;
    const nav = useNavigate();


    useEffect(() => {
        if(!isTokenExpired())
        {
            try{
                Axios.get(`${url}/${userId}`).then(data => {
                    setProfileImage(data.data);
                })
                .catch(() => nav('/dashboard/users/page/404', { replace: true }));
            }
            catch(err)
            {
                console.log(err);
            }
        }
        else
            logout();
        
    }, []);

    // Handle the logout operation
    function handleLogout() {
        cookie.remove("id");
        cookie.remove("token");
        cookie.remove("role");
        window.location.pathname = "/";
    }

    return (
        <div className="top-bar">
            <div className="d-flex align-items-center justify-content-between h-100">
                <div className="d-flex align-items-center gap-5">
                    <h3>MTÜ SETS</h3>
                    <FontAwesomeIcon onClick={() => setOpen(prev => !prev)} cursor={"pointer"} icon={faBars} />
                </div>

                <div className="d-flex align-items-center">
                    {profileImage && (
                        <img
                            src={profileImage}
                            alt="Profile"
                            className="rounded-circle me-2"
                            style={{ width: '40px', height: '40px', objectFit: 'cover' }} // Adjust size as needed
                        />
                    )}
                    <DropdownButton id="dropdown-basic-button" title="Hesap Ayarları" >
                        <Dropdown.Item onClick={() => nav('/dashboard/profile')} >Profile</Dropdown.Item>
                        <Dropdown.Item onClick={handleLogout}>Logout</Dropdown.Item>
                    </DropdownButton>
                </div>
            </div>
        </div>
    );
}
