import { useEffect, useState, useRef } from "react";
import axios from 'axios';
import { LOGIN, baseURL } from "../../../API/Api";
import LoadingSubmit from "../../../Components/Loading/Loading.jsx";
import Cookie from 'cookie-universal';
import Form from 'react-bootstrap/Form';
import { useNavigate } from 'react-router-dom';
import "./Auth.css";
export default function Login() {

    // State
    const [form, SetForm] = useState({
        email: "",
        password: "",
    });

    // Ref
    const focusInput = useRef("");

    // Handle focus
    useEffect(() => {
        focusInput.current.focus();
    }, []);


    // Handle loading
    const [loading, SetLoad] = useState(false);

    // Handle cookies
    const cookie = Cookie();

    // Handle error
    const [err, SetError] = useState("");

    // Navigate
    const navigate = useNavigate();

    // Handle form change
    function handleChange(e) {
        SetForm({ ...form, [e.target.name]: e.target.value });
    }

    // Handle Submit
    async function handleSubmit(e) {
        e.preventDefault();
        SetLoad(true);
        try {
            const res = await axios.post(`${baseURL}/${LOGIN}`, form);
            SetLoad(false);

            cookie.set('e-commerce', res.data.token);
            cookie.set('ID', res.data.id);
            cookie.set('Name', res.data.name);
            cookie.set('role', res.data.roles);

            console.log(res.data.roles)
            const go = res.data.roles === "Admin" ? "Users" : "Categories";
            window.location.pathname = `/dashboard/${go}`;
        }
        catch (err)
        {
            SetLoad(false);
            if (err.response.status === 401) {
                SetError("Incorrect credintinals");
            }
            else {
                SetError("Invalid data");
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

                            <h1>Login</h1>

                            <Form.Group className="form-custom" controlId="email">
                                <Form.Control ref={focusInput} name="email" value={form.email} onChange={handleChange} type="email" placeholder="Enter your email..." required minLength="8" />
                                <Form.Label>Email:</Form.Label>
                            </Form.Group>

                            <Form.Group className="form-custom" controlId="password">
                                <Form.Control name="password" value={form.password} onChange={handleChange} type="password" placeholder="Enter your password..." required minLength="8" />
                                <Form.Label>Password:</Form.Label>
                            </Form.Group>


                            <button className="btn btn-primary">Submit</button>
                            {err !== "" && <span className="error">{err}</span>}
                        </div>    
                    </Form>
                </div>
            </div>
        </>
    );
};