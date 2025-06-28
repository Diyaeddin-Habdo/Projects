import { useEffect, useState, useRef } from "react";
import axios from 'axios';
import { acdLOGIN , stdLOGIN, baseURL } from "../../../API/Api";
import LoadingSubmit from "../../../Components/Loading/Loading.js";
import Cookie from 'cookie-universal';
import Form from 'react-bootstrap/Form';
import "./Auth.css";

export default function Login(props) {

    // Handle form data
    const [form, SetForm] = useState({
        email: "",
        password: "",
    });

    // Focus on input
    const focusInput = useRef("");

    // Handle focus
    useEffect(() => {
        focusInput.current.focus();
    }, []);


    // Handle loading
    const [loading, SetLoad] = useState(false);

    // Get cookies
    const cookie = Cookie();

    // Handle error
    const [err, SetError] = useState("");


    // Handle form change
    function handleChange(e) {
        SetForm({ ...form, [e.target.name]: e.target.value });
    }

    // Handle Submit
    async function handleSubmit(e) {
        e.preventDefault();
        SetLoad(true);

        const login = (props.title === "std") ? stdLOGIN : acdLOGIN;
        console.log(login);
        try {
            const res = await axios.post(`${baseURL}/${login}`, form);
            SetLoad(false);
            console.log(res.data);
            cookie.set('token', res.data.token);
            cookie.set('id', res.data.id);
            cookie.set('role', res.data.role);
            cookie.set('departmentId',res.data.departmentId);

            // 9763 = "teacher" , 3953 = "Advisor", 1753 = "student"
            const go = res.data.role === "1753" ? "Documents" : "Pending";
            window.location.pathname = `/dashboard/${go}`;
        }
        catch (err)
        {
            SetLoad(false);
            if (err.response.status === 404) {
                SetError("Yalnış Email veya Şifre");
            }
            else {
                SetError("İç Sunucu Hatası");
            }
        }
    }

    return (
        <>
        { loading && <LoadingSubmit /> }
            <div className="container">
                <div className="row" style={{ height:"100vh" }}>
                    <Form className="form" onSubmit={handleSubmit}>

                        <div className="custom-form">

                            <h1>Giriş</h1>

                            <Form.Group className="form-custom" controlId="email">
                                <Form.Control ref={focusInput} name="email" value={form.email} onChange={handleChange} type="email" placeholder="Email..." required minLength="8" />
                                <Form.Label>Email:</Form.Label>
                            </Form.Group>

                            <Form.Group className="form-custom" controlId="password">
                                <Form.Control name="password" value={form.password} onChange={handleChange} type="password" placeholder="Şifre..." required minLength="8" />
                                <Form.Label>Password:</Form.Label>
                            </Form.Group>


                            <button className="btn btn-primary">Gönder</button>
                            {err !== "" && <span className="error">{err}</span>}
                        </div>    
                    </Form>
                </div>
            </div>
        </>
    );
};