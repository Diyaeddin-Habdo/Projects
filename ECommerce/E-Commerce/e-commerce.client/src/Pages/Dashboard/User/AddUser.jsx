import { useEffect, useState } from "react";
import { Form, Button } from "react-bootstrap";
import { Axios } from "../../../API/axios.js";
import { USERS } from "../../../API/Api.js";
import LoadingSubmit from "../../../Components/Loading/Loading.jsx";
export default function AddUser() {


    const [name, SetName] = useState("");
    const [email, SetEmail] = useState("");
    const [roles, SetRoles] = useState("User");
    const [password, SetPassword] = useState("");
    const [Loading, SetLoading] = useState(false);


    // nahdle submit
    async function HandleSubmit(e) {
        SetLoading(true);
        e.preventDefault();

        try {
            const res = await Axios.post(`${USERS}`, {
                name: name,
                email: email,
                password: password,
                roles: roles,
            });

            SetLoading(false);
            window.location.pathname = "/dashboard/Users";
        }
        catch (err) {
            SetLoading(false);
            console.log(err);
        }
    }



    return (
        <Form className="bg-white w-100 mx-2 p-3" onSubmit={HandleSubmit}>
            {Loading && <LoadingSubmit />}
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">

                <Form.Label>Name</Form.Label>
                <Form.Control value={name} onChange={(e) => SetName(e.target.value)} type="text" placeholder="name..." required />
            </Form.Group>

            <Form.Group className="mb-3" controlId="exampleForm.ControlInput2">
                <Form.Label>Email</Form.Label>
                <Form.Control value={email} onChange={(e) => SetEmail(e.target.value)} type="Email" placeholder="email..." required />
            </Form.Group>


            <Form.Group className="mb-3" controlId="exampleForm.ControlInput3">
                <Form.Label>Password</Form.Label>
                <Form.Control value={password} onChange={(e) => SetPassword(e.target.value)} type="password" placeholder="password..." required />
            </Form.Group>


            {/*Drop down codu */}

            <Form.Group className="mb-3" controlId="formRoles">
                <Form.Label>Role</Form.Label>
                <Form.Select value={roles} onChange={(e) => SetRoles(e.target.value)}>

                    <option disabled value="">Select Role</option>
                    <option value="User">User</option>
                    <option value="Product Manager">Product Manager</option>
                    <option value="Admin">Admin</option>
                </Form.Select>
            </Form.Group>



            <Button
                disabled=
                {
                    name.length > 1 && email.length > 6 && password.length >= 8 && roles !== "" ? false : true
            } className="btn btn-primary" type="Submit">
                Save
            </Button>
        </Form>

    );
}