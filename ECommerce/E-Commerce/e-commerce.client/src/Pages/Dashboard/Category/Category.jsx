import { useEffect, useState } from "react";
import { Form, Button } from "react-bootstrap";
import { Axios } from "../../../API/axios.js";
import { CATEGORIES } from "../../../API/Api.js";
import LoadingSubmit from "../../../Components/Loading/Loading.jsx";
import { useNavigate, useParams } from "react-router-dom";


export default function User() {


    const [name, SetName] = useState("");
    const [image, SetImage] = useState("");
    const [disalbed, SetDisabled] = useState(true);
    const [Loading, SetLoading] = useState(false);

    // Get ID
    //const { Id } = useParams();
    const Id = Number(window.location.pathname.replace("/dashboard/categories/", ""));

    const nav = useNavigate();

    // Get category by Id
    useEffect(() => {
        SetLoading(true);

        Axios.get(`${CATEGORIES}/${Id}`).then(data => {
            SetName(data.data.name);
            SetImage(data.data.image);
            SetLoading(false);
        }).then(() => SetDisabled(false))
            .catch(() => nav('/dashboard/categories/page/404', { replace: true }));
    }, [])

    // handle submit
    async function HandleSubmit(e) {
        SetLoading(true);
        e.preventDefault();

        const form = new FormData();
        form.append('name', name);
        form.append('image', image);

        try {
            const res = await Axios.put(`${CATEGORIES}/${Id}`, form);

            SetLoading(false);
            window.location.pathname = "/dashboard/categories";
        }
        catch (err) {
            SetLoading(false);
            console.log(err);
        }
    }



    return (
        <Form className="bg-white w-100 mx-2 p-3" onSubmit={HandleSubmit}>
            {Loading && <LoadingSubmit />}
            <Form.Group className="mb-3" controlId="name">

                <Form.Label>Name</Form.Label>
                <Form.Control value={name} onChange={(e) => SetName(e.target.value)} type="text" placeholder="name..." required />
            </Form.Group>

            <Form.Group className="mb-3" controlId="image">
                <Form.Label>Image</Form.Label>
                <Form.Control onChange={(e) => SetImage(e.target.files[0])} type="file" required />
            </Form.Group>

            <Button disabled={disalbed} className="btn btn-primary" type="Submit">
                Save
            </Button>
        </Form>

    );
}