import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App.js';
import './index.css';
import './Css/Components/Button.css';
import './Css/Components/Alerts.css';
import './Css/Components/Loading.css';
import './Pages/Auth/AuthOperation/Auth.css';
import "react-loading-skeleton/dist/skeleton.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router } from 'react-router-dom';
import MenuContext from './Context/MenuContext.js';
import WindowContext from './Context/WindowContext.js';

// Routing
ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <WindowContext>
            <MenuContext>
                <Router>
                    <App />
                </Router>
            </MenuContext>
        </WindowContext>
    </React.StrictMode>
)
