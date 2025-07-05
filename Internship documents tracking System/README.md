# 📋 Internship Document Tracing System

<div align="center">

*A modern web application for streamlined internship document management and approval workflows*

</div>

---

## 🎯 **Project Overview**

The Internship Document Tracing System revolutionizes the traditional paper-based internship document management process by providing a comprehensive digital platform. This system enables seamless collaboration between students, instructors, and advisors through an intuitive approval workflow.

### ✨ **Key Highlights**
- 🔐 **Secure Authentication** with JWT tokens
- 📄 **Multi-format Document Support** 
- 🔄 **Real-time Status Tracking**
- 👥 **Role-based Access Control**

---

## 🖼️ **Application Screenshots**

<div style="display: flex; flex-wrap: wrap; gap: 10px; margin-bottom: 20px;">
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/Database Diagram.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/01.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/02.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/03.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/04.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/05.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/06.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/07.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/08.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/09.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/10.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/11.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/12.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/13.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/14.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/15.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/16.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/17.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/18.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/19.png" width="100%"/>
  </div>
  <div style="flex: 0 0 calc(33.33% - 10px);">
    <img src="screenshots/20.png" width="100%"/>
  </div>
</div>

---

## 🛠️ **Technology Stack**

<div align="center">

### **Backend Technologies**
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![SQL Server](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)

### **Frontend Technologies**
![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)
![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)

</div>

### 🏗️ **Architecture & Features**

| Component | Technology | Description |
|-----------|------------|-------------|
| **API** | ASP.NET Core Web API | RESTful API with JWT authentication |
| **Database** | SQL Server + ADO.NET | Robust data management with stored procedures |
| **Frontend** | React SPA | Modern, responsive user interface |
| **Architecture** | 3-Tier Pattern | Separation of concerns and maintainability |
| **Security** | JWT + Role-based Access | Secure authentication and authorization |

### 🎭 **User Roles & Permissions**

| Role | Permissions | Dashboard Features |
|------|-------------|-------------------|
| 🎓 **Student** | Upload documents, Track status | Document submission, Status tracking |
| 👨‍🏫 **Instructor** | Review & approve documents | Review queue, Approval workflow |
| 👨‍💼 **Advisor** | Final approval authority | Final review, Batch processing |

</div>

---

## 🔄 **Workflow Process**

```mermaid
graph TD
    A[Student Uploads Document] --> B[Instructor Review]
    B --> C{Instructor Decision}
    C -->|Approve| D[Advisor Review]
    C -->|Reject| E[Return to Student]
    D --> F{Advisor Decision}
    F -->|Approve| G[Document Approved]
    F -->|Reject| E
    E --> A
```

---

## 🔒 **Security Features**

- 🛡️ **JWT Authentication** - Secure token-based authentication
- 🔐 **Role-based Access Control** - Granular permissions
- 🔒 **Input Validation** - Prevention of malicious inputs
- 🚫 **SQL Injection Protection** - Parameterized queries & Stored Procedure
- 📁 **File Upload Security** - Type validation and size limits
---

## 📞 **Contact**
- 💻 **Portfolio**:[Diyaeddin Habdo](https://diyaeddin-habdo.github.io/portfolio/)
- 📧 **Email**: [diyahabdo@gmail.com](mailto:diyahabdo@gmail.com)  
- 💼 **LinkedIn**: [Diyaeddin Habdo](https://www.linkedin.com/in/diyaeddin-habdo-0b26a3236/)  
- 🐱 **GitHub**: [@Diyaeddin-Habdo](https://github.com/Diyaeddin-Habdo)  
- 📸 **Instagram**: [@Diyaeddin_376](https://www.instagram.com/eng.diyaeddin?igsh=ZHpqOGtsNWQ0aGox) 

---

<div align="center">

**⭐ Star this repository if you found it helpful!**

*Made with ❤️ using C# ASP.NET Core and React*

</div>