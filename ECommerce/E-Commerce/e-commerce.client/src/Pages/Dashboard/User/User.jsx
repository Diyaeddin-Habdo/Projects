import { useEffect, useState } from "react";
import { Form, Button } from "react-bootstrap";
import { Axios } from "../../../API/axios.js";
import { USERS } from "../../../API/Api.js";
import LoadingSubmit from "../../../Components/Loading/Loading.jsx";
import { useNavigate } from "react-router-dom";


export default function User() {


    const [name, SetName] = useState("");
    const [email, SetEmail] = useState("");
    const [roles, SetRoles] = useState("User");
    const [Disable, SetDisable] = useState(true);
    const [Loading, SetLoading] = useState(false);

    // Get ID
    const Id = Number(window.location.pathname.replace("/dashboard/Users/", ""));

    const nav = useNavigate();

    // Get user by Id
    useEffect(() => {
        SetLoading(true);

        Axios.get(`${USERS}/${Id}`).then(data => {
            SetName(data.data.name);
            SetEmail(data.data.email);
            SetRoles(data.data.roles);
            SetLoading(false);
        }).then(() => SetDisable(false))
            .catch(() => nav('/dashboard/users/page/404', { replace: true }));
    }, [])

    // nahdle submit
    async function HandleSubmit(e) {
        SetLoading(true);
        e.preventDefault();

        try {
            const res = await Axios.put(`${USERS}/${Id}`, {
                name: name,
                email: email,
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
            {Loading && <LoadingSubmit /> }
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">

                <Form.Label>Name</Form.Label>
                <Form.Control value={name} onChange={(e) => SetName(e.target.value)} type="text" placeholder="name..." required />
            </Form.Group>

            <Form.Group className="mb-3" controlId="exampleForm.ControlInput2">
                <Form.Label>Email</Form.Label>
                <Form.Control value={email} onChange={(e) => SetEmail(e.target.value)} type="Email" placeholder="email..." required/>
            </Form.Group>


            {/*Drop down codu */}

            <Form.Group className="mb-3" controlId="formRoles">
                <Form.Label>Role</Form.Label>
                <Form.Select value={roles} onChange={(e) => SetRoles(e.target.value)}>

                    <option disabled value = "">Select Role</option>
                    <option value="User">User</option>
                    <option value="Product Manager">Product Manager</option>
                    <option value="Admin">Admin</option>
                </Form.Select>
            </Form.Group>



            <Button disabled={Disable} className="btn btn-primary" type="Submit">
                Save
            </Button>
        </Form>
    
    );
}