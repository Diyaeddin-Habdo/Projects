import { useEffect, useState } from "react";
import { Form, Button } from "react-bootstrap";
import { Axios } from "../../../API/axios.js";
import { DOCUMENTS, isTokenExpired, logout, sDOCUMENTS, STATUS } from "../../../API/Api.js";
import LoadingSubmit from "../../../Components/Loading/Loading.js";
import Cookie from "cookie-universal";
import { useNavigate } from "react-router-dom";
import "./Status.css";
import MessageBox from "./MessageBox.js";
import "./FileError.css";



export default function Documents() {

    const[documentId,SetDocumentId] = useState("");
    const [StudentId, SetStudentId] = useState("");
    const [SGKformu, SetSGKformu] = useState("");
    const [BasvuruFormu, SetBasvuruFormu] = useState("");
    const [KabulFormu, SetKabulFormu] = useState("");
    const [TaahhutnameFormu, SetTaahhutnameFormu] = useState("");
    
    const[status,SetStatus] = useState(true);

    
    const [Loading, SetLoading] = useState(false);

    const cookie = Cookie();
    const nav = useNavigate();

     // Handle status
     const [message, SetMessage] = useState("");
     const[confirmStatus,SetConfirmStatus] = useState("");
     const [FileErrorMessage, SetErrorMessage] = useState("");

     const MAX_FILE_SIZE = 2 * 1024 * 1024; // 2 MB

    const handleFileChange = (e, setFile,formName) => {
        const file = e.target.files[0];
        if (file && file.type === "application/pdf" && file.size <= MAX_FILE_SIZE) {
            SetErrorMessage("");
            setFile(file);
        } else {
            SetErrorMessage(`${formName} PDF formatında ve en fazla 2 MB boyutunda olmalı.`);
            e.target.value = ""; // Invalid dosyayı sıfırlamak için
        }
    };

    useEffect(() => {
        if(!isTokenExpired())
        {
            const currentUserId = cookie.get("id");
        try{
            SetLoading(true);
            Axios.get(`${STATUS}/${currentUserId}`).then(data => {
                if(data.data === "Hoca Reddetti" || data.data === "Belgeler yuklenmedi" || data.data === "Danisman Reddetti")
                {
                    SetConfirmStatus(data.data);
                    SetStatus(true);
                    SetMessage(data.data);
                }
                else
                {
                    SetStatus(false);
                    SetMessage(data.data);
                }
            })
            .catch(() => nav('/dashboard/users/page/404', { replace: true }));
            SetLoading(false);
        }
        catch(err)
        {
            SetLoading(false);
            console.log(err);
        }
        }
        else
            logout();
        
    }, []);

    useEffect(() => {
        if (!isTokenExpired()) {
            const currentUserId = cookie.get("id");
            try {
                SetLoading(true);
                Axios.get(`${sDOCUMENTS}/${currentUserId}`)
                    .then(data => {
                        SetDocumentId(data.data.id);
                        SetLoading(false);  // Set loading to false after successful response
                    })
                    .catch((error) => {
                        SetLoading(false);  // Ensure loading is stopped on error
    
                        if (error.response) {
                            // API returned a response, but with an error status code
                            if (error.response.status === 400) {
                                console.log(`Bad Request: ${error.response.data}`);
                            } else if (error.response.status === 404) {
                                console.log(`Not Found: ${error.response.data}`);
                            } else {
                                console.log(`Error: ${error.response.data}`);
                            }
                        } else if (error.request) {
                            // No response was received from the server
                            console.log('No response from server:', error.request);
                        } else {
                            // Other types of errors
                            console.log('Error:', error.message);
                        }
                    });
            } catch (err) {
                SetLoading(false);
                console.log('Unexpected error:', err);
            }
        } else {
            logout();
        }
    }, []);
    


    // nahdle submit
    async function HandleSubmit(e) {
        SetLoading(true);
        e.preventDefault();

        const currentUserId = cookie.get("id");

        const form = new FormData();
        form.append('studentId', currentUserId);
        form.append('sgkStajFormu', SGKformu);
        form.append('stajBasvuruFormu', BasvuruFormu);
        form.append('stajKabulFormu', KabulFormu);
        form.append('stajTaahhutnameFormu', TaahhutnameFormu);

        try {
            if(!isTokenExpired())
            {
                if(confirmStatus.toLowerCase() === "Hoca Reddetti".toLowerCase() || confirmStatus.toLowerCase() === "Danisman Reddetti".toLowerCase())
                {
                    await Axios.put(`${DOCUMENTS}/${documentId}`, form);
                    SetLoading(false);
                    window.location.pathname = "/dashboard";
                }
                else
                {
                    await Axios.post(`${DOCUMENTS}`, form);
                    SetLoading(false);
                    window.location.pathname = "/dashboard";
                }
            }
            else
                logout();
        }
        catch (err) {
            SetLoading(false);
            console.log(err);
        }
    }

    return (

        
        <Form className="bg-white w-100 mx-2 p-3" onSubmit={HandleSubmit}>
            {Loading && <LoadingSubmit />}
           
            
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput2">
                <Form.Label>SGK Formu</Form.Label>
                <Form.Control disabled = {status === false} onChange={(e) => handleFileChange(e, SetSGKformu,"SGK Formu")} type="file" required />
            </Form.Group>


            <Form.Group className="mb-3" controlId="exampleForm.ControlInput3">
                <Form.Label>Basvuru Formu</Form.Label>
                <Form.Control disabled = {status === false} onChange={(e) => handleFileChange(e, SetBasvuruFormu,"Basvuru Formu")} type="file" required />
            </Form.Group>

            <Form.Group className="mb-3" controlId="exampleForm.ControlInput4">
                <Form.Label>Kabul Formu</Form.Label>
                <Form.Control disabled = {status === false} onChange={(e) => handleFileChange(e, SetKabulFormu,"Kabul Formu")} type="file"  required />
            </Form.Group>

            <Form.Group className="mb-3" controlId="exampleForm.ControlInput5">
                <Form.Label>Taahhutname Formu</Form.Label>
                <Form.Control disabled = {status === false} onChange={(e) => handleFileChange(e, SetTaahhutnameFormu,"Taahhutname Formu")} type="file" required />
            </Form.Group>
            {FileErrorMessage && <span className="FileError">{FileErrorMessage}</span>}
            <Button
                disabled = {status === false}
            className="btn btn-primary" type="Submit">
                Kaydet
            </Button>
            {message !== "" && <span className="message">{message}</span>}
            
        </Form>
        
    );
}