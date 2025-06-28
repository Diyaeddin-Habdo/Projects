import { Navigate,Outlet } from "react-router-dom";
import Cookie from "cookie-universal";
import Err403 from '../Errors/403.js';
export default function RequireAuth({allowedRole}) {

    const cookie = Cookie();
    const token = cookie.get("token");
    const role = cookie.get("role");
        

    return token ? (allowedRole.includes(String(role)) ? <Outlet /> : <Err403 role={role} />) : <Navigate to={'/'} replace={true} />;
        
}