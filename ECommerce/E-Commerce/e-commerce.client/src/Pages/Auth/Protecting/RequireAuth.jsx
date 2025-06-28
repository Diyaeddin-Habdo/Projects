import { Navigate,Outlet } from "react-router-dom";
import Cookie from "cookie-universal";
import Err403 from '../Errors/403.jsx';
export default function RequireAuth({allowedRole}) {

    const cookie = Cookie();
    const token = cookie.get("e-commerce");
    const role = cookie.get("role");
        

    return token ? (allowedRole.includes(role) ? <Outlet /> : <Err403 role={role} />) : <Navigate to={'/login'} replace={true} />;
        
}