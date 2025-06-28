import { faUsers } from "@fortawesome/free-solid-svg-icons";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { faTruckFast } from "@fortawesome/free-solid-svg-icons";
import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"; 

export const links = [
    {
        name: "Users", 
        path: "Users",
        icon: faUsers, 
        role : "Admin",
    },
    {
        name: "Add User", 
        path: "/dashboard/user/add",
        icon: faPlus, 
        role: "Admin",
    },
    {
        name: "Categories",
        path: "/dashboard/categories",
        icon: faCartShopping,
        role: ["Admin", "Product Manager"],
    },
    {
        name: "Add Category",
        path: "/dashboard/category/add",
        icon: faPlus,
        role: ["Admin", "Product Manager"],
    },
    {
        name: "Products",
        path: "/dashboard/products",
        icon: faTruckFast,
        role: ["Admin", "Product Manager"],
    },
    {
        name: "Add Product",
        path: "/dashboard/product/add",
        icon: faPlus,
        role: ["Admin", "Product Manager"],
    }
    //,
    //{
    //    name: "Writer", 
    //    path: "/dashboard/Writer",
    //    icon: faPlus, 
    //    role: ["Admin","Writer"],
    //}
] 