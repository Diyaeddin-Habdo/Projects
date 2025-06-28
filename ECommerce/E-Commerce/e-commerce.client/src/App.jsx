import { useEffect, useState } from 'react';
import HomePage from './Pages/Website/HomePage.jsx';
import Login from './Pages/Auth/AuthOperation/Login.jsx';
import RequireAuth from './Pages/Auth/Protecting/RequireAuth.jsx';
import Err403 from './Pages/Auth/Errors/403.jsx';
import Err404 from './Pages/Auth/Errors/404.jsx';
import RequireBack from './Pages/Auth/Protecting/RequireBack.jsx';
import Register from './Pages/Auth/AuthOperation/Register.jsx';
import Users from './Pages/Dashboard/User/Users.jsx';
import User from './Pages/Dashboard/User/User.jsx';
import AddUser from './Pages/Dashboard/User/AddUser.jsx';
import Categories from './Pages/Dashboard/Category/Categories.jsx';
import WebsiteCategories from './Pages/Website/Categories.jsx';
import Category from './Pages/Dashboard/Category/Category.jsx';
import AddCategory from './Pages/Dashboard/Category/AddCategory.jsx';
import Products from './Pages/Dashboard/Product/Products.jsx';
import AddProduct from './Pages/Dashboard/Product/AddProduct.jsx';
import Product from './Pages/Dashboard/Product/Product.jsx';
import { Route,Routes } from 'react-router-dom';
import './App.css';
import Dashboard from './Pages/Dashboard/Dashboard.jsx';
import Website from './Pages/Website/Website.jsx';
import SingleProduct from './Pages/Website/SingleProduct/SingleProduct.jsx';


export default function App() {
   
    return (
        <div className="App">
            <Routes>

                {/* publich routes*/}
                <Route element={<Website/>}>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/categories" element={<WebsiteCategories />} />
                    <Route path="/product/:id" element={<SingleProduct />} />
                </Route>
                <Route path="/" element={<RequireBack/> } >
                    <Route path="/Login" element={<Login />} ></Route>
                    <Route path="/Register" element={<Register />} ></Route>
                </Route>
                <Route path="/*" element={<Err404/> } />



                {/*Protected routes*/}
                <Route element={<RequireAuth allowedRole={["Admin","Product Manager"] } />}>
                    <Route path="/dashboard" element={<Dashboard />} >


                        
                        <Route element={<RequireAuth allowedRole={['Admin']}/>}>
                            <Route path="Users" element={<Users/> }/>
                            <Route path="Users/:id" element={<User />} />
                            <Route path="user/add" element={<AddUser />} />
                        </Route>



                        <Route element={<RequireAuth allowedRole={['Admin', 'Product Manager']} />}>
                            <Route path="categories" element={<Categories />} />
                            <Route path="categories/:id" element={<Category />} />
                            <Route path="category/add" element={<AddCategory />} />

                            <Route path="products" element={<Products />} />
                            <Route path="products/:id" element={<Product />} />
                            <Route path="product/add" element={<AddProduct />} />
                        </Route>
                        
                    </Route>    
                </Route>
            </Routes>
        </div>
    );
};


{/*<Route path="403" element={<Err403/> }/>*/ }