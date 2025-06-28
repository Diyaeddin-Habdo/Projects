import { useRef, useState, useEffect } from "react";
import axios from 'axios';
import { REGISTER, baseURL } from "../../../API/Api.js";
import LoadingSubmit from "../../../Components/Loading/Loading.jsx";
import Cookie from 'cookie-universal';
import './Auth.css';
import Form from 'react-bootstrap/Form';
import { useNavigate } from 'react-router-dom';

export default function Register() {

    // State
    const [form, SetForm] = useState({
        name: "",
        email: "",
        password:"",
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

    // Handle form change
    function handleChange(e) {
        SetForm({ ...form, [e.target.name]: e.target.value });
    }
    // Navigate
    const navigate = useNavigate();
    // Handle Submit
    async function handleSubmit(e) {
        e.preventDefault();
        SetLoad(true);
        try {
            const res = await axios.post(`${baseURL}/${REGISTER}`, form);
            SetLoad(false);
            const token = res.data.token;
            const email = res.data.email;
            cookie.set('e-commerce', token);
            navigate("/dashboard/Users", { replace: true });
        }
        catch (err)
        {
            SetLoad(false);
            if (err.response.status === 409) {
                SetError("Email is already been taken");
            }
            else {
                SetError("Invalid data");
            }
        }
    }
    
    return (
        <>
            {loading && <LoadingSubmit /> }
            <div className="container">
                <div className="row" style={{ height: "100vh" }}>
                    <Form className="form" onSubmit={handleSubmit}>

                        <div className="custom-form">

                            <h1>Register Now</h1>

                            <Form.Group className="form-custom" controlId="name">
                                <Form.Control ref={focusInput} name="name" value={form.name} onChange={handleChange} type="text" placeholder="Enter your name..." required minLength="8" />
                                <Form.Label>Name:</Form.Label>
                            </Form.Group>

                            <Form.Group className="form-custom" controlId="email">
                                <Form.Control name="email" value={form.email} onChange={handleChange} type="email" placeholder="Enter your email..." required minLength="8" />
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