import { useState } from "react";
import { Form, Button } from "react-bootstrap";
import { Axios } from "../../../API/axios.js";
import { CATEGORIES } from "../../../API/Api.js";
import LoadingSubmit from "../../../Components/Loading/Loading.jsx";
export default function AddCategory() {


    const [name, SetName] = useState("");
    const [image, SetImage] = useState("");
    const [Loading, SetLoading] = useState(false);


    // nahdle submit
    async function HandleSubmit(e) {
        SetLoading(true);
        e.preventDefault();

        const form = new FormData();
        form.append('name', name);
        form.append('image', image);
        try {
            const res = await Axios.post(`${CATEGORIES}`, form);

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
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">

                <Form.Label>Name</Form.Label>
                <Form.Control value={name} onChange={(e) => SetName(e.target.value)} type="text" placeholder="name..." required />
            </Form.Group>

            <Form.Group className="mb-3" controlId="image">
                <Form.Label>Image</Form.Label>
                <Form.Control onChange={(e) => SetImage(e.target.files[0])} type="file" required />
            </Form.Group>


            <Button
                disabled=
                {
                    name.length > 1  ? false : true
                } className="btn btn-primary" type="Submit">
                Save
            </Button>
        </Form>

    );
}