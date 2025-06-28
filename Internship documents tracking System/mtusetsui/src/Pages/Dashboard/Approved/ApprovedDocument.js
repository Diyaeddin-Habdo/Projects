import { useEffect, useState } from "react";
import { Form, Button } from "react-bootstrap";
import { Axios } from "../../../API/axios.js";
import { sDOCUMENTS, isTokenExpired, logout, DOCUMENTS } from "../../../API/Api.js";
import LoadingSubmit from "../../../Components/Loading/Loading.js";
import Cookie from "cookie-universal";
import { useNavigate } from "react-router-dom";
import "./style.css";

export default function OgrenciBelgeleri() {

    // Get documents data
    const [documentId, SetDocumentId] = useState("");
    const [studentId, SetStudentId] = useState("");
    const [SGKformu, SetSGKformu] = useState("");
    const [BasvuruFormu, SetBasvuruFormu] = useState("");
    const [KabulFormu, SetKabulFormu] = useState("");
    const [TaahhutnameFormu, SetTaahhutnameFormu] = useState("");
    const [status, SetStatus] = useState("");
    const [uploadTime, SetuploadTime] = useState("");

    // Set approved or unapproved message
    const [message, setMessage] = useState("");

    // Set loading
    const [Loading, SetLoading] = useState(false);

    // Get the id of clicked student
    const Id = Number(window.location.pathname.replace("/dashboard/Approved/", ""));

    // Get cookies
    const cookie = Cookie();

    // Set navigate
    const nav = useNavigate();

    useEffect(() => {

        // check if token expired first
        if(!isTokenExpired())
        {
            try {
                SetLoading(true);
                Axios.get(`${sDOCUMENTS}/${Id}`)
                    .then(data => {
                        SetDocumentId(data.data.id);
                        SetStudentId(data.data.studentId);
                        SetSGKformu(data.data.sgkStajFormu);
                        SetBasvuruFormu(data.data.stajBasvuruFormu);
                        SetKabulFormu(data.data.stajKabulFormu);
                        SetTaahhutnameFormu(data.data.stajTaahhutnameFormu);
                        SetuploadTime(data.data.uploadTime);
                    })
                    .catch(() => nav('/dashboard/users/page/404', { replace: true }));
                SetLoading(false);
            } catch (err) {
                SetLoading(false);
                console.log(err);
            }
        }
        else
            logout();
    }, [Id, nav]);

    async function HandleSubmit(e) {
        SetLoading(true);
        e.preventDefault();

        const currentUserId = cookie.get("id");

        try {
            if (!isTokenExpired()) {
                await Axios.put(`${DOCUMENTS}/${documentId}/${studentId}/${currentUserId}/${status}/${message}`);
                SetLoading(false);
                window.location.pathname = "/dashboard";
            } else {
                logout();
            }
        } catch (err) {
            SetLoading(false);
            console.log(err);
        }
    }

    const isFormValid = status !== "" && message.trim().length >= 9;
    return (
        <Form className="bg-white w-100 mx-2 p-3" onSubmit={HandleSubmit}>
            {Loading && <LoadingSubmit />}

            <div className="form-download-section">
                <Form.Group className="form-item">
                    <Form.Label>SGK Formu</Form.Label>
                    <a href={SGKformu} target="_blank" rel="noreferrer" className="download-link">
                        SGK Formunu İndir
                    </a>
                </Form.Group>

                <Form.Group className="form-item">
                    <Form.Label>Başvuru Formu</Form.Label>
                    <a href={BasvuruFormu} target="_blank" rel="noreferrer" className="download-link">
                        Başvuru Formunu İndir
                    </a>
                </Form.Group>

                <Form.Group className="form-item">
                    <Form.Label>Kabul Formu</Form.Label>
                    <a href={KabulFormu} target="_blank" rel="noreferrer" className="download-link">
                        Kabul Formunu İndir
                    </a>
                </Form.Group>

                <Form.Group className="form-item">
                    <Form.Label>Taahhütname Formu</Form.Label>
                    <a href={TaahhutnameFormu} target="_blank" rel="noreferrer" className="download-link">
                        Taahhütname Formunu İndir
                    </a>
                </Form.Group>
            </div>



            <Form.Group className="mb-3">
                <Form.Label>Onay Durumu</Form.Label>
                <Form.Select value={status} onChange={(e) => SetStatus(e.target.value)} required>
                    <option value="">Seçin...</option>
                    {String(cookie.get("role")) === "3953" ? (
                        <>
                            <option value="Danisman Onayladi">Onaylandi</option>
                            <option value="Danisman Reddetti">Reddedildi</option>
                        </>
                    ) : (
                        <>
                            <option value="Hoca Onayladi">Onaylandi</option>
                            <option value="Hoca Reddetti">Reddedildi</option>
                        </>
                    )}
                </Form.Select>
            </Form.Group>


            <Form.Group className="mb-3">
                <Form.Label>Mesaj</Form.Label>
                <Form.Control
                    as="textarea"
                    rows={3}
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    placeholder="Onay/reddetme kararıyla ilgili bir mesaj yazın(en az 9 karakter)..."
                    required
                />
            </Form.Group>

            <Button className="btn btn-primary" type="submit" disabled={!isFormValid}>
                Kaydet 
            </Button>
        </Form>
    );
}
