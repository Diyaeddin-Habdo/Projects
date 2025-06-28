import { useState, useEffect } from "react";
import { Axios } from "../../../API/axios.js";
import { isTokenExpired, logout, SendMESSAGES } from "../../../API/Api";
import MessageBox from "./MessageBox.js";
import LoadingSubmit from "../../../Components/Loading/Loading.js";
import Cookie from "cookie-universal";
import { useNavigate } from "react-router-dom";
import "./Status.css";


export default function Messages() {
    const [messages, setMessages] = useState([]);  // API'dan gelen tüm mesajlar
    const [loading, setLoading] = useState(false);
    const [message, SetMessage] = useState("");
    const cookie = Cookie();
    const nav = useNavigate();

    useEffect(() => {
        const currentUserId = cookie.get("id");
        if (!isTokenExpired()) {
            try {
                setLoading(true);
                Axios.get(`${SendMESSAGES}/${currentUserId}`)
                    .then(data => {
                        setMessages(data.data);  // Gelen mesajları state'e set et
                        setLoading(false);  // İstek başarılıysa loading durumunu durdur
                    })
                    .catch((error) => {
                        setLoading(false);  // Hata oluşursa loading'i durdur
    
                        if (error.response) {
                            // Sunucudan bir yanıt alındı, ancak hata durumu var
                            if (error.response.status === 400) {
                                console.log(`Bad Request: ${error.response.data}`);
                            } else if (error.response.status === 404) {
                                console.log(`Not Found: ${error.response.data}`);
                                SetMessage("No Messages.");
                            } else {
                                console.log(`Error: ${error.response.data}`);
                            }
                        } else if (error.request) {
                            // Sunucudan yanıt alınmadı
                            console.log('No response from server:', error.request);
                        } else {
                            // Diğer hata durumları
                            console.log('Error:', error.message);
                        }
                    });
            } catch (err) {
                setLoading(false);
                console.log('Unexpected error:', err);
            }
        } else {
            logout();  // Token süresi dolmuşsa çıkış yap
        }
    }, []);
    

    return (
        <>
        {loading && <LoadingSubmit />}
        <div className="message-container">
        {message !== "" && <span className="message">{message}</span>}
            {messages.map((messageData, index) => (
                <MessageBox 
                    key={index} 
                    toId={`${messageData.toId}`} 
                    message={messageData.message}
                    timestamp={new Date(messageData.time).toLocaleDateString(undefined, {
                        weekday: 'long', // Gün ismi
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                        second: '2-digit',
                        hour12: false  // Saniye: 00-59
                })}  // Tarih ve saati formatla
                />
            ))}
        </div>
    </>
    );
}
