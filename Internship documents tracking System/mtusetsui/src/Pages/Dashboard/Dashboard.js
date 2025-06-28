import { Outlet } from 'react-router-dom';
import TopBar from '../../Components/Dashboard/TopBar.js';
import SideBar from '../../Components/Dashboard/SideBar.js';
import './dashboard.css';
export default function Dashboard() {
    return (
        <div className="position-relative dashboard ">

            <TopBar />
            <div className="d-flex gap-1" style={{ marginTop: '70px' }}>
                <SideBar />
                <Outlet />
            </div>


        </div>

  );
};