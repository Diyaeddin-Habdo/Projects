import React, { useEffect, useState } from 'react';
import './MessageBox.css';
import LoadingSubmit from "../../../Components/Loading/Loading.js";
import { isTokenExpired, logout, TEACHER } from '../../../API/Api.js';
import { Axios } from '../../../API/axios.js';
import { useNavigate } from "react-router-dom";

const MessageBox = ({ fromID, message, timestamp }) => {

    const [loading, setLoading] = useState(false);
    const [teacherName,SetTeacherName] = useState("");
    const nav = useNavigate();


    useEffect(() => {
        if(!isTokenExpired())
        {
            try{
                setLoading(true);
                Axios.get(`${TEACHER}/${fromID}`).then(data => {
                    SetTeacherName(data.data);
                })
                .catch(() => nav('/dashboard/users/page/404', { replace: true }));
                setLoading(false);
            }
            catch(err)
            {
                setLoading(false);
                console.log(err);
            }
        }
        else
            logout();
        
    }, []);


    return (
        <div className="message-box">
            <div className="message-header">  {/* Flex ile hizalama için yeni bir div */}
                {teacherName && <span className="teacher-name">{teacherName}</span>}
                {timestamp && <span className="message-timestamp">{timestamp}</span>}
            </div>
            {message && <span className="message">{message}</span>}
        </div>
    );
};

export default MessageBox;
