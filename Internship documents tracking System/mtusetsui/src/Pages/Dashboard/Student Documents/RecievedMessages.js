import { useState, useEffect } from "react";
import { Axios } from "../../../API/axios.js";
import { isTokenExpired, logout, MESSAGES } from "../../../API/Api.js";
import MessageBox from "./MessageBox.js";
import LoadingSubmit from "../../../Components/Loading/Loading.js";
import Cookie from "cookie-universal";
import { useNavigate } from "react-router-dom";
import "./Status.css";


export default function RecievedMessages() {
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
                Axios.get(`${MESSAGES}/${currentUserId}`)
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
                    fromID={`${messageData.fromId}`} 
                    message={messageData.message}
                    timestamp={new Date(messageData.time).toLocaleDateString('tr-TR', {
                    day: '2-digit',        // Gün: 01-31
                    month: '2-digit',      // Ay: 01-12
                    year: 'numeric',       // Yıl: 4 haneli
                    hour: '2-digit',       // Saat: 00-23
                    minute: '2-digit',     // Dakika: 00-59
                    second: '2-digit'      // Saniye: 00-59
                })}  // Tarih ve saati formatla
                />
            ))}
        </div>
    </>
    );
}
