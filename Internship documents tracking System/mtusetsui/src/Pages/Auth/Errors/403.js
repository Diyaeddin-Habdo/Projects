import './403.css';
import { Link } from 'react-router-dom'
export default function Err403({role}) {

    return (
        <div className="text-wrapper">
            <div className="title" data-content={404}>
                403 - ACCESS DENIED
            </div>

            <div className="subtitle">
                Oops,You don't have permission to access this page.
                <Link className="d-block text-center btn btn-primary" to={role === "3953" ? "/dashboard/users" : (role === "3953" ? "/dashboard" : "/dashboard")}>
                    {role === "3953" ? "Go To Users Page" : (role === "3953" ? "Got To Onaylanan Page" : "Go To Yuklenecekler Page")}
                </Link>
            </div>

        </div>
    );
}