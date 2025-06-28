import { useState, useEffect, useRef } from "react";
import { Form, FormControl, Button } from "react-bootstrap";
import { baseURL, CATEGORIES, PRODUCTS } from '../../../API/Api.js';
import LoadingSubmit from "../../../Components/Loading/Loading.jsx";
import axios from 'axios';
import Cookie from 'cookie-universal';
import { Axios } from "../../../API/axios.js";
import uploadImage from "../../../assets/images/upload.png";
import { useNavigate } from "react-router-dom";
export default function Product() {


    const [form, SetForm] = useState({
        categoryId: "Select Category",
        title: "",
        description: "",
        rating: 0,
        price: 0,
        discount: 0,
        about: "",
        images: [],
    });

    

    const [Loading, setLoading] = useState(false);
    const [selectImages, SetSelectImages] = useState(false);

    const cookie = Cookie();
    const [categories, SetCategories] = useState([]);

    // Get ID
    const Id = Number(window.location.pathname.replace("/dashboard/products/", ""));
    const nav = useNavigate();

    // Get product by Id
    useEffect(() => {
        setLoading(true);

        Axios.get(`${PRODUCTS}/${Id}`).then(data =>
        {
            SetForm(data.data);            
            setLoading(false);
        }).catch(() => nav('/dashboard/users/page/404', { replace: true }));
    }, [])


    // useRef for open image dialog
    const openImages = useRef(null);
    function handleOpenImages() {
        openImages.current.click();
/*        SetSelectImages(true);*/
    }

    // selected images to show
    const imagesShow = form.images.map((img, key) => (
        <div key={key} className="d-flex align-items-center justify-content-start gap-2 border p-2 w-100">
            <img src={selectImages ? URL.createObjectURL(img) : img} width="80px"></img>
            <div>
                <p className="mb-1">{selectImages ? img.name : img }</p>
                <p>{selectImages ? img.size / 1024 < 900
                    ? (img.size / 1024).toFixed(2) + "KB"
                    : (img.size / (1024 * 1024)).toFixed(2) + "MB" :    " "
                }</p>
            </div>
        </div>
    ));

    // Gets categories
    useEffect(() => {
        axios.get(`${baseURL}/${CATEGORIES}`, {
            headers: {
                Authorization: "Bearer " + cookie.get('e-commerce'),
            }
        })
            .then((data) => SetCategories(data.data))
            .catch((err) => console.log(err));
    }, []);

    // includes categories into dropdown
    const categoriesShow = categories.map((item, key) => (
        <option key={key} value={item.id}>{item.name}</option>
    ));

    // handle changes on form
    const handleChange = (e) => {
        const { name, value } = e.target;
        SetForm((prevForm) => ({
            ...prevForm,
            [name]: value,
        }));
    };

    // include images to form.images
    const handleImageChange = (e) => {
        const files = Array.from(e.target.files);
        SetForm((prevForm) => ({
            ...prevForm,
            images: files,
        }));
        SetSelectImages(true);
    };

    //Handle submit
    async function handleSubmit(e) {
        e.preventDefault();
        setLoading(true);
        const formData = new FormData();
        for (const key in form) {
            if (key === 'images') {
                form.images.forEach((image) => {
                    formData.append('images', image);
                });
            } else {
                formData.append(key, form[key]);
            }
        }

        try {
            const response = await Axios.put(`${PRODUCTS}`, formData);
            window.location.pathname = "/dashboard/products";
        } catch (err) {
            setLoading(false);
            console.log(err);
        }
    }

    // Return table component
    return (
        <Form className="bg-white w-100 mx-2 p-3" onSubmit={handleSubmit}>
            {Loading && <LoadingSubmit />}

            <Form.Group className="mb-3" controlId="categoryId">
                <Form.Label>Category</Form.Label>
                <Form.Select
                    name="categoryId"
                    value={form.categoryId}
                    onChange={handleChange}
                >
                    <option disabled>Select Category</option>
                    {categoriesShow}
                </Form.Select>
            </Form.Group>


            <Form.Group className="mb-3" controlId="title">
                <Form.Label>Title</Form.Label>
                <Form.Control
                    name="title"
                    value={form.title}
                    onChange={handleChange}
                    type="text"
                    placeholder="Title..."
                    required
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="description">
                <Form.Label>Description</Form.Label>
                <Form.Control
                    name="description"
                    value={form.description}
                    onChange={handleChange}
                    type="text"
                    placeholder="Description..."
                    required
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="rating">
                <Form.Label>Rating</Form.Label>
                <Form.Control
                    name="rating"
                    value={form.rating}
                    onChange={handleChange}
                    type="number"
                    placeholder="Rating..."
                    min="0"
                    max="5"
                    required
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="price">
                <Form.Label>Price</Form.Label>
                <Form.Control
                    name="price"
                    value={form.price}
                    onChange={handleChange}
                    type="number"
                    placeholder="Price..."
                    min="0"
                    step="0.01"
                    required
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="discount">
                <Form.Label>Discount</Form.Label>
                <Form.Control
                    name="discount"
                    value={form.discount}
                    onChange={handleChange}
                    type="number"
                    placeholder="Discount..."
                    min="0"
                    max="100"
                    step="0.01"
                    required
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="about">
                <Form.Label>About</Form.Label>
                <Form.Control
                    name="about"
                    value={form.about}
                    onChange={handleChange}
                    as="textarea"
                    rows={10}
                    placeholder="About..."
                    required
                />
            </Form.Group>

            <Form.Group className="mb-3" controlId="images">
                <Form.Label>Images</Form.Label>
                <Form.Control
                    ref={openImages}
                    hidden
                    name="images"
                    onChange={handleImageChange}
                    type="file"
                    multiple
                    required
                />
            </Form.Group>

            <div className="d-flex align-items-center gap-2 py-3 rounded mb-2 w-100 flex-column"
                style={{ border: "2px dashed #0086fe", cursor: "pointer" }}
                onClick={handleOpenImages}
            >
                <img src={uploadImage} alt="upload here" width="100px"></img>
                <p className="fw-bold mb-0" style={{ color: "#0086fe" }}>Upload Images</p>
            </div>


            <div className="d-flex align-items-start flex-column gap-2">
                {imagesShow}
            </div>

            <Button
                disabled={form.title.length < 2}
                className="btn btn-primary mt-2"
                type="submit"
            >
                Save
            </Button>
        </Form>

    );
}