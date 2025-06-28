import React from 'react';
import './Home.css';
import mtuImage from '../../R.png';
import { useNavigate } from 'react-router-dom';

export default function HomePage() {

    const navigate = useNavigate();
    return (
        <div className="homepage-container">
            <div className="logo-section">
                <img src={mtuImage} alt="MTU logo" className="mtu-img" />
                <p>Malatya Turgut Özal Üniversitesi</p>
                <p>Staj Evrak Takip Sistemi</p>
            </div>
            <h1 className="mtu-title">MTÜ SETS</h1>
            <div className="card-container">
                <button className="custom-button" onClick={() => navigate('/std-login')} >Öğrenci Girişi</button>
                <button className="custom-button" onClick={() => navigate('/acd-login')} >Akademisyen Girişi</button>
            </div>
        </div>
    );
}
