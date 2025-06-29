# ☕ Cafe Order Management System

A comprehensive cafe management solution with real-time order processing - Built with Flutter mobile app and ASP.NET Core Web API backend.

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Features](#features)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [User Roles](#user-roles)
- [Screenshots](#screenshots)
- [Contact](#Contact)

## 🎯 About the Project

This is a full-stack cafe management system designed to streamline restaurant operations from order taking to payment processing. The system provides role-based access for different staff members and real-time order management capabilities.

### Key Benefits
- **Order Processing**: Communication between waiters, chefs, and cashiers
- **Multi-role Management**: Different interfaces for different staff roles
- **Mobile-first Design**: Flutter-powered mobile application for enhanced mobility
- **Comprehensive Reporting**: Daily, monthly, and yearly sales analytics
- **Table Management**: Table status tracking

## ✨ Features

### 👨‍🍳 For Waiters
- **Table Overview**: View all tables and their current status
- **Order Taking**: Select tables and add menu items to orders
- **Table Status Management**: Track occupied and available tables

### 👨‍🍳 For Chefs
- **Order Queue**: View all incoming orders from all waiters and tables
- **Order Management**: Mark orders as prepared and ready for serving
- **Order Completion**: Transfer completed orders back to waiters

### 💰 For Cashiers
- **Table-based Billing**: Access orders by table number
- **Order Summary**: View all items ordered by customers at specific tables
- **Payment Processing**: Calculate total amounts and process payments
- **Table Reset**: Clear table status after payment completion

### 👨‍💼 For Managers
- **User Management**: Create, update, and manage staff accounts
- **Menu Management**: Add, edit, and remove menu items
- **Table Configuration**: Manage table setup and configuration
- **Analytics Dashboard**: Comprehensive reporting system
  - Daily sales reports
  - Monthly performance analytics
  - Yearly revenue tracking
  - Product popularity analysis

## 🛠️ Technologies

### Frontend (Mobile App)
- **Flutter**: Cross-platform mobile development framework
- **Dart**: Programming language for Flutter
- **HTTP Package**: REST API communication
- **Provider/Bloc**: State management

### Backend (Web API)
- **ASP.NET Core Web API**: RESTful API framework
- **C#**: Primary programming language
- **ADO.NET**: Data access technology

### Database
- **SQL Server**: Primary database system
- **T-SQL**: Advanced query language
- **Stored Procedures**: Optimized database operations
- **Database Views**: Complex data retrieval
- **Indexes**: Performance optimization

### Development Tools
- **Visual Studio**: IDE for backend development
- **Visual Studio Code**: Flutter development
- **SQL Server Management Studio**: Database management
- **Postman**: API testing

## 🏗️ Architecture

The system follows a modern microservices-inspired architecture:

```
┌─────────────────────────────┐
│      Flutter Mobile App     │  ← Cross-platform UI
│        (Frontend)           │
└─────────────┬───────────────┘
              │ HTTP/REST
              │
┌─────────────▼───────────────┐
│    ASP.NET Core Web API     │  ← Business Logic
│       (Backend API)         │
└─────────────┬───────────────┘
              │ Entity Framework
              │
┌─────────────▼───────────────┐
│       SQL Server            │  ← Data Storage
│       Database              │
└─────────────────────────────┘
```

### Architecture Components

#### 📱 Mobile Application Layer
- User interface and experience
- State management


#### 🔌 API Layer
- RESTful endpoints
- Business logic implementation
- Data validation
- Error handling

#### 🗃️ Data Layer
- ADO.NET
- Data access logic


## 👥 User Roles

### 🍽️ Waiter Role
**Permissions**: Table management, order creation
```
- View all tables
- Select available tables
- Add items to orders
- Submit orders to kitchen
```

### 👨‍🍳 Chef Role
**Permissions**: Order processing, kitchen management
```
- View incoming orders
- Mark orders as prepared
- Complete order processing
- Kitchen queue management
```

### 💳 Cashier Role
**Permissions**: Payment processing, billing
```
- Access table orders
- View order totals
- Process payments
```

### 🏢 Manager Role
**Permissions**: Full system administration
```
- User management (CRUD)
- Menu item management
- Table configuration
- Sales reporting
- System analytics
```

## 🖼️ Screenshots

### 📱 Login Screen
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/AppIcon.png" width="200" alt="App Icon"/>
  <img src="screenshots/Login1.png" width="200" alt="Login Screen 1"/>
  <img src="screenshots/login2.png" width="200" alt="Login Screen 2"/>
  <img src="screenshots/login3.png" width="200" alt="Login Screen 3"/>
  <img src="screenshots/login4.png" width="200" alt="Login Screen 4"/>
</div>

### 🧑‍💼 Waiter Interface
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/TablesList.png" width="200" alt="Tables List"/>
  <img src="screenshots/TablesList2.png" width="200" alt="Table Selection"/>
  <img src="screenshots/AddOrders.png" width="200" alt="Add Orders"/>
  <img src="screenshots/AddOrders2.png" width="200" alt="Menu Selection"/>
  <img src="screenshots/AddOrders3.png" width="200" alt="Order Options"/>
  <img src="screenshots/ShowAddedOrders.png" width="200" alt="Order Summary"/>
</div>

### 👨‍🍳 Chef Interface
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/chefScreen.png" width="300" alt="Kitchen Display"/>
  <img src="screenshots/Orderdelivered.png" width="300" alt="Order Delivered"/>
</div>

### 💰 Cashier Interface
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/cashierScreen.png" width="300" alt="Payment Screen"/>
  <img src="screenshots/cashierScreen2.png" width="300" alt="Order Summary"/>
  <img src="screenshots/TableOrdersScreen.png" width="300" alt="Table Orders"/>
  <img src="screenshots/Payed.png" width="300" alt="Payment Completed"/>
</div>

### 👔 Manager Interface
#### Tables Management
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/TablesManagement.png" width="200" alt="Tables Management"/>
  <img src="screenshots/AddTable.png" width="200" alt="Add Table"/>
  <img src="screenshots/UpdateTable.png" width="200" alt="Update Table"/>
  <img src="screenshots/TableStatus.png" width="200" alt="Table Status"/>
</div>

#### Products Management
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/ProductsManagement.png" width="200" alt="Products Management"/>
  <img src="screenshots/AddProduct.png" width="200" alt="Add Product"/>
  <img src="screenshots/UpdateProduct.png" width="200" alt="Update Product"/>
</div>

#### Users Management
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/UsersManagement.png" width="200" alt="Users Management"/>
  <img src="screenshots/AddUser.png" width="200" alt="Add User"/>
  <img src="screenshots/UserAdded.png" width="200" alt="User Added"/>
  <img src="screenshots/UpdateUser.png" width="200" alt="Update User"/>
  <img src="screenshots/UserUpdated.png" width="200" alt="User Updated"/>
  <img src="screenshots/DeleteUser.png" width="200" alt="Delete User"/>
  <img src="screenshots/UpdateManager.png" width="200" alt="Update Manager"/>
  <img src="screenshots/SearchUser.png" width="200" alt="Search User"/>
</div>

#### Reports & Analytics
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/Reports1.png" width="200" alt="Sales Reports"/>
  <img src="screenshots/Reports2.png" width="200" alt="Product Reports"/>
  <img src="screenshots/Reports3.png" width="200" alt="Time-based Reports"/>
  <img src="screenshots/Reports4.png" width="200" alt="Detailed Reports"/>
  <img src="screenshots/Reports5.png" width="200" alt="Export Reports"/>
</div>

#### Multi-language Support
<div style="display: flex; flex-wrap: wrap; gap: 10px;">
  <img src="screenshots/DifferentLanguage0.png" width="200" alt="Language Selection"/>
  <img src="screenshots/DifferentLanguage1.png" width="200" alt="English Interface"/>
  <img src="screenshots/DifferentLanguage2.png" width="200" alt="Turkish Interface"/>
</div>


## 📬 Contact  
- 💻 **Portfolio**:[Diyaeddin Habdo](https://diyaeddin-habdo.github.io/portfolio/)
- 📧 **Email**: [diyahabdo@gmail.com](mailto:diyahabdo@gmail.com)  
- 💼 **LinkedIn**: [Diyaeddin Habdo](https://www.linkedin.com/in/diyaeddin-habdo-0b26a3236/)  
- 🐱 **GitHub**: [@Diyaeddin-Habdo](https://github.com/Diyaeddin-Habdo)  
- 📸 **Instagram**: [@Diyaeddin_376](https://www.instagram.com/eng.diyaeddin?igsh=ZHpqOGtsNWQ0aGox)  